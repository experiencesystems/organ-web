/* -------------------------------------------------------------	Banco Commerce	------------------------------------------------------------------*/
use sys;
drop database if exists dbEcommerce;
create database dbEcommerce;
use dbEcommerce;
-- =================================================================== USUÁRIO ============================================     
    drop table if exists `AspNetRoles`;
	create table `AspNetRoles`(
	`Id` nvarchar(128)  not null ,
	`Name` nvarchar(256)  not null ,
	primary key (`Id`)) engine = InnoDB;
    
	CREATE UNIQUE index  `RoleNameIndex` on `AspNetRoles` (`Name`);

	drop table if exists `AspNetUserRoles`;
	create table `AspNetUserRoles` (
		`UserId` nvarchar(128)  not null ,
		`RoleId` nvarchar(128)  not null ,
		primary key ( `UserId`,`RoleId`) ) engine = InnoDB;
	 
	CREATE index  `IX_UserId` on `AspNetUserRoles` (`UserId`);
	CREATE index  `IX_RoleId` on `AspNetUserRoles` (`RoleId`);
   
   drop table if exists tbUsuario;
	create table tbUsuario (
		`Id` nvarchar(128)  not null ,
			Foto blob,
			Ativacao bool default true,
			Assinatura int not null,
            CPF numeric(11) not null,
             constraint UQUsuarioCPF unique(CPF),
		`Email` varchar(100) ,-- !
		`ConfirmacaoEmail` bool not null ,
		`SenhaHash` longtext,
		`CarimboSeguranca` longtext,
		`UserName` varchar(50)  not null ,-- !
	      constraint PKAspNetUsers primary key ( `Id`)
	)engine = InnoDB;
    
    insert into tbUsuario(`Id`, `Email`, `ConfirmacaoEmail`, `SenhaHash`, `CarimboSeguranca`, `UserName`, Foto, CPF, Assinatura)
			   values('02719894-e4a9-46c8-999e-ba942abd5f8f', 'milenamonteiro@gmail.com', 0, 
					  'ABecbdkGhzyTR1/t+F8FpUnN+AHXhiXYu4qPCVc4SroxOyzj3p0R+TnWK0p1o6q3Rw==',
                      'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Mirena',  LOAD_FILE("/error.gif"), 111111111111, 0),
                      
                      ('02719894-e4a9-46c8-999e-ba942abd5f8g', 'moreexpsystems@gmail.com', 0,
                      'ABecbdkGhzyTR1/t+F8FpUnN+AHXhiXYu4qPCVc4SroxOyzj3p0R+TnWK0p1o6q3Rw=+',
                      'e7aac8f8-7c92-44fb-9850-5f0fb0024c9b', 'Empresinha', LOAD_FILE("/error.gif"), 111111111112, 1);
    
    drop table if exists `AspNetUserClaims`;
	create table `AspNetUserClaims` (
	`Id` int not null  auto_increment ,
	`UserId` nvarchar(128)  not null ,
	`TipoClaim` longtext,
	`ValorClaim` longtext,
	primary key ( `Id`) )engine = InnoDB;
    
	CREATE index  `IX_UserId` on `AspNetUserClaims` (`UserId`);

	drop table if exists `AspNetUserLogins`;
	create table `AspNetUserLogins` (
		`LoginProvider` nvarchar(128)  not null ,
		`ProviderKey` nvarchar(128)  not null ,
		`UserId` nvarchar(128)  not null ,
		primary key ( `LoginProvider`,`ProviderKey`,`UserId`) )
		engine = InnoDB;

	CREATE index  `IX_UserId` on `AspNetUserLogins` (`UserId`);

	alter table `AspNetUserRoles` add constraint `FK_AspNetUserRoles_AspNetRoles_RoleId`  foreign key (`RoleId`) references `AspNetRoles` ( `Id`)  on update cascade on delete cascade;
	alter table `AspNetUserRoles` add constraint `FK_AspNetUserRoles_AspNetUsers_UserId`  foreign key (`UserId`) references tbUsuario ( `Id`)  on update cascade on delete cascade;
	alter table `AspNetUserClaims` add constraint `FK_AspNetUserClaims_AspNetUsers_UserId`  foreign key (`UserId`) references tbUsuario ( `Id`)  on update cascade on delete cascade; 
	alter table `AspNetUserLogins` add constraint `FK_AspNetUserLogins_AspNetUsers_UserId`  foreign key (`UserId`) references tbUsuario ( `Id`)  on update cascade on delete cascade;

