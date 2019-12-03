-- IMAGEM NO BANCO http://www.linhadecodigo.com.br/artigo/100/blob-fields-in-mysql-databases.aspx
drop database if exists dborgan;
create database dborgan;
use dborgan;

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

drop table if exists tbDDD;
create table tbDDD(
DDD numeric(2) not null,
 constraint PKDDD primary key(DDD)
)engine = InnoDB;

alter table tbTelefone add constraint FKTelefoneTipo foreign key(IdTipo) references tbTipoTel(Id),
   add constraint FKTelefoneDDD foreign key(IdDDD) references tbDDD(DDD);
   
drop table if exists tbFuncionario;
create table tbFuncionario(
Id int auto_increment,
 constraint PKFunc primary key(Id),
`Status` bool default true,
Nome varchar(50) not null,
Email varchar(100) not null,
Funcao int not null
)engine = InnoDB;

drop table if exists tbTelFunc;
create table tbTelFunc(
IdFunc int,
IdTelefone int,
 constraint PKTelPessoa primary key (IdFunc, IdTelefone)
)engine = InnoDB;
alter table tbTelFunc add constraint FKTelFunc foreign key(IdTelefone) references tbTelefone(Id),
  add constraint FKFuncTel foreign key(IdFunc) references tbFuncionario(Id);
  
drop table if exists tbFornecedor;
create table tbFornecedor(
Id int auto_increment,
 constraint PKFornecedor primary key(Id),
`Status` bool not null default true,
Nome varchar(50) not null,
Email varchar(100) not null
)engine = InnoDB;

drop table if exists tbTelForn;
create table tbTelForn(
IdForn int,
IdTelefone int,
 constraint PKTelPessoa primary key (IdForn, IdTelefone)
)engine = InnoDB;
alter table tbTelForn add constraint FKTelForn foreign key(IdTelefone) references tbTelefone(Id),
 add constraint FKFornTel foreign key(IdForn) references tbFornecedor(Id);
 
drop table if exists tbUM;
create table tbUM(
Id varchar(6) not null,
 constraint PKUM primary key(Id),
`Desc` varchar(20)
)engine = InnoDB;

drop table if exists tbEstoque;
create table tbEstoque(
Id int auto_increment,
 constraint PKEstoque primary key(Id),
Qtd double not null,
UM varchar(6),
IdFornecedor int
)engine = InnoDB;
alter table tbEstoque add constraint FKEstoqueFornecedor foreign key(IdFornecedor) references tbFornecedor(Id),
  add constraint FKEstoqueUM foreign key(UM) references tbUM(Id);

drop table if exists tbSemente;
create table tbSemente(
IdEstoque int not null,
 constraint PKSemente primary key(IdEstoque),
Nome varchar(30) not null,
`Desc` varchar(100)  
)engine = InnoDB;
alter table tbSemente add constraint FKSementeEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbInsumo;
create table tbInsumo(
IdEstoque int not null,
 constraint PKInsumo primary key(IdEstoque),
Nome varchar(30) not null,
`Desc` varchar(100),
Categoria int not null
)engine = InnoDB;
alter table tbInsumo add constraint FKInsumoEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbMaquina;
create table tbMaquina (
    IdEstoque int not null,
    constraint PKMaquina primary key (IdEstoque),
    Nome varchar(30) not null,
    Tipo int not null,
    Montadora varchar(50),
    `Desc` varchar(100)
)  engine=InnoDB;
alter table tbMaquina add constraint FKMaquinaEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbPlantio;
create table tbPlantio(
Id int auto_increment,
 constraint PKPlantio primary key(Id),
Nome varchar(50) not null,
Sistema int not null,
DataColheita date not null,
DataInicio date not null,
TipoPlantio int not null,
`Status` bool default true
)engine = InnoDB;

