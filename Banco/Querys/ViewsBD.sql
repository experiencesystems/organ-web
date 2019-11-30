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
        tbTipoTel Ti on Ti.Id = T.IdTipo
	group by T.Id); 

drop view if exists vwFornecedor;
create view vwFornecedor as(
	select 
        F.Id,
        F.Nome `Razão Social`,
        F.Email `Email`,
        group_concat(ifnull(T.Telefone, 'Sem Telefone')
            separator '; ') `Telefones`,
		case 
			when F.`Status` = true then 'Ativo'
            else 'Desativado'
		end as `Situação`
		
    from
        tbFornecedor F
            left join
        tbTelForn TF on F.Id = TF.IdForn
            left join
        vwTelefone T on T.Id = TF.IdTelefone
    group by F.Id); 
    
drop view if exists vwFuncionario;
create view vwFuncionario as(
	select 
        F.Id,
        F.Nome,
        F.Funcao `Função`,
        group_concat(ifnull(T.Telefone, 'Sem Telefone')
            separator '; ') `Telefones`,
        F.Email,
        case 
			when F.`Status` = true then 'Ativo'
            else 'Demitido'
		end as `Situação`
    from
        tbFuncionario F
            left join
        tbTelFunc TF on F.Id = TF.IdFunc
            left join
        vwTelefone T on T.Id = TF.IdTelefone
    group by F.Id
);

drop view if exists vwItems; 
create view vwItems as(
	select 
        S.IdEstoque `Id`,
        S.Nome `Item`,
        E.Qtd `Quantidade`,
        U.`Desc` `Unidade de Medida`,
        'Semente' `Categoria`,
        ifnull(S.`Desc`, 'Sem Descrição') `Descrição`,
        ifnull(F.`Razão Social`, 'Sem fornecedor') `Fornecedor`,
        'Semente' `Tipo`
    from
        tbSemente S 
            inner join
		tbEstoque E on E.Id = S.IdEstoque
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	group by `Id`
)
union(
	select 
        I.IdEstoque,
        I.Nome,
        E.Qtd,
        U.`Desc`,
        I.Categoria,
        ifnull(I.`Desc`, 'Sem Descrição'),
        ifnull(F.`Razão Social`, 'Sem Fornecedor'),
        'Insumo' `Tipo`
    from
        tbInsumo I 
            inner join
        tbEstoque E on E.Id = I.IdEstoque
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	group by I.IdEstoque
)
union(
	select 
        M.IdEstoque,
        M.Nome,
        E.Qtd,
        U.`Desc`,
        M.Tipo,
        ifnull(M.`Desc`, 'Sem Descrição'),
        ifnull(F.`Razão Social`, 'Sem Fornecedor'),
        'Máquina' `Tipo`
    from
        tbMaquina M
            inner join
        tbEstoque E on M.IdEstoque = E.Id
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	order by M.IdEstoque
)
union(
	select 
        P.IdEstoque,
        P.Nome,
        E.Qtd,
        U.`Desc`,
        'Produto',
        ifnull(P.`Desc`, 'Sem Descrição'),
        ifnull(F.`Razão Social`, 'Sem Fornecedor'),
        'Produto' `Tipo`
    from
        tbProduto P
            inner join
        tbEstoque E on P.IdEstoque = E.Id
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	group by P.IdEstoque
)order by `Id`; 

