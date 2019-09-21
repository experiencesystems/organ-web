use sys;
drop database if exists dbOrgan;
create database dbOrgan;
use dbOrgan;

-- =================================================================== USUÁRIO ============================================ 
    drop table if exists `AspNetRoles`;
    create table `AspNetRoles`(
		`Id` nvarchar(128)  not null ,
		`Name` nvarchar(256)  not null ,
		primary key (`Id`)) 
		engine=InnoDb auto_increment=0;
        
	CREATE UNIQUE index  `RoleNameIndex` on `AspNetRoles` (`Name`);
    
    drop table if exists `AspNetUserRoles`;
	create table `AspNetUserRoles` (
		`UserId` nvarchar(128)  not null ,
		`RoleId` nvarchar(128)  not null ,
		primary key ( `UserId`,`RoleId`) )	
		engine=InnoDb auto_increment=0;
        
	CREATE index  `IX_UserId` on `AspNetUserRoles` (`UserId`);
	CREATE index  `IX_RoleId` on `AspNetUserRoles` (`RoleId`);
    
    drop table if exists `AspNetUsers`;
	create table `AspNetUsers` (
		`Id` nvarchar(128)  not null ,
			`DataCadastro` datetime default current_timestamp(),
			`Confirmacao` bool not null,
			`Ativacao` bool not null default true,
			`Assinatura` bool not null,
            `IdPessoa` int not null,
		`Email` nvarchar(256) ,
		`EmailConfirmed` bool not null ,
		`PasswordHash` longtext,
		`SecurityStamp` longtext,
		`PhoneNumber` longtext,
		`PhoneNumberConfirmed` bool not null ,
		`TwoFactorEnabled` bool not null ,
		`LockoutEndDateUtc` datetime,
		`LockoutEnabled` bool not null ,
		`AccessFailedCount` int not null ,
		`UserName` nvarchar(256)  not null ,
		primary key ( `Id`) )
		engine=InnoDb auto_increment=0;
        
	CREATE UNIQUE index  `UserNameIndex` on `AspNetUsers` (`UserName`);
    
    drop table if exists `AspNetUserClaims`;
	create table `AspNetUserClaims` (
		`Id` int not null  auto_increment ,
		`UserId` nvarchar(128)  not null ,
		`ClaimType` longtext,
		`ClaimValue` longtext,
		primary key ( `Id`) ) 
		engine=InnoDb auto_increment=0;
        
	CREATE index  `IX_UserId` on `AspNetUserClaims` (`UserId`);
    
    drop table if exists `AspNetUserLogins`;
	create table `AspNetUserLogins` (
		`LoginProvider` nvarchar(128)  not null ,
		`ProviderKey` nvarchar(128)  not null ,
		`UserId` nvarchar(128)  not null ,
		primary key ( `LoginProvider`,`ProviderKey`,`UserId`) ) 
		engine=InnoDb auto_increment=0;
        
	CREATE index  `IX_UserId` on `AspNetUserLogins` (`UserId`);
    
	alter table `AspNetUserRoles` add constraint `FK_AspNetUserRoles_AspNetRoles_RoleId`  foreign key (`RoleId`) references `AspNetRoles` ( `Id`)  on update cascade on delete cascade;
	alter table `AspNetUserRoles` add constraint `FK_AspNetUserRoles_AspNetUsers_UserId`  foreign key (`UserId`) references `AspNetUsers` ( `Id`)  on update cascade on delete cascade;
	alter table `AspNetUserClaims` add constraint `FK_AspNetUserClaims_AspNetUsers_UserId`  foreign key (`UserId`) references `AspNetUsers` ( `Id`)  on update cascade on delete cascade; 
	alter table `AspNetUserLogins` add constraint `FK_AspNetUserLogins_AspNetUsers_UserId`  foreign key (`UserId`) references `AspNetUsers` ( `Id`)  on update cascade on delete cascade;
-- =======================================================================================================================

