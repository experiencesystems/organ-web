drop database if exists dbEcommerce;
create database dbEcommerce;
use dbEcommerce;

drop table if exists `AspNetRoles`;
create table `AspNetRoles`(
	`Id` nvarchar(128)  not null ,
	`Name` nvarchar(256)  not null ,
	 constraint PKAspNetRoles primary key (`Id`)
) engine = innodb;

create unique index  `RoleNameIndex` on `AspNetRoles` (`Name`);

drop table if exists `AspNetUserRoles`;
create table `AspNetUserRoles` (
	`UserId` nvarchar(128)  not null ,
	`RoleId` nvarchar(128)  not null ,
	constraint PKAspNetUserRoles primary key ( `UserId`,`RoleId`)
) engine = innodb;
 
create index  `IX_UserId` on `AspNetUserRoles` (`UserId`);
create index  `IX_RoleId` on `AspNetUserRoles` (`RoleId`);

drop table if exists tbUsuario;
create table tbUsuario (
	`Id` nvarchar(128)  not null ,
	Foto mediumblob,
	Assinatura int default 4,
	CPF numeric(11) not null,
	 constraint UQUsuarioCPF unique(CPF),
	`Email` varchar(100) ,-- !
	`ConfirmacaoEmail` bool not null ,
	`SenhaHash` longtext,
	`CarimboSeguranca` longtext,
	`UserName` varchar(50) not null,-- !
	  constraint PKAspNetUsers primary key ( `Id`)
)engine = innodb;

drop table if exists `AspNetUserClaims`;
create table `AspNetUserClaims` (
	`Id` int not null  auto_increment ,
	`UserId` nvarchar(128)  not null ,
	`TipoClaim` longtext,
	`ValorClaim` longtext,
	constraint PKAspNetUserClaims primary key ( `Id`)
)engine = innodb;

create index  `IX_UserId` on `AspNetUserClaims` (`UserId`);

drop table if exists `AspNetUserLogins`;
create table `AspNetUserLogins` (
	`LoginProvider` nvarchar(128)  not null ,
	`ProviderKey` nvarchar(128)  not null ,
	`UserId` nvarchar(128)  not null ,
	constraint PKAspNetUserLogins primary key ( `LoginProvider`,`ProviderKey`,`UserId`)
)engine = innodb;

create index  `IX_UserId` on `AspNetUserLogins` (`UserId`);

alter table `AspNetUserRoles` add constraint `FK_AspNetUserRoles_AspNetRoles_RoleId`  foreign key (`RoleId`) references `AspNetRoles` ( `Id`)  on update cascade on delete cascade,
							  add constraint `FK_AspNetUserRoles_AspNetUsers_UserId`  foreign key (`UserId`) references tbUsuario ( `Id`)  on update cascade on delete cascade;
alter table `AspNetUserClaims` add constraint `FK_AspNetUserClaims_AspNetUsers_UserId`  foreign key (`UserId`) references tbUsuario ( `Id`)  on update cascade on delete cascade; 
alter table `AspNetUserLogins` add constraint `FK_AspNetUserLogins_AspNetUsers_UserId`  foreign key (`UserId`) references tbUsuario ( `Id`)  on update cascade on delete cascade;

drop table if exists tbDadosBancarios;
create table tbDadosBancarios(
	Id int auto_increment,
	 constraint PKDadosBancarios primary key(Id),
	NomeTitular varchar(30) not null,
	CVV numeric(4) not null,
	Banco int, -- Listinha dos Banco s2 s2
	NumCartao numeric(19) not null,
	Validade date not null, 
	IdUsuario nvarchar(128) not null
)engine = innodb;
alter table tbDadosBancarios add constraint FKDBUsuario foreign key(IdUsuario) references tbUsuario(`Id`);

drop table if exists tbAnunciante;
create table tbAnunciante(
	IdUsuario nvarchar(128) not null,
     constraint PKAnunciante primary key(IdUsuario),
    NomeFazenda varchar(50) not null,
    NomeBanco varchar(50),
    NumEnd int not null,
    CompEnd varchar(50),
    CEP char(8) not null
)engine = innodb;
alter table tbAnunciante add constraint FKAnuncianteUsuario foreign key(IdUsuario) references tbUsuario(`Id`);

create table tbEndereco(
	CEP char(8),
	 constraint PKLocalizacao primary key (CEP)
)engine = innodb;

