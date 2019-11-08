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
	END$$
    
    -- =================================================================== FLUXO DE CAIXA ============================================  
	drop table if exists tbDespesa;
	create table tbDespesa(
		Id int auto_increment,
         constraint PKDespesa primary key(Id),
		ValorPago double,
        `Data` date not null
    )engine = InnoDB;
    
    insert into tbDespesa(ValorPago, `Data`) values(1000.00, '01/01/01'),
												   (700.00, '01/01/01');
	insert into tbDespesa(ValorPago, `Data`) values(1100.00, '01/02/01'),
												   (850.00, '01/01/01');
    
    drop table if exists tbContas;
	create table tbContas(
		Id int auto_increment,
         constraint PKConta primary key(Id),
		Nome varchar(30) not null
    )engine = InnoDB;
    
    insert into tbContas(Nome) value('Conta de Luz');
    insert into tbContas(Nome) value('Conta de Água');
    
    
    drop table if exists tbDespesaAdm;
	create table tbDespesaAdm(
		IdDespesa int not null,
        IdDespAdm int not null,
         constraint PKDespesaAdmin primary key(IdDespesa, IdDespAdm)
    )engine = InnoDB;
    alter table tbDespesaAdm add constraint FKDespesaAdmDespesa foreign key(IdDespesa) references tbDespesa(Id),
							   add constraint FKDespesaAdmConta foreign key(IdDespAdm) references tbContas(Id);
                               
	insert into tbDespesaAdm value(2, 1);
    insert into tbDespesaAdm value(4, 2); 
    
	drop table if exists tbDespesaFunc;
	create table tbDespesaFunc(
		IdDespesa int not null,
        IdFunc int not null,
         constraint PKDespesaFunc primary key(IdDespesa, IdFunc)
    )engine = InnoDB;
    alter table tbDespesaFunc add constraint FKDespesaFunc foreign key(IdDespesa) references tbDespesa(Id),
							   add constraint FKFuncDespesa foreign key(IdFunc) references tbFuncionario(Id);
    
	insert into tbDespesaFunc value(1, 1);
	insert into tbDespesaFunc value(3, 1);
-- =============================================================================================================================== 