-- =================================================================== ENDEREÇO ==========================================
	drop table if exists tbEndereco;
    create table tbEndereco(
		CEP char(8),
		 constraint PKLocalizacao primary key (CEP),
		IdRua int not null
	);
    
    drop table if exists tbRua;
	create table tbRua(
		Id int auto_increment,
		 constraint PKRua primary key (Id),
		Logradouro varchar(50) not null,
		IdBairro int not null
	);
    
	drop table if exists tbBairro;
	create table tbBairro(
		Id int auto_increment,
		 constraint PKBairro primary key (Id),
		Bairro varchar(50) not null,
		IdCidade int not null
	);
    
    drop table if exists tbCidade;
	create table tbCidade(
		Id int auto_increment,
		 constraint PKCidade primary key (Id),
		Cidade varchar(50) not null,
		IdEstado tinyint not null
	);
    
    drop table if exists tbEstado;
	create table tbEstado(
		Id tinyint auto_increment,
		 constraint PKEstado primary key (Id),
		Estado varchar(50) not null,
		UF char(2) not null
	);
    
	alter table tbCidade add constraint FKCidadeEstado foreign key(IdEstado) references tbEstado(Id);
	alter table tbBairro add constraint FKBairroCidade foreign key(IdCidade) references tbCidade(Id);
	alter table tbRua add constraint FKRuaBairro foreign key(IdBairro) references tbBairro(Id);
	alter table tbEndereco add constraint FKEnderecoRua foreign key(IdRua) references tbRua(Id);
-- =======================================================================================================================

-- =================================================================== TELEFONE ========================================== 
	drop table if exists tbTelefone;
    create table tbTelefone(
		Id int auto_increment,
		 constraint PKTelefone primary key(Id),
		Numero numeric(9),
		IdTipo int not null,
		IdDDD int not null
	);
    
    drop table if exists tbTipoTel;
	create table tbTipoTel(
		Id int auto_increment,
         constraint PKTipoTel primary key(Id),
		Tipo varchar(25) not null
    );
    
    drop table if exists tbDDD;
    create table tbDDD(
		Id int auto_increment,
         constraint PKDDD primary key(Id),
		DDD tinyint not null
    );
    
    alter table tbTelefone add constraint FKTelefoneTipo foreign key(IdTipo) references tbTipoTel(Id), add constraint FKTelefoneDDD foreign key(IdDDD) references tbDDD(Id);
-- =======================================================================================================================

-- =================================================================== PESSOA ============================================   
    drop table if exists tbPessoa;
    create table tbPessoa(
		Id int auto_increment,
         constraint PKPessoa primary key (Id),
		Nome varchar(100) not null,
        Email varchar(100) not null,
        NumeroEndereco int not null,
        CompEndereco varchar(50),
        CEP char(8) not null
    );
    alter table tbPessoa add constraint FKPessoaEndereco foreign key(CEP) references tbEndereco(CEP);
    
    drop table if exists tbTelefonePessoa;
    create table tbTelefonePessoa(
		IdPessoa int,
        IdTelefone int,
         constraint PKTelPessoa primary key (IdPessoa, IdTelefone)
    );
	alter table tbTelefonePessoa add constraint FKTelPessoa foreign key(IdTelefone) references tbTelefone(Id), add constraint FKPessoaTel foreign key(IdPessoa) references tbPessoa(Id);
    
    drop table if exists tbDadosBancarios;
	create table tbDadosBancarios(
		Id int auto_increment,
         constraint PKDadosBancarios primary key(Id),
		CVV numeric(4) not null,
        Banco varchar(100),
        NomeTitular varchar(100) not null,
        NumeroCartao numeric(16) not null,
        Validade date not null, -- ,forma de pagamento?
        IdPessoa int not null    
	);
    alter table tbDadosBancarios add constraint FKDBPessoa foreign key(IdPessoa) references tbPessoa(Id);
	
    drop table if exists tbPessoaFisica;
    create table tbPessoaFisica(
		Id int auto_increment,
         constraint PKFisica primary key(Id),
		CPF numeric(11) not null,
         constraint UQCPF unique(CPF),
        RG varchar(9) not null,
        DataNasc date not null,
		IdPessoa int not null
    );
    alter table tbPessoaFisica add constraint FKPessoaFisica foreign key(IdPessoa) references tbPessoa(Id);
   
    drop table if exists tbPessoaJuridica;
    create table tbPessoaJuridica(
		Id int auto_increment,
         constraint PKJuridica primary key(Id),
		RazaoSocial varchar(100) not null,
        CNPJ numeric(14) not null,
         constraint UQCNPJ unique(CNPJ),
        IdPessoa int not null		
    );
    alter table tbPessoaJuridica add constraint FKPessoaJuridica foreign key(IdPessoa) references tbPessoa(Id);
	
    drop table if exists tbPessoaUsuario;
    create table /* tbEssa Merda Dessa Tabela Desse Bosta Desse AspNetIdentity Que Não Me Deixa Criar o Banco Em Paz*/ tbPessoaUsuario(
		IdUser nvarchar(128)  not null,
        IdPessoa int not null,
         constraint FKPessoaUsuario primary key(IdUser, IdPessoa)
    );
    alter table tbPessoaUsuario add constraint FKPessoaUsuario foreign key (IdPessoa) references tbPessoa(Id), add constraint FKUserPessoa foreign key(IdUser) references `AspNetUsers`(`Id`);
 -- =======================================================================================================================   
  