drop table if exists tbLogradouro;
create table tbLogradouro(
	Id int auto_increment,
	 constraint PKRua primary key (Id),
	Logradouro varchar(80) not null,
	IdBairro int not null,
	CEP char(8) not null
)engine = innodb;

drop table if exists tbBairro;
create table tbBairro(
	Id int auto_increment,
	 constraint PKBairro primary key (Id),
	Bairro varchar(30) not null,
	IdCidade int not null
)engine = innodb;

drop table if exists tbCidade;
create table tbCidade(
	Id int auto_increment,
	 constraint PKCidade primary key (Id),
	Cidade varchar(30) not null,
	IdEstado tinyint not null
)engine = innodb;

drop table if exists tbEstado;
create table tbEstado(
	Id tinyint auto_increment,
	 constraint PKEstado primary key (Id),
	Estado varchar(30) not null,
	UF char(2) not null
)engine = innodb;

alter table tbCidade add constraint FKCidadeEstado foreign key(IdEstado) references tbEstado(Id);
alter table tbBairro add constraint FKBairroCidade foreign key(IdCidade) references tbCidade(Id);
alter table tbLogradouro add constraint FKRuaBairro foreign key(IdBairro) references tbBairro(Id),
						 add constraint FKRuaCEP foreign key(CEP) references tbEndereco(CEP);

alter table tbAnunciante add constraint FKAnuncianteEndereco foreign key(CEP) references tbEndereco(CEP);

drop table if exists tbUM;
create table tbUM(
Id varchar(6) not null,
 constraint PKUM primary key(Id),
`Desc` varchar(20) not null
)engine = InnoDB;

drop table if exists tbProduto;
create table tbProduto(
	Id int auto_increment,
	 constraint PKProduto primary key(Id),
	ValorUnit double not null,
    UM varchar(6),
	Nome varchar(30) not null,
    Categoria int not null
)engine = innodb;
alter table tbProduto add constraint FKProdutoUM foreign key(UM) references tbUM(Id);

drop table if exists tbAnuncio;
create table tbAnuncio(
	Id int auto_increment,
	 constraint PKAnuncio primary key(Id),
	Nome varchar(30) not null,
	`Desc` varchar(100),
	`Status` bool default true,
	Foto mediumblob,
	Quantidade double not null,
	Desconto int default 0, 
	DuracaoDesc int default 0,
    DataDesc datetime,
	IdProduto int not null,
	IdAnunciante nvarchar(128) not null,
    `Data` datetime default current_timestamp
)engine = innodb;
alter table tbAnuncio add constraint FKAnuncioProduto foreign key(IdProduto) references tbProduto(Id),
					  add constraint FKAnuncioAnunciante foreign key(IdAnunciante) references tbAnunciante(IdUsuario);  

drop table if exists tbWishlist;
create table tbWishList(
	IdUsuario nvarchar(128) not null,
	IdAnuncio int not null,
	 constraint PKWishList primary key(IdUsuario, IdAnuncio)
)engine = innodb;
alter table tbWishlist add constraint FKWishlistAnuncio foreign key(IdAnuncio) references tbAnuncio(Id) on delete cascade,
					   add constraint FKWishlistUsuario foreign key(IdUsuario) references tbUsuario(Id);

drop table if exists tbAvaliacao;
create table tbAvaliacao(
	IdAnuncio int not null,
	IdUsuario nvarchar(128) not null,
	 constraint PKAvaliacao primary key(IdAnuncio, IdUsuario),
	Nota int 
) engine = innodb;
alter table tbAvaliacao add constraint FKAvaliacaoAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
						add constraint FKAvaliacaoUsuario foreign key(IdUsuario) references tbUsuario(Id);

drop table if exists tbComentario;
create table tbComentario(
	Id int auto_increment,
	 constraint PKComentario primary key(Id),
	`Data` datetime default current_timestamp,
	Comentario varchar(100) not null,
	IdAnuncio int not null,
	IdUsuario nvarchar(128) not null
)engine = innodb;
alter table tbComentario add constraint FKComentarioAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
						 add constraint FKComentarioUsuario foreign key(IdUsuario) references tbUsuario(Id);

