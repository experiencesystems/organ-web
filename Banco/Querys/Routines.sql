use dbOrgan;
/*https://web.archive.org/web/20130509230922/http://dev.mysql.com/tech-resources/articles/mysql-storedprocedures.pdf*/
    
DELIMITER $ 
drop procedure if exists spInsertEstoque$
create procedure spInsertEstoque(
in
	Qnt double,
    UnM varchar(6)
)
begin
	if Qnt < 0 then
		signal sqlstate '45000'
		   set message_text = 'Valor menor que zero!';
	else
      
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
    `Desc1` varchar(100)
)
begin
	declare conta1 int;
    declare conta2 int;
    declare idE int;
    set conta1 = (select count(*) from tbEstoque); 
        
	call spInsertEstoque(Qnt, UnM);
    
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
    `Desc1` varchar(100)
)
begin
	declare conta1 int;
    declare conta2 int;
    declare idE int;
    set conta1 = (select count(*) from tbEstoque); 
    
	call spInsertEstoque(Qnt, UnM);
    
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
    Categoria1 int
)
begin
	declare conta1, conta2, idE int;
    set conta1 = (select count(*) from tbEstoque); 
       
	call spInsertEstoque(Qnt, UnM);

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
    `Desc1` varchar(100)
)
begin
    declare conta1, conta2, idE int;
        
    set conta1 = (select count(*) from tbEstoque); 
        
	call spInsertEstoque(Qnt, UnM);

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
    IdCol int    
)
begin
    declare qnt double;
    declare col int;
    
    set qnt = (QntTot - QntPerdas);
    if(isnull(IdCol)) then
		call spInsertProduto(qnt, UnM, Nome1, `Desc1`);
		
		set col = (select IdEstoque from tbProduto order by IdEstoque desc limit 1);
	else
		set col = (select IdEstoque from tbProduto where IdEstoque = IdCol);
    end if;
    
	insert into tbColheita(`Data`, QtdPerdas, QtdTotal, IdPlantio, IdProd, `Status`) value(Dataa, QntPerdas, QntTot, IdPlant, col, Stats);
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

use dbEcommerce;
DELIMITER $
drop function if exists spIsAn$
create function spIsAn(IdU nvarchar(128))
	returns boolean DETERMINISTIC
begin
	declare resp boolean;
	
	if(exists(select * from tbAnunciante where IdUsuario = IdU)) then
	 set resp = true;
	else
	 set resp = false;
	end if;
	return resp;
end$

DELIMITER $
drop function if exists spUsuario$
create function spUsuario(IdU nvarchar(128))
	returns varchar(50) DETERMINISTIC
begin
	declare nome varchar(50);
    
    if(spIsAn(IdU)) then
		set nome = (select NomeFazenda from tbAnunciante where IdUsuario = IdU);
	else
		set nome = (select `UserName` from tbUsuario where Id = IdU);
	end if;
    return nome;
end$

DELIMITER $
drop function if exists spLike$
create function spLike(IdA int)
	returns int DETERMINISTIC
begin
	declare likes int;
    
    if(exists(select * from tbAvaliacao where IdAnuncio = IdA)) then
		set likes = (select count(`Like`) from tbAvaliacao where `Like` = true and IdAnuncio = IdA);
	else
		set likes = 0;
	end if;
    return likes;
end$

DELIMITER $
drop event if exists VerDesc$
CREATE EVENT VerDesc
    ON SCHEDULE EVERY 3 HOUR -- e executa a cada três horas
				STARTS CURRENT_TIMESTAMP -- evento começa agora
    COMMENT 'Evento pra duração do desconto :D'
    DO begin 
		declare dtini, dtfim datetime;
        declare dia, ida, idm int;
--         declare done bool default false;
--         declare cur cursor for select DataDesc, DuracaoDesc from tbAnuncio; -- cursor ta setado pra cada id na tbanuncio onde desconto > 0
--         declare continue handler for not found set done = true; -- se o cursor não achar mais nada ele seta done pra true
--         
--         open cur; -- abre o cursor
		set ida = (select Id from tbAnuncio order by Id limit 1);
        set idm = (select max(id) from tbAnuncio);
        loopa : loop -- abre looping
-- 			fetch cur into dtini, dia; -- coloca o id dentro da variavale ida
            if (ida = idm) then -- diz quando fechar o looping, qnd done for true q só acontece quando ele não acha mais nada
				leave loopa; -- sai do loop
			end if;
            -- código que verifica se a data de desconto já foi
            
            set dtini = (select DataDesc from tbAnuncio where Id = ida);
            set dia = (select DuracaoDesc from tbAnuncio where Id = ida);
            set dtfim = date_add(dtini, interval dia day);
            
            if(dtfim <= now()) then
				update tbAnuncio set Desconto = 0, DuracaoDesc = 0, DataDesc = null where Id = ida;
            end if;
            
            set ida = ida + 1;
		end loop loopa; -- fim do loop
--         close cur; -- fim do cursor
	END$
DELIMITER ;
