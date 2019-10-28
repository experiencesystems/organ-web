use dbOrgan;

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
create procedure spInsertInsumo(
in 
	Qnt double,
    UnM int,
    ValUnit double,
    Nome varchar(50),
    `Desc` varchar(300),
    IdCategoria int
)
begin
	declare conta1, conta2, idE int;
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
/*
    call spInsertSemente(2, 1, 1.50, 'Semente de Milho', null, null, null, null)$$
    call spInsertProduto(1, 1, 5.0, 'Milho', null)$$
    call spInsertInsumo(1, 3, 2.0, 'Pá', null, 2)$$
    call spInsertMaquina(1, 3, 2500, 'Tratorzinho', 1, 'Joana Motors', null, 2, 2300, 20, 240)$$*/
DELIMITER ;
