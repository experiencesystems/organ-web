use dbOrgan;
/*SELECT Las(T_INSERT_ID();*/

drop view if exists vwEndereco;
create view vwEndereco as(
select E.CEP,  R.Logradouro `Rua`, concat(B.Bairro,' - ', C.Cidade,'/', Es.UF) `BCE` 
 from tbEndereco E 
	inner join tbLogradouro R on E.IdRua = R.Id
	inner join tbBairro B on R.IdBairro = B.Id
	inner join tbCidade C on B.IdCidade = C.Id
	inner join tbEstado Es on C.IdEstado = Es.Id
);

drop view if exists vwTelefone;
create view vwTelefone as(
select T.Id, concat('(',T.IdDDD,') ',T.Numero,' - ',Ti.Tipo) `Telefone`
 from tbTelefone T
	inner join tbTipoTel Ti on Ti.Id = T.IdTipo
); 

drop view if exists vwPessoa;
create view vwPessoa as(
select P.Id, P.Nome, P.Email, concat(E.Rua,', ', P.NumeroEndereco,' - ', ifnull(P.CompEndereco, 'Sem Complemento'),' - ',E.BCE,' - ',E.CEP) `Endereço`,  group_concat(T.Telefone separator '; ') `Telefones`
 from tbPessoa P 
	inner join vwEndereco E on P.CEP = E.CEP
	inner join tbTelefonePessoa TP on P.Id = TP.IdPessoa
	inner join vwTelefone T on T.Id = TP.IdTelefone
 group by P.Id
);
 
 drop view if exists vwPessoaFisica;
 create view vwPessoaFisica as(
 select P.Id, P.Nome, F.CPF, F.RG, date_format(F.DataNasc, ('%d/%m/%Y')) `Data de Nascimento`, P.Telefones, P.Email, P.`Endereço`
  from tbPessoaFisica F 
	inner join vwPessoa P on F.IdPessoa = P.Id
);

drop view if exists vwPessoaJuridica;
create view vwPessoaJuridica as(
select P.Id, P.Nome, J.RazaoSocial, J.CNPJ, J.IE, P.Telefones, P.Email, P.`Endereço`
 from tbPessoaJuridica J 
	inner join vwPessoa P on J.IdPessoa = P.Id
);
 
drop view if exists vwFuncionario;
create view vwFuncionario as(
select F.Id, PF.Nome,  C.Nome `Cargo`, F.Salario `Salário`, PF.CPF, PF.RG, PF.`Data de Nascimento`, PF.Telefones, PF.Email, PF.`Endereço`
 from tbFuncionario F
	inner join vwPessoaFisica PF on F.IdPessoa = PF.Id
	inner join tbCargo C on F.IdCargo = C.Id
 where F.`Status` = true
); 

drop view if exists vwFornecedor;
create view vwFornecedor as(
select F.Id, PJ.Nome `Nome Fantasia`, PJ.RazaoSocial `Razão Social`, PJ.CNPJ, PJ.IE, PJ.Telefones, PJ.Email, PJ.`Endereço`
 from tbFornecedor F
	inner join vwPessoaJuridica PJ on PJ.Id = F.IdPessoa
 where F.`Status` = true
);  

drop view if exists vwItems;
create view vwItems as
(SELECT S.IdEstoque `Id`,
			S.Nome `Item`,
            E.Qtd `Quantidade`,
            -- U.`Desc` `Unidade de Medida`,
            E.ValorUnit `Valor Unitário (R$)`,
			(E.Qtd * E.ValorUnit) `Valor Total (por Produto)`,
           'Semente' `Categoria`,
           F.`Nome Fantasia` `Fornecedor`,
           'Semente' `Tipo`
FROM tbEstoque E
INNER JOIN tbSemente S ON E.Id = S.IdEstoque
inner join vwFornecedor F on E.IdFornecedor = F.Id
-- inner join tbUM U on E.UM = U.Id
)
UNION
(SELECT I.IdEstoque, 
		I.Nome,
		E.Qtd,
		-- U.`Desc`,
		E.ValorUnit,
		(E.Qtd * E.ValorUnit),
		C.Categoria,
        F.`Nome Fantasia`,
           'Insumo' `Tipo`
FROM tbEstoque E
INNER JOIN tbInsumo I ON E.Id = I.IdEstoque
INNER JOIN tbCategoria C ON C.Id = I.IdCategoria
inner join vwFornecedor F on E.IdFornecedor = F.Id
-- inner join tbUM U on E.UM = U.Id
)
UNION
(SELECT M.IdEstoque,
		M.Nome,
		E.Qtd,
		-- U.`Desc`,
		E.ValorUnit,
		(E.Qtd * E.ValorUnit),
		M.Tipo,
        F.`Nome Fantasia`,
           'Máquina' `Tipo`
FROM tbMaquina M
INNER JOIN tbEstoque E ON M.IdEstoque = E.Id
inner join vwFornecedor F on E.IdFornecedor = F.Id
-- inner join tbUM U on E.UM = U.Id
)    UNION
(SELECT P.IdEstoque,
		P.Nome,
		E.Qtd,
		-- U.`Desc`,
		E.ValorUnit,
		(E.Qtd * E.ValorUnit),
		'Produto',
        F.`Nome Fantasia`,
           'Produto' `Tipo`
FROM tbProduto P
INNER JOIN tbEstoque E ON P.IdEstoque = E.Id
inner join vwFornecedor F on E.IdFornecedor = F.Id
-- inner join tbUM U on E.UM = U.Id
)
    order by `Categoria`;

-- MUDAR VALOR DOS NOMES Das( DATas( PRA PORTUGUES    SET lc_time_names = 'pt_BR';     

drop view if exists vwPragaOrDoenca ;
create view vwPragaOrDoenca as(
select pd.Id,
	   pd.Nome `Nome`,
	   case
			when pd.`P/D` = true then 'Praga'
            else'Doença'
	   end as
       `Tipo`,
	   group_concat(a.Nome separator ', ') `Áreas`, c.`Status`
from tbAreaPD apd
	inner join tbPragaOrDoenca pd on apd.IdPD = pd.Id
    inner join tbArea a on apd.IdArea = a.Id
    inner join tbControlePD cpd on cpd.IdPd = pd.Id
    inner join tbControle c on c.Id = cpd.IdControle
group by `Id`);

drop view if exists vwControle;
create view vwControle as(
select c.Id,
	   DATE_FORMAT(c.`Data`, '%d/%m/%Y') `Data`,
	   c.`Status`,
       ifnull(c.`Desc`, 'Sem Descrição') `Descrição`,
       c.Efic `Eficiência(%)`,
	   c.NumLiberacoes `Número de Liberações`,
       group_concat(distinct p.Nome separator ', ' ) `Pragas/Doenças(`,
       group_concat(distinct concat(i.Item, ' - ', ic.QtdUsada) separator ', ') `Itens Usados - Quantidade`
from tbControle c
	inner join tbControlePD cpd on c.Id = cpd.IdControle
    inner join tbPragaOrDoenca p on cpd.IdPD = p.Id
    inner join tbItensControle ic on c.Id = ic.IdControle
	inner join vwItems i on ic.IdEstoque = i.Id
group by c.Id);


 