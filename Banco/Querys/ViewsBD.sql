use dbOrgan; 
-- MUDAR VALOR DOS NOMES Das( DATas( PRA PORTUGUESSET lc_time_names = 'pt_BR'; 

drop view if exists vwPragaOrDoenca ;
CREATE VIEW vwPragaOrDoenca AS
    (SELECT 
        pd.Id,
        pd.Nome `Nome`,
        CASE
            WHEN pd.`P/D` = TRUE THEN 'Praga'
            ELSE 'Doença'
        END AS `Tipo`,
        GROUP_CONCAT(a.Nome
            SEPARATOR ', ') `Áreas`,
        CASE
            WHEN c.`Status` = TRUE THEN 'Controlado'
            ELSE 'Não controlado'
        END AS `Status`
    FROM
        tbAreaPD apd
            INNER JOIN
        tbPragaOrDoenca pd ON apd.IdPD = pd.Id
            INNER JOIN
        tbArea a ON apd.IdArea = a.Id
            INNER JOIN
        tbControlePD cpd ON cpd.IdPd = pd.Id
            INNER JOIN
        tbControle c ON c.Id = cpd.IdControle
    GROUP BY pd.Id , c.`Status`);

drop view if exists vwControle;
create view vwControle as(
select c.Id,
   DATE_FORMAT(c.`Data`, '%d/%m/%Y') `Data`,
   case
when c.`Status` = true then 'Controlado'
else 'Não controlado'
   end as
   `Status`,
   ifnull(c.`Desc`, 'Sem Descrição') `Descrição`,
   c.Efic `Eficiência(%)`,
   c.NumLiberacoes `Número de Liberações`,
   group_concat(distinct p.Nome separator ', ' ) `Pragas/Doenças`,
   group_concat(distinct concat(i.Item, ' - ', ic.QtdUsada) separator ', ') `Itens Usados - Quantidade`
from tbControle c
inner join tbControlePD cpd on c.Id = cpd.IdControle
inner join tbPragaOrDoenca p on cpd.IdPD = p.Id
inner join tbItensControle ic on c.Id = ic.IdControle
inner join vwItems i on ic.IdEstoque = i.Id
group by c.Id, c.`Status`);

drop view if exists vwTelefone;
create view vwTelefone as(
select T.Id, concat('(',T.IdDDD,') ',T.Numero,' - ',Ti.Tipo) `Telefone`
 from tbTelefone T
inner join tbTipoTel Ti on Ti.Id = T.IdTipo
); 

drop view if exists vwFornecedor;
create view vwFornecedor as(
select F.Id, F.Nome `Razão Social`, F.Email `Email`, group_concat(T.Telefone separator '; ') `Telefones`
 from tbFornecedor F
inner join tbTelForn TF on F.Id = TF.IdForn
inner join vwTelefone T on T.Id = TF.IdTelefone
group by F.Id
); 

drop view if exists vwFuncionario;
create view vwFuncionario as(
select F.Id, F.Nome,  C.Nome `Cargo`, group_concat(T.Telefone separator '; ') `Telefones`, F.Email
 from tbFuncionario F
inner join tbCargo C on F.IdCargo = C.Id
inner join tbTelFunc TF on F.Id = TF.IdFunc
inner join vwTelefone T on T.Id = TF.IdTelefone 
 where F.`Status` = true
); 

drop view if exists vwItems;
create view vwItems as
(SELECT S.IdEstoque `Id`,
S.Nome `Item`,
E.Qtd `Quantidade`,
U.`Desc` `Unidade de Medida`,
S.`Desc` `Descrição`,
'Semente' `Categoria`,
F.`Razão Social` `Fornecedor`,
'Semente' `Tipo`
FROM tbEstoque E
INNER JOIN tbSemente S ON E.Id = S.IdEstoque
inner join vwFornecedor F on E.IdFornecedor = F.Id
inner join tbUM U on E.UM = U.Id
)
UNION
(SELECT I.IdEstoque, 
I.Nome,
E.Qtd,
U.`Desc`,
I.`Desc`,
C.Categoria,
F.`Razão Social`,
   'Insumo' `Tipo`
FROM tbEstoque E
INNER JOIN tbInsumo I ON E.Id = I.IdEstoque
INNER JOIN tbCategoria C ON C.Id = I.IdCategoria
inner join vwFornecedor F on E.IdFornecedor = F.Id
inner join tbUM U on E.UM = U.Id
)
UNION
(SELECT M.IdEstoque,
M.Nome,
E.Qtd,
U.`Desc`,
M.`Desc`,
M.Tipo,
F.`Razão Social`,
'Máquina' `Tipo`
FROM tbMaquina M
INNER JOIN tbEstoque E ON M.IdEstoque = E.Id
inner join vwFornecedor F on E.IdFornecedor = F.Id
inner join tbUM U on E.UM = U.Id
)UNION
(SELECT P.IdEstoque,
P.Nome,
E.Qtd,
U.`Desc`,
P.`Desc`,
'Produto',
F.`Razão Social`,
'Produto' `Tipo`
FROM tbProduto P
INNER JOIN tbEstoque E ON P.IdEstoque = E.Id
inner join vwFornecedor F on E.IdFornecedor = F.Id
inner join tbUM U on E.UM = U.Id
)
order by `Categoria`;
  
DELIMITER $
drop trigger if exists trgInsertHistorico$
create TRIGGER trgInsertHistorico after insert
ON tbEstoque
FOR EACH ROW
BEGIN   

call spVerQtd(NEW.Qtd);
insert into tbHistEstoque(`Desc`, IdEstoque) values('Novo Item', NEW.Id);

END$

drop trigger if exists trgUpdateHistorico$
create TRIGGER trgUpdateHistorico after update
ON tbEstoque
FOR EACH ROW
BEGIN   
declare cat, nome varchar(30);
declare forn varchar(50);
call spVerQtd(NEW.Qtd);

set cat = (select `Categoria` from vwItems where `Id` = OLD.Id);
set nome = (select `Item` from vwItems where `Id` = OLD.Id);
set forn = (select `Nome` from tbFornecedor where `Id` = OLD.IdFornecedor);


insert into tbHistEstoque(QtdAntiga, UMAntiga, FornAntigo, `Desc`, IdEstoque, CategoriaAntiga, NomeAntigo)
   values(OLD.Qtd, OLD.UM, forn, 'Item Alterado', OLD.Id, cat, nome);

END$

drop trigger if exists trgDeleteHistorico$ 
create TRIGGER trgDeleteHistorico before delete 
ON tbEstoque
FOR EACH ROW
BEGIN   

declare cat, nome varchar(30);
declare forn varchar(50);

set cat = (select `Categoria` from vwItems where `Id` = OLD.Id);
set nome = (select `Item` from vwItems where `Id` = OLD.Id);
set forn = (select `Nome` from tbFornecedor where `Id` = OLD.IdFornecedor);

SET FOREIGN_KEY_CHECKS=0;

insert into tbHistEstoque(QtdAntiga, UMAntiga, FornAntigo, `Desc`, IdEstoque, CategoriaAntiga, NomeAntigo)
   values(OLD.Qtd, OLD.UM, forn, 'Item Excluido', OLD.Id, cat, nome);
   
SET FOREIGN_KEY_CHECKS=1;
END$
DELIMITER ;


drop view if exists vwHistorico;
create view vwHistorico as(
select Id, IdEstoque `Id do Item`, ifnull(NomeAntigo, '-') `Nome do Item`, ifnull(CategoriaAntiga, '-') `Categoria`,
   ifnull(FornAntigo, '-') `Fornecedor`, ifnull(QtdAntiga, '-') `Quantidade`, ifnull(UMAntiga, '-') `Unidade de Medida`,
   DataAlteracao `Data de Alteração`, `Desc` `Descrição de Alteração`
 from tbHistEstoque
 order by `Data de Alteração` desc
);

DELIMITER $
drop trigger if exists trgItensPlantio$
create trigger trgItensPlantio
before insert 
on tbItensPlantio
for each row
begin
call spVerQtd(NEW.QtdUsada);
call spCertQtd(NEW.QtdUsada, NEW.IdEstoque);

update tbEstoque
set Qtd = (Qtd - NEW.QtdUsada)
where Id = NEW.IdEstoque;
end$

drop trigger if exists trgItensControle$
create trigger trgItensControle
before insert 
on tbItensControle
for each row
begin
call spVerQtd(NEW.QtdUsada);
call spCertQtd(NEW.QtdUsada, NEW.IdEstoque);

update tbEstoque
set Qtd = (Qtd - NEW.QtdUsada)
where Id = NEW.IdEstoque;
end$

drop trigger if exists trgAreaPlantio$
create trigger trgAreaPlantio
before insert 
on tbAreaControle
for each row
begin

update tbArea
set Disp = 2
where Id = NEW.IdEstoque;
end$
DELIMITER ;
   
use dbEcommerce;
drop view if exists vwEndereco;
create view vwEndereco as(
select E.CEP,  R.Logradouro `Rua`, concat(B.Bairro,' - ', C.Cidade,'/', Es.UF) `BCE` 
 from tbEndereco E 
	inner join tbLogradouro R on E.IdRua = R.Id
	inner join tbBairro B on R.IdBairro = B.Id
	inner join tbCidade C on B.IdCidade = C.Id
	inner join tbEstado Es on C.IdEstado = Es.Id
);

DELIMITER $

drop trigger if exists trgHistCarrinho$
create trigger trgHistCarrinho
after delete
on tbCarrinho
for each row
begin
	declare nome varchar(30);
    set nome = (select nome from tbAnuncio where Id = OLD.IdAnuncio);
	insert into tbHistCarrinho value(OLD.IdUsuario, nome, OLD.Id, OLD.Qtd);
end$

DELIMITER ;
