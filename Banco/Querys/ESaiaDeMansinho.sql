-- IMAGEM NO BANCO http://www.linhadecodigo.com.br/artigo/100/blob-fields-in-mysql-databases.aspx
drop database if exists dbOrgan;
create database dbOrgan;
use dbOrgan;
-- Blob Tipo pra foto ou arquivo
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
		`Id` nvarchar(128)  not null,
			Foto blob,
			Ativacao bool default true,
		`Email` varchar(100) ,-- !
		`ConfirmacaoEmail` bool not null ,
		`SenhaHash` longtext,
		`CarimboSeguranca` longtext,
		`UserName` varchar(50)  not null ,-- !
	      constraint PKAspNetUsers primary key ( `Id`)
	)engine = InnoDB;
    
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

insert into tbUsuario(`Id`, `Email`, `ConfirmacaoEmail`, `SenhaHash`, `CarimboSeguranca`, `UserName`, Foto)
			   values('02719894-e4a9-46c8-999e-ba942abd5f8f', 'milenamonteiro@gmail.com', 0, 
					  'ABecbdkGhzyTR1/t+F8FpUnN+AHXhiXYu4qPCVc4SroxOyzj3p0R+TnWK0p1o6q3Rw==',
                      'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Mirena',  LOAD_FILE("/error.gif")),
                      
                      ('02719894-e4a9-46c8-999e-ba942abd5f8g', 'moreexpsystems@gmail.com', 0,
                      'ABecbdkGhzyTR1/t+F8FpUnN+AHXhiXYu4qPCVc4SroxOyzj3p0R+TnWK0p1o6q3Rw=+',
                      'e7aac8f8-7c92-44fb-9850-5f0fb0024c9b', 'Empresinha', LOAD_FILE("/error.gif"));
  
-- =======================================================================================================================

-- =================================================================== TELEFONE ========================================== 
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
    
    insert into tbTipoTel(Tipo) values("Fixo"),
									  ("Celular");
        
    drop table if exists tbDDD;
	create table tbDDD(
		DDD numeric(2) not null,
         constraint PKDDD primary key(DDD)
    )engine = InnoDB;
    
    insert into tbDDD(DDD) values(11), (12), (13), (14), (15), (16), (17), (18), (19), (21), (22), (24), (27), (28), (31), (32), (33), (34),
								 (35), (37), (38), (41), (42), (43), (44), (45), (46), (47), (48), (49), (51), (53), (54), (55), (61), (62),
                                 (63), (64), (65), (66), (67), (68), (69), (71), (73), (74), (75), (77), (79), (81), (82), (83), (84), (85),
                                 (86), (87), (88), (89), (91), (92), (93), (94), (95), (96), (97), (98), (99); 
    
    alter table tbTelefone add constraint FKTelefoneTipo foreign key(IdTipo) references tbTipoTel(Id),
						   add constraint FKTelefoneDDD foreign key(IdDDD) references tbDDD(DDD);
                           
	insert into tbTelefone(Numero, IdTipo, IdDDD) values(989896912, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 11)),
														(989896913, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 11)),
                                                        (89896912, (select Id from tbTipoTel where Tipo = "Fixo"), (select DDD from tbDDD where DDD = 64));
-- ============================================================================================================================

-- =================================================================== FUNCIONARIO ============================================  
	drop table if exists tbFuncionario;
	create table tbFuncionario(
		Id int auto_increment,
         constraint PKFunc primary key(Id),
		`Status` bool default true,
        Nome varchar(30) not null,
        IdCargo int not null
    )engine = InnoDB;
    
    drop table if exists tbCargo;
	create table tbCargo(
		Id int auto_increment,
         constraint PKCargo primary key(Id),
		Nivel int,
        Nome varchar(30) not null
    )engine = InnoDB;
    alter table tbFuncionario add constraint FKFuncCargo foreign key(IdCargo) references tbCargo(Id);
    
    insert into tbCargo(Nivel, Nome) value(1, 'Operador(a) de trator');
    insert into tbFuncionario(Nome, IdCargo) value('Joãona', 1);
    
	drop table if exists tbTelFunc;
	create table tbTelFunc(
		IdFunc int,
        IdTelefone int,
         constraint PKTelPessoa primary key (IdFunc, IdTelefone)
    )engine = InnoDB;
	alter table tbTelFunc add constraint FKTelFunc foreign key(IdTelefone) references tbTelefone(Id),
						  add constraint FKFuncTel foreign key(IdFunc) references tbFuncionario(Id);
                                 
	insert into tbTelFunc value(1,1);
    
    drop table if exists tbUsuarioFunc;
    create table tbUsuarioFunc(
		IdFunc int not null,
        IdUsuario nvarchar(128) not null,
         constraint PKUsuarioFunc primary key(IdFunc, IdUsuario)
    )engine = InnoDB;
    alter table tbUsuarioFunc add constraint FKUsuarioFunc foreign key(IdUsuario) references tbUsuario(`Id`),
							  add constraint FKFuncUsuario foreign key(IdFunc) references tbFuncionario(Id);