drop table if exists tbSolo;
create table tbSolo(
Id int auto_increment,
 constraint PKSolo primary key(Id),
Nome varchar(25) not null,
Tipo int not null,
IncSolar decimal(5,2) default 0.00,
IncVento decimal(5,2) default 0.00
)engine = InnoDB;

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

drop table if exists tbAreaPlantio;
create table tbAreaPlantio(
IdPlantio int not null,
IdArea int not null,
 constraint PKAreaPlantio primary key(IdPlantio, IdArea),
Densidade int not null
)engine = InnoDB;
alter table tbAreaPlantio add constraint FKAreaPlantioPlantio foreign key(IdPlantio) references tbPlantio(Id),
 add constraint FKAreaPlantioArea foreign key(IdArea) references tbArea(Id);

drop table if exists tbItensPlantio;
create table tbItensPlantio(
a int auto_increment,
QtdUsada double not null,
IdPlantio int not null,
IdEstoque int not null,
 constraint PKItensPlantio primary key(a)
)engine = InnoDB;
alter table tbItensPlantio add constraint FKItensPlantioPlantio foreign key(IdPlantio) references tbPlantio(Id),
   add constraint FKItensPlantioEstoque foreign key(IdEstoque) references tbEstoque(Id);

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
Id int auto_increment,
`Data` date not null,
QtdPerdas double not null default 0,
QtdTotal double not null,
IdPlantio int not null,
IdProd int not null,
`Status` bool not null,
 constraint PKColheita primary key(Id)
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
  
drop table if exists tbControle;
create table tbControle(
Id int auto_increment,
constraint PKControle primary key(Id),
`Status` bool,
`Desc` varchar(100),
Efic decimal(5,2) not null,
NumLiberacoes int not null,
`Data` date not null
)engine = InnoDB;

drop table if exists tbItensControle;
create table tbItensControle(
a int auto_increment,
QtdUsada double not null,
IdControle int not null,
IdEstoque int not null,
 constraint PKItensControle primary key(a)
)engine = InnoDB;
alter table tbItensControle add constraint FKItensControleControle foreign key(IdControle) references tbControle(Id),
add constraint FKItensControleEstoque foreign key(IdEstoque) references tbEstoque(Id);

drop table if exists tbPragaOrDoenca;
create table tbPragaOrDoenca(
Id int auto_increment,
 constraint PKPD primary key(Id),
Nome varchar(30) not null,
`P/D` bool not null
)engine = InnoDB;

drop table if exists tbControlePD;
create table tbControlePD(
IdControle int not null,
IdPD int not null,
 constraint PKControlePD primary key(IdControle, IdPD)
)engine = InnoDB;
alter table tbControlePD add constraint FKControlePD foreign key(IdControle) references tbControle(Id),
 add constraint FKPDControle foreign key(IdPD) references tbPragaOrDoenca(Id);

drop table if exists tbAreaPD;
create table tbAreaPD(
`Status` bool not null,
IdArea int not null,
IdPd int not null,
 constraint PKAreaPD primary key(IdArea, IdPD)
)engine = InnoDB;
alter table tbAreaPD add constraint FKAreaPD foreign key(IdArea) references tbArea(Id),
 add constraint FKPDArea foreign key(IdPd) references tbPragaOrDoenca(Id);
   
create table tbFuncControle(
IdFunc int not null,
IdControle int not null,
 constraint PKFuncControle primary key(IdFunc, IdControle)
)engine = InnoDB;
alter table tbFuncControle add constraint FKFuncControleFuncionario foreign key(IdFunc) references tbFuncionario(Id),
   add constraint FKFuncControleControle foreign key(IdControle) references tbControle(Id);

drop table if exists tbHistEstoque;
create table tbHistEstoque(
Id int auto_increment,
 constraint PKHistEstoque primary key(Id),
QtdAntiga double,
DataAlteracao datetime default current_timestamp,
`Desc` varchar(50),
IdEstoque int
)engine = InnoDB;
alter table tbHistEstoque add constraint FKHistEstoque foreign key(IdEstoque) references tbEstoque(Id);

/*https://web.archive.org/web/20130509230922/http://dev.mysql.com/tech-resources/articles/mysql-storedprocedures.pdf*/
    
DELIMITER $ 
drop procedure if exists spInsertEstoque$
create procedure spInsertEstoque(
in
	Qnt double,
    UnM varchar(6),
    Descr varchar(20)
)
begin
	if Qnt < 0 then
		signal sqlstate '45000'
		   set message_text = 'Valor menor que zero!';
	else
    if (not exists(select * from tbUM where Id = UnM) and not(isnull(Descr))) then
		insert into tbUM value(UnM, Descr);
    end if;
    
      insert into tbEstoque(Qtd, UM) values(Qnt, UnM);
    end if;
end$
    
DELIMITER $ 
drop procedure if exists spInsertSemente$
create procedure spInsertSemente(
in 
	Qnt double,
    UnM varchar(6),
	Nome1 varchar(30),
    `Desc1` varchar(100),
    Descr varchar(20)
)
begin
	declare conta1 int;
    declare conta2 int;
    declare idE int;
    set conta1 = (select count(*) from tbEstoque); 
        
	call spInsertEstoque(Qnt, UnM, Descr);
    
    set conta2 = (select count(*) from tbEstoque); 
      
    if (conta2 = conta1 + 1) then
		set idE = (select Id from tbEstoque order by Id desc limit 1 );
		insert into tbSemente(IdEstoque, Nome, `Desc`) value(IdE, Nome1, `Desc1`);
	end if;
end$

DELIMITER $ 
drop procedure if exists spInsertMaquina$
create procedure spInsertMaquina(
in 
	Qnt double,
    UnM varchar(6),
    Nome1 varchar(30),
    Tipo1 int,
    Montadora1 varchar(50),
    `Desc1` varchar(100),
    Descr varchar(20)
)
begin
	declare conta1 int;
    declare conta2 int;
    declare idE int;
    set conta1 = (select count(*) from tbEstoque); 
    
	call spInsertEstoque(Qnt, UnM, Descr);
    
    set conta2 = (select count(*) from tbEstoque); 
        
    if (conta2 = conta1 + 1) then
		set idE = (select Id from tbEstoque order by Id desc limit 1 );
		insert into tbMaquina(IdEstoque, Nome, Tipo, Montadora, `Desc`)
						value(IdE, Nome1, Tipo1, Montadora1, `Desc1`);
	end if;
end$

DELIMITER $ 
drop procedure if exists spInsertInsumo$
create procedure spInsertInsumo(
in 
	Qnt double,
    UnM varchar(6),
    Nome1 varchar(30),
    `Desc1` varchar(100),
    Categoria1 int,
    Descr varchar(20)
)
begin
	declare conta1, conta2, idE int;
    set conta1 = (select count(*) from tbEstoque); 
       
	call spInsertEstoque(Qnt, UnM, Descr);

    set conta2 = (select count(*) from tbEstoque); 
        
    if (conta2 = conta1 + 1) then
		set idE = (select Id from tbEstoque order by Id desc limit 1 );
		insert into tbInsumo(IdEstoque, Nome, `Desc`, Categoria) value(IdE, Nome1, `Desc1`, Categoria1);
	end if;
end$

DELIMITER $ 
drop procedure if exists spInsertProduto$
create procedure spInsertProduto(
in 
	Qnt double,
    UnM varchar(6),
    Nome1 varchar(30),
    `Desc1` varchar(100),
    Descr varchar(20)
)
begin
    declare conta1, conta2, idE int;
        
    set conta1 = (select count(*) from tbEstoque); 
        
	call spInsertEstoque(Qnt, UnM, Descr);

    set conta2 = (select count(*) from tbEstoque); 
        
    if (conta2 = conta1 + 1) then
		set idE = (select Id from tbEstoque order by Id desc limit 1 );
		insert into tbProduto(IdEstoque, Nome, `Desc`) value(IdE, Nome1, `Desc1`);
	end if;
end$

DELIMITER $ 
drop procedure if exists spInsertColheita$
create procedure spInsertColheita(
in 
	Dataa date,
	QntPerdas double,
    QntTot double,
    UnM varchar(6),
    Nome1 varchar(30),
    `Desc1` varchar(100),
    IdPlant int,
    Stats int,
    Produto int,
    Descr varchar(20)    
)
begin
    declare qnt double;
    declare prod int;
    
    set qnt = (QntTot - QntPerdas);
    if(isnull(Produto)) then
		call spInsertProduto(qnt, UnM, Nome1, `Desc1`, Descr);
		
		set prod = (select IdEstoque from tbProduto order by IdEstoque desc limit 1);
	else
		set prod = (select IdEstoque from tbProduto where IdEstoque = Produto);
    end if;
    
	insert into tbColheita(`Data`, QtdPerdas, QtdTotal, IdPlantio, IdProd, `Status`) value(Dataa, QntPerdas, QntTot, IdPlant, prod, Stats);
end$

DELIMITER $ 
drop procedure if exists spVerQtd$
CREATE PROCEDURE spVerQtd (IN qtd double)
BEGIN
IF qtd < 0 THEN
SIGNAL SQLSTATE '45000'
   SET MESSAGE_TEXT = 'Valor menor que zero!';
END IF;
END$

DELIMITER $ 
drop procedure if exists spCertQtd$
CREATE PROCEDURE spCertQtd (IN qtd double, IdE int)
BEGIN
declare qtdE double;
set qtdE = (select Qtd from tbEstoque where Id = IdE);

IF qtd > qtdE THEN
SIGNAL SQLSTATE '44001'
   SET MESSAGE_TEXT = 'Quantidade maior do que a presente no estoque';
END IF;
END$
DELIMITER ;

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

insert into tbTipoTel(Tipo) values("Fixo"), ("Celular");

insert into tbDDD(DDD) values(11), (12), (13), (14), (15), (16), (17), (18), (19), (21), (22), (24), (27), (28), (31), (32), (33), (34),
							 (35), (37), (38), (41), (42), (43), (44), (45), (46), (47), (48), (49), (51), (53), (54), (55), (61), (62),
							 (63), (64), (65), (66), (67), (68), (69), (71), (73), (74), (75), (77), (79), (81), (82), (83), (84), (85),
							 (86), (87), (88), (89), (91), (92), (93), (94), (95), (96), (97), (98), (99); 

insert into tbTelefone(Numero, IdTipo, IdDDD) values(989896912, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 66)),
													(989896913, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 65)),
													(89896912, (select Id from tbTipoTel where Tipo = "Fixo"), (select DDD from tbDDD where DDD = 64));

insert into tbFuncionario(Nome, Funcao, Email) value('Mariana', 1, 'marianamonteiro@gmail.com');
Insert into tbFuncionario(Nome, Funcao, Email) value('Arnaldo', 1, 'arnaldo@gmail.com'); 
Insert into tbFuncionario(Nome, Funcao, Email) value('Carlos', 2, 'carlos@gmail.com'); 
Insert into tbFuncionario(Nome, Funcao, Email) value('Júlia', 3, 'julia@gmail.com'); 
Insert into tbFuncionario(Nome, Funcao, Email) value('Arnaldo Batista', 4, 'batista@gmail.com'); 

insert into tbTelFunc values(1,1), (1,3);
insert into tbTelFunc values(2,2);
insert into tbTelFunc values(3,3);
insert into tbTelFunc values(4,3); 

update tbFuncionario set `Status` = false where Id=3;
select * from vwFuncionario;

insert into tbFornecedor(Nome, Email) value('Experience Farms', 'expfarms@gmail.com');
insert into tbFornecedor(Nome, Email) value('Máquinas Maquinadas', 'mmltda@gmail.com');
insert into tbFornecedor(Nome, Email) value('Cresce Forte', 'cresceforte@gmail.com');

insert into tbTelefone(Numero, IdTipo, IdDDD) values(989896916, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 66));
insert into tbTelefone(Numero, IdTipo, IdDDD) values(989896915, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 65));
insert into tbTelefone(Numero, IdTipo, IdDDD) values(989796916, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 65));

insert into tbTelForn value(1,4);
insert into tbTelForn value(3,5), (3,6); 

update tbFornecedor set `Status` = false where Id = 3;


insert into tbUM value('UN', 'Unidade');
insert into tbUM value('DZ', 'Dúzia');

insert into tbEstoque(Qtd, UM, IdFornecedor) values(5, 'UN', 1);
insert into tbSemente(IdEstoque, Nome) values(1, "Semente de Soja");
update tbEstoque set Qtd = Qtd+2 where Id = 1;

insert into tbEstoque(Qtd, UM, IdFornecedor) values(7, 'UN', 3); 
insert into tbSemente(IdEstoque, Nome) values(2, "Semente de Milho");
update tbEstoque set Qtd = 4 where Id = 1;

insert into tbEstoque(Qtd, UM, IdFornecedor) values(1, 'UN', 3), (2, 'UN', 1), (5, 'UN', 3);
insert into tbInsumo(IdEstoque, Nome, Categoria) values(3, "CresceForte", 6), (4, "Pá", 2), (5, "Inseticida", 9);

insert into tbEstoque(Qtd,UM, IdFornecedor) values(5, 'UN', 2), (6, 'UN', 2);
insert into tbMaquina(IdEstoque, Nome, Tipo, Montadora) values(6,'Adubex', 4, 'AdubadoraX'), (7,'Semotors', 1, 'SemeadoraX');

update tbEstoque set Qtd = 0 where Id = 7;
delete from tbMaquina where IdEstoque = 7;
delete from tbEstoque where Id = 7;
select * from vwItems;

insert into tbSolo(Nome, Tipo) values('Arenoso', 1), ('Vermelho', 1);

insert into tbArea(Nome,  IdSolo) values('Area 1', 1), ('Area 2', 1), ('Area 3', 2), ('Area 4', 2), ('Area 5', 2);  

insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Soja', 1, '01/01/01', '01/01/01', 1);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 1, 1);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(0.75, 1, 3);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(1, 1, 6);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(1, 1, 1), (1, 2, 1);

insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Soja 2', 1, '19/02/01', '19/04/01', 1);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 2, 1);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(2, 3, 1);

insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Milho', 2, '19/02/01', '19/04/01', 2);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 3, 2);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(0.05, 3, 3);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(1, 3, 6);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(3, 4, 1), (3, 5, 1);


-- -				 (datacolehita, qtdperda, qtdtotal, unidade de medida, nome do produto, descricão do produto, idplantio, status colheita, idproduto, Descr)
call spInsertColheita('01/01/01', 1, 4, 'UN', 'Soja', null, 1, true, null, null);
call spInsertColheita('19/02/01', 1, 8, 'UN', 'Milho', 'Milho Transgênico', 3, true, null, 'Barro');
call spInsertColheita('19/02/01', 0, 4, 'UN', 'Soja', null, 2, false, 8, null);
select * from vwColheita; 

insert into tbControle(`Status`, Efic, NumLiberacoes, `Data`) values(true, 100, 2, '01/01/01'), (true, 50, 3, '01/01/01');

insert into tbPragaOrDoenca(Nome, `P/D`) values('Praga do Mal', true), ('Doença Nem Tão do Mal', false);
   
insert into tbControlePD values(1, 1), (2,2); 
   
insert into tbAreaPD values(true, 2, 1), (true, 3, 2);

insert into tbItensControle(QtdUsada, IdControle, IdEstoque) values(0.25, 1, 5), (0.25, 2, 5);
