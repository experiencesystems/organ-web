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


 