drop table if exists tbCarrinho;
create table tbCarrinho(
	Id int auto_increment,
	IdUsuario nvarchar(128) not null,
	IdAnuncio int not null,
	 constraint PKCarrinho primary key(Id),
	Qtd int not null, 
    `Status` bool default true 
)engine = innodb;
alter table tbCarrinho add constraint FKCarrinhoAnuncio foreign key(IdAnuncio) references tbAnuncio(Id) on delete cascade,
					   add constraint FKCarrinhoUsuario foreign key(IdUsuario) references tbUsuario(Id) on delete cascade;

drop table if exists tbHistCarrinho;
create table tbHistCarrinho(
	IdUsuario nvarchar(128) not null,
	NomeAnuncio varchar(30) not null,
    CategoriaAnuncio varchar(30) not null,
	Id int not null,
	 constraint PKHistCarrinho primary key(Id),
	Qtd int not null
);

drop table if exists tbPedido;
create table tbPedido(
	Id int auto_increment,
	 constraint PKPedido primary key(Id),
	IdUsuario nvarchar(128) not null,
	`Data` datetime default current_timestamp,
	`Status` int default 1,	
	IdPagamento int not null,
    ValFrete double not null,
    CEPEntrega char(8) not null,
    NumEntrega int not null,
    CompEntrega varchar(50)
)engine = innodb;
alter table tbPedido add constraint FKPedidoUsuario foreign key(IdUsuario) references tbUsuario(`Id`);

drop table if exists tbPedidoAnuncio;
create table tbPedidoAnuncio(
	IdPedido int,
    IdAnuncio int,
     constraint PKPedidoAnuncio primary key(IdPedido, IdAnuncio),
	Qtd int 
)engine = innodb;
alter table tbPedidoAnuncio add constraint FKPedidoAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
							add constraint FKAnuncioPedido foreign key(IdPedido) references tbPedido(Id);

drop table if exists tbPagamento;
create table tbPagamento(
	Id int auto_increment,
	 constraint PKPagamento primary key(Id),
	QtdParcelas int not null default 1,
	VlParcela double not null,
	Tipo int not null 
)engine = innodb;
alter table tbPedido add constraint FKPedidoPagamento foreign key(IdPagamento) references tbPagamento(Id);

drop table if exists tbVenda;
create table tbVenda(
	Id int auto_increment,
	 constraint PKVenda primary key(Id),
	`Data` datetime default current_timestamp,
    IdPedido int not null,
    `Status` bool default false
)engine = innodb;
alter table tbVenda add constraint FKVendaPedido foreign key(IdPedido) references tbPedido(Id);

use dbEcommerce;
DELIMITER $
drop function if exists spIsAn$
create function spIsAn(IdU nvarchar(128))
	returns boolean DETERMINISTIC
begin
	declare resp boolean;
	
	if(exists(select * from tbAnunciante where IdUsuario = IdU)) then
	 set resp = true;
	else
	 set resp = false;
	end if;
	return resp;
end$

DELIMITER $
drop function if exists spUsuario$
create function spUsuario(IdU nvarchar(128))
	returns varchar(50) DETERMINISTIC
begin
	declare nome varchar(50);
    
    if(spIsAn(IdU)) then
		set nome = (select NomeFazenda from tbAnunciante where IdUsuario = IdU);
	else
		set nome = (select `UserName` from tbUsuario where Id = IdU);
	end if;
    return nome;
end$

DELIMITER $
drop function if exists isComprador$
create function isComprador(IdUsu nvarchar(128), IdAn int)
	returns char(3) DETERMINISTIC
begin
	declare resp char(3);
    declare IdP int;

    if(exists(select * from tbPedido where IdUsuario = IdUsu)) then
		set IdP = (select p.Id from tbPedido p
					inner join tbPedidoAnuncio pa on pa.IdPedido = p.Id
				   where p.IdUsuario = IdUsu and pa.IdAnuncio = IdAn);
		if(exists(select * from tbVenda where IdPedido = IdP)) then
			set resp = 'Sim';
		else
			set resp = 'Não';
		end if;
	else
		set resp = 'Não';
    end if;
    return resp;
end$

DELIMITER $
drop event if exists VerDesc$
CREATE EVENT VerDesc
    ON SCHEDULE EVERY 3 HOUR -- e executa a cada três horas
				STARTS CURRENT_TIMESTAMP -- evento começa agora
    COMMENT 'Evento pra duração do desconto :D'
    DO begin 
		declare dtini, dtfim datetime;
        declare dia, ida, idm int;
