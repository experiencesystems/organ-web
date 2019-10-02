use dbOrgan;
-- =================================================================== FLUXO DE CAIXA ============================================  
	drop view if exists vwCompra;
    create view vwCompra as(
    select C.Id 'Compra', C.`Data`, E.Id 'Estoque', SUM(((IC.QtdProd * E.ValorUnit) - ((IC.QtdProd * E.ValorUnit) * IC.DescontoProd)) - (((IC.QtdProd * E.ValorUnit) - ((IC.QtdProd * E.ValorUnit) * IC.DescontoProd)) * C.Desconto)) ValorTotal
		from tbItensComprados IC INNER JOIN tbEstoque E on IC.IdEstoque = E.Id
								 INNER JOIN tbCompra C on IC.IdCompra = C.Id
    );                             
	
    drop view if exists vwSaida;
    create View vwSaida as select Co.`Data`, (SUM(M.ValorPago) + SUM(D.ValorPago)  + SUM(Co.ValorTotal)) `Saída` from tbManutencao M, tbDespesa D, vwCompra Co;
	
	drop view if exists vwVenda;
	create view vwVenda as(
    select V.Id 'IdVenda',V.`Data` `D`, (DATE_FORMAT(V.`Data`, '%d/%m/%Y')) `Data`, E.Id 'IdEstoque',
    SUM(((IV.QtdVendida * E.ValorUnit) - ((IV.QtdVendida * E.ValorUnit) * IV.DescontoProd))
    - (((IV.QtdVendida * E.ValorUnit)
    - ((IV.QtdVendida * E.ValorUnit) * IV.DescontoProd)) * V.Desconto)) ValorTotal
		from tbItensVendidos IV INNER JOIN tbEstoque E on IV.IdEstoque = E.Id
								INNER JOIN tbVenda V on IV.IdVenda = V.Id
	);
	
    drop view if exists vwSaldo;
    create view vwSaldo as select  (IFNULL(V.ValorTotal, 0) - IFNULL(S.Saída, 0))  `Saldo` from vwSaida S, vwVenda V;
    
    drop view if exists vwItems;
    create view vwItems as
	(SELECT S.IdEstoque `Id`,
			S.Nome `Item`,
            E.Qtd `Quantidade`,
            E.UM `Unidade de Medida`,
            E.ValorUnit `Valor Unitário (R$)`,
		   (E.Qtd * E.ValorUnit) `Valor Total (p/Produto)`,
           'Semente' `Categoria`
	FROM tbEstoque E
	INNER JOIN tbSemente S ON E.Id = S.IdEstoque)
	UNION
	(SELECT I.IdEstoque, 
			I.Nome,
            E.Qtd,
            E.UM,
            E.ValorUnit,
            (E.Qtd * E.ValorUnit),
            C.Categoria
	FROM tbEstoque E
	INNER JOIN tbInsumo I ON E.Id = I.IdEstoque
	INNER JOIN tbCategoria C ON C.Id = I.IdCategoria)
	UNION
	(SELECT M.IdEstoque,
			M.Nome,
            E.Qtd,
            E.UM,
            E.ValorUnit,
            (E.Qtd * E.ValorUnit),
            M.Tipo
	FROM tbMaquina M
	INNER JOIN tbEstoque E ON M.IdEstoque = E.Id)
    UNION
	(SELECT P.IdEstoque,
			P.Nome,
            E.Qtd,
            E.UM,
            E.ValorUnit,
            (E.Qtd * E.ValorUnit),
            'Produto'
	FROM tbProduto P
	INNER JOIN tbEstoque E ON P.IdEstoque = E.Id);
	
    drop view if exists vwFluxoDeCaixa;
    create view vwFluxoDeCaixa as
    select IFNULL(S.`Saída`,0) `Saída`, IFNULL(V.ValorTotal,0) `Entrada`, Sal.Saldo, monthname(S.`Data`) `MÊS` from vwVenda V, vwSaida S, vwSaldo Sal where month(S.`Data`) = month(V.D) group by `MÊS`;
    
    
-- MUDAR VALOR DOS NOMES DAS DATAS PRA PORTUGUES    SET lc_time_names = 'pt_BR';
-- =============================================================================================================================== 