-- ========================== COMPRA ===================
    drop table if exists tbCompra;
	create table tbCompra(
		Id int auto_increment,
         constraint PKCompra primary key(Id),
        -- `Data` date not null,
        IdForn int not null,
        IdPagamento int not null,
		Desconto decimal(5,2) default 0.00,
        IdDespesa int not null
        )engine = InnoDB;
    alter table tbCompra add constraint FKCompraForn foreign key(IdForn) references tbFornecedor(Id),
						 add constraint FKCompraPgmt foreign key(IdPagamento) references tbPagamento(Id),
                         add constraint FKCompraDesp foreign key(IdDespesa) references tbDespesa(Id);
                         
	insert into tbPagamento(Tipo) values(1);
    insert into tbPagamento(Tipo) values(1);
    
    insert into tbDespesa(`Data`) values('01/01/01'),
										('01/02/01'),
                                        ('02/02/01');
	
    insert into tbCompra(IdDespesa, IdForn, IdPagamento) value(6, 1, 1);
    insert into tbCompra(IdDespesa, IdForn, IdPagamento) value(7, 1, 1);
    insert into tbCompra(IdDespesa, IdForn, IdPagamento) value(8, 1, 1);    
    
    drop table if exists tbItensComprados;
	create table tbItensComprados(
		IdCompra int not null,
        IdEstoque int not null,
         constraint PKEstocaCompra primary key(IdCompra, IdEstoque),
        -- DescontoProd int default 0.00,
        QtdProd double not null
    )engine = InnoDB;
    alter table tbItensComprados add constraint FKItensCompraEstoque foreign key(IdCompra) references tbCompra(Id),
							     add constraint FKItensEstoqueCompra foreign key(IdEstoque) references tbEstoque(Id);
                                 
	insert into tbItensComprados(IdCompra, IdEstoque, QtdProd) values(1, 1, (select Qtd from tbEstoque where Id = 1)),
																	 (1, 2, (select Qtd from tbEstoque where Id = 2)),
                                                                     (1, 3, (select Qtd from tbEstoque where Id = 3)),
                                                                     (1, 4, (select Qtd from tbEstoque where Id = 4)),
                                                                     (1, 5, (select Qtd from tbEstoque where Id = 5)),
                                                                     (1, 6, (select Qtd from tbEstoque where Id = 6));
                                                                     
	insert into tbEstoque(Qtd, UM, ValorUnit) values(1, 1, 0.50);
    update tbEstoque set Qtd = Qtd+1 where Id = 1;
    insert into tbSemente(IdEstoque, Nome) values(7, "Semente de Mandioquinha");

    insert into tbItensComprados(IdCompra, IdEstoque, QtdProd) values(2, 7, (select Qtd from tbEstoque where Id = 7)),
																	 (2, 1, 1);
	update tbEstoque set Qtd = Qtd+1 where Id = 1;
    insert into tbItensComprados(IdCompra, IdEstoque, QtdProd) values(3, 1, 1);
    
	update tbDespesa 
    set ValorPago = (
		select SUM(spValorTotal(IC.QtdProd, E.ValorUnit, C.Desconto))
         from tbItensComprados IC INNER JOIN tbEstoque E on IC.IdEstoque = E.Id
								  INNER JOIN tbCompra C on IC.IdCompra = C.Id
		 where C.Id = 1
        )
    where Id = 6;
    
    update tbDespesa 
    set ValorPago = (
		select SUM(spValorTotal(IC.QtdProd, E.ValorUnit, C.Desconto))
         from tbItensComprados IC INNER JOIN tbEstoque E on IC.IdEstoque = E.Id
								  INNER JOIN tbCompra C on IC.IdCompra = C.Id
		 where C.Id = 2
        )
    where Id = 7;
    
    update tbDespesa 
    set ValorPago = (
		select SUM(spValorTotal(IC.QtdProd, E.ValorUnit, C.Desconto))
         from tbItensComprados IC INNER JOIN tbEstoque E on IC.IdEstoque = E.Id
								  INNER JOIN tbCompra C on IC.IdCompra = C.Id
		 where C.Id = 3
        )
    where Id = 8;
-- =====================================================

-- ================== VENDA ============================
    drop table if exists tbVenda;
	create table tbVenda(
		Id int auto_increment,
         constraint PKVenda primary key(Id),
		Desconto decimal(5,2) default 0.00,
        `Data` date not null,
        IdCliente int not null,
        IdPagamento int not null
    )engine = InnoDB;
    alter table tbVenda add constraint FKVendaCliente foreign key(IdCliente) references tbCliente(Id),
						add constraint FKVendaPgmt foreign key(IdPagamento) references tbPagamento(Id);
    
    
    insert into tbVenda(`Data`, IdCliente, IdPagamento) value('01/01/01', 1, 1);
    
    drop table if exists tbItensVendidos;
	create table tbItensVendidos(
		IdVenda int not null,
        IdEstoque int not null,
         constraint PKVendaEstoque primary key(IdVenda, IdEstoque),
        DescontoProd decimal(5,2)  not null default 0.00,
        QtdVendida double not null
    )engine = InnoDB;
    alter table tbItensVendidos add constraint FKItensVendaEstoque foreign key(IdVenda) references tbVenda(Id),
								add constraint FKItensEstoqueVenda foreign key(IdEstoque) references tbEstoque(Id);
                                
	insert into tbItensVendidos(IdVenda, IdEstoque, QtdVendida) values(1, 1, 1);