-- ======================================================================================================================= 
 
 -- ========================================================== FORNECEDOR ==============================================  
	drop table if exists tbFornecedor;
	create table tbFornecedor(
		Id int auto_increment,
         constraint PKFornecedor primary key(Id),
		`Status` bool not null default true,
        Nome varchar(50) not null,
        Email varchar(100) not null
    )engine = InnoDB;
    
    insert into tbFornecedor(Nome, Email) value('Expereince Systems', 'moreexpsystems@gmail.com');
    
    drop table if exists tbTelForn;
	create table tbTelForn(
		IdForn int,
        IdTelefone int,
         constraint PKTelPessoa primary key (IdForn, IdTelefone)
    )engine = InnoDB;
	alter table tbTelForn add constraint FKTelForn foreign key(IdTelefone) references tbTelefone(Id),
								 add constraint FKFornTel foreign key(IdForn) references tbFornecedor(Id);
                                 
	insert into tbTelForn value(1,2);
-- =======================================================================================================================
 
-- =================================================================== ESTOQUE ============================================  
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
        IdFornecedor int not null
    )engine = InnoDB;
    alter table tbEstoque add constraint FKEstoqueFornecedor foreign key(IdFornecedor) references tbFornecedor(Id),
						  add constraint FKEstoqueUM foreign key(UM) references tbUM(Id);
    
	
                          -- ------------------------------- Semente ------------------------------------
    drop table if exists tbSemente;
	create table tbSemente(
		IdEstoque int not null,
         constraint PKSemente primary key(IdEstoque),
		Nome varchar(30) not null,
        `Desc` varchar(100)      
    )engine = InnoDB;
    alter table tbSemente add constraint FKSementeEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    
                         -- ------------------------------- Insumo ------------------------------------ 
    drop table if exists tbInsumo;
	create table tbInsumo(		
        IdEstoque int not null,
         constraint PKInsumo primary key(IdEstoque),
		Nome varchar(30) not null,
        `Desc` varchar(100),
        IdCategoria int not null
    )engine = InnoDB;
    alter table tbInsumo add constraint FKInsumoEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    drop table if exists tbCategoria;
	create table tbCategoria(
		Id int auto_increment,
         constraint PKCategoria primary key(Id),
		Categoria varchar(15) not null
    )engine = InnoDB;
    alter table tbInsumo add constraint FKInsumoCategoria foreign key(IdCategoria) references tbCategoria(Id);
    
    

                          -- ------------------------------- Maquina ------------------------------------ 
    drop table if exists tbMaquina;
	create table tbMaquina(
		IdEstoque int not null,
         constraint PKMaquina primary key(IdEstoque),
		Nome varchar(30) not null,
        Tipo int not null,
		Montadora varchar(30),
        `Desc` varchar(100)
    )engine = InnoDB;
    alter table tbMaquina add constraint FKMaquinaEstoque foreign key(IdEstoque) references tbEstoque(Id);
    

-- ======================================================================================================================= 
   
-- =================================================================== PLANTIO ============================================
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
    
    insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Soja', 1, '01/01/01', '01/01/01', 1);
    
	drop table if exists tbSolo;
	create table tbSolo(
		Id int auto_increment,
		 constraint PKSolo primary key(Id),
		Nome varchar(50) not null,
        Tipo int not null,
        IncSolar decimal(5,2) default 0.00,
        IncVento decimal(5,2) default 0.00
    )engine = InnoDB;
    
    insert into tbSolo(Nome, Tipo) values('Arenoso', 1), ('Vermelho', 1);
    
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
    
    insert into tbArea(Nome,  IdSolo) values('Area1', 1), ('Area2', 1), ('Area3', 2);
    
    drop table if exists tbAreaPlantio;
	create table tbAreaPlantio(
        IdPlantio int not null,
        IdArea int not null,
         constraint PKAreaPlantio primary key(IdPlantio, IdArea)
    )engine = InnoDB;
    alter table tbAreaPlantio add constraint FKAreaPlantioPlantio foreign key(IdPlantio) references tbPlantio(Id),
									 add constraint FKAreaPlantioArea foreign key(IdArea) references tbArea(Id);
                                     
	insert into tbAreaPlantio values(1, 1), (1, 2);
    
    drop table if exists tbItensPlantio;
	create table tbItensPlantio(
		QtdUsada double not null,
        IdPlantio int not null,
        IdEstoque int not null,
         constraint PKItensPlantio primary key(IdPlantio, IdEstoque)
    )engine = InnoDB;
    alter table tbItensPlantio add constraint FKItensPlantioPlantio foreign key(IdPlantio) references tbPlantio(Id),
							   add constraint FKItensPlantioEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    insert into tbItensPlantio values(1, 1, 1);
	
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
		`Data` date not null,
        QtdPerdas double not null default 0,
        QtdTotal double not null,
        IdPlantio int not null,
        IdProd int not null,
         constraint PKColheita primary key(IdPlantio, IdProd)
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
-- ======================================================================================================================== 