-- =================================================================== PROC ESTOQUE ===============================================
	DELIMITER $$
    
    drop procedure if exists spInsertEstoque$$
    create procedure spInsertEstoque(
    in
		Qnt double,
        UnM int,
        ValUnit double
    )
    begin
		if Qnt < 0 then
			signal sqlstate '45000'
			   set message_text = 'Valor menor que zero!';
		else
        
        insert into tbEstoque(Qtd, UM, ValorUnit) values(Qnt, UnM, ValUnit);
        end if;
    end$$
    
    drop procedure if exists spInsertSemente$$
    create procedure spInsertSemente(
	in 
		Qnt double,
        UnM int,
        ValUnit double,
        Nome varchar(50),
        Solo varchar(50),
        IncSol decimal(5,2),
        IncVento decimal(5,2),
        Acidez decimal(5,2)
    )
    begin
        declare conta1 int;
        declare conta2 int;
        declare idE int;
        set conta1 = (select count(*) from tbEstoque); 
        
		call spInsertEstoque(Qnt, UnM, ValUnit);

        set conta2 = (select count(*) from tbEstoque); 
        
        if (conta2 = conta1 + 1) then
			set idE = (select Id from tbEstoque order by Id desc limit 1 );
			insert into tbSemente(IdEstoque, Nome, Solo, IncSol, IncVento, Acidez) value(IdE, Nome, Solo, IncSol, IncVento, Acidez);
		end if;
    end$$

    drop procedure if exists spInsertMaquina$$
    create procedure spInsertMaquina(
	in 
		Qnt double,
        UnM int,
        ValUnit double,
        Nome varchar(50),
        Tipo int,
        Montadora varchar(75),
        `Desc` varchar(300),
        VidaUtil int,
        ValorInicial double,
        DeprMes double,
        DeprAno double
    )
    begin
        declare conta1 int;
        declare conta2 int;
        declare idE int;
        set conta1 = (select count(*) from tbEstoque); 
        
		call spInsertEstoque(Qnt, UnM, ValUnit);

        set conta2 = (select count(*) from tbEstoque); 
        
        if (conta2 = conta1 + 1) then
			set idE = (select Id from tbEstoque order by Id desc limit 1 );
			insert into tbMaquina(IdEstoque, Nome, Tipo, Montadora, `Desc`, VidaUtil, ValorInicial, DeprMes, DeprAno)
							value(IdE, Nome, Tipo, Montadora, `Desc`, VidaUtil, ValorInicial, DeprMes, DeprAno);
		end if;
    end$$

    drop procedure if exists spInsertInsumo$$
    create procedure spInsertInsumo( -- !!!!!!!!CONSERTAR!!!!!!!!!
	in 
		Qnt double,
        UnM int,
        ValUnit double,
        Nome varchar(50),
        `Desc` varchar(300),
        Categoria varchar(30)
    )
    begin
        declare conta1, conta2, idE int;
        declare IdCategoria varchar(30);
        
        if (exists(select Categoria from tbCategoria where Categoria like('%'+Categoria+'%'))) then
			set IdCategoria = (select Id from tbCategoria where Categoria like('%'+Categoria+'%'));
		else 
			insert into tbCategoria(Categoria) values(Categoria);
            set IdCategoria = (select Id from tbCategoria order by Id desc limit 1);
		end if;
        
        set conta1 = (select count(*) from tbEstoque); 
        
		call spInsertEstoque(Qnt, UnM, ValUnit);

        set conta2 = (select count(*) from tbEstoque); 
        
        if (conta2 = conta1 + 1) then
			set idE = (select Id from tbEstoque order by Id desc limit 1 );
			insert into tbInsumo(IdEstoque, Nome, `Desc`, IdCategoria) value(IdE, Nome, `Desc`, IdCategoria);
		end if;
    end$$

    drop procedure if exists spInsertProduto$$
    create procedure spInsertProduto(
	in 
		Qnt double,
        UnM int,
        ValUnit double,
        Nome varchar(50),
        `Desc` varchar(300)
    )
    begin
        declare conta1, conta2, idE int;
        
        set conta1 = (select count(*) from tbEstoque); 
        
		call spInsertEstoque(Qnt, UnM, ValUnit);

        set conta2 = (select count(*) from tbEstoque); 
        
        if (conta2 = conta1 + 1) then
			set idE = (select Id from tbEstoque order by Id desc limit 1 );
			insert into tbProduto(IdEstoque, Nome, `Desc`) value(IdE, Nome, `Desc`);
		end if;
    end$$

    call spInsertSemente(2, 1, 1.50, 'Semente de Milho', null, null, null, null)$$
    call spInsertProduto(1, 1, 5.0, 'Milho', null)$$
    call spInsertInsumo(1, 3, 2.0, 'Pá', null, 'Ferrament')$$
    call spInsertMaquina(1, 3, 2500, 'Tratorzinho', 1, 'Joana Motors', null, 2, 2300, 20, 240)$$
    DELIMITER ;
    select * from vwItems; 
-- =============================================================================================================================== 

-- =================================================================== MANUTENÇÃO ================================================
create view vwQtdMa as
select count(IdMaquina) `Quantidade de Manutenções`,sum(ma.ValorPago) `Custo Total` from tbMaquinaManutencao mm inner join tbManutencao ma on ma.Id = mm.IdManutencao;

select M.Nome `Máquina`, M.Tipo `Tipo de Máquina`, Ma.Nome `Manutenção`, Ma.`Data` `Data da Manutenção`, Ma.ValorPago `Valor da Manutenção`
 from tbMaquina M 
 inner join tbMaquinaManutencao mm 
	on mm.IdMaquina = M.IdEstoque
 inner join tbManutencao Ma 
	on Ma.Id = mm.IdManutencao;
select * from tbManutencao;
-- =============================================================================================================================== 

-- =================================================================== PROC ESTOQUE ===============================================
-- =============================================================================================================================== 