-- =====================================================

	DELIMITER $$ 
    drop function if exists spValorTotal$$
    create function spValorTotal(QtdProd double, ValorUnitario double, Desconto decimal(5,2))
    returns double 
    deterministic
    begin
			declare VDS, ValorTotal Valor Sem Desconto double;
			set VDS = ((QtdProd * ValorUnitario));
			set ValorTotal = VDS - (VDS * (Desconto/100));
            return ValorTotal;
	end$$
    
    DELIMITER $$ 
    drop function if exists spValorParcela$$
    create function spValorParcela(ValorPago double, QtdParcela double)
    returns double 
    deterministic
    begin
			declare ValorParcela double;
			set ValorParcela = (ValorPago/QtdParcela);
            return ValorParcela;
	end$$
    
    DELIMITER ;
	
	drop view if exists vwCompra;
    create view vwCompra as(
    select C.Id `Compra`, DATE_FORMAT(D.`Data`, '%d/%m/%Y') `Data`,
		   group_concat(distinct concat(I.Item, ' - ', IC.QtdProd) separator ', ') `Itens - Quantidade Comprada`,
           SUM(spValorTotal(IC.QtdProd, E.ValorUnit, C.Desconto)) `Valor Total`,           
		   group_concat(
			distinct concat(
				P.QtdParcelas,
                ' - ',
                (D.ValorPago/P.QtdParcelas)
			) separator ', '
		   ) `Qtd. de Parcelas - Valor da Parcela`,
           P.Tipo `Tipo de Pagamento`
		from tbItensComprados IC INNER JOIN tbEstoque E on IC.IdEstoque = E.Id
								 INNER JOIN vwItems I on I.Id = E.Id
								 INNER JOIN tbCompra C on IC.IdCompra = C.Id
                                 INNER JOIN tbPagamento P on C.IdPagamento = P.Id
                                 INNER JOIN tbDespesa D on C.IdDespesa = D.Id
	group by `Compra`
    );
	
	drop view if exists vwSaida;
	create View vwSaida as( 
    select DATE_FORMAT(Co.`Data`, '%d/%m/%Y') `Data`,
		   (SUM(M.ValorPago) + SUM(D.ValorPago)  + SUM(Co.`Valor Total`)) `Saída`
	from tbManutencao M, tbDespesa D, vwCompra Co group by Co.`Data`
	);
    select * from vwSaida;
 
	drop view if exists vwVenda;
	create view vwVenda as(
    select V.Id `Venda`, (DATE_FORMAT(V.`Data`, '%d/%m/%Y')) `Data`, E.Id 'IdEstoque',
    SUM(spValorTotal(IV.QtdVendida, E.ValorUnit, V.Desconto)) `Valor Total`
		from tbItensVendidos IV INNER JOIN tbEstoque E on IV.IdEstoque = E.Id
								INNER JOIN tbVenda V on IV.IdVenda = V.Id
	group by `Venda`
    );

    drop view if exists vwSaldo;
    create view vwSaldo as select (IFNULL(V.`Valor Total`, 0) - IFNULL(S.Saída, 0))  `Saldo` from vwSaida S, vwVenda V;
	
    drop view if exists vwFluxoDeCaixa; 
    create view vwFluxoDeCaixa as
    select IFNULL(S.`Saída`,0) `Saída`, IFNULL(V.ValorTotal, 0) `Entrada`, Sal.Saldo, monthname(S.`Data`) `MÊS`, year(S.`Data`) `ANO`
    from vwVenda V, vwSaida S, vwSaldo Sal
    where S.`Data` = V.`Data`
    group by `ANO`; 
    
	DELIMITER $$
    drop function if exists spFluxoCaixa$$ 
	create function spFluxoCaixa()
	returns @FluxoCaixa table(
					@Compra double,
					@Venda double,
					@Saldo double,
					@MES varchar(15),
					@ANO int)
	begin	
		declare double Venda, Compra, TVenda, TCompra, Saldo;
        declare date  DtC, dtV;
        DECLARE done INT DEFAULT 0;
        DECLARE curCompra CURSOR FOR SELECT `Valor Total` FROM vwCompra group by `Data`;
        declare curDtC cursor for select `Data` from vwCompra;
        DECLARE curVenda CURSOR FOR SELECT ValorTotal FROM vwVenda;
        declare curDtV cursor for select `Data` from vwVenda;
        
		  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

		  OPEN curCompra;
          open curDtC;
          open curVenda;
          open curDtV;

		  REPEAT
			FETCH curCompra INTO Compra;
            fetch curDtC into DtC;
            
			SET TCompra +=  Compra;
              
		  UNTIL done END REPEAT;
		  CLOSE curCompra;
          close curDtC;
          
          REPEAT
            fetch curVenda into Venda;
            fetch curDtV into DtV;
            
			SET TVenda +=  Venda;
		  
          UNTIL done END REPEAT;
		  CLOSE curVenda;
          close curDtV;
          
		  
	end
	DELIMITER ;
    
    
    
	drop table if exists tbManutencao;
	create table tbManutencao(
		Id int auto_increment,
		 constraint PKManutencao primary key(Id),
		Nome varchar(30),
        Detalhes varchar(300),
        `Data` date not null,
        ValorPago double not null
    )engine = InnoDB;
    
    insert into tbManutencao(Nome, `Data`, ValorPago) value("Revisão Anual", '01/01/01', 5000.00);
    
    drop table if exists tbMaquinaManutencao;
	create table tbMaquinaManutencao(
		IdMaquina int not null,
        IdManutencao int not null,
		 constraint PKMaquinaManutencao primary key(IdMaquina, IdManutencao)
    )engine = InnoDB;
    alter table tbMaquinaManutencao add constraint FKMaquinaManutencao foreign key(IdMaquina) references tbMaquina(IdEstoque),
									add constraint FKManutencaoMaquina foreign key(IdManutencao) references tbManutencao(Id);
    
    insert into tbMaquinaManutencao value(5,1);    
    
    