--         declare done bool default false;
--         declare cur cursor for select DataDesc, DuracaoDesc from tbAnuncio; -- cursor ta setado pra cada id na tbanuncio onde desconto > 0
--         declare continue handler for not found set done = true; -- se o cursor não achar mais nada ele seta done pra true
--         
--         open cur; -- abre o cursor
		set ida = (select Id from tbAnuncio order by Id limit 1);
        set idm = (select max(id) from tbAnuncio);
        loopa : loop -- abre looping
-- 			fetch cur into dtini, dia; -- coloca o id dentro da variavale ida
            if (ida = idm) then -- diz quando fechar o looping, qnd done for true q só acontece quando ele não acha mais nada
				leave loopa; -- sai do loop
			end if;
            -- código que verifica se a data de desconto já foi
            
            set dtini = (select DataDesc from tbAnuncio where Id = ida);
            set dia = (select DuracaoDesc from tbAnuncio where Id = ida);
            set dtfim = date_add(dtini, interval dia day);
            
            if(dtfim <= now()) then
				update tbAnuncio set Desconto = 0, DuracaoDesc = 0, DataDesc = null where Id = ida;
            end if;
            
            set ida = ida + 1;
		end loop loopa; -- fim do loop
--         close cur; -- fim do cursor
	END$
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

use dbEcommerce;

insert into tbEstado(Estado, UF) values("São Paulo", "SP"),
									("Acre", "AC"),
									("Alagoas", "AL"),
									("Amapá", "AP"),
									("Amazonas", "AM"),
									("Bahia", "BA"),
									("Ceará", "CE"),
									("Distrito Federal(Brasília)", "DF"),
									("Espiríto Santo", "ES"),
									("Goiás", "GO"),
									("Maranhão", "MA"),
									("Mato Grosso", "MT"),
									("Mato Grosso do Sul", "MS"),
									("Minas Gerais", "MG"),
									("Pará", "PA"),
									("Paraíba", "PB"),
									("Paraná", "PR"),
									("Pernambuco", "PE"),
									("Piauí", "PI"),
									("Rio Grande do Norte", "RN"),
									("Rio Grande do Sul", "RS"),
									("Rio de Janeiro", "RJ"),
									("Rondônia", "RO"),
									("Roraima", "RR"),
									("Santa Catarina", "SC"),
									("Sergipe", "SE"),
									("Tocantins", "TO");

insert into tbCidade(Cidade, IdEstado) values("Cuiabá", 12);
insert into tbBairro(Bairro, IdCidade) values("Baú", 1);
insert into tbEndereco(CEP) values("78008105"),
								  ("78008110");
										 
insert into tbLogradouro(Logradouro, IdBairro, CEP) values("Avenida Bosque da Saúde - até 161/162", 1, "78008105"),
														  ("Avenida Historiador Rubens de Mendonça - até 1250 - lado par", 1, "78008110");
                                         
insert into tbUsuario(`Id`, `Email`, `ConfirmacaoEmail`, `SenhaHash`, `CarimboSeguranca`, `UserName`, Foto, CPF, Assinatura)
values('02719894-e4a9-46c8-999e-ba942abd5f8f', 'jessica@gmail.com', 0, 
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9fd6-39f7c6a99a52', 'Jéssica',  load_file('C:\Jessica.png'), 11111111111, 4),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8g', 'expfarms@gmail.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Experience Farms', load_file('C:\farm.png'), 11111111112, 1),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8h', 'fazendinha@a.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9850-5f0fb0024c9a', 'Fazendinha Feliz', load_file('C:\farm.png'), 11111111113, 3),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8i', 'fazenda@a.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9850-5f0fb0024c9a', 'Fazenda da Serra', load_file('C:\farm.png'), 11111111114, 3);
       
insert into `AspNetRoles` (`Id`, `Name`) value ('02719894-e4a9-46c8-999e-ba942abd5f8u', 'Admin');

insert into `AspNetUserRoles` (`UserId`, `RoleId`) values ('02719894-e4a9-46c8-999e-ba942abd5f8g', '02719894-e4a9-46c8-999e-ba942abd5f8u'),
														  ('02719894-e4a9-46c8-999e-ba942abd5f8h', '02719894-e4a9-46c8-999e-ba942abd5f8u'),
														  ('02719894-e4a9-46c8-999e-ba942abd5f8i', '02719894-e4a9-46c8-999e-ba942abd5f8u');

