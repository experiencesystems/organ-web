use sys;
drop database if exists dbOrgan;
create database dbOrgan;
use dbOrgan;

-- EU COLOQUEI UM MONTE DE TITULO OBRIGATÓRIO, SE TIVER ALGUM QUE NÃO SEJA, PODE MUDAR

create table `AspNetRoles`(
	`Id` nvarchar(128)  not null ,
	`Name` nvarchar(256)  not null ,
	primary key (`Id`)) 
	engine=InnoDb 
	auto_increment=0;
    
CREATE UNIQUE index  `RoleNameIndex` on `AspNetRoles` (`Name`);

create table `AspNetUserRoles` (
	`UserId` nvarchar(128)  not null ,
	`RoleId` nvarchar(128)  not null ,
	primary key ( `UserId`,`RoleId`) )	
	engine=InnoDb auto_increment=0;
 
CREATE index  `IX_UserId` on `AspNetUserRoles` (`UserId`);

CREATE index  `IX_RoleId` on `AspNetUserRoles` (`RoleId`);

create table `AspNetUsers` (
	`Id` nvarchar(128)  not null ,
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

create table `AspNetUserClaims` (
	`Id` int not null  auto_increment ,
	`UserId` nvarchar(128)  not null ,
	`ClaimType` longtext,
	`ClaimValue` longtext,
	primary key ( `Id`) ) 
	engine=InnoDb auto_increment=0;
    
CREATE index  `IX_UserId` on `AspNetUserClaims` (`UserId`);

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

create table tbDadosUsuario(
	Id int auto_increment, 
     constraint PKUsuario primary key(Id),
	DataCadastro datetime default current_timestamp(),
    Confirmacao bool not null,
	Ativacao bool not null default true,
    Assinatura bool not null,
	`CLI/FUNC` bool not null,
     -- true Cli, false Func
	
    IdUsuario nvarchar(128)  not null,
     constraint FKDadosUsuario foreign key(IdUsuario) references `AspNetUsers`(`Id`)
);

create table tbLocalizacao(
	CEP char(8),
    Numero int,
    Endereco varchar(150) not null,
    Bairro varchar(100) not null,
    Complemento varchar(100) not null,
    Cidade varchar(50) not null,
    UF char(2) not null,
    
     constraint PKLocalizacao primary key (CEP, Numero)
);

create table tbFazenda(
	Id int auto_increment,
     constraint PKFazenda primary key (Id),
	
    Area decimal(5,2) not null,
    Perimetro decimal(5,2) not null,
    Coordenadas geometry not null,
    CEP char(8) not null,
    Numero int not null,
     constraint FKFazendaLocalizacao foreign key (CEP, Numero) references tbLocalizacao(CEP, Numero) on delete cascade
);

create table tbCargo(
	Id int,
     constraint PKCargo primary key (Id),
     
	Nivel varchar(50) not null,
    Cargo varchar(50) not null
);

create table tbFuncionario(
	Id int,
     constraint PKFuncionario primary key (Id),
	Nome varchar(100) not null,
     Sobrenome varchar(100) not null,
    CPF numeric(11) not null,
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
     
	IdUsuario int not null,
     constraint FKFuncUsuario foreign key (IdUsuario) references tbDadosUsuario(Id) on delete cascade
);

create index  IXIdFuncionario on tbFuncionario (Id);
create index  IXIdUsuario on tbFuncionario (IdUsuario);

create table tbTelefone(
	Id int,
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
	Id int not null,
	 constraint PKTarefa primary key(Id),
	
    Titulo varchar(100) not null,
    Descricao varchar(500),
    StatusTarefa bool not null,
    Prioridade int not null,
    DataInicio datetime not null,
    DataFim datetime not null
);

create table tbMonitoramento(
	Id int,
     constraint PKMonitoramento primary key(Id),
     
	Titulo varchar(50) not null,
	Observação varchar(300),
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
	Id int,
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
	IdTarefa int,
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
CREATE PROCEDURE spVerificaQuantidade (IN qtd DECIMAL(7,2))
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
	Id int,
     constraint PKCategoria primary key(Id), 
     
	Nome varchar(30) not null,
     constraint UQNome unique (Nome),  
     
	`EVENTO/ITEM` boolean not null
);

create table tbEvento(
	Id int,
	  constraint PKEvento primary key (Id),
      
	`Data/Hora` datetime not null,
	Tipo varchar(50) not null,
	Nome varchar(50) not null,
	Descricao varchar(300),
	IdCategoria int,
     constraint FKEventoCategoria foreign key (IdCategoria) references tbCategoria(Id) on delete no action
);

create table tbFornecedor(
	Id int,
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
     constraint FKFuncLocalizacao foreign key (CEP, Numero) references tbLocalizacao(CEP, Numero) on delete cascade
);

create table tbFornecedorTelefone(
	IdTel int not null,
	 constraint FKTelefoneFornecedor foreign key (IdTel) references tbTelefone (Id) on delete cascade,
	 
	IdForn int not null,
	 constraint FKFornecedorTelefone foreign key (IdForn) references tbFornecedor(Id) on delete cascade,

	 constraint PKFornecedorTelefone primary key (IdTel, IdForn)
);

create table tbItem(
	Id int,
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

create table tbSemente(
	Id int,
     constraint PKSemente primary key (Id),
     
	Nome varchar(50) not null,
	SoloIdeal varchar(75) not null default 'Sem tipo de solo ideal',
	`Incidência Solar Ideal` decimal(5,2) not null default 0,
    `Incidência Vento Ideal` decimal(5,2) not null default 0,
	Acidez decimal(5,2) not null default 0,
	IdEstoque int not null,
     constraint FKSementeEstoque foreign key (IdEstoque) references tbEstoque(Id) on delete no action,
     
	IdFornecedor int not null,
     constraint FKSementeForn foreign key (IdFornecedor) references tbFornecedor(Id) on delete no action
);

create table tbCultura(
	Id int,
     constraint PKCultura primary key(Id),

	Nome varchar(50) not null,
    Descricao varchar(300)
);

create table tbPlantio(
	Id int,
     constraint PKPlantio primary key (Id),
     
	Nome varchar(75) not null,
	IdCultura int not null,
	 constraint FKPlantioCultura foreign key (IdCultura) references tbCultura(Id) on delete no action, 
	Tipo varchar(75), -- QUE MERDA É ESSA
	Densidade decimal(7,2) not null,
	DataCriação datetime not null default current_timestamp(),
	DataInício date not null,
	DataColheita date not null,
	IdSemente int not null,
     constraint FKPlantioSemente foreign key (IdSemente) references tbSemente(Id) on delete no action,
     
	QtdSemUsada decimal(7,2) not null,
	Epoca varchar(30) not null,
	`KG/HA de Semente` decimal(6,2) not null
);

create table tbEstadio(
	Id int,
     constraint PKEstadio primary key (Id),
	Nome varchar(75) not null,
	Tempo varchar(75) not null, -- QUE MERDA É ESSA
	Descricao varchar(300),
    IdPlantio int not null,
     constraint FKEstadioPlantio foreign key (IdPlantio) references tbPlantio(Id) on delete cascade
);

create table tbSolo(
	Id int,
     constraint PKSolo primary key(Id),
     
	Nome varchar(75),
	Tipo varchar(50) not null,
	`Incidência Solar` decimal(5,2) not null,
    `Incidência Vento` decimal(5,2) not null,
    Acidez decimal(5,2) not null
);

create table tbArea(
	Id int,
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
	Id int,
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

create table tbManutenção(
	Id int,
     constraint PKManuntencao primary key (Id),
     
	Nome varchar(75) not null,
	DataManuntencao date not null,
	Detalhes varchar(300)
);

create table tbManutencaoMaquina(
	IdMaquina int not null,
     constraint FKMaquinaManuntencao foreign key (IdMaquina) references tbMaquina(Id) on delete cascade,
	IdManuntencao int not null,
     constraint FKManuntencaoMaquina foreign key (IdManuntencao) references tbManuntencao(Id) on delete cascade,
     
     constraint PKManuntencaoMaquina primary key (IdMaquina, IdManuntencao)
);

create table tbPraga(
	Id int,
     constraint PKPraga primary key (Id),
	NomePopular varchar(50) not null,
	NomeCientifico varchar(75) default 'Nome Científico não especificado',
	Descricao varchar(300)
);

create table tbDoença(
	Id int,
     constraint PKDoenca primary key (Id),
	Nome varchar(50) not null,
	Sintomas varchar(300) not null,
	Tratamento varchar(300) not null,
	Descrição varchar(300) not null
);

create table tbControle(
	Id int,
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
	Id int,
     constraint PKPagamento primary key (Id),
	Valor decimal(7,2) not null,
	Parcelas int not null,
	TipoPagamento varchar(50) not null
);

create table tbDespesa(
	Id int,
     constraint PKDespesa primary key (Id),
	Nome varchar(50) not null default 'Sem Nome',
	Descricao varchar(300),
    IdCategoria int not null,
     constraint FKDespesaCategoria foreign key (IdCategoria) references tbCategoria(Id) on delete no action,
	IdPagamento int not null,
     constraint FKDespesaPagamento foreign key (IdPagamento) references tbPagamento(Id) on delete no action
);