-- =================================================================== CONTROLE ============================================ 
	drop table if exists tbControle;
	create table tbControle(
		Id int auto_increment,
			constraint PKControle primary key(Id),
		`Status` int,
        `Desc` varchar(300),
        Efic decimal(5,2) not null,
        NumLiberacoes int not null,
        `Data` date not null
    )engine = InnoDB;
    
    insert into tbControle(`Status`, Efic, NumLiberacoes, `Data`) values(true, 100, 2, '01/01/01'),
																		(true, 50, 3, '01/01/01');
    
    drop table if exists tbItensControle;
	create table tbItensControle(
		QtdUsada double not null,
        IdControle int not null,
        IdEstoque int not null,
         constraint PKItensControle primary key(IdControle, IdEstoque)
    )engine = InnoDB;
    alter table tbItensControle add constraint FKItensControleControle foreign key(IdControle) references tbControle(Id),
								add constraint FKItensControleEstoque foreign key(IdEstoque) references tbEstoque(Id);
                                
	insert into tbItensControle values(0.25, 1, 4),
									  (0.25, 2, 4);
    
    drop table if exists tbPragaOrDoenca;
	create table tbPragaOrDoenca(
		Id int auto_increment,
         constraint PKPD primary key(Id),
		Nome varchar(30) not null,
        `P/D` bool not null
    )engine = InnoDB;
    
    insert into tbPragaOrDoenca(Nome, `P/D`) values('Praga do Mal', true),
												   ('Doença Nem Tão do Mal', false);
    
	drop table if exists tbControlePD;
	create table tbControlePD(
		IdControle int not null,
        IdPD int not null,
         constraint PKControlePD primary key(IdControle, IdPD)
    )engine = InnoDB;
    alter table tbControlePD add constraint FKControlePD foreign key(IdControle) references tbControle(Id),
							 add constraint FKPDControle foreign key(IdPD) references tbPragaOrDoenca(Id);
	
    insert into tbControlePD values(1, 1),
								   (2,2);
    
    drop table if exists tbAreaPD;
	create table tbAreaPD(
		`Status` bool not null,
        IdArea int not null,
        IdPd int not null,
         constraint PKAreaPD primary key(IdArea, IdPD)
    )engine = InnoDB;
    alter table tbAreaPD add constraint FKAreaPD foreign key(IdArea) references tbArea(Id),
						 add constraint FKPDArea foreign key(IdPd) references tbPragaOrDoenca(Id);
                         
	insert into tbAreaPD values(true, 2, 1),
							   (true, 3, 2);
                               
	create table tbFuncControle(
		IdFunc int not null,
        IdControle int not null,
         constraint PKFuncControle primary key(IdFunc, IdControle)
    )engine = InnoDB;
    alter table tbFuncControle add constraint FKFuncControleFuncionario foreign key(IdFunc) references tbFuncionario(Id),
							   add constraint FKFuncControleControle foreign key(IdControle) references tbControle(Id);