insert into tbDadosBancarios(NomeTitular, CVV, Banco, NumCartao, Validade, IdUsuario) values("João Meu Pai", 1111, 1, 11111111111111111, '01/01/01', '02719894-e4a9-46c8-999e-ba942abd5f8f');

       
insert into tbAnunciante(IdUsuario, NomeFazenda, NumEnd, CEP, NomeBanco) values('02719894-e4a9-46c8-999e-ba942abd5f8g', 'Experience Farms', 1, "78008105", 'dbOrgan'),
																			   ('02719894-e4a9-46c8-999e-ba942abd5f8h', 'Fazenda Triste', 2, "78008110", 'dbOrgan 1'),
																			   ('02719894-e4a9-46c8-999e-ba942abd5f8i', 'Fazenda Triste', 2, "78008110", 'dbOrgan 2');
select * from vwDadosBancarios;
select * from vwAnunciante;

insert into tbUM value('SC', 'Saca(s) - 1Kg');
insert into tbUM value('UN', 'Unidade(s)');
insert into tbUM value('DZ', 'Dúzia(s)');
insert into tbUM value('CX', 'Caixa(s)');
insert into tbProduto(ValorUnit, UM, Nome, Categoria) values(10.00, 'SC', 'Semente de Soja', 10), 
															(15.00, 'SC', 'Semente de Milho', 10), 
															(15.00, 'SC', 'Soja', 3), 
															(4.00, 'UN', 'Morango', 7), 
															(17.50, 'DZ', 'Laranja', 7), 
															(3000.00, 'UN', 'Adubadora', 12), 
															(5000.00, 'UN', 'Adubadora500x', 12), 
															(700.00, 'UN', 'Colheitadeira', 15), 
															(28.90, 'SC', 'Cenoura', 6), 
															(14.00, 'DZ', 'Milho', 9), 
															(25.00, 'UN', 'Pá', 16), 
															(27.50, 'SC', 'Semente de Abóbora', 10), 
															(27.50, 'SC', 'Semente de Soja Orgânica', 5),  
															(4.00, 'UN', 'Morango', 7), 
                                                            (4.00, 'UN', 'Alho', 7), 
                                                            (4.00, 'UN', 'Cebola Roxa', 7), 
                                                            (4.00, 'SC', 'Pimentão', 7), 
                                                            (4.00, 'UN', 'Pimenta', 7), 
                                                            (4.00, 'UN', 'Ervilha', 7), 
                                                            (4.00, 'UN', 'Repolho Roxo', 7), 
                                                            (4.00, 'UN', 'Maçã', 7), 
                                                            (4.00, 'SC', 'Tomate', 7), 
                                                            (4.00, 'SC', 'Brocolis', 7); 
                                                                        
