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

*/
select * from VwItems; 

/*CREATE PROCEDURE spInsertPessoa(
    IN 
		IdEstado int,
		Cidade varchar(30),
        Bairro varchar(30),
        Rua varchar(50),
        CEP char(8),
        NumeroCasa int,
        CompCasa varchar(2),
        NumeroTel numeric(9),
        DDD numeric(2),
        Tipo varchar(20),
        Nome varchar(100),
        Email varchar(100)
    )
	BEGIN
		IF qtd < 0 THEN
			SIGNAL SQLSTATE '45000'
			   SET MESSAGE_TEXT = 'Valor menor que zero!';
		END IF;
	END$$*/

