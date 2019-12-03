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
drop function if exists isComprador$
create function isComprador(IdUsu nvarchar(128), IdAn int)
	returns char(3) DETERMINISTIC
begin
	declare resp char(3);
    declare IdP int;

    if(exists(select * from tbPedido where IdUsuario = IdUsu)) then
		set IdP = (select p.Id from tbPedido p
					inner join tbPedidoAnuncio pa on pa.IdPedido = p.Id
				   where p.IdUsuario = IdUsu and pa.IdAnuncio = IdAn);
		if(exists(select * from tbVenda where IdPedido = IdP)) then
			set resp = 'Sim';
		else
			set resp = 'Não';
		end if;
	else
		set resp = 'Não';
    end if;
    return resp;
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