-- =========================================================================================================================

 -- ======================================================HistoricoEstoque=======================================================

    drop table if exists tbHistEstoque;
	create table tbHistEstoque(
		Id int auto_increment,
         constraint PKHistEstoque primary key(Id),
        QtdAntiga double,        
        NomeAntigo varchar(15),
        CategoriaAntiga varchar(15),
        UMAntiga varchar(6),
        DataAlteracao datetime default current_timestamp,
        `Desc` varchar(100),
        IdEstoque int,
        IdFornecedorAntigo int
    )engine = InnoDB;
    alter table tbHistEstoque add constraint FKHistEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
       drop view if exists vwTelefone;
	create view vwTelefone as(
	select T.Id, concat('(',T.IdDDD,') ',T.Numero,' - ',Ti.Tipo) `Telefone`
	 from tbTelefone T
		inner join tbTipoTel Ti on Ti.Id = T.IdTipo
	); 

	drop view if exists vwFornecedor;
	create view vwFornecedor as(
	select F.Id, F.Nome `Razão Social`, F.Email `Email`, group_concat(T.Telefone separator '; ') `Telefones`
	 from tbFornecedor F
		inner join tbTelForn TF on F.Id = TF.IdForn
		inner join vwTelefone T on T.Id = TF.IdTelefone
	); 
    
	drop view if exists vwItems;
	create view vwItems as
	(SELECT S.IdEstoque `Id`,
				S.Nome `Item`,
				E.Qtd `Quantidade`,
				U.`Desc` `Unidade de Medida`,
                S.`Desc` `Descrição`,
			    'Semente' `Categoria`,
			    F.`Nome` `Fornecedor`,
			    'Semente' `Tipo`
	FROM tbEstoque E
	INNER JOIN tbSemente S ON E.Id = S.IdEstoque
	inner join vwFornecedor F on E.IdFornecedor = F.Id
	inner join tbUM U on E.UM = U.Id
	)
	UNION
	(SELECT I.IdEstoque, 
			I.Nome,
			E.Qtd,
			U.`Desc`,
            I.`Desc`,
			C.Categoria,
			F.`Nome`,
			   'Insumo' `Tipo`
	FROM tbEstoque E
	INNER JOIN tbInsumo I ON E.Id = I.IdEstoque
	INNER JOIN tbCategoria C ON C.Id = I.IdCategoria
	inner join vwFornecedor F on E.IdFornecedor = F.Id
	inner join tbUM U on E.UM = U.Id
	)
	UNION
	(SELECT M.IdEstoque,
			M.Nome,
			E.Qtd,
			U.`Desc`,
            M.`Desc`,
			M.Tipo,
			F.`Nome`,
			'Máquina' `Tipo`
	FROM tbMaquina M
	INNER JOIN tbEstoque E ON M.IdEstoque = E.Id
	inner join vwFornecedor F on E.IdFornecedor = F.Id
	inner join tbUM U on E.UM = U.Id
	)    UNION
	(SELECT P.IdEstoque,
			P.Nome,
			E.Qtd,
			U.`Desc`,
            P.`Desc`,
			'Produto',
			F.`Nome`,
			'Produto' `Tipo`
	FROM tbProduto P
	INNER JOIN tbEstoque E ON P.IdEstoque = E.Id
	inner join vwFornecedor F on E.IdFornecedor = F.Id
	inner join tbUM U on E.UM = U.Id
	)
		order by `Categoria`;
  
    DELIMITER $
	drop procedure if exists spVerQtd$
	CREATE PROCEDURE spVerQtd (IN qtd decimal(7,2))
	BEGIN
		IF qtd < 0 THEN
			SIGNAL SQLSTATE '45000'
			   SET MESSAGE_TEXT = 'Valor menor que zero!';
		END IF;
	END$

	drop trigger if exists trgInsertHistorico$
	create TRIGGER trgInsertHistorico after insert
	ON tbEstoque
	FOR EACH ROW
	BEGIN       

			call spVerQtd(NEW.Qtd);
			insert into tbHistEstoque(`Desc`, IdEstoque) values('Novo Item', NEW.Id);
			
	END$

	drop trigger if exists trgUpdateHistorico$
	create TRIGGER trgUpdateHistorico after update
	ON tbEstoque
	FOR EACH ROW
	BEGIN   
			declare Descr varchar(300);
			call spVerQtd(NEW.Qtd);
				set Descr = 'Item Alterado';

			insert into tbHistEstoque(QtdAntiga, VUAntigo, UMAntiga, IdFornecedorAntigo, `Desc`, IdEstoque, CategoriaAntiga, NomeAntigo) values(OLD.Qtd, OLD.ValorUnit, OLD.UM, OLD.IdFornecedor, Descr, OLD.Id, cat, nome);
			
	END$

	drop trigger if exists trgDeleteHistorico$ 
	create TRIGGER trgDeleteHistorico before delete 
    ON tbEstoque
	FOR EACH ROW
	BEGIN   
			SET FOREIGN_KEY_CHECKS=0;
			insert into tbHistEstoque(QtdAntiga, VUAntigo, UMAntiga, IdFornecedorAntigo, `Desc`, IdEstoque, CategoriaAntiga, NomeAntigo) values(OLD.Qtd, OLD.ValorUnit, OLD.UM, OLD.IdFornecedor, 'Item Excluido-', OLD.Id, cat, nome);
			SET FOREIGN_KEY_CHECKS=1;
            END$
	DELIMITER ;
    
    
	insert into tbUM value('a', 'A');
    insert into tbEstoque(Qtd, UM, IdFornecedor) values(5, 'a', 1);
    insert into tbSemente(IdEstoque, Nome) values(1, "Semente de Soja");
	insert into tbEstoque(Qtd, UM, IdFornecedor) values(1, 'a', 1), -- UM 2- L, 1 - Kg, 3 - Unidade
														(2, 'a', 1),
														(5, 'a', 1);
    insert into tbCategoria(Categoria) values("Fertilizante"),
											 ("Ferramenta"),
                                             ("Pesticida");
	insert into tbInsumo(IdEstoque, Nome, IdCategoria) values(2, "CresceForte", 1),
															 (3, "Pá", 2),
                                                             (4, "MataBichoEPlanta", 3);

    insert into tbEstoque(Qtd,UM, IdFornecedor) values(5, 'a', 1),
													  (6, 'a', 1); -- Id 5 e 6
	insert into tbMaquina(IdEstoque, Nome, Tipo, Montadora) values(5,'TratorX', 1, 'MaquinasBoas'),
																  (6,'ColhedeiraY', 2, 'MaquinasRuins e Caras');
	insert into tbEstoque(Qtd, UM, IdFornecedor) values(3, 'a', 1);
    insert into tbProduto(IdEstoque, Nome) value(7, 'Soja');
    
	insert into tbColheita values('01/01/01',  1, 4, 1, 7);

 -- =========================================================================================================================

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
            CPF numeric(12) not null,
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
                      'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Mirena',  LOAD_FILE("/error.gif"), 1111111111111, 0),
                      
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
        IdUsario nvarchar(128) not null
	)engine = InnoDB;
    alter table tbDadosBancarios add constraint FKDBUsuario foreign key(IdUsuario) references tbUsuario(`Id`);
	
    insert into tbDadosBancarios(NomeTitular, CVV, Banco, NumCartao, Validade, IdUsuario) values("João Meu Pai", 1111, 1, 11111111111111111, "01/01/01", 1);


