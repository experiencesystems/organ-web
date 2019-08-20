use sys;
drop database dbOrgan;
create database dbOrgan;
use dbOrgan;

create table `AspNetRoles`(
	`Id` nvarchar(128)  not null ,
	`Name` nvarchar(256)  not null ,
	primary key (`Id`)) 
	engine=InnoDb 
	auto_increment=0;
    
CREATE UNIQUE index  `RoleNameIndex` on `AspNetRoles` (`Name`) using HASH;

create table `AspNetUserRoles` (
	`UserId` nvarchar(128)  not null ,
	`RoleId` nvarchar(128)  not null ,
	primary key ( `UserId`,`RoleId`) )	
	engine=InnoDb auto_increment=0;
 
CREATE index  `IX_UserId` on `AspNetUserRoles` (`UserId`) using HASH;

CREATE index  `IX_RoleId` on `AspNetUserRoles` (`RoleId`) using HASH;

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
    
CREATE UNIQUE index  `UserNameIndex` on `AspNetUsers` (`UserName`) using HASH;

create table `AspNetUserClaims` (
	`Id` int not null  auto_increment ,
	`UserId` nvarchar(128)  not null ,
	`ClaimType` longtext,
	`ClaimValue` longtext,
	primary key ( `Id`) ) 
	engine=InnoDb auto_increment=0;
    
CREATE index  `IX_UserId` on `AspNetUserClaims` (`UserId`) using HASH;

create table `AspNetUserLogins` (
	`LoginProvider` nvarchar(128)  not null ,
	`ProviderKey` nvarchar(128)  not null ,
	`UserId` nvarchar(128)  not null ,
	primary key ( `LoginProvider`,`ProviderKey`,`UserId`) ) 
	engine=InnoDb auto_increment=0;

CREATE index  `IX_UserId` on `AspNetUserLogins` (`UserId`) using HASH;

alter table `AspNetUserRoles` add constraint `FK_AspNetUserRoles_AspNetRoles_RoleId`  foreign key (`RoleId`) references `AspNetRoles` ( `Id`)  on update cascade on delete cascade;
alter table `AspNetUserRoles` add constraint `FK_AspNetUserRoles_AspNetUsers_UserId`  foreign key (`UserId`) references `AspNetUsers` ( `Id`)  on update cascade on delete cascade;
alter table `AspNetUserClaims` add constraint `FK_AspNetUserClaims_AspNetUsers_UserId`  foreign key (`UserId`) references `AspNetUsers` ( `Id`)  on update cascade on delete cascade; 
alter table `AspNetUserLogins` add constraint `FK_AspNetUserLogins_AspNetUsers_UserId`  foreign key (`UserId`) references `AspNetUsers` ( `Id`)  on update cascade on delete cascade;

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
     constraint FKFazendaLocalizacao foreign key (CEP, Numero) references tbLocalizacao(CEP, Numero) on update cascade on delete cascade
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
	Nome varchar(150) not null,
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
    IdCargo int not null,
     constraint FKFuncCargo foreign key (IdCargo) references tbCargo(Id) on delete cascade on update cascade,
     
	CEP char(8) not null,
    Numero int not null,
     constraint FKFuncLocalizacao foreign key (CEP, Numero) references tbLocalizacao(CEP, Numero) on delete cascade on update cascade,
     
	IdUsuario int not null,
     constraint FKFuncUsuario foreign key (IdUsuario) references `AspNetUsers`(Id) on delete cascade on update cascade
);

create index  IXIdFuncionario on tbFuncionario (Id) using HASH;
create index  IXIdUsuario on tbFuncionario (IdUsuario) using HASH;

create table tbTelefone(
	Id int,
     constraint PKTelefone primary key (Id),
	
    DDD numeric(2) not null,
    Numero numeric(9) not null,
    Tipo varchar(30)
);

create table tbFuncionarioTelefone(
	IdTel int not null,
	 constraint FKTelefoneFuncionario foreign key (IdTel) references tbTelefone (Id) on update cascade on delete cascade,
	 
	IdFunc int not null,
	 constraint FKFuncionarioTelefone foreign key (IdFunc) references tbFuncionario (Id) on update cascade on delete cascade,

	 constraint PKFuncionarioTelefone primary key (IdTel, IdFunc)
);

create index  IXIdFuncionario on tbFuncionarioTelefone (IdFunc) using HASH;
create index  IXIdTelefone on tbFuncionarioTelefone (IdTel) using HASH;

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

create table tbEquipe(
	Id int,
     constraint PKEquipe primary key (Id),
	
    Nome varchar(50) not null
);

create table tbEquipeFuncionario(
	IdEquipe int not null,
     constraint FKEquipeFunc foreign key (IdEquipe) references tbEquipe (Id) on update cascade on delete cascade,
	
    IdFunc int not null,
     constraint FKFuncEquipe foreign key (IdFunc) references tbFuncionario (Id) on update cascade on delete cascade,
	
    LiderOrNao bool not null,
    
     constraint PKEquipeFuncionario primary key (IdEquipe, IdFunc)
);

create index IXFuncionario on tbEquipeFuncionario (IdFunc) using hash;

create table tbTarefaFuncionario(
	IdTarefa int,
     constraint FKTarefaFunc foreign key (IdTarefa) references tbTarefa (Id),
	
    IdFunc int,
     constraint FKFuncTarefa foreign key (IdFunc) references tbFuncionario (Id) on update cascade on delete cascade,
     
	 constraint PKTarefaFuncionario primary key (IdTarefa, IdFunc)
);

create index IXFuncionario on tbTarefaFuncionario (IdFunc) using hash;