insert into tbAnuncio(Nome, `Desc`, `Status`, Foto, IdProduto, IdAnunciante, Quantidade, Desconto, DuracaoDesc, DataDesc)
	values('Sacas de Soja', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'),3, '02719894-e4a9-46c8-999e-ba942abd5f8g', 3, null, null, null),
		  ('Sacas de Cebola Roxa', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 16, '02719894-e4a9-46c8-999e-ba942abd5f8h', 5, 10, 1, NOW()),
		  ('Sacas de Brocolis', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 23, '02719894-e4a9-46c8-999e-ba942abd5f8i', 2, 50, 4, NOW()),
		  ('Sacas de Pimenta', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 3, '02719894-e4a9-46c8-999e-ba942abd5f8g', 4, null, null, null),
		  ('Milhão Bão', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8h', 5, null, null, null),
		  ('Pá da Boa', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 11, '02719894-e4a9-46c8-999e-ba942abd5f8i', 6, null, null, null),
          ('Semente de Milho', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 2, '02719894-e4a9-46c8-999e-ba942abd5f8g', 2, null, null, null),
          ('Adubadoras Incriveís', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 6, '02719894-e4a9-46c8-999e-ba942abd5f8h', 2, 5, 15, NOW()),
          ('12 Sacas Semente de Abóbora', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 12, '02719894-e4a9-46c8-999e-ba942abd5f8i', 12, 10, 3, NOW()),
          ('2 Dúzias de Milho', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 10, '02719894-e4a9-46c8-999e-ba942abd5f8g', 2, null, null, null),
          ('Sementes de Soja Transgênica', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 1, '02719894-e4a9-46c8-999e-ba942abd5f8h', 2, null, null, null),
          ('Sementes de Soja Orgânica', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 13, '02719894-e4a9-46c8-999e-ba942abd5f8i', 2, 15, 5, NOW()),
          ('Cenouras Da Estação', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8g', 12, null, null, null),
          ('SACÃO DE CENOURA', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8h', 21, 30, 5, NOW()),
          ('SACÃO DE ALHO', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 7, '02719894-e4a9-46c8-999e-ba942abd5f8i', 11, 15, 4, NOW()),
          ('SACÃO DE PIMENTÃO', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 17, '02719894-e4a9-46c8-999e-ba942abd5f8g', 23, null, null, null),
          ('SACÃO DE TOMATE', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 22, '02719894-e4a9-46c8-999e-ba942abd5f8h', 15, 10, 7, NOW()),
          ('Adubadora5000X', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 7, '02719894-e4a9-46c8-999e-ba942abd5f8i', 1, 25, 5, NOW()),
          ('Colheitadeira 2001 Semi-Nova', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 8, '02719894-e4a9-46c8-999e-ba942abd5f8g', 1, null, null, null),
          ('Milho Transgênico', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 10, '02719894-e4a9-46c8-999e-ba942abd5f8h', 7, 25, 7, NOW()),
          ('Morangos Orgânicos', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 4, '02719894-e4a9-46c8-999e-ba942abd5f8i', 8, null, null, null),
          ('Repolho Roxo Orgânico', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 20, '02719894-e4a9-46c8-999e-ba942abd5f8g', 17, 40, 7, NOW()),
          ('Maçãs Orgânicas!', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 21, '02719894-e4a9-46c8-999e-ba942abd5f8h', 21, null, null, null),
          ('Ervilhas', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 4, '02719894-e4a9-46c8-999e-ba942abd5f8i', 19, null, null, null),
          ('2  Cenoura pelo preço de 1', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8g', 24, 50, 15, NOW()),
          ('Black Friday do Milharal', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 2, '02719894-e4a9-46c8-999e-ba942abd5f8h', 4, 70, 1, NOW());

insert into tbAnuncio(Nome, `Desc`, `Status`, Foto, IdProduto, IdAnunciante, Quantidade, Desconto, DuracaoDesc, DataDesc)
	values('Soja', 'É assim!', true, load_file("C:\farm.jpg"), 3, '02719894-e4a9-46c8-999e-ba942abd5f8g', 2, 10, 1, '19/11/27 17:00');          
          
insert into tbWishList value('02719894-e4a9-46c8-999e-ba942abd5f8f', 4);

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Espero poder comprar essas pás', 6, '02719894-e4a9-46c8-999e-ba942abd5f8f');

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Também!', 6, '02719894-e4a9-46c8-999e-ba942abd5f8h');

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Então compra!', 6, '02719894-e4a9-46c8-999e-ba942abd5f8f');

select * from vwComentario;

insert into tbCarrinho(IdUsuario, IdAnuncio, Qtd) values('02719894-e4a9-46c8-999e-ba942abd5f8f', 1, 1),
														('02719894-e4a9-46c8-999e-ba942abd5f8f', 2, 2),
														('02719894-e4a9-46c8-999e-ba942abd5f8f', 3, 1);

select * from vwCarrinho;

insert into tbPagamento(QtdParcelas, VlParcela, Tipo) value(2, 1.00, 1);     
                        
insert into tbPedido (IdUsuario, ValFrete, CEPEntrega, NumEntrega, IdPagamento) values('02719894-e4a9-46c8-999e-ba942abd5f8f', 3.00, "78008110", 1, 1),
																					  ('02719894-e4a9-46c8-999e-ba942abd5f8f', 3.25, "78008110", 7, 1);
                                                                                                                                                                  
insert into tbPedidoAnuncio(IdPedido, IdAnuncio, Qtd) values(1, 1, 1);
insert into tbPedidoAnuncio(IdPedido, IdAnuncio, Qtd) values(1, 2, 2);
insert into tbPedidoAnuncio(IdPedido, IdAnuncio, Qtd) values(2, 3, 1);

select * from vwPedido;
select * from vwCarrinho; 
                                  
update tbPedido set `Status` = 2 where Id = 1;
select * from vwPedido;
select * from vwVenda;

insert into tbAvaliacao value(1, '02719894-e4a9-46c8-999e-ba942abd5f8f', 4);

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Adorei as Sojas, super veganaaaas!', 1, '02719894-e4a9-46c8-999e-ba942abd5f8f');
select * from vwAnuncio;
select * from vwComentario;