-- ================================================== ENDEREÇO ===========================================================
	drop table if exists tbEndereco;
	create table tbEndereco(
		CEP char(8),
		 constraint PKLocalizacao primary key (CEP),
		IdRua int not null
	)engine = InnoDB;
    
    drop table if exists tbLogradouro;
	drop table if exists tbEstado;
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
    
    drop table if exists tbEstado;
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
		Estado varchar(20) not null,
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
		Nome varchar(75) not null,
        `Desc` varchar(300) not null,
        `Status` bool not null,
        Foto Blob not null,
        Desconto decimal(5,2),
        IdProduto int not null
    )engine = InnoDB;
    alter table tbAnuncio add constraint FKAnuncioProduto foreign key(IdProduto) references tbProduto(Id);
    
    drop table if exists tbItensAnuncio;
    create table tbItensAnuncio(
		IdAnuncio int not null,
        IdEstoque int not null,
         constraint PKItensAnuncio primary key(IdAnuncio, IdEstoque),
		Quantidade int not null,
        DescontoProd decimal(5,2)
    )engine = InnoDB;
    alter table tbItensAnuncio add constraint FKItensAnuncioAnuncio foreign key(IdAnuncio) references tbAnuncio(Id),
							   add constraint FKItensAnuncioEstoque foreign key(IdEstoque) references tbEstoque(Id);
	
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
        Comentario varchar(300) not null,
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
    
    drop table if exists tbVendaAnuncio;
    create table tbVendaAnuncio(
		Id int auto_increment,
         constraint PKVendaAnuncio primary key(Id),
		`Data` datetime default current_timestamp,
        Contrato Blob,
        CEP char(8) not null,
        IdPagamento int not null,
        IdPedido int not null
    )engine = InnoDB;
    alter table tbVendaAnuncio add constraint FKVendaAnuncioEndereco foreign key(CEP) references tbEndereco(CEP),
							   add constraint FKVendaAnuncioPagamento foreign key(IdPagamento) references tbPagamento(Id),
                               add constraint FKVendaAnuncioPedido foreign key(IdPedido) references tbPedido(Id);
	
    drop table if exists tbEntrega;
    create table tbEntrega(
		Id int auto_increment,
         constraint PKEntrega primary key(Id),
        IdVA int not null,
        ValorFrete double not null,
        DescFrete double default 0.00
    )engine = InnoDB; 
    alter table tbEntrega add constraint FKEntregaVA foreign key(IdVA) references tbVendaAnuncio(Id);
    
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
use sys;