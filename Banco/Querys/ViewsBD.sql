use dbOrgan; 
-- MUDAR VALOR DOS NOMES Das( DATas( PRA PORTUGUESSET lc_time_names = 'pt_BR'; 

drop view if exists vwPragaOrDoenca ;
create view vwPragaOrDoenca as(
	select 
        pd.Id,
        pd.Nome `Nome`,
        case
            when pd.`P/D` = true then 'Praga'
            else 'Doença'
        end as `Tipo`,
        group_concat(a.Nome separator ', ') `Áreas`,
        case
            when c.`Status` = true then 'Controlado'
            else 'Não controlado'
        end as `Status`
    from
        tbAreaPD apd
            inner join
        tbPragaOrDoenca pd on apd.IdPD = pd.Id
            inner join
        tbArea a on apd.IdArea = a.Id
            inner join
        tbControlePD cpd on cpd.IdPd = pd.Id
            inner join
        tbControle c on c.Id = cpd.IdControle
    group by pd.Id , c.`Status`);

drop view if exists vwTelefone;
create view vwTelefone as(
	select 
        T.Id,
        concat('(',
                T.IdDDD,
                ') ',
                T.Numero,
                ' - ',
                Ti.Tipo) `Telefone`
    from
        tbTelefone T
            inner join
        tbTipoTel Ti on Ti.Id = T.IdTipo); 

drop view if exists vwFornecedor;
create view vwFornecedor as
    (select 
        F.Id,
        F.Nome `Razão Social`,
        F.Email `Email`,
        group_concat(T.Telefone
            separator '; ') `Telefones`
    from
        tbFornecedor F
            inner join
        tbTelForn TF on F.Id = TF.IdForn
            inner join
        vwTelefone T on T.Id = TF.IdTelefone
    group by F.Id); 

drop view if exists vwFuncionario;
create view vwFuncionario as
    (select 
        F.Id,
        F.Nome,
        F.Funcao `Função`,
        group_concat(T.Telefone
            separator '; ') `Telefones`,
        F.Email
    from
        tbFuncionario F
            inner join
        tbTelFunc TF on F.Id = TF.IdFunc
            inner join
        vwTelefone T on T.Id = TF.IdTelefone
    where
        F.`Status` = true); 
  

drop view if exists vwItems;
create view vwItems as(
	select 
        S.IdEstoque `Id`,
        S.Nome `Item`,
        E.Qtd `Quantidade`,
        U.`Desc` `Unidade de Medida`,
        S.`Desc` `Descrição`,
        'Semente' `Categoria`,
        F.`Razão Social` `Fornecedor`,
        'Semente' `Tipo`
    from
        tbEstoque E
            inner join
        tbSemente S on E.Id = S.IdEstoque
            inner join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id)
union(
	select 
        I.IdEstoque,
        I.Nome,
        E.Qtd,
        U.`Desc`,
        I.`Desc`,
        I.Categoria,
        F.`Razão Social`,
        'Insumo' `Tipo`
    from
        tbEstoque E
            inner join
        tbInsumo I on E.Id = I.IdEstoque
            inner join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id)
union(
	select 
        M.IdEstoque,
        M.Nome,
        E.Qtd,
        U.`Desc`,
        M.`Desc`,
        M.Tipo,
        F.`Razão Social`,
        'Máquina' `Tipo`
    from
        tbMaquina M
            inner join
        tbEstoque E on M.IdEstoque = E.Id
            inner join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id)
union(
	select 
        P.IdEstoque,
        P.Nome,
        E.Qtd,
        U.`Desc`,
        P.`Desc`,
        'Produto',
        F.`Razão Social`,
        'Produto' `Tipo`
    from
        tbProduto P
            inner join
        tbEstoque E on P.IdEstoque = E.Id
            inner join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
)order by `Id`;


drop view if exists vwControle;
create view vwControle as(
	select 
        c.Id,
        date_format(c.`Data`, '%d/%m/%Y') `Data`,
        case
            when c.`Status` = true then 'Controlado'
            else 'Não controlado'
        end as `Status`,
        ifnull(c.`Desc`, 'Sem Descrição') `Descrição`,
        c.Efic `Eficiência(%)`,
        c.NumLiberacoes `Número de Liberações`,
        group_concat(distinct p.Nome
            separator ', ') `Pragas/Doenças`,
        group_concat(distinct concat(i.Item, ' - ', ic.QtdUsada)
            separator ', ') `Itens Usados - Quantidade`
    from
        tbControle c
            inner join
        tbControlePD cpd on c.Id = cpd.IdControle
            inner join
        tbPragaOrDoenca p on cpd.IdPD = p.Id
            inner join
        tbItensControle ic on c.Id = ic.IdControle
            inner join
        vwItems i on ic.IdEstoque = i.Id
    group by c.Id , c.`Status`);

DELIMITER $
drop trigger if exists trgInsertHistorico$
create trigger trgInsertHistorico after insert
on tbEstoque
for each row
begin   

call spVerQtd(NEW.Qtd);
insert into tbHistEstoque(`Desc`, IdEstoque) values('Novo Item', NEW.Id);

end$

drop trigger if exists trgUpdateHistorico$
create trigger trgUpdateHistorico after update
on tbEstoque
for each row
begin   
declare cat, nome varchar(30);
declare forn, descs varchar(50);

call spVerQtd(NEW.Qtd);

set cat = (select `Categoria` from vwItems where `Id` = OLD.Id);
set nome = (select `Item` from vwItems where `Id` = OLD.Id);
set forn = (select `Nome` from tbFornecedor where `Id` = OLD.IdFornecedor);

if(exists(select IdEstoque from tbItensPlantio where IdEstoque = NEW.Id order by IdEstoque desc limit 1)) then
	set descs = 'Item utilizado no plantio';
elseif(exists(select IdEstoque from tbItensControle where IdEstoque = NEW.Id order by IdEstoque desc limit 1)) then
	set descs = 'Item utilizado no controle';
else set descs = 'Item Alterado';
end if;

insert into tbHistEstoque(QtdAntiga, UMAntiga, FornAntigo, `Desc`, IdEstoque, CategoriaAntiga, NomeAntigo)
				   values(OLD.Qtd, OLD.UM, forn, descs, OLD.Id, cat, nome);

end$

drop trigger if exists trgDeleteHistorico$ 
create trigger trgDeleteHistorico before delete 
on tbEstoque
for each row
begin   

declare cat, nome varchar(30);
declare forn varchar(50);

set cat = (select `Categoria` from vwItems where `Id` = OLD.Id);
set nome = (select `Item` from vwItems where `Id` = OLD.Id);
set forn = (select `Nome` from tbFornecedor where `Id` = OLD.IdFornecedor);

if(OLD.Qtd = 0)
	then
		set FOREIGN_KEY_CHECKS=0;

		insert into tbHistEstoque(QtdAntiga, UMAntiga, FornAntigo, `Desc`, IdEstoque, CategoriaAntiga, NomeAntigo)
		   values(OLD.Qtd, OLD.UM, forn, 'Item Excluido', OLD.Id, cat, nome);
	else
		SIGNAL SQLSTATE '45002'
			SET MESSAGE_TEXT = 'Quantidade diferente de zero.';
	end if;
end$

drop trigger if exists trgDeleteEstoque$ 
create trigger trgDeleteEstoque
after delete 
on tbEstoque
for each row
begin   
set FOREIGN_KEY_CHECKS=1;
end$

drop trigger if exists trgDeleteHistPlant$ 
create trigger trgDeleteHistPlant before delete 
on tbPlantio
for each row
begin   

set FOREIGN_KEY_CHECKS=0;

insert into tbHistPlantio(Id, Nome)
				   values(OLD.Id, OLD.Nome);

end$

drop trigger if exists trgDeletePlantio$ 
create trigger trgDeletePlantio
after delete 
on tbPlantio
for each row
begin   
set FOREIGN_KEY_CHECKS=1;
end$
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
on tbAreaPlantio
for each row
begin

update tbArea
set Disp = 2
where Id = NEW.IdArea;
end$

drop trigger if exists trgAreaControle$
create trigger trgAreaControle
before insert 
on tbAreaPD
for each row
begin

update tbArea
set Disp = 2
where Id = NEW.IdArea;
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

drop view if exists vwAnuncio;
create view vwAnuncio as(
select 
	A.Id `Id`,
    A.Nome `Anúncio`,
    A.Foto `Foto`,
    A.`Data` `Data de Postagem`,
    AN.NomeFazenda `Anunciante`,
    concat(E.Rua,', ', AN.NumEnd,' - ', ifnull(AN.CompEnd, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço do Anunciante`,
    A.`Desc` `Descrição`,
    A.Desconto `Desconto(%)`,
    P.Nome `Produto`,
    concat(A.Quantidade, ' ', P.UM ) `Quantidade`,
    (P.ValorUnit * A.Quantidade) `Preço`,
    P.Categoria `Categoria`,
    spNota(A.Id) `Nota`,
    ifnull((select count(`Like`) from tbAvaliacao where IdAnuncio = A.Id), '0') `Likes`
from tbAnuncio A
	inner join tbProduto P on P.Id = A.IdProduto
    inner join tbAnunciante AN on AN.IdUsuario = A.IdAnunciante
    inner join vwEndereco E on E.CEP = AN.CEP
);

drop view if exists vwPedido;
create view vwPedido as(
select 
	P.Id `Id`,
    A.IdAnunciante `Anunciante`,
    A.Nome `Anúncio`, 
	group_concat(distinct concat(A.Nome, ' - ', PA.Qtd)separator ', ') `Nome do Anúncio - Quantidade Pedida`,
	sum(Pr.ValorUnit * PA.Qtd) `Valor Total s/Desconto`,
    sum((Pr.ValorUnit * PA.Qtd)-((Pr.ValORUnit * PA.Qtd)*(A.Desconto/100))) `Valor Total c/Desconto`,
	concat(U.`UserName`, '-', U.CPF) `Comprador - CPF`,
    concat(E.Rua,', ', P.NumEntrega,' - ', ifnull(P.CompEntrega, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço de Entrega`,
    P.ValFrete `Valor do Frete`,
    P.`Status` `Situação do Pedido`,
    P.`Data` `Data do Pedido`
from tbPedido P
	inner join vwEndereco E on E.CEP = P.CEPEntrega
    inner join tbUsuario U on U.`Id` = P.IdUsuario
    inner join tbPedidoAnuncio PA on PA.IdPedido = P.Id
    inner join tbAnuncio A on A.Id = PA.IdAnuncio
    inner join tbProduto Pr on Pr.Id = A.IdProduto
);

drop view if exists vwComentario;
create view vwComentario as(
select 
	C.Id `Id`,
    C.Comentario `Comentário`,
    spUsuario(C.IdUsuario) `Usuário`,
    A.Nome `Anúncio`, 
    ifnull(C.`Like`, 0) `Likes`,
    ifnull(C.Deslike, 0) `Deslikes`,
    C.`Data` `Data de Postagem`
from tbComentario C
	inner join tbAnuncio A on A.Id = C.IdAnuncio
);

drop view if exists vwCarrinho;
create view vwCarrinho as(
select 
	C.IdUsuario `Id`,
    A.Nome `Anúncio`,
    C.Qtd `Quantidade adicionada`
from tbCarrinho C
	inner join tbAnuncio A on A.Id = C.IdAnuncio
);

drop view if exists vwVenda;
create view vwVenda as(
select 
	V.Id `Id`,
    V.`Data` `Data`,
	group_concat(distinct concat(A.Nome, ' - ', PA.Qtd)separator ', ') `Nome do Anúncio - Quantidade Pedida`,
	(sum((Pr.ValorUnit * PA.Qtd)-((Pr.ValorUnit * PA.Qtd)*(A.Desconto/100))) + P.ValFrete) `Valor Total`,
	concat(E.Rua,', ', P.NumEntrega,' - ', ifnull(P.CompEntrega, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço de Entrega`,
    An.NomeFazenda `Anunciante`,
    V.`Status`
from tbVenda V
	inner join tbPedido P on P.Id = V.IdPedido
	inner join vwEndereco E on E.CEP = P.CEPEntrega
    inner join tbUsuario U on U.`Id` = P.IdUsuario
    inner join tbPedidoAnuncio PA on PA.IdPedido = P.Id
    inner join tbAnuncio A on A.Id = PA.IdAnuncio
    inner join tbProduto Pr on Pr.Id = A.IdProduto
    inner join tbAnunciante An on An.IdUsuario = A.IdAnunciante
);

DELIMITER $
drop trigger if exists trgHistCarrinho$
create trigger trgHistCarrinho
before delete
on tbCarrinho
for each row
begin
	declare nome, cat varchar(30);
    set nome = (select nome from tbAnuncio where Id = OLD.IdAnuncio);
    set cat = (select Categoria from tbAnuncio where Id = OLD.IdAnuncio);
	insert into tbHistCarrinho value(OLD.IdUsuario, nome, cat, OLD.Qtd);
end$

drop trigger if exists trgPedidoNovo$
create trigger trgPedidoNovo
after insert
on tbPedido
for each row
begin
	declare idanu int;
    set idanu = (select IdAnuncio from tbPedidoAnuncio where IdPedido = NEW.Id);
	delete from tbCarrinho where ((IdAnuncio = idanu) and (IdUsuario = NEW.IdUsuario));
end$
DELIMITER ;