-- =================================================================== Estoque ============================================  
	drop table if exists tbEstoque;
    create table tbEstoque(
		Id int auto_increment,
         constraint PKEstoque primary key(Id),
		Quantidade double not null,
        IdUM int not null,
        ValorUnit double not null default 0.00
    );
    
    drop table if exists tbHistEstoque;
    create table tbHistEstoque(
		Id int auto_increment,
         constraint PKHistEstoque primary key(Id),
		QuantidadeAlterada double not null,
        QuantidadeAntiga double not null default 0.00,
        DataAlteracao datetime not null default current_timestamp,
        Descricao varchar(150) not null,
        IdUM int not null,
        IdEstoque int not null
    );
    alter table tbHistEstoque add constraint FKHistEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    drop table if exists tbUnidadeDeMedida;
    create table tbUnidadeDeMedida(
		Id int auto_increment,
         constraint PKUnidadeMedida primary key(Id),
		UnidadeDeMedida varchar(10)
    );
    alter table tbEstoque add constraint FKEstoqueUM foreign key(IdUM) references tbUnidadeDeMedida(Id);
    alter table tbHistEstoque add constraint FKHistEstoqueUM foreign key(IdUM) references tbUnidadeDeMedida(Id);
   
                      -- ------------------------------- Semente ------------------------------------
    drop table if exists tbSemente;
    create table tbSemente(
		Id int auto_increment,
         constraint PKSemente primary key(Id),
		Nome varchar(75) not null,
        SoloIdeal varchar(75),
        IncSolarIdeal decimal(5,2) not null default 0.00,
        IncVentoIdeal decimal(5,2) not null default 0.00,
        NivelAcidezIdeal decimal(5,2) not null default 0.00,
        IdEstoque int not null
    );
    alter table tbSemente add constraint FKSementeEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    drop table if exists tbSolo;
    create table tbSolo(
		Id int auto_increment,
		 constraint PKSolo primary key(Id),
		Nome varchar(75) not null,
        Tipo int not null,
        IncidenciaSolar decimal(5,2) not null default 0.00,
        IncidenciaVento decimal(5,2) not null default 0.00,
        NivelAcidez decimal(5,2) not null default 0.00
    );
    
    
                         -- ------------------------------- Insumo ------------------------------------ 
    drop table if exists tbInsumo;                     
	create table tbInsumo(
		Id int auto_increment,
         constraint PKInsumo primary key(Id),
		Nome varchar(100) not null,
        Descricao varchar(300),
        IdEstoque int not null,
        IdCategoria int not null
    );
    alter table tbInsumo add constraint FKInsumoEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    drop table if exists tbCategoriaInsumo;
    create table tbCategoriaInsumo(
		Id int auto_increment,
         constraint PKCategoriaInsumo primary key(Id),
		Categoria varchar(50) not null
    );
    alter table tbInsumo add constraint FKInsumoCategoria foreign key(IdCategoria) references tbCategoriaInsumo(Id);
    
                          -- ------------------------------- Maquina ------------------------------------ 
    drop table if exists tbMaquina;
    create table tbMaquina(
		Id int auto_increment,
         constraint PKMaquina primary key(Id),
		Montadora varchar(100),
        Descricao varchar(300),
        VidaUtil int,
        ValorInicial double not null,
        DepreciacaoMes double,
        DepreciacaoAno double,
        IdEstoque int not null
    );
    alter table tbMaquina add constraint FKMaquinaEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    drop table if exists tbManutencao;
    create table tbManutencao(
		Id int auto_increment,
		 constraint PKManutencao primary key(Id),
		Nome varchar(75),
        Detalhes varchar(300),
        DataManutencao date not null,
        ValorPago double not null
    );
    
    drop table if exists tbMaquinaManutencao;
    create table tbMaquinaManutencao(
		IdMaquina int not null,
        IdManutencao int not null,
		 constraint PKMaquinaManutencao primary key(IdMaquina, IdManutencao)
    );
    alter table tbMaquinaManutencao add constraint FKMaquinaManutencao foreign key(IdMaquina) references tbMaquina(Id),
									add constraint FKManutencaoMaquina foreign key(IdManutencao) references tbManutencao(Id);
    