drop view if exists vwArea;
create view vwArea as(
	select a.Id `Id`,
		   a.Nome `Área`,
           a.Tamanho `Tamanho(em ha)`,
           s.Id `IdSolo`,
           s.Tipo `Tipo de Solo`,
           s.IncSolar `Incidência Solar`,
           s.IncVento `Incidênica do Vento`,
           case
				when a.Disp = 1 then 'Disponível'
				when a.Disp = 2 then 'Em Uso'
				else 'Indiponível'
		   end as `Disponibilidade`
	from tbArea a
		inner join
	tbSolo s on a.IdSolo = s.Id
group by Id
);

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
        group_concat(distinct p.Id separator ', ') `IdPD`,
        group_concat(distinct p.Nome separator ', ') `Pragas/Doenças`,
        ifnull(group_concat(distinct (concat (i.Item, ' - ', ic.QtdUsada)) separator ', '), 'Nenhum Item Usado') `Itens Usados - Quantidade`,
        group_concat(distinct(ifnull(F.Nome, 'Sem Funcionários')) separator ',') `Funcionários Participantes`
    from
        tbControle c
            left join
		tbFuncControle FC on c.Id = FC.IdControle
			left join
		tbFuncionario F on FC.IdFunc = F.Id
			inner join
        tbControlePD cpd on c.Id = cpd.IdControle
            inner join
        tbPragaOrDoenca p on cpd.IdPD = p.Id
            left join
        tbItensControle ic on c.Id = ic.IdControle
            left join
        vwItems i on ic.IdEstoque = i.Id
    group by c.Id, c.`Status`);

drop view if exists vwHistorico;
create view vwHistorico as(
select date_format(HE.DataAlteracao, '%e/%m/%y às %H:%i') `Data de Alteração`, HE.Id, HE.IdEstoque `Id do Item`, ifnull(I.`Item`, 'Item Excluído') `Nome do Item`,
 ifnull(HE.QtdAntiga, '0') `Quantidade Antiga`,ifnull(I.`Quantidade`, '0') `Quantidade Atual`, HE.`Desc` `Descrição de Alteração`
 from tbHistEstoque HE
	left join 
 vwItems I on HE.IdEstoque = I.Id
 group by Id
 order by `Data de Alteração` desc
);

drop view if exists vwPlantio;
create view vwPlantio as(
	select P.Id,
		   P.Nome `Plantio`,
		   P.Sistema `Sistema`,
           P.TipoPlantio `Tipo`,
           date_format(P.DataInicio, '%d/%m/%y') `Data de Início`,
           date_format(P.DataColheita, '%d/%m/%y') `Data Prevista pra Colheita`,
           group_concat(distinct(A.Nome) separator ', ') `Áreas`,
           group_concat(distinct(concat(I.Item)) separator ',') `Itens Usados`,
           group_concat(distinct(ifnull(F.Nome, 'Sem Funcionários')) separator ', ') `Funcionários Participantes`
	from
		tbPlantio P
			left join
		tbFuncPlantio FC on P.Id = FC.IdPlantio
			left join 
		tbFuncionario F on FC.IdFunc = F.Id
			left join
		tbAreaPlantio AP on P.Id = AP.IdPlantio
			left join 
		tbArea A on AP.IdArea = A.Id
			left join 
		tbItensPlantio IP on P.Id = IP.IdPlantio
			left join
		vwItems I on IP.IdEstoque = I.Id
	group by P.Id
    order by P.Id
);

drop view if exists vwColheita;
create view vwColheita as(
	select c.Id `Id`,
		   date_format(c.`Data`, '%e/%m/%y') `Data da Colheita`,
           case
				when c.`Status` = true then 'Normal'
				else 'Final'
		   end as `Situação da Colheita`,
		   c.IdProd `IdProd`,
           p.Nome `Produto`,
           (c.QtdTotal - c.QtdPerdas) `Quantidade Colhida`,
           c.QtdPerdas `Quantidade Perdida`,
           c.QtdTotal `Quantidade Total`,
           c.IdPlantio `IdPlantio`,
           ifnull(pr.Nome, 'Plantio colhido definitivamente') `Plantio`
	from tbColheita c
		left join
	tbPlantio pr on c.IdPlantio = pr.Id
		inner join
	tbProduto p on c.IdProd = p.IdEstoque
	group by Id
    order by Id desc
);


DELIMITER $
drop trigger if exists trgInsertHistorico$
create trigger trgInsertHistorico after insert
on tbEstoque
for each row
begin   

call spVerQtd(NEW.Qtd);
insert into tbHistEstoque(`Desc`, IdEstoque) values(concat('Adicionado ', NEW.Qtd), NEW.Id);

end$

