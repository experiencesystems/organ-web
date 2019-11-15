use dbOrgan; 
-- MUDAR VALOR DOS NOMES Das( DATas( PRA PORTUGUES    SET lc_time_names = 'pt_BR';     

drop view if exists vwPragaOrDoenca ;
create view vwPragaOrDoenca as(
select pd.Id,
	   pd.Nome `Nome`,
	   case
			when pd.`P/D` = true then 'Praga'
            else 'Doença'
	   end as
       `Tipo`,
	   group_concat(a.Nome separator ', ') `Áreas`, 
       case
			when c.`Status` = true then 'Controlado'
            else 'Não controlado'
	   end as
       `Status`
from tbAreaPD apd
	inner join tbPragaOrDoenca pd on apd.IdPD = pd.Id
    inner join tbArea a on apd.IdArea = a.Id
    inner join tbControlePD cpd on cpd.IdPd = pd.Id
    inner join tbControle c on c.Id = cpd.IdControle
group by pd.Id, c.`Status`);

drop view if exists vwControle;
create view vwControle as(
select c.Id,
	   DATE_FORMAT(c.`Data`, '%d/%m/%Y') `Data`,
	   case
			when c.`Status` = true then 'Controlado'
            else 'Não controlado'
	   end as
       `Status`,
       ifnull(c.`Desc`, 'Sem Descrição') `Descrição`,
       c.Efic `Eficiência(%)`,
	   c.NumLiberacoes `Número de Liberações`,
       group_concat(distinct p.Nome separator ', ' ) `Pragas/Doenças`,
       group_concat(distinct concat(i.Item, ' - ', ic.QtdUsada) separator ', ') `Itens Usados - Quantidade`
from tbControle c
	inner join tbControlePD cpd on c.Id = cpd.IdControle
    inner join tbPragaOrDoenca p on cpd.IdPD = p.Id
    inner join tbItensControle ic on c.Id = ic.IdControle
	inner join vwItems i on ic.IdEstoque = i.Id
group by c.Id, c.`Status`);