DELIMITER $
drop function if exists spNumMan$
create function spNumMan(IdMaq int)
	returns int
begin
	declare NumMan int;
    if(exists(select IdManutencao from tbMaquinaManutencao where IdMaquina = IdMaq)) then
		set NumMan = (select count(distinct IdManutencao) from tbMaquinaManutencao where IdMaquina = IdMaq);
    else
		set NumMan = 0;
	end if;
	return NumMan;
end$

drop function if exists spValMan$
create function spValMan(IdMaq int)
	returns double
begin
	declare ValMan double;
    if(exists(select IdManutencao from tbMaquinaManutencao where IdMaquina = IdMaq)) then
		set ValMan = (select sum(ValorPago)
						from tbManutencao m
							inner join tbMaquinaManutencao mm on mm.IdManutencao = m.Id
						where mm.IdMaquina = IdMaq);
	else
		set ValMan = 0;
	end if;
    return ValMan;
end$
DELIMITER ;
    
drop view if exists vwQtdMa;
create view vwQtdMa as(
select IdEstoque `Id`
	   , Nome
       , spNumMan(IdEstoque) `Quantidade de Manutenções`
       , spValMan(IdEstoque) `Custo Total`
from tbMaquina
order by IdEstoque);

drop view if exists vwManutencao;
create view vwManutencao as(
select mm.IdMaquina, mm.IdManutencao, M.Nome `Máquina`, M.Tipo `Tipo de Máquina`, Ma.Nome `Manutenção`, (DATE_FORMAT(Ma.`Data`, '%d/%m/%Y')) `Data da Manutenção`, Ma.ValorPago `Valor da Manutenção`
 from tbMaquina M 
 inner join tbMaquinaManutencao mm 
on mm.IdMaquina = M.IdEstoque
 inner join tbManutencao Ma 
on Ma.Id = mm.IdManutencao);



-- ================== VIEWS E ROUTINES =================*/
    

