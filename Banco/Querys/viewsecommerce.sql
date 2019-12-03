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

drop view if exists vwDadosBancarios;
create view vwDadosBancarios as(
select d.Id,
	   u.`UserName` `Nome do Usuário`,
       d.NomeTitular `Titular do Cartão`,
       d.CVV `CVV`,
       d.NumCartao `Número do Cartão`,
       date_format(d.Validade, '%e/%m/%Y') `Validade`,
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
    P.Categoria `Categoria`
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
    A.IdAnunciante `IdAnunciante`,
    An.NomeFazenda `Anunciante`,
    A.Id `IdAnuncio`,
    A.Nome `Anúncio`,
	group_concat(distinct concat(A.Nome, ' - ', PA.Qtd)separator ', ') `Nome do Anúncio - Quantidade Pedida`,
	sum(Pr.ValorUnit * PA.Qtd) `Valor Total s/Desconto`,
    sum((Pr.ValorUnit * PA.Qtd)-((Pr.ValORUnit * PA.Qtd)*(A.Desconto/100))) `Valor Total c/Desconto`,
    U.Id `IdCliente`,
	concat(U.`UserName`, '-', U.CPF) `Comprador - CPF`,
    concat(E.Rua,', ', P.NumEntrega,' - ', ifnull(P.CompEntrega, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço de Entrega`,
    P.ValFrete `Valor do Frete`,
    case 
		when P.`Status`= 1 then 'Aguardando confirmação' 
		when P.`Status`= 2 then 'Venda Confirmada' 
		when P.`Status`= 3 then 'Enviando para transportadora' 
		when P.`Status`= 4 then 'Entregue'
		when P.`Status`= 5 then 'Entrega Confirmada' 
		when P.`Status`= 6 then 'Cancelado'
        else 'Rejeitado'
        end as `Situação do Pedido`,
    DATE_FORMAT(P.`Data`, '%a - %e/%m/%y') `Data do Pedido`
from tbPedido P
	inner join vwEndereco E on E.CEP = P.CEPEntrega
    inner join tbUsuario U on U.`Id` = P.IdUsuario
    inner join tbPedidoAnuncio PA on PA.IdPedido = P.Id
    inner join tbAnuncio A on A.Id = PA.IdAnuncio
    inner join tbProduto Pr on Pr.Id = A.IdProduto
    inner join tbAnunciante An on a.IdAnunciante = An.IdUsuario 
group by P.Id, A.IdAnunciante, A.Id, E.Rua, E.BCE
);

drop view if exists vwComentario;
create view vwComentario as(
select 
	C.Id `Id`,    
    spUsuario(C.IdUsuario) `Usuário`,
    C.Comentario `Comentário`,
    A.Id `IdAnuncio`,
    A.Nome `Anuncio`,
    DATE_FORMAT(C.`Data`, '%e/%m/%y às %H:%i') `Data de Postagem`,
    isComprador(C.IdUsuario, C.IdAnuncio) `Usuário comprou o produto?`
from tbComentario C
	inner join tbAnuncio A on A.Id = C.IdAnuncio
);

drop view if exists vwWishlist;
create view vwWishlist as(
select w.IdUsuario,
	   w.IdAnuncio,
	   ifnull(a.Nome, 'Wishlist Vazia!') `Anúncios Salvos`
       from tbWishlist w 
       left join tbAnuncio a
		on a.Id = w.IdAnuncio
);

drop view if exists vwCarrinho;
create view vwCarrinho as(
select 
	C.IdUsuario `IdUsuario`,
    C.IdAnuncio `IdAnuncio`,
    A.Nome `Anúncio`,
    C.Qtd `Quantidade adicionada`
from tbCarrinho C
	inner join tbAnuncio A on A.Id = C.IdAnuncio
where C.`Status` = true
);

drop view if exists vwVenda;
create view vwVenda as(
select 
	V.Id `Id`,
    P.IdUsuario `IdCliente`,
    DATE_FORMAT(V.`Data`, '%a - %e/%m/%y às %H:%i') `Data da Venda`,
	group_concat(distinct concat(A.Nome, ' - ', PA.Qtd)separator ', ') `Nome do Anúncio - Quantidade Pedida`,
	(sum((Pr.ValorUnit * PA.Qtd)-((Pr.ValorUnit * PA.Qtd)*(A.Desconto/100))) + P.ValFrete) `Valor Total`,
	concat(E.Rua,', ', P.NumEntrega,' - ', ifnull(P.CompEntrega, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço de Entrega`,
    An.IdUsuario `IdAnunciante`, An.NomeFazenda `Anunciante`,
    case
		when V.`Status` = true then 'Venda finalizada'
        else 'Venda em andamento'
        end as `Situação da Venda`
from tbVenda V
	inner join tbPedido P on P.Id = V.IdPedido
	inner join vwEndereco E on E.CEP = P.CEPEntrega
    inner join tbUsuario U on U.`Id` = P.IdUsuario
    inner join tbPedidoAnuncio PA on PA.IdPedido = P.Id
    inner join tbAnuncio A on A.Id = PA.IdAnuncio
    inner join tbProduto Pr on Pr.Id = A.IdProduto
    inner join tbAnunciante An on An.IdUsuario = A.IdAnunciante
group by v.Id, A.IdAnunciante, E.Rua, E.BCE
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
    declare idc int;
    set idusu = (select IdUsuario from tbPedido where Id = NEW.IdPedido);
	update tbCarrinho set `Status` = false where IdUsuario = idusu;
end$

DELIMITER $
drop trigger if exists trgPedidoAprov$
create trigger trgPedidoAprov
after update
on tbPedido
for each row
begin
	if(NEW.`Status` = 2) then
		insert into tbVenda(IdPedido) value(NEW.Id);
	end if;
end$
DELIMITER ;

DELIMITER $
drop trigger if exists trgDeleteAnuncio$
DELIMITER $
create trigger trgDeleteAnuncio
before delete
on tbAnuncio
for each row
begin
declare ped int;
	if(exists(select * from tbAnuncioPedido where IdAnuncio = OLD.Id)) then
		set ped = (select IdPedido from tbAnuncioPedido where IdAnuncio = OLD.Id order by IdPedido desc limit 1);
		if((select `Status` from tbPedido where Id = ped) != 1) then
			signal sqlstate '45000'
				set message_text = 'Existem pedidos que aguardam respota! É impossível excluir esse anúncio!';
		end if;
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