-- ======================================================================================================================= 

  
-- ========================================================== COMPRA ==============================================  
	drop table if exists tbFornecedor;
    create table tbFornecedor(
		Id int auto_increment,
         constraint PKFornecedor primary key(Id),
		`Status` bool not null default true,
        IdPessoa int not null
    );
    alter table tbFornecedor add constraint FKFornecedorPessoa foreign key(IdPessoa) references tbPessoa(Id);
    
    drop table if exists tbPagamento;
    create table tbPagamento(
		Id int auto_increment,
         constraint PKPagamento primary key(Id),
		QuantidadeParcelas int not null default 1,
        ValorParcela double not null,
        Tipo int not null
    );
    
    drop table if exists tbCompra;
    create table tbCompra(
		Id int auto_increment,
         constraint PKCompra primary key(Id),
		DescontoCompra double not null default 0.00,
        DataCompra date not null,
        IdForn int not null,
        IdPgmt int not null
    );
    alter table tbCompra add constraint FKCompraForn foreign key(IdForn) references tbFornecedor(Id), add constraint FKCompraPgmt foreign key(IdPgmt) references tbPagamento(Id);
    
    drop table if exists tbItensComprados;
    create table tbItensComprados(
		IdCompra int not null,
        IdEstoque int not null,
         constraint PKEstocaCompra primary key(IdCompra, IdEstoque),
        DescontoProduto double not null default 0.00,
        Quantidade double not null
    );
    alter table tbItensComprados add constraint FKItensCompraEstoque foreign key(IdCompra) references tbCompra(Id), add constraint FKItensEstoqueCompra foreign key(IdEstoque) references tbEstoque(IdEstoque);
-- =======================================================================================================================

-- =================================================================== VENDA ============================================ 
    drop table if exists tbVenda;
    create table tbVenda(
		Id int auto_increment,
         constraint PKVenda primary key(Id),
		DescontoVenda double not null default 0.00,
        DataVenda date not null,
        IdForn int not null,
        IdPgmt int not null
    );
    alter table tbVenda add constraint FKCompraForn foreign key(IdForn) references tbFornecedor(Id), add constraint FKCompraPgmt foreign key(IdPgmt) references tbPagamento(Id);
    
    drop table if exists tbItensVendidos;
    create table tbItensVendidos(
		IdVenda int not null,
        IdEstoque int not null,
         constraint PKVendaEstoque primary key(IdVenda, IdEstoque),
        DescontoProduto double not null default 0.00,
        Quantidade double not null
    );
    alter table tbItensVendidos add constraint FKItensVendaEstoque foreign key(IdVenda) references tbVenda(Id), add constraint FKItensEstoqueVenda foreign key(IdEstoque) references tbEstoque(IdEstoque);
-- ======================================================================================================================= 
 
 
-- =================================================================== FUNCIONARIO ============================================  
	drop table if exists tbFuncionario;
    create table tbFuncionario(
		Id int auto_increment,
         constraint PKFunc primary key(Id),
		`Status` bool not null default true,
        Salario double not null,
        IdPessoa int not null,
        IdCargo int not null
    );
    alter table tbFuncionario add constraint FKFuncPessoa foreign key(IdPessoa) references tbPessoa(Id);
    
    drop table if exists tbCargo;
    create table tbCargo(
		Id int auto_increment,
         constraint PKCargo primary key(Id),
		Nivel nvarchar(1),
        Cargo varchar(75) not null
    );
    alter table tbFuncionario add constraint FKFuncCargo foreign key(IdCargo) references tbCargo(Id);
    
    drop table if exists tbEquipe;
    create table tbEquipe(
		Id int auto_increment,
         constraint FKEquipe primary key(Id),
		Descricao varchar(300)
	);
    
    drop table if exists tbFuncEquipe;
    create table tbFuncEquipe(
		IdFunc int not null,
        IdEquipe int not null,
         constraint PKFuncEquipe primary key(IdFunc, IdEquipe)
    );
    alter table tbFuncEquipe add constraint FKFuncEquipe foreign key(IdFunc) references tbFuncionario(Id),
							 add constraint FKEquipeFunc foreign key(IdEquipe) references tbEquipe(Id);
-- ======================================================================================================================= 


-- =================================================================== PESSOA ============================================   
-- ======================================================================================================================= 




