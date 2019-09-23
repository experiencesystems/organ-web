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
        IdSoloIdeal int not null,
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
    alter table tbSemente add constraint FKSementeSolo foreign key(IdSoloIdeal) references tbSolo(Id);
    
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







/*
create table tbFazenda(
	Id int auto_increment,
     constraint PKFazenda primary key (Id),
	
    Area decimal(5,2) not null,
    Perimetro double(5,2) not null,
    Coordenadas geometry not null,
    CEP char(8) not null,
    Numero int not null,
     constraint FKFazendaLocalizacao foreign key (CEP, Numero) references tbLocalizacao(CEP, Numero) on delete cascade
);

insert into tbLocalizacao values ('05234001', 12, 'Rua Gonçales', 'Vila Mariana', 'APTO 23', 'Sao Paulo', 'SP');
insert into tbFazenda (Area, Perimetro, Coordenadas, CEP, Numero) values (13.00, 26.00, ST_GeomFromText('POINT(1 1)'), '05234001', 12);

create table tbCargo(
	Id int auto_increment,
     constraint PKCargo primary key (Id),
     
	Nivel varchar(50) not null,
    Cargo varchar(50) not null
);

create table tbFuncionario(
	Id int auto_increment,
     constraint PKFuncionario primary key (Id),
	Nome varchar(100) not null,
    Sobrenome varchar(100) not null,
    CPF varchar(11) not null,
     constraint UQCPF unique (CPF),
	
    RG varchar(9) not null,
    DataNascimento date not null,
    Email varchar(100) not null,
    Salario decimal(7,2) not null,
    GrauInstrucao varchar(50) not null,
    DataContratacao date not null,
    TipoContratacao varchar(50) not null,
    PeriodoContratacao int not null,
     `MES/ANO` bool not null,
    IdCargo int not null,
     constraint FKFuncCargo foreign key (IdCargo) references tbCargo(Id) on delete cascade,
     
	CEP char(8) not null,
    Numero int not null,
     constraint FKFuncLocalizacao foreign key (CEP, Numero) references tbLocalizacao(CEP, Numero) on delete cascade,
     
	IdUsuario nvarchar(128) not null,
     constraint FKFuncUsuario foreign key (IdUsuario) references AspNetUsers(Id) on delete cascade
);

insert into tbLocalizacao values ('05234000', 12, 'Rua Gonçales', 'Vila Mariana', 'APTO 23', 'Sao Paulo', 'SP');
insert into tbCargo (Nivel, Cargo) values ('Alto', 'Administrador');

-- select*from AspNetUsers;
-- select*from tbFazenda;



-- UPDATE AspNetUsers set UserName = 'juxax@provmail.net' where UserName = 'Milena';

/* insert into tbFuncionario (Nome, Sobrenome, CPF, RG, DataNascimento, Email, Salario, GrauInstrucao, DataContratacao,
TipoContratacao, PeriodoContratacao, `MES/ANO`, IdCargo, CEP, Numero, IdUsuario) values (
 'Gilberto', 'Ramos', '26633622255', '356252527', '2008-7-04', 'gilberto@ramos.com', 122.30, 'Alto', '2018-7-04',
'Temporaria', 1, true, 1, '05234000', 12, '175d624d-af57-4636-ac2e-a05445b8ed00'
 );

SELECT`Extent1`.`Numero`, `Extent1`.`Id`,`Extent1`.`Nome`,`Extent1`.`Sobrenome`, `Extent1`.`CPF`,`Extent1`.`RG`, `Extent1`.`DataNascimento`, `Extent1`.`Email`, `Extent1`.`Salario`, `Extent1`.`GrauInstrucao`, `Extent1`.`DataContratacao`, `Extent1`.`TipoContratacao`, `Extent1`.`PeriodoContratacao`, `Extent1`.`MES/ANO`, `Extent1`.`IdCargo`, `Extent1`.`CEP`,`Extent1`.`IdUsuario`,`Extent2`.`Id` AS `Id1`, `Extent2`.`Nivel`, `Extent2`.`Cargo`, `Extent3`.`CEP` AS `CEP1`,`Extent3`.`Numero` AS `Numero1`, `Extent3`.`Endereco`, `Extent3`.`Bairro`, `Extent3`.`Complemento`,`Extent3`.`Cidade`, `Extent3`.`UF`,`Extent4`.`Id` AS `Id2`, `Extent4`.`DataCadastro`, `Extent4`.`Confirmacao`, `Extent4`.`Assinatura`,`Extent4`.`CLI/FUNC`, `Extent4`.`Email` AS `Email1`, `Extent4`.`EmailConfirmed`, `Extent4`.`PasswordHash`, `Extent4`.`SecurityStamp`, `Extent4`.`PhoneNumber`, `Extent4`.`PhoneNumberConfirmed`, `Extent4`.`TwoFactorEnabled`,`Extent4`.`LockoutEndDateUtc`,`Extent4`.`LockoutEnabled`, `Extent4`.`AccessFailedCount`,`Extent4`.`UserName`FROM `tbFuncionario` AS `Extent1` INNER JOIN `tbCargo` AS `Extent2` ON `Extent1`.`IdCargo` = `Extent2`.`Id` INNER JOIN `tbLocalizacao` AS `Extent3` ON (`Extent1`.`Numero` = `Extent3`.`Numero`) AND (`Extent1`.`CEP` = `Extent3`.`CEP`) INNER JOIN `AspNetUsers` AS `Extent4` ON `Extent1`.`IdUsuario` = `Extent4`.`Id`;
SELECT`Extent1`.`IdCargo`,`Extent1`.`Sobrenome`, `Extent2`.`Cargo`FROM `tbFuncionario` AS `Extent1` INNER JOIN `tbCargo` AS `Extent2` ON `Extent1`.`IdCargo` = `Extent2`.`Id`;


SELECT`Extent1`.`Numero`, `Extent1`.`Id`, `Extent1`.`Nome`, `Extent1`.`Sobrenome`,`Extent1`.`CPF`,`Extent1`.`RG`,`Extent1`.`DataNascimento`, `Extent1`.`Email`, `Extent1`.`Salario`, `Extent1`.`GrauInstrucao`, `Extent1`.`DataContratacao`, `Extent1`.`TipoContratacao`, `Extent1`.`PeriodoContratacao`, `Extent1`.`MES/ANO`, `Extent1`.`IdCargo`, `Extent1`.`CEP`, `Extent1`.`IdUsuario`,`Extent2`.`Id` AS `Id1`, `Extent2`.`Nivel`, `Extent2`.`Cargo`, `Extent3`.`CEP` AS `CEP1`, `Extent3`.`Numero` AS `Numero1`, `Extent3`.`Endereco`, `Extent3`.`Bairro`, `Extent3`.`Complemento`, `Extent3`.`Cidade`, `Extent3`.`UF`FROM `tbFuncionario` AS `Extent1` INNER JOIN `tbCargo` AS `Extent2` ON `Extent1`.`IdCargo` = `Extent2`.`Id` INNER JOIN `tbLocalizacao` AS `Extent3` ON (`Extent1`.`Numero` = `Extent3`.`Numero`) AND (`Extent1`.`CEP` = `Extent3`.`CEP`);

create index  IXIdFuncionario on tbFuncionario (Id);
create index  IXIdUsuario on tbFuncionario (IdUsuario);

create table tbTelefone(
	Id int auto_increment,
     constraint PKTelefone primary key (Id),
	
    DDD numeric(2) not null,
    Numero numeric(9) not null,
    Tipo varchar(30)
);

create table tbFuncionarioTelefone(
	IdTel int not null,
	 constraint FKTelefoneFuncionario foreign key (IdTel) references tbTelefone (Id) on delete cascade,
	 
	IdFunc int not null,
	 constraint FKFuncionarioTelefone foreign key (IdFunc) references tbFuncionario (Id) on delete cascade,

	 constraint PKFuncionarioTelefone primary key (IdTel, IdFunc)
);

create index  IXIdFuncionario on tbFuncionarioTelefone (IdFunc);
create index  IXIdTelefone on tbFuncionarioTelefone (IdTel);

create table tbTarefa(
	Id int auto_increment,
	 constraint PKTarefa primary key(Id),
	
    Titulo varchar(100) not null,
    Descricao varchar(500),
    StatusTarefa bool not null,
    Prioridade int not null,
    DataInicio datetime not null,
    DataFim datetime not null
);

create table tbMonitoramento(
	Id int auto_increment,
     constraint PKMonitoramento primary key(Id),
     
	Titulo varchar(50) not null,
	Observacao varchar(300),
	`Status` boolean not null,
	Resultado varchar(50) not null
);

create table tbMonitoramentoTarefa(
	IdTarefa int not null,
     constraint FKTarefaMonitoramento foreign key (IdTarefa) references tbTarefa(Id) on delete cascade,
	IdMonitoramento int not null,
     constraint FKMonitaramentoTarefa foreign key (IdMonitoramento) references tbMonitoramento(Id) on delete cascade,
     
     constraint PKMonitoramentoTarefa primary key (IdTarefa, IdMonitoramento)
);

create table tbEquipe(
	Id int auto_increment,
     constraint PKEquipe primary key (Id),
	
    Nome varchar(50) not null
);

create table tbEquipeFuncionario(
	IdEquipe int not null,
     constraint FKEquipeFunc foreign key (IdEquipe) references tbEquipe (Id) on delete cascade,
	
    IdFunc int not null,
     constraint FKFuncEquipe foreign key (IdFunc) references tbFuncionario (Id) on delete cascade,
	
    LiderOrNao bool not null,
    
     constraint PKEquipeFuncionario primary key (IdEquipe, IdFunc)
);

create index IXFuncionario on tbEquipeFuncionario (IdFunc);

create table tbTarefaFuncionario(
	IdTarefa int auto_increment,
     constraint FKTarefaFunc foreign key (IdTarefa) references tbTarefa (Id),
	
    IdFunc int,
     constraint FKFuncTarefa foreign key (IdFunc) references tbFuncionario (Id) on delete cascade,
     
	 constraint PKTarefaFuncionario primary key (IdTarefa, IdFunc)
);

create index IXFuncionario on tbTarefaFuncionario (IdFunc);

create table tbEstoque(
	Id int auto_increment,
     constraint PKEstoque primary key(Id),
	
    Quantidade decimal(7,2) not null default 0,
     
    UnidadeMedida varchar(15) not null
);


create table tbHistEstoque(
	IdEstoque int,
     constraint FKHistoricoEstoque foreign key (IdEstoque) references tbEstoque(Id) on delete cascade,
    
    DtAlteracao datetime default current_timestamp(),
    
     constraint PKHistEstoque primary key (IdEstoque, DtAlteracao),
     
    QtdAlterada decimal (7,2) not null,
    DescAlteracao varchar(20),
    QtdAntiga decimal (7,2) not null default 0
);

DELIMITER $$ -- TERMINAR ESSAS MERDAAAAAAAAAAAAAAAS ;-;
( -- TRIGGER INSERT ESTOQUE
create TRIGGER trgInsertHistEstoque after insert
ON tbEstoque
FOR EACH ROW
BEGIN
	-- set @qtd_estoque = (select OLD.Quantidade from tbEstoque) ;
      DECLARE qtd_estoque decimal(7,2);	
      DECLARE desc_hist varchar(20);
      DECLARE cod_estoque int;
      
      select NEW.Quantidade into qtd_estoque;
      
		set desc_hist = 'Item adicionado';
		select NEW.Id  into cod_estoque;
		
        if (exists (select * from tbEstoque where Id = cod_estoque) and (NEW.Quantidade >= 0)) then
		insert into tbHistEstoque(IdEstoque, QtdAlterada, DescAlteracao) 
				values(cod_estoque, qtd_estoque, desc_hist);
        end if;
        
END$$

( -- PROCEDURE VERIFICAR QUANTIDADE
CREATE PROCEDURE spVerificaQuantidade (IN qtd decimal(7,2))
BEGIN
    IF qtd < 0 THEN
        SIGNAL SQLSTATE '45000'
           SET MESSAGE_TEXT = 'Valor menor que zero!';
    END IF;
END$$

( -- TRIGGER DE VERIFICAÇÃO
create TRIGGER trgVerifcaInsert before insert
ON tbEstoque
FOR EACH ROW
BEGIN            
		call spVerificaQuantidade(NEW.Quantidade);   
END$$ 
DELIMITER ;

create table tbCategoria(
	Id int auto_increment,
     constraint PKCategoria primary key(Id), 
     
	Nome varchar(30) not null,
     constraint UQNome unique (Nome)
);

create table tbEvento(
	Id int auto_increment,
	  constraint PKEvento primary key (Id),
      
	`Data/Hora` datetime not null,
	Tipo varchar(50) not null,
	Nome varchar(50) not null,
	Descricao varchar(300),
	IdCategoria int,
     constraint FKEventoCategoria foreign key (IdCategoria) references tbCategoria(Id) on delete no action
);

create table tbFornecedor(
	Id int auto_increment,
     constraint PKCultura primary key(Id),
     
	Nome varchar(75) not null,
	CNPJ numeric(14) not null,
     constraint UQCNPJ unique (CNPJ),
     
	RazaoSocial varchar(100) not null,
	Site varchar(100),
	Email varchar(100) not null default 'SEM EMAIL',
	`Status` boolean not null,
	CEP char(8) not null,
    Numero int not null,
     constraint FKFornLocalizacao foreign key (CEP, Numero) references tbLocalizacao(CEP, Numero) on delete cascade
);

create table tbFornecedorTelefone(
	IdTel int not null,
	 constraint FKTelefoneFornecedor foreign key (IdTel) references tbTelefone (Id) on delete cascade,
	 
	IdForn int not null,
	 constraint FKFornecedorTelefone foreign key (IdForn) references tbFornecedor(Id) on delete cascade,

	 constraint PKFornecedorTelefone primary key (IdTel, IdForn)
);

create table tbItem(
	Id int auto_increment,
	 constraint PKItem primary key (Id),
	 
	Nome varchar(75) not null,
	Descricao varchar(300),
	ValorUnit decimal(7,2) not null,
	IdEstoque int not null,
	 constraint FKItemEstoque foreign key (IdEstoque) references tbEstoque(Id) on delete no action,
     
	IdCategoria int not null,
	 constraint FKItemCategoria foreign key (IdCategoria) references tbCategoria(Id) on delete no action,
     
	IdFornecedor int not null,
     constraint FKItemForn foreign key (IdFornecedor) references tbFornecedor(Id) on delete no action
);

select * from aspnetusers;
select * from tbestoque;
select * from tblocalizacao;
select * from tbcategoria;
select * from tbFornecedor;
select * from tbitem;

insert into tbfornecedor(Nome, CNPJ, RazaoSocial, Site, Email, `Status`, CEP, Numero) values ('Fornecedor 1', 12345678912345, 'Razão de viver', 'www.kiko.com', 'kikao@gmail.com', 
true, '05234001', 12);
insert into tbcategoria(nome) values ('Valioso');
insert into tbestoque (quantidade, unidademedida) values (34, 'un');
insert into tbitem (nome, descricao, valorunit, idestoque, idcategoria, idfornecedor) values ('Item 1', 'Legal', 56.98, 1, 1, 2);

create table tbSemente(
	Id int auto_increment,
     constraint PKSemente primary key (Id),
     
	Nome varchar(50) not null,
	SoloIdeal varchar(75) not null default 'Sem tipo de solo ideal',
	`Incidência Solar Ideal` decimal(5,2) not null default 0,
    `Incidência Vento Ideal` decimal(5,2) not null default 0,
	Acidez decimal(5,2) not null default 0,
    IdCategoria int not null,
     constraint FKSementeCategoria foreign key (IdCategoria) references tbCategoria(Id) on delete no action,
     
	IdEstoque int not null,
     constraint FKSementeEstoque foreign key (IdEstoque) references tbEstoque(Id) on delete no action,
     
	IdFornecedor int not null,
     constraint FKSementeForn foreign key (IdFornecedor) references tbFornecedor(Id) on delete no action
);

create table tbCultura(
	Id int auto_increment,
     constraint PKCultura primary key(Id),

	Nome varchar(50) not null,
    Descricao varchar(300)
);

create table tbPlantio(
	Id int auto_increment,
     constraint PKPlantio primary key (Id),
     
	Nome varchar(75) not null,
	IdCultura int not null,
	 constraint FKPlantioCultura foreign key (IdCultura) references tbCultura(Id) on delete no action, 
	Tipo varchar(75), -- QUE MERDA É ESSA
	Densidade decimal(7,2) not null,
	DataCriacao datetime not null default current_timestamp(),
	DataInício date not null,
	DataColheita date not null,
	IdSemente int not null,
     constraint FKPlantioSemente foreign key (IdSemente) references tbSemente(Id) on delete no action,
     
	QtdSemUsada decimal(7,2) not null,
	Epoca varchar(30) not null,
	`KG/HA de Semente` decimal(6,2) not null
);

create table tbEstadio(
	Id int auto_increment,
     constraint PKEstadio primary key (Id),
	Nome varchar(75) not null,
	Tempo varchar(75) not null, -- QUE MERDA É ESSA
	Descricao varchar(300),
    IdPlantio int not null,
     constraint FKEstadioPlantio foreign key (IdPlantio) references tbPlantio(Id) on delete cascade
);

create table tbSolo(
	Id int auto_increment,
     constraint PKSolo primary key(Id),
     
	Nome varchar(75),
	Tipo varchar(50) not null,
	`Incidência Solar` decimal(5,2) not null,
    `Incidência Vento` decimal(5,2) not null,
    Acidez decimal(5,2) not null
);

create table tbArea(
	Id int auto_increment,
     constraint PKArea primary key(Id),
     
	Nome varchar(75) not null,
	Disponibilidade boolean not null,
	Coordenadas geometry not null,
    IdSolo int not null,
     constraint FKAreaSolo foreign key (IdSolo) references tbSolo(Id) on delete cascade
);

create table tbAreaPlantio(
	IdPlantio int not null,
     constraint FKIdPlantioArea foreign key (IdPlantio) references tbPlantio(Id) on delete cascade,
     
    IdArea int not null,
     constraint FKIAreaPlantio foreign key (IdArea) references tbArea(Id) on delete no action,
     
     constraint PKAreaPlantio primary key (IdPlantio, IdArea)
);

create table tbMaquina(
	Id int auto_increment,
     constraint PKMaquina primary key (Id),
     
	Nome varchar(75) not null,	
	Descricao varchar(300),
	DataCadastro datetime not null default current_timestamp(),
	ValorCadastro decimal(7,2) not null,
	VidaUtil decimal(4,2) not  null,
	DepreciacaoAno decimal(7,2) not null,
	DepreciacaoMensal decimal(7,2) not null,
    
    Montadora int not null, -- Montadora é o fornecedor?, se não, tem código fornecedor? e Montadora é varchar?
     constraint FKMaquinaFornecedor foreign key (Montadora) references tbFornecedor(Id) on delete no action
);

create table tbManutencao(
	Id int auto_increment,
     constraint PKManuntencao primary key (Id),
     
	Nome varchar(75) not null,
	DataManuntencao date not null,
	Detalhes varchar(300)
);

create table tbManutencaoMaquina(
	IdMaquina int not null,
     constraint FKMaquinaManuntencao foreign key (IdMaquina) references tbMaquina(Id) on delete cascade,
	IdManuntencao int not null,
     constraint FKManuntencaoMaquina foreign key (IdManuntencao) references tbManutencao(Id) on delete cascade,
     
     constraint PKManuntencaoMaquina primary key (IdMaquina, IdManuntencao)
);

create table tbPraga(
	Id int auto_increment,
     constraint PKPraga primary key (Id),
	NomePopular varchar(50) not null,
	NomeCientifico varchar(75) default 'Nome Científico não especificado',
	Descricao varchar(300)
);

create table tbDoenca(
	Id int auto_increment,
     constraint PKDoenca primary key (Id),
	Nome varchar(50) not null,
	Sintomas varchar(300) not null,
	Tratamento varchar(300) not null,
	Descrição varchar(300) not null
);

create table tbControle(
	Id int auto_increment,
     constraint PKControle primary key (Id),
	`Status` boolean not null,
	Tipo varchar(75) not null,
	Descricao varchar(300),
	Eficiencia varchar(5) not null, -- QUAL O TIPO DISSO?
	NumeroLiberacoes int not null,
    IdEstadio int not null,
     constraint FKEstadioControle foreign key (IdEstadio) references tbEstadio(Id)
);

create table tbControlePraga(
	IdPraga int not null,
     constraint FKPragaControle foreign key (IdPraga) references tbPraga(Id) on delete cascade,
	IdControle int not null,
     constraint FKControlePraga foreign key (IdControle) references tbControle(Id) on delete cascade,
    
     constraint PKControlePraga primary key (IdPraga, IdControle)
);

create table tbControleDoenca(
	IdDoenca int not null,
     constraint FKDoencaControle foreign key (IdDoenca) references tbDoenca(Id) on delete cascade,
	IdControle int not null,
     constraint FKControleDoenca foreign key (IdControle) references tbControle(Id) on delete cascade,
    
     constraint PKControleDoenca primary key (IdDoenca, IdControle)
);

create table tbControleMaquina(
	IdMaquina int not null,
     constraint FKMaquinaControle foreign key (IdMaquina) references tbMaquina(Id) on delete cascade,
	IdControle int not null,
     constraint FKControleMaquina foreign key (IdControle) references tbControle(Id) on delete cascade,
    
     constraint PKControleMaquina primary key (IdMaquina, IdControle)
);

create table tbControleArea(
	IdArea int not null,
     constraint FKAreaControle foreign key (IdArea) references tbArea(Id) on delete cascade,
    IdControle int not null,
     constraint FKControleArea foreign key (IdControle) references tbControle(Id) on delete cascade,
    
     constraint PKControleArea primary key (IdArea, IdControle)
);

create table tbControleItem(
	IdItem int not null,
     constraint FKItemControle foreign key (IdItem) references tbItem(Id) on delete cascade,
	IdControle int not null,
     constraint FKControleItem foreign key (IdControle) references tbControle(Id) on delete cascade,

	Dose decimal(5,2) not null,
	UnidadeMedida varchar(30) not null,
    
     constraint PKControleItem primary key (IdItem, IdControle)
);

create table tbPagamento(
	Id int auto_increment,
     constraint PKPagamento primary key (Id),
	Valor decimal(7,2) not null,
	Parcelas int not null,
	TipoPagamento varchar(50) not null
);

create table tbDespesa(
	Id int auto_increment,
     constraint PKDespesa primary key (Id),
	Nome varchar(50) not null default 'Sem Nome',
	Descricao varchar(300),
    IdCategoria int not null,
     constraint FKDespesaCategoria foreign key (IdCategoria) references tbCategoria(Id) on delete no action,
	IdPagamento int not null,
     constraint FKDespesaPagamento foreign key (IdPagamento) references tbPagamento(Id) on delete no action
);

create view VwItems as
(SELECT S.Id, S.Nome `Item`, E.Quantidade, E.UnidadeMedida `Unidade de Medida`, C.Nome `Categoria`
FROM tbEstoque E
INNER JOIN tbSemente S ON E.Id = S.IdEstoque
INNER JOIN tbCategoria C ON C.Id = S.IdCategoria)
UNION
(SELECT I.Id, I.Nome, E.Quantidade, E.UnidadeMedida, C.Nome
FROM tbEstoque E
INNER JOIN tbItem I ON E.Id = I.IdEstoque
INNER JOIN tbCategoria C ON C.Id = I.IdCategoria);

select * from VwItems;

*/