DELIMITER $ 
drop trigger if exists trgUpdateHistorico$
create trigger trgUpdateHistorico after update
on tbEstoque
for each row
begin   
declare descs varchar(50);
declare nqtd double;

call spVerQtd(NEW.Qtd);

if(exists(select * from tbItensPlantio where IdEstoque = NEW.Id order by a desc limit 1)) then 
	set descs = 'Item utilizado no plantio';
else set descs = 'Item Alterado';
end if;
if(exists(select * from tbItensControle where IdEstoque = NEW.Id order by a desc limit 1)) then
	set descs = 'Item utilizado no controle';
else set descs = 'Item Alterado';
end if;

if(NEW.Qtd > OLD.Qtd) then
	begin
    set nqtd = round((NEW.Qtd - OLD.Qtd), 2);
	set descs = concat(descs, ' - Adicionado ', cast(nqtd as char));
    end;
elseif(NEW.Qtd < OLD.Qtd) then
	begin
    set nqtd = round((OLD.Qtd - NEW.Qtd), 2);
	set descs = concat(descs,' - Retirado ', cast(nqtd as char));
    end;
else set descs = 'Item Alterado';
end if;

insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
				   values(OLD.Qtd, descs, OLD.Id);

end$

DELIMITER $
drop trigger if exists trgDeleteHistorico$ 
create trigger trgDeleteHistorico before delete 
on tbEstoque
for each row
begin
declare nome varchar(30);   
if(OLD.Qtd = 0)
	then
		set FOREIGN_KEY_CHECKS=0;
		
        set nome = (select Item from vwItems where Id = OLD.Id);
		insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
		   values(OLD.Qtd, concat(nome, ' foi excluído'), OLD.Id);
	else
		SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Quantidade diferente de zero.';
	end if;
end$

DELIMITER $
drop trigger if exists trgDeleteEstoque$ 
create trigger trgDeleteEstoque
after delete 
on tbEstoque
for each row
begin   
set FOREIGN_KEY_CHECKS=1;
end$

DELIMITER $
drop trigger if exists trgColheita$ 
create trigger trgColheita after insert 
on tbColheita
for each row
begin   
if (NEW.`Status` = false) then
	begin
	set FOREIGN_KEY_CHECKS = 0;
    delete from tbPlantio where Id = NEW.IdPedido;
	set FOREIGN_KEY_CHECKS = 1;
    end;
end if;

if(exists(select * from tbColheita where IdProd = NEW.IdProd)) then
	update tbEstoque set Qtd = (Qtd + (NEW.QtdTotal - NEW.QtdPerdas)) where Id = NEW.IdProd;
end if;
end$

DELIMITER $
drop trigger if exists trgItensPlantio$
create trigger trgItensPlantio
before insert 
on tbItensPlantio
for each row
begin
declare qt double;
call spVerQtd(NEW.QtdUsada);
call spCertQtd(NEW.QtdUsada, NEW.IdEstoque);

if((select Tipo from vwItems where Id = NEW.IdEstoque) != 'Máquina')then
	update tbEstoque
	set Qtd = (Qtd - NEW.QtdUsada)
	where Id = NEW.IdEstoque;
else 
	begin
    set qt = (select Quantidade from vwItems where Id = NEW.IdEstoque);
	insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
				   values(qt, 'Máquina utilizada no plantio', NEW.IdEstoque);
	end;
end if;
end$

DELIMITER $
drop trigger if exists trgItensControle$
create trigger trgItensControle
before insert 
on tbItensControle
for each row
begin
declare qt double;
call spVerQtd(NEW.QtdUsada);
call spCertQtd(NEW.QtdUsada, NEW.IdEstoque);

if((select Tipo from vwItems where Id = NEW.IdEstoque) != 'Máquina')then
	update tbEstoque
	set Qtd = (Qtd - NEW.QtdUsada)
	where Id = NEW.IdEstoque;
else 
	begin
    set qt = (select Quantidade from vwItems where Id = NEW.IdEstoque);
	insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
				   values(qt, 'Máquina utilizada no controle', NEW.IdEstoque);
	end;
end if;
end$

