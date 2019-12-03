use dborgan; 
-- MUDAR VALOR DOS NOMES Das( DATas( PRA PORTUGUESSET 

set lc_time_names = 'pt_BR'; 

drop view if exists vwPragaOrDoenca ;
create view vwPragaOrDoenca as(
	select 
        pd.Id,
        pd.Nome `Nome`,
        case
            when pd.`P/D` = true then 'Praga'
            else 'Doença'
        end as `Tipo`,
        group_concat(a.Nome separator ', ') `Áreas`,
        case
            when c.`Status` = true then 'Controlado'
            else 'Não controlado'
        end as `Status`
    from
        tbAreaPD apd
            inner join
        tbPragaOrDoenca pd on apd.IdPD = pd.Id
            inner join
        tbArea a on apd.IdArea = a.Id
            inner join
        tbControlePD cpd on cpd.IdPd = pd.Id
            inner join
        tbControle c on c.Id = cpd.IdControle
    group by pd.Id , c.`Status`);

drop view if exists vwTelefone;
create view vwTelefone as(
	select 
        T.Id,
        concat('(',
                T.IdDDD,
                ') ',
                T.Numero,
                ' - ',
                Ti.Tipo) `Telefone`
    from
        tbTelefone T
            inner join
        tbTipoTel Ti on Ti.Id = T.IdTipo
	group by T.Id); 

drop view if exists vwFornecedor;
create view vwFornecedor as(
	select 
        F.Id,
        F.Nome `Razão Social`,
        F.Email `Email`,
        group_concat(ifnull(T.Telefone, 'Sem Telefone')
            separator '; ') `Telefones`,
		case 
			when F.`Status` = true then 'Ativo'
            else 'Desativado'
		end as `Situação`
		
    from
        tbFornecedor F
            left join
        tbTelForn TF on F.Id = TF.IdForn
            left join
        vwTelefone T on T.Id = TF.IdTelefone
    group by F.Id); 
    
drop view if exists vwFuncionario;
create view vwFuncionario as(
	select 
        F.Id,
        F.Nome,
        F.Funcao `Função`,
        group_concat(ifnull(T.Telefone, 'Sem Telefone')
            separator '; ') `Telefones`,
        F.Email,
        case 
			when F.`Status` = true then 'Ativo'
            else 'Demitido'
		end as `Situação`
    from
        tbFuncionario F
            left join
        tbTelFunc TF on F.Id = TF.IdFunc
            left join
        vwTelefone T on T.Id = TF.IdTelefone
    group by F.Id
);

drop view if exists vwItems; 
create view vwItems as(
	select 
        S.IdEstoque `Id`,
        S.Nome `Item`,
        E.Qtd `Quantidade`,
        U.`Desc` `Unidade de Medida`,
        'Semente' `Categoria`,
        ifnull(S.`Desc`, 'Sem Descrição') `Descrição`,
        ifnull(F.`Razão Social`, 'Sem fornecedor') `Fornecedor`,
        'Semente' `Tipo`
    from
        tbSemente S 
            inner join
		tbEstoque E on E.Id = S.IdEstoque
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	group by `Id`
)
union(
	select 
        I.IdEstoque,
        I.Nome,
        E.Qtd,
        U.`Desc`,
        I.Categoria,
        ifnull(I.`Desc`, 'Sem Descrição'),
        ifnull(F.`Razão Social`, 'Sem Fornecedor'),
        'Insumo' `Tipo`
    from
        tbInsumo I 
            inner join
        tbEstoque E on E.Id = I.IdEstoque
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	group by I.IdEstoque
)
union(
	select 
        M.IdEstoque,
        M.Nome,
        E.Qtd,
        U.`Desc`,
        M.Tipo `Categoria`,
        ifnull(M.`Desc`, 'Sem Descrição'),
        ifnull(F.`Razão Social`, 'Sem Fornecedor'),
        'Máquina' `Tipo`
    from
        tbMaquina M
            inner join
        tbEstoque E on M.IdEstoque = E.Id
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	order by M.IdEstoque
)
union(
	select 
        P.IdEstoque,
        P.Nome,
        E.Qtd,
        U.`Desc`,
        'Produto',
        ifnull(P.`Desc`, 'Sem Descrição'),
        ifnull(F.`Razão Social`, 'Sem Fornecedor'),
        'Produto' `Tipo`
    from
        tbProduto P
            inner join
        tbEstoque E on P.IdEstoque = E.Id
            left join
        vwFornecedor F on E.IdFornecedor = F.Id
            inner join
        tbUM U on E.UM = U.Id
	group by P.IdEstoque
)order by `Id`; 

drop view if exists vwArea;
create view vwArea as(
	select a.Id `Id`,
		   a.Nome `Área`,
           a.Tamanho `Tamanho(em ha)`,
           s.Id `IdSolo`,
           s.Tipo `Tipo de Solo`,
           s.IncSolar `Incidência Solar`,
           s.IncVento `Incidênica do Vento`,
           case
				when a.Disp = 1 then 'Disponível'
				when a.Disp = 2 then 'Em Uso'
				else 'Indiponível'
		   end as `Disponibilidade`
	from tbArea a
		inner join
	tbSolo s on a.IdSolo = s.Id
group by Id
);

drop view if exists vwControle;
create view vwControle as(
	select 
        c.Id,
        date_format(c.`Data`, '%d/%m/%Y') `Data`,
        case
            when c.`Status` = true then 'Controlado'
            else 'Não controlado'
        end as `Status`,
        ifnull(c.`Desc`, 'Sem Descrição') `Descrição`,
        c.Efic `Eficiência(%)`,
        c.NumLiberacoes `Número de Liberações`,
        group_concat(distinct p.Id separator ', ') `IdPD`,
        group_concat(distinct p.Nome separator ', ') `Pragas/Doenças`,
        ifnull(group_concat(distinct (concat (i.Item, ' - ', ic.QtdUsada)) separator ', '), 'Nenhum Item Usado') `Itens Usados - Quantidade`,
        group_concat(distinct(ifnull(F.Nome, 'Sem Funcionários')) separator ',') `Funcionários Participantes`
    from
        tbControle c
            left join
		tbFuncControle FC on c.Id = FC.IdControle
			left join
		tbFuncionario F on FC.IdFunc = F.Id
			inner join
        tbControlePD cpd on c.Id = cpd.IdControle
            inner join
        tbPragaOrDoenca p on cpd.IdPD = p.Id
            left join
        tbItensControle ic on c.Id = ic.IdControle
            left join
        vwItems i on ic.IdEstoque = i.Id
    group by c.Id, c.`Status`);
    
drop view if exists vwHistorico;
create view vwHistorico as(
select date_format(HE.DataAlteracao, '%e/%m/%y às %H:%i') `Data de Alteração`, HE.Id, HE.IdEstoque `Id do Item`, ifnull(I.`Item`, 'Item Excluído') `Nome do Item`,
 ifnull(HE.QtdAntiga, '0') `Quantidade Antiga`,ifnull(I.`Quantidade`, '0') `Quantidade Atual`, ifnull(HE.`Desc`, 'Sem Descrição') `Descrição de Alteração`
 from tbHistEstoque HE
	left join 
 vwItems I on HE.IdEstoque = I.Id
 group by Id, I.Item, I.Quantidade
 order by `Data de Alteração` desc
);

drop view if exists vwPlantio;
create view vwPlantio as(
	select P.Id,
		   P.Nome `Plantio`,
		   P.Sistema `Sistema`,
           P.TipoPlantio `Tipo`,
           P.DataInicio `Data de Início`,
		   P.DataColheita `Data Prevista pra Colheita`,
           group_concat(distinct(A.Nome) separator ', ') `Áreas`,
           group_concat(distinct(concat(I.Item)) separator ',') `Itens Usados`,
           group_concat(distinct(ifnull(F.Nome, 'Sem Funcionários')) separator ', ') `Funcionários Participantes`,
           case 
			when P.`Status` = true then 'Ativo'
            else 'Finalizado'
            end as `Status`
	from
		tbPlantio P
			left join
		tbFuncPlantio FC on P.Id = FC.IdPlantio
			left join 
		tbFuncionario F on FC.IdFunc = F.Id
			left join
		tbAreaPlantio AP on P.Id = AP.IdPlantio
			left join 
		tbArea A on AP.IdArea = A.Id
			left join 
		tbItensPlantio IP on P.Id = IP.IdPlantio
			left join
		vwItems I on IP.IdEstoque = I.Id
	group by P.Id
    order by P.Id
);

drop view if exists vwColheita;
create view vwColheita as(
	select c.Id `Id`,
		   date_format(c.`Data`, '%e/%m/%y') `Data da Colheita`,
           case
				when c.`Status` = true then 'Normal'
				else 'Final'
		   end as `Situação da Colheita`,
		   c.IdProd `IdProd`,
           p.Nome `Produto`,
           (c.QtdTotal - c.QtdPerdas) `Quantidade Colhida`,
           c.QtdPerdas `Quantidade Perdida`,
           c.QtdTotal `Quantidade Total`,
           c.IdPlantio `IdPlantio`,
           ifnull(pr.Nome, 'Plantio colhido definitivamente') `Plantio`
	from tbColheita c
		left join
	tbPlantio pr on c.IdPlantio = pr.Id
		inner join
	tbProduto p on c.IdProd = p.IdEstoque
	group by Id
    order by Id desc
);


DELIMITER $
drop trigger if exists trgInsertHistorico$
create trigger trgInsertHistorico after insert
on tbEstoque
for each row
begin   

call spVerQtd(NEW.Qtd);
insert into tbHistEstoque(`Desc`, IdEstoque) values(concat('Adicionado ', NEW.Qtd), NEW.Id);

end$

DELIMITER $ 
drop trigger if exists trgUpdateHistorico$
create trigger trgUpdateHistorico after update
on tbEstoque
for each row
begin   
declare descs varchar(50);
declare nqtd double;

call spVerQtd(NEW.Qtd);

if(exists(select * from tbItensPlantio where IdEstoque = NEW.Id order by a desc limit 1)) then 
	set descs = 'Item utilizado no plantio';
else set descs = 'Item Alterado';
end if;
if(exists(select * from tbItensControle where IdEstoque = NEW.Id order by a desc limit 1)) then
	set descs = 'Item utilizado no controle';
else set descs = 'Item Alterado';
end if;

if(NEW.Qtd > OLD.Qtd) then
	begin
    set nqtd = round((NEW.Qtd - OLD.Qtd), 2);
	set descs = concat(descs, ' - Adicionado ', cast(nqtd as char));
    end;
elseif(NEW.Qtd < OLD.Qtd) then
	begin
    set nqtd = round((OLD.Qtd - NEW.Qtd), 2);
	set descs = concat(descs,' - Retirado ', cast(nqtd as char));
    end;
else set descs = 'Item Alterado';
end if;

insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
				   values(OLD.Qtd, descs, OLD.Id);

end$

DELIMITER $
drop trigger if exists trgDeleteHistorico$ 
create trigger trgDeleteHistorico before delete 
on tbEstoque
for each row
begin
declare nome varchar(30);   
if(OLD.Qtd = 0)
	then
		set FOREIGN_KEY_CHECKS=0;
		
        set nome = (select Item from vwItems where Id = OLD.Id);
		insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
		   values(OLD.Qtd, concat(nome, ' foi excluído'), OLD.Id);
	else
		SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Quantidade diferente de zero.';
	end if;
end$

DELIMITER $
drop trigger if exists trgDeleteEstoque$ 
create trigger trgDeleteEstoque
after delete 
on tbEstoque
for each row
begin   
set FOREIGN_KEY_CHECKS=1;
end$

DELIMITER $
drop trigger if exists trgColheita$ 
create trigger trgColheita after insert 
on tbColheita
for each row
begin
if((select `Status` from tbPlantio where Id = NEW.IdPlantio) = true) then  
if (NEW.`Status` = false) then
	begin
	update tbPlantio set `Status` = false where Id = NEW.IdPlantio;
	/*
	Se não funcionar substitui as 3 linhas acima por isso:
	update tbPlantio set `Status` = false where Id = NEW.IdPlantio
	*/
    end;
end if;

if(exists(select * from tbColheita where IdProd = NEW.IdProd)) then
	update tbEstoque set Qtd = (Qtd + (NEW.QtdTotal - NEW.QtdPerdas)) where Id = NEW.IdProd;
end if;
else
	SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Impossível realizar colheita de um plantio finalizado.';
end if;
end$

DELIMITER $
drop trigger if exists trgItensPlantio$
create trigger trgItensPlantio
before insert 
on tbItensPlantio
for each row
begin
declare qt double;
call spVerQtd(NEW.QtdUsada);
call spCertQtd(NEW.QtdUsada, NEW.IdEstoque);

if((select Tipo from vwItems where Id = NEW.IdEstoque) != 'Máquina')then
	update tbEstoque
	set Qtd = (Qtd - NEW.QtdUsada)
	where Id = NEW.IdEstoque;
else 
	begin
    set qt = (select Quantidade from vwItems where Id = NEW.IdEstoque);
	insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
				   values(qt, 'Máquina utilizada no plantio', NEW.IdEstoque);
	end;
end if;
end$

DELIMITER $
drop trigger if exists trgItensControle$
create trigger trgItensControle
before insert 
on tbItensControle
for each row
begin
declare qt double;
call spVerQtd(NEW.QtdUsada);
call spCertQtd(NEW.QtdUsada, NEW.IdEstoque);

if((select Tipo from vwItems where Id = NEW.IdEstoque) != 'Máquina')then
	update tbEstoque
	set Qtd = (Qtd - NEW.QtdUsada)
	where Id = NEW.IdEstoque;
else 
	begin
    set qt = (select Quantidade from vwItems where Id = NEW.IdEstoque);
	insert into tbHistEstoque(QtdAntiga, `Desc`, IdEstoque)
				   values(qt, 'Máquina utilizada no controle', NEW.IdEstoque);
	end;
end if;
end$

DELIMITER $
drop trigger if exists trgAreaPlantio$
create trigger trgAreaPlantio
before insert 
on tbAreaPlantio
for each row
begin

update tbArea
set Disp = 2
where Id = NEW.IdArea;
end$


DELIMITER $
drop trigger if exists trgAreaControle$
create trigger trgAreaControle
before insert 
on tbAreaPD
for each row
begin

update tbArea
set Disp = 3
where Id = NEW.IdArea;
end$
DELIMITER ; 