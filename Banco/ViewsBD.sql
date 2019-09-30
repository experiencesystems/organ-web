use dbOrgan;
-- =================================================================== FLUXO DE CAIXA ============================================  
	drop view if exists VwCompra;
    create view VwCompra as(
    select C.Id 'Compra', C.`Data`, E.Id 'Estoque', SUM(((IC.QtdProd * E.ValorUnit) - ((IC.QtdProd * E.ValorUnit) * IC.DescontoProd)) - (((IC.QtdProd * E.ValorUnit) - ((IC.QtdProd * E.ValorUnit) * IC.DescontoProd)) * C.Desconto)) ValorTotal
		from tbItensComprados IC INNER JOIN tbEstoque E on IC.IdEstoque = E.Id
								 INNER JOIN tbCompra C on IC.IdCompra = C.Id
    );                             
	
    drop view if exists VwSaida;
    create View VwSaida as select Co.`Data`, (SUM(M.ValorPago) + SUM(D.ValorPago)  + SUM(Co.ValorTotal)) `Saída` from tbManutencao M, tbDespesa D, VwCompra Co;
	
	drop view if exists VwVenda;
	create view VwVenda as(
    select V.Id 'IdVenda',V.`Data` `D`, (DATE_FORMAT(V.`Data`, '%d/%m/%Y')) `Data`, E.Id 'IdEstoque',
    SUM(((IV.QtdVendida * E.ValorUnit) - ((IV.QtdVendida * E.ValorUnit) * IV.DescontoProd))
    - (((IV.QtdVendida * E.ValorUnit)
    - ((IV.QtdVendida * E.ValorUnit) * IV.DescontoProd)) * V.Desconto)) ValorTotal
		from tbItensVendidos IV INNER JOIN tbEstoque E on IV.IdEstoque = E.Id
								INNER JOIN tbVenda V on IV.IdVenda = V.Id
	);
	
    drop view if exists VwSaldo;
    create view VwSaldo as select  (IFNULL(V.ValorTotal, 0) - IFNULL(S.Saída, 0))  `Saldo` from vwSaida S, VwVenda V;
    
    drop view if exists VwItems;
    create view VwItems as
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
    select IFNULL(S.`Saída`,0) `Saída`, IFNULL(V.ValorTotal,0) `Entrada`, Sal.Saldo, monthname(S.`Data`) `MÊS` from VwVenda V, VwSaida S, VwSaldo Sal where month(S.`Data`) = month(V.D) group by `MÊS`;
    
    
-- MUDAR VALOR DOS NOMES DAS DATAS PRA PORTUGUES    SET lc_time_names = 'pt_BR';

    

    
-- =============================================================================================================================== 