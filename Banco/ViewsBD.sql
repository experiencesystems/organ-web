use dbOrgan;
-- =================================================================== FLUXO DE CAIXA ============================================  
	    create view VwCompra as
    select C.Id 'Compra', E.Id 'Estoque', SUM(((IC.QtdProd * E.ValorUnit) - ((IC.QtdProd * E.ValorUnit) * IC.DescontoProd)) - (((IC.QtdProd * E.ValorUnit) - ((IC.QtdProd * E.ValorUnit) * IC.DescontoProd)) * C.Desconto)) ValorTotal
		from tbItensComprados IC INNER JOIN tbEstoque E on IC.IdEstoque = E.Id
								 INNER JOIN tbCompra C on IC.IdCompra = C.Id;
                                 
	create View VwSaida as
     select (SUM(M.ValorPago) + SUM(D.ValorPago)  + SUM(Co.ValorTotal)) 'Saída' from tbManutencao M, tbDespesa D, VwCompra Co;
	
    create view VwVenda as
    select V.Id 'Venda', E.Id 'Estoque',
    SUM(((IV.QtdVendida * E.ValorUnit) - ((IV.QtdVendida * E.ValorUnit) * IV.DescontoProd))
    - (((IV.QtdVendida * E.ValorUnit)
    - ((IV.QtdVendida * E.ValorUnit) * IV.DescontoProd)) * V.Desconto)) ValorTotal
		from tbItensVendidos IV INNER JOIN tbEstoque E on IV.IdEstoque = E.Id
								 INNER JOIN tbVenda V on IV.IdVenda = V.Id;
	
    -- A view entrada ainda não existe eu ainda não testei pra ver se os valores da conta dão certo
    
    create view VwSaldo as
     select  V.ValorTotal - S.Saída 'Saldo' from vwSaida S, VwVenda V;
-- =============================================================================================================================== 