DELIMITER $
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


DELIMITER $
drop trigger if exists trgAreaControle$
create trigger trgAreaControle
before insert 
on tbAreaPD
for each row
begin

update tbArea
set Disp = 3
where Id = NEW.IdArea;
end$
DELIMITER ; 

   
use dbEcommerce;
drop view if exists vwEndereco;
create view vwEndereco as(
select E.CEP,  R.Logradouro `Rua`, concat(B.Bairro,' - ', C.Cidade,'/', Es.UF) `BCE` 
 from tbEndereco E 
	inner join tbLogradouro R on E.CEP = R.CEP
	inner join tbBairro B on R.IdBairro = B.Id
	inner join tbCidade C on B.IdCidade = C.Id
	inner join tbEstado Es on C.IdEstado = Es.Id
); 

drop view if exists vwUsuario;
create view vwDadosBancarios as(
select d.Id,
	   u.`UserName` `Nome do Usuário`,
       d.NomeTitular `Titular do Cartão`,
       d.CVV `CVV`,
       d.NumCartao `Número do Cartão`,
       d.Validade `Validade`,
       d.Banco `Banco`
 from tbDadosBancarios d
	inner join tbUsuario u on d.IdUsuario = u.Id
);

drop view if exists vwAnunciante;
create view vwAnunciante as(
select a.IdUsuario `Id`,
	   u.UserName `Nome do Usuário`,
       u.Foto `Foto do Perfil`,
       u.CPF `CPF`,
       u.Email `Email`,
	   a.NomeFazenda `Nome da Fazenda`,
       a.FotoFazenda `Foto da Fazenda`,
       concat(E.Rua,', ', a.NumEnd,' - ', ifnull(a.CompEnd, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço da fazenda`,
       case
		when u.Assinatura = 1 then 'Mensal'
        when u.Assinatura = 2 then 'Semestral'
        when u.Assinatura = 3 then 'Anual'
        else 'Inválido! - Anunciante sem o Organ!'
        end as `Tipo de Assinatura`
 from tbAnunciante a
	inner join vwEndereco E on a.CEP = E.CEP
	inner join tbUsuario u on a.IdUsuario = u.Id
group by a.IdUsuario
);

drop view if exists vwAnuncio;
create view vwAnuncio as(
select 
	A.Id `Id`,
    A.Nome `Anúncio`,
    A.Foto `Foto`,
    A.`Data` `Data de Postagem`, 
    A.Id `IdAnuncio`,
    AN.NomeFazenda `Anunciante`,
    concat(E.Rua,', ', AN.NumEnd,' - ', ifnull(AN.CompEnd, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço do Anunciante`,
    A.`Desc` `Descrição`,
    concat(A.Desconto, '%') `Desconto`,
    A.DuracaoDesc `Duração do Desconto(Em Dias)`,
    P.Nome `Produto`,
    concat(A.Quantidade, ' ', P.UM ) `Quantidade`,
    (P.ValorUnit * A.Quantidade) `Preço`,
    P.Categoria `Categoria`,
    spLike(A.Id) `Likes`
from tbAnuncio A
	inner join tbProduto P on P.Id = A.IdProduto
    inner join tbAnunciante AN on AN.IdUsuario = A.IdAnunciante
    inner join vwEndereco E on E.CEP = AN.CEP
    left join tbAvaliacao av on A.Id = av.IdAnuncio
); 

drop view if exists vwPedido;
create view vwPedido as(
select 
	P.Id `Id`,
    A.IdAnunciante `Anunciante`,
    A.Id `IdAnuncio`,
    A.Nome `Anúncio`,
    P.Id `IdProduto`,
	group_concat(distinct concat(A.Nome, ' - ', PA.Qtd)separator ', ') `Nome do Anúncio - Quantidade Pedida`,
	sum(Pr.ValorUnit * PA.Qtd) `Valor Total s/Desconto`,
    sum((Pr.ValorUnit * PA.Qtd)-((Pr.ValORUnit * PA.Qtd)*(A.Desconto/100))) `Valor Total c/Desconto`,
    U.Id `IdCliente`,
	concat(U.`UserName`, '-', U.CPF) `Comprador - CPF`,
    concat(E.Rua,', ', P.NumEntrega,' - ', ifnull(P.CompEntrega, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço de Entrega`,
    P.ValFrete `Valor do Frete`,
    P.`Status` `Situação do Pedido`,
    DATE_FORMAT(P.`Data`, '%a - %e/%m/%y') `Data do Pedido`
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
    A.Id `IdAnuncio`,
    spUsuario(C.IdUsuario) `Usuário`,
    DATE_FORMAT(C.`Data`, '%e/%m/%y às %H:%i') `Data de Postagem`
from tbComentario C
	inner join tbAnuncio A on A.Id = C.IdAnuncio
);

drop view if exists vwCarrinho;
create view vwCarrinho as(
select 
	C.IdUsuario `Id`,
    C.IdAnuncio `IdAnuncio`,
    A.Nome `Anúncio`,
    C.Qtd `Quantidade adicionada`
from tbCarrinho C
	inner join tbAnuncio A on A.Id = C.IdAnuncio);

drop view if exists vwVenda;
create view vwVenda as(
select 
	V.Id `Id`,
    DATE_FORMAT(V.`Data`, '%a - %e/%m/%y às %H:%i') `Data`,
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
    declare idprod int;
    
    set idprod = (select IdProduto from tbAnuncio where Id = OLD.IdAnuncio);
    set nome = (select nome from tbAnuncio where Id = OLD.IdAnuncio);
    set cat = (select Categoria from tbProduto where Id = idprod);
	insert into tbHistCarrinho value(OLD.IdUsuario, nome, cat, OLD.Qtd);
end$

DELIMITER $
drop trigger if exists trgPedidoNovo$
create trigger trgPedidoNovo
after insert
on tbPedidoAnuncio
for each row
begin
	declare idusu nvarchar(128);
    set idusu = (select IdUsuario from tbPedido where Id = NEW.IdPedido);
	delete from tbCarrinho where ((IdAnuncio = new.IdAnuncio) and (IdUsuario = idusu));
end$

DELIMITER $
drop trigger if exists trgPedidoAprov$
create trigger trgPedidoAprov
after update
on tbPedido
for each row
begin
	if(NEW.`Status` = 1) then
		insert into tbVenda(IdPedido) value(NEW.Id);
	end if;
end$
DELIMITER ;
/*
Format	Description
%a	Abbreviated weekday name (Sun to Sat)
%b	Abbreviated month name (Jan to Dec)
%c	Numeric month name (0 to 12)
%D	Day of the month as a numeric value, followed by suffix (1st, 2nd, 3rd, ...)
%d	Day of the month as a numeric value (01 to 31)
%e	Day of the month as a numeric value (0 to 31)
%f	Microseconds (000000 to 999999)
%H	Hour (00 to 23)
%h	Hour (00 to 12)
%I	Hour (00 to 12)
%i	Minutes (00 to 59)
%j	Day of the year (001 to 366)
%k	Hour (0 to 23)
%l	Hour (1 to 12)
%M	Month name in full (January to December)
%m	Month name as a numeric value (00 to 12)
%p	AM or PM
%r	Time in 12 hour AM or PM format (hh:mm:ss AM/PM)
%S	Seconds (00 to 59)
%s	Seconds (00 to 59)
%T	Time in 24 hour format (hh:mm:ss)
%U	Week where Sunday is the first day of the week (00 to 53)
%u	Week where Monday is the first day of the week (00 to 53)
%V	Week where Sunday is the first day of the week (01 to 53). Used with %X
%v	Week where Monday is the first day of the week (01 to 53). Used with %X
%W	Weekday name in full (Sunday to Saturday)
%w	Day of the week where Sunday=0 and Saturday=6
%X	Year for the week where Sunday is the first day of the week. Used with %V
%x	Year for the week where Monday is the first day of the week. Used with %V
%Y	Year as a numeric, 4-digit value
%y	Year as a numeric, 2-digit value
*/