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
			declare VDS, ValorTotal /*Valor Sem Desconto*/ double;
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
	
   /* drop view if exists vwSaida;
	create View vwSaida as( 
    select DATE_FORMAT(Co.`Data`, '%d/%m/%Y') `Data`,
		   (SUM(M.ValorPago) + SUM(D.ValorPago)  + SUM(Co.`Valor Total`)) `Saída`
	from tbManutencao M, tbDespesa D, vwCompra Co group by Co.`Data`
	);
    select * from vwSaida;
    */
	drop view if exists vwVenda;
	create view vwVenda as(
    select V.Id `Venda`, (DATE_FORMAT(V.`Data`, '%d/%m/%Y')) `Data`, E.Id 'IdEstoque',
    SUM(spValorTotal(IV.QtdVendida, E.ValorUnit, V.Desconto)) `Valor Total`
		from tbItensVendidos IV INNER JOIN tbEstoque E on IV.IdEstoque = E.Id
								INNER JOIN tbVenda V on IV.IdVenda = V.Id
	group by `Venda`
    );
    /*
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
    */


-- ================== VIEWS E ROUTINES =================