-- =======================================================================================================================

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
	)engine = InnoDB;
    alter table tbDadosBancarios add constraint FKDBUsuario foreign key(IdUsuario) references tbUsuario(`Id`);
	
    insert into tbDadosBancarios(NomeTitular, CVV, Banco, NumCartao, Validade, IdUsuario) values("João Meu Pai", 1111, 1, 11111111111111111, '01/01/01', '02719894-e4a9-46c8-999e-ba942abd5f8f');


-- ================================================== ENDEREÇO ===========================================================
	drop table if exists tbEndereco;
	create table tbEndereco(
		CEP char(8),
		 constraint PKLocalizacao primary key (CEP),
		IdRua int not null
	)engine = InnoDB;
    
    drop table if exists tbLogradouro;
	create table tbLogradouro(
		Id int auto_increment,
		 constraint PKRua primary key (Id),
		Logradouro varchar(40) not null,
		IdBairro int not null
	)engine = InnoDB;
    
	drop table if exists tbBairro;
	create table tbBairro(
		Id int auto_increment,
		 constraint PKBairro primary key (Id),
		Bairro varchar(30) not null,
		IdCidade int not null
	)engine = InnoDB;
    
    drop table if exists tbCidade;
	create table tbCidade(
		Id int auto_increment,
		 constraint PKCidade primary key (Id),
		Cidade varchar(30) not null,
		IdEstado tinyint not null
	)engine = InnoDB;
    
    drop table if exists tbEstado;
	create table tbEstado(
		Id tinyint auto_increment,
		 constraint PKEstado primary key (Id),
		Estado varchar(30) not null,
		UF char(2) not null
	)engine = InnoDB;
    
	alter table tbCidade add constraint FKCidadeEstado foreign key(IdEstado) references tbEstado(Id);
	alter table tbBairro add constraint FKBairroCidade foreign key(IdCidade) references tbCidade(Id);
	alter table tbLogradouro add constraint FKRuaBairro foreign key(IdBairro) references tbBairro(Id);
	alter table tbEndereco add constraint FKEnderecoRua foreign key(IdRua) references tbLogradouro(Id);
    
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
	
    insert into tbCidade(Cidade, IdEstado) values("Osasco", 1);
    insert into tbBairro(Bairro, IdCidade) values("Vila Yara (Real)", 1);
    insert into tbLogradouro(Logradouro, IdBairro) values("Rua das Flores", 1),
														 ("Rua das Árvores", 1);
    insert into tbEndereco(CEP, IdRua) values("00000000", 1),
											 ("11111111", 2);
-- =======================================================================================================================

-- =================================================================== ANÚNCIO ====================================================
	drop table if exists tbProduto;
    create table tbProduto(
		Id int auto_increment,
         constraint PKProduto primary key(Id),
		ValorUnit double not null,
        Quantidade int not null,
        Nome varchar(30) not null
    )engine = InnoDB;

	drop table if exists tbAnuncio;
	create table tbAnuncio(
		Id int auto_increment,
         constraint PKAnuncio primary key(Id),
		Nome varchar(30) not null,
        `Desc` varchar(100) not null,
        `Status` bool not null,
        Foto blob not null,
        Desconto decimal(5,2),
        IdProduto int not null,
        IdUsuario nvarchar(128) not null
    )engine = InnoDB;
    alter table tbAnuncio add constraint FKAnuncioProduto foreign key(IdProduto) references tbProduto(Id),
						  add constraint FKAnuncioUsuario foreign key(IdUsuario) references tbUsuario(`Id`);

	drop table if exists tbWishlist;
    create table tbWishList(
		IdUsuario nvarchar(128) not null,
        IdAnuncio int not null,
		 constraint PKWishList primary key(IdUsuario, IdAnuncio)
    )engine = InnoDB;
    alter table tbWishlist add constraint FKWishlistAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
						   add constraint FKWishlistUsuario foreign key(IdUsuario) references tbUsuario(Id);
    
	drop table if exists tbCarrinho;
    create table tbCarrinho(
		IdUsuario nvarchar(128) not null,
        IdAnuncio int not null,
         constraint PKCarrinho primary key(IdUsuario, IdAnuncio),
        Qtd int not null,
        `Status` int not null
    )engine = InnoDB;
    alter table tbCarrinho add constraint FKCarrinhoAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
						   add constraint FKCarrinhoUsuario foreign key(IdUsuario) references tbUsuario(Id);
	
    drop table if exists tbPedido;
    create table tbPedido(
		Id int auto_increment,
         constraint PKPedido primary key(Id),
		IdAnuncio int not null,
        IdUsuario nvarchar(128) not null,
        `Data` datetime default current_timestamp,
        `Status` int not null
    )engine = InnoDB;
    alter table tbPedido add constraint FKPedidoCarrinho foreign key(IdAnuncio, IdUsuario) references tbCarrinho(IdAnuncio, IdUsuario);

    drop table if exists tbAvaliacao;
    create table tbAvaliacao(
		IdAnuncio int not null,
        IdUsuario nvarchar(128) not null,
         constraint PKAvaliacao primary key(IdAnuncio, IdUsuario),
		`Like` bool default false,
		Nota int
	) engine = InnoDB;
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
    )engine = InnoDB;
	alter table tbComentario add constraint FKComentarioAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
							add constraint FKComentarioUsuario foreign key(IdUsuario) references tbUsuario(Id);

	drop table if exists tbResposta;
    create table tbResposta(
		IdComentario int not null,
        IdResposta int not null,
         constraint PKResposta primary key(IdComentario, IdResposta)
    )engine = InnoDB;
    alter table tbResposta add constraint FKRespostaComentario foreign key(IdComentario) references tbComentario(Id),
						   add constraint FKRespostaResposta foreign key(IdResposta) references tbComentario(Id);
        
    drop table if exists tbPagamento;
	create table tbPagamento(
		Id int auto_increment,
         constraint PKPagamento primary key(Id),
		QtdParcelas int not null default 1,
        VlParcela double not null,
        Tipo int not null 
    )engine = InnoDB;
    
    drop table if exists tbVenda;
    create table tbVenda(
		Id int auto_increment,
         constraint PKVendaAnuncio primary key(Id),
		`Data` datetime default current_timestamp,
        Contrato blob,
        CEP char(8) not null,
        NumEndereco numeric(4) not null,
        CompEndereco varchar(50) not null,
        IdPagamento int not null,
        IdPedido int not null
    )engine = InnoDB;
    alter table tbVenda add constraint FKVendaAnuncioEndereco foreign key(CEP) references tbEndereco(CEP),
							   add constraint FKVendaAnuncioPagamento foreign key(IdPagamento) references tbPagamento(Id),
                               add constraint FKVendaAnuncioPedido foreign key(IdPedido) references tbPedido(Id);
	
    drop table if exists tbEntrega;
    create table tbEntrega(
		Id int auto_increment,
         constraint PKEntrega primary key(Id),
        IdVenda int not null,
        ValorFrete double not null,
        DescFrete double default 0.00
    )engine = InnoDB; 
    alter table tbEntrega add constraint FKEntregaVenda foreign key(IdVenda) references tbVenda(Id);
    
    drop table if exists tbPacote;
    create table tbPacote(
		Id int auto_increment,
         constraint PKPacote primary key(Id),
		IdEntrega int not null,
        Peso double not null,
        Alt double not null,
        Largura double not null,
        Diameto double not null,
        Comp double not null,
        eFragil bool default false
    )engine = InnoDB;
    alter table tbPacote add constraint FKPacoteEntrega foreign key(IdEntrega) references tbEntrega(Id);
-- ================================================================================================================================ alter table tbDespesaFunc add constraint FKDespesaFunc foreign key(IdDespesa) references tbDespesa(Id),           add constraint FKFuncDespesa foreign key(IdFunc) references tbFuncionario(Id)
