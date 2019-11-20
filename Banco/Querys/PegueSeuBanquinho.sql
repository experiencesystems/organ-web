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
	Assinatura int not null,
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

drop table if exists tbEndereco;
create table tbEndereco(
	CEP char(8),
	 constraint PKLocalizacao primary key (CEP),
	IdRua int not null
)engine = innodb;

drop table if exists tbLogradouro;
create table tbLogradouro(
	Id int auto_increment,
	 constraint PKRua primary key (Id),
	Logradouro varchar(40) not null,
	IdBairro int not null
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
alter table tbLogradouro add constraint FKRuaBairro foreign key(IdBairro) references tbBairro(Id);
alter table tbEndereco add constraint FKEnderecoRua foreign key(IdRua) references tbLogradouro(Id);

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
	Foto blob,
	Quantidade double not null,
	Desconto decimal(5,2) default 0,
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
alter table tbWishlist add constraint FKWishlistAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
					   add constraint FKWishlistUsuario foreign key(IdUsuario) references tbUsuario(Id);

drop table if exists tbAvaliacao;
create table tbAvaliacao(
	IdAnuncio int not null,
	IdUsuario nvarchar(128) not null,
	 constraint PKAvaliacao primary key(IdAnuncio, IdUsuario),
	`Like` bool default false,
	Nota int 
) engine = innodb;
alter table tbAvaliacao add constraint FKAvaliacaoAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
						add constraint FKAvaliacaoUsuario foreign key(IdUsuario) references tbUsuario(Id);

drop table if exists tbComentario;
create table tbComentario(
	Id int auto_increment,
	 constraint PKComentario primary key(Id),
	`Data` datetime default current_timestamp,
	`Like` int,
	Deslike int,
	Comentario varchar(100) not null,
	IdAnuncio int not null,
	IdUsuario nvarchar(128) not null
)engine = innodb;
alter table tbComentario add constraint FKComentarioAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
						 add constraint FKComentarioUsuario foreign key(IdUsuario) references tbUsuario(Id);

drop table if exists tbResposta;
create table tbResposta (
    IdComentario int not null,
    IdResposta int not null,
    constraint PKResposta primary key (IdComentario , IdResposta)
)  engine=innodb;
alter table tbResposta add constraint FKRespostaComentario foreign key(IdComentario) references tbComentario(Id),
					   add constraint FKResposta foreign key(IdResposta) references tbComentario(Id);

drop table if exists tbCarrinho;
create table tbCarrinho(
	IdUsuario nvarchar(128) not null,
	IdAnuncio int not null,
	 constraint PKCarrinho primary key(IdUsuario, IdAnuncio),
	Qtd int not null
)engine = innodb;
alter table tbCarrinho add constraint FKCarrinhoAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
					   add constraint FKCarrinhoUsuario foreign key(IdUsuario) references tbUsuario(Id);

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
	`Status` int default 0,
    ValFrete double not null,
    CEPEntrega char(8) not null,
    NumEntrega int not null,
    CompEntrega varchar(50)
)engine = innodb;
alter table tbPedido add constraint FKPedidoUsuario foreign key(IdUsuario) references tbUsuario(`Id`);

create table tbPedidoAnuncio(
	IdPedido int not null,
    IdAnuncio int not null,
     constraint PKPedidoAnuncio primary key(IdPedido, IdAnuncio),
	Qtd int not null
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

drop table if exists tbVenda;
create table tbVenda(
	Id int auto_increment,
	 constraint PKVenda primary key(Id),
	`Data` datetime default current_timestamp,
	Contrato blob,
	IdPagamento int not null,
    IdPedido int not null,
    `Status` bool default false
)engine = innodb;
alter table tbVenda add constraint FKVendaPagamento foreign key(IdPagamento) references tbPagamento(Id),
					add constraint FKVendaPedido foreign key(IdPedido) references tbPedido(Id);

  