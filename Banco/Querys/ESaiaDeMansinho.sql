-- IMAGEM NO BANCO http://www.linhadecodigo.com.br/artigo/100/blob-fields-in-mysql-databases.aspx
drop database if exists dbOrgan;
create database dbOrgan;
use dbOrgan;

drop table if exists tbTelefone;
create table tbTelefone(
Id int auto_increment,
 constraint PKTelefone primary key(Id),
Numero numeric(9),
IdTipo int not null,
IdDDD  numeric(2) not null
)engine = InnoDB;

drop table if exists tbTipoTel;
create table tbTipoTel(
Id int auto_increment,
 constraint PKTipoTel primary key(Id),
Tipo varchar(15) not null
)engine = InnoDB;

drop table if exists tbDDD;
create table tbDDD(
DDD numeric(2) not null,
 constraint PKDDD primary key(DDD)
)engine = InnoDB;

alter table tbTelefone add constraint FKTelefoneTipo foreign key(IdTipo) references tbTipoTel(Id),
   add constraint FKTelefoneDDD foreign key(IdDDD) references tbDDD(DDD);
   
drop table if exists tbFuncionario;
create table tbFuncionario(
Id int auto_increment,
 constraint PKFunc primary key(Id),
`Status` bool default true,
Nome varchar(50) not null,
Email varchar(100) not null,
Funcao int not null
)engine = InnoDB;

drop table if exists tbTelFunc;
create table tbTelFunc(
IdFunc int,
IdTelefone int,
 constraint PKTelPessoa primary key (IdFunc, IdTelefone)
)engine = InnoDB;
alter table tbTelFunc add constraint FKTelFunc foreign key(IdTelefone) references tbTelefone(Id),
  add constraint FKFuncTel foreign key(IdFunc) references tbFuncionario(Id);
  
drop table if exists tbFornecedor;
create table tbFornecedor(
Id int auto_increment,
 constraint PKFornecedor primary key(Id),
`Status` bool not null default true,
Nome varchar(50) not null,
Email varchar(100) not null
)engine = InnoDB;

drop table if exists tbTelForn;
create table tbTelForn(
IdForn int,
IdTelefone int,
 constraint PKTelPessoa primary key (IdForn, IdTelefone)
)engine = InnoDB;
alter table tbTelForn add constraint FKTelForn foreign key(IdTelefone) references tbTelefone(Id),
 add constraint FKFornTel foreign key(IdForn) references tbFornecedor(Id);
 
drop table if exists tbUM;
create table tbUM(
Id varchar(6) not null,
 constraint PKUM primary key(Id),
`Desc` varchar(20) not null
)engine = InnoDB;

drop table if exists tbEstoque;
create table tbEstoque(
Id int auto_increment,
 constraint PKEstoque primary key(Id),
Qtd double not null,
UM varchar(6),
IdFornecedor int
)engine = InnoDB;
alter table tbEstoque add constraint FKEstoqueFornecedor foreign key(IdFornecedor) references tbFornecedor(Id),
  add constraint FKEstoqueUM foreign key(UM) references tbUM(Id);

drop table if exists tbSemente;
create table tbSemente(
IdEstoque int not null,
 constraint PKSemente primary key(IdEstoque),
Nome varchar(30) not null,
`Desc` varchar(100)  
)engine = InnoDB;
alter table tbSemente add constraint FKSementeEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbInsumo;
create table tbInsumo(
IdEstoque int not null,
 constraint PKInsumo primary key(IdEstoque),
Nome varchar(30) not null,
`Desc` varchar(100),
Categoria int not null
)engine = InnoDB;
alter table tbInsumo add constraint FKInsumoEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbMaquina;
create table tbMaquina (
    IdEstoque int not null,
    constraint PKMaquina primary key (IdEstoque),
    Nome varchar(30) not null,
    Tipo int not null,
    Montadora varchar(50),
    `Desc` varchar(100)
)  engine=InnoDB;
alter table tbMaquina add constraint FKMaquinaEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbPlantio;
create table tbPlantio(
Id int auto_increment,
 constraint PKPlantio primary key(Id),
Nome varchar(50) not null,
Sistema int not null,
DataColheita date not null,
DataInicio date not null,
TipoPlantio int not null
)engine = InnoDB;

drop table if exists tbHistPlantio;
create table tbHistPlantio(
	Id int,
     constraint PKHistPlantio primary key(Id),
	Nome varchar(50) not null,
    DataAlteracao datetime default current_timestamp,
    `Desc` varchar(30) default 'Item Deletado'
)engine = InnoDB;

drop table if exists tbSolo;
create table tbSolo(
Id int auto_increment,
 constraint PKSolo primary key(Id),
Nome varchar(25) not null,
Tipo int not null,
IncSolar decimal(5,2) default 0.00,
IncVento decimal(5,2) default 0.00
)engine = InnoDB;

drop table if exists tbArea;
create table tbArea(
	Id int auto_increment,
	 constraint PKArea primary key(Id),
	Nome varchar(30) not null,
	Disp int default 1,
	Tamanho int default 1,
	IdSolo int not null
)engine = InnoDB;
alter table tbArea add constraint FKAreaSolo foreign key(IdSolo) references tbSolo(Id);

drop table if exists tbAreaPlantio;
create table tbAreaPlantio(
IdPlantio int not null,
IdArea int not null,
 constraint PKAreaPlantio primary key(IdPlantio, IdArea),
Densidade int not null
)engine = InnoDB;
alter table tbAreaPlantio add constraint FKAreaPlantioPlantio foreign key(IdPlantio) references tbPlantio(Id),
 add constraint FKAreaPlantioArea foreign key(IdArea) references tbArea(Id);

drop table if exists tbItensPlantio;
create table tbItensPlantio(
QtdUsada double not null,
IdPlantio int not null,
IdEstoque int not null,
 constraint PKItensPlantio primary key(IdPlantio, IdEstoque)
)engine = InnoDB;
alter table tbItensPlantio add constraint FKItensPlantioPlantio foreign key(IdPlantio) references tbPlantio(Id),
   add constraint FKItensPlantioEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbProduto;
create table tbProduto(
IdEstoque int not null,
 constraint PKProduto primary key(IdEstoque),
Nome varchar(20) not null,
`Desc` varchar(100)
)engine = InnoDB;
alter table tbProduto add constraint FKProdutoEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbColheita;
create table tbColheita(
Id int auto_increment,
`Data` date not null,
QtdPerdas double not null default 0,
QtdTotal double not null,
IdPlantio int not null,
IdProd int not null,
 constraint PKColheita primary key(Id)
)engine = InnoDB;
alter table tbColheita add constraint FKColheitaPlantio foreign key(IdPlantio) references tbPlantio(Id),
   add constraint FKColheitaProd foreign key(IdProd) references tbProduto(IdEstoque);

create table tbFuncPlantio(
IdFunc int not null,
IdPlantio int not null,
 constraint PKFuncPlantio primary key(IdFunc, IdPlantio)
)engine = InnoDB;
alter table tbFuncPlantio add constraint FKFuncPlantioFuncionario foreign key(IdFunc) references tbFuncionario(Id),
  add constraint FKFuncPlantioPlantio foreign key(IdPlantio) references tbPlantio(Id);
  
drop table if exists tbControle;
create table tbControle(
Id int auto_increment,
constraint PKControle primary key(Id),
`Status` bool,
`Desc` varchar(100),
Efic decimal(5,2) not null,
NumLiberacoes int not null,
`Data` date not null
)engine = InnoDB;

drop table if exists tbItensControle;
create table tbItensControle(
QtdUsada double not null,
IdControle int not null,
IdEstoque int not null,
 constraint PKItensControle primary key(IdControle, IdEstoque)
)engine = InnoDB;
alter table tbItensControle add constraint FKItensControleControle foreign key(IdControle) references tbControle(Id),
add constraint FKItensControleEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbPragaOrDoenca;
create table tbPragaOrDoenca(
Id int auto_increment,
 constraint PKPD primary key(Id),
Nome varchar(30) not null,
`P/D` bool not null
)engine = InnoDB;

drop table if exists tbControlePD;
create table tbControlePD(
IdControle int not null,
IdPD int not null,
 constraint PKControlePD primary key(IdControle, IdPD)
)engine = InnoDB;
alter table tbControlePD add constraint FKControlePD foreign key(IdControle) references tbControle(Id),
 add constraint FKPDControle foreign key(IdPD) references tbPragaOrDoenca(Id);

drop table if exists tbAreaPD;
create table tbAreaPD(
`Status` bool not null,
IdArea int not null,
IdPd int not null,
 constraint PKAreaPD primary key(IdArea, IdPD)
)engine = InnoDB;
alter table tbAreaPD add constraint FKAreaPD foreign key(IdArea) references tbArea(Id),
 add constraint FKPDArea foreign key(IdPd) references tbPragaOrDoenca(Id);
   
create table tbFuncControle(
IdFunc int not null,
IdControle int not null,
 constraint PKFuncControle primary key(IdFunc, IdControle)
)engine = InnoDB;
alter table tbFuncControle add constraint FKFuncControleFuncionario foreign key(IdFunc) references tbFuncionario(Id),
   add constraint FKFuncControleControle foreign key(IdControle) references tbControle(Id);

drop table if exists tbHistEstoque;
create table tbHistEstoque(
Id int auto_increment,
 constraint PKHistEstoque primary key(Id),
QtdAntiga double,
DataAlteracao datetime default current_timestamp,
`Desc` varchar(50),
IdEstoque int
)engine = InnoDB;
alter table tbHistEstoque add constraint FKHistEstoque foreign key(IdEstoque) references tbEstoque(Id)