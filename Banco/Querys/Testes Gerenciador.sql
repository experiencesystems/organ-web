use dbOrgan;
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
select * from vwFornecedor;

insert into tbUM value('a', 'A');

insert into tbEstoque(Qtd, UM, IdFornecedor) values(5, 'a', 1);
insert into tbSemente(IdEstoque, Nome) values(1, "Semente de Soja");
update tbEstoque set Qtd = Qtd+2 where Id = 1;

insert into tbEstoque(Qtd, UM, IdFornecedor) values(7, 'a', 3); 
insert into tbSemente(IdEstoque, Nome) values(2, "Semente de Milho");
update tbEstoque set Qtd = 4 where Id = 1;

insert into tbEstoque(Qtd, UM, IdFornecedor) values(1, 'a', 3), (2, 'a', 1), (5, 'a', 3);
insert into tbInsumo(IdEstoque, Nome, Categoria) values(3, "CresceForte", 6), (4, "Pá", 2), (5, "Inseticida", 9);

insert into tbEstoque(Qtd,UM, IdFornecedor) values(5, 'a', 2), (6, 'a', 2);
insert into tbMaquina(IdEstoque, Nome, Tipo, Montadora) values(6,'Adubex', 4, 'AdubadoraX'), (7,'Semotors', 1, 'SemeadoraX');

update tbEstoque set Qtd = 0 where Id = 7;
delete from tbMaquina where IdEstoque = 7;
delete from tbEstoque where Id = 7;

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
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 3, 1);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(0.05, 3, 3);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(1, 3, 6);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(3, 4, 1), (3, 5, 1);

select * from vwPlantio; use dborgan;

-- -				 (datacolehita, qtdperda, qtdtotal, unidade de medida, nome do produo, descrica do produto, idplantio, status colheita, idcolheita)
call spInsertColheita('01/01/01', 1, 4, 'a', 'Soja', null, 1, true, null);
call spInsertColheita('19/02/01', 1, 8, 'a', 'Milho', 'Milho Transgênico', 3, true, null);
call spInsertColheita('19/02/01', 0, 4, 'a', 'Soja', null, 2, false, 9);
select * from vwColheita; 

insert into tbControle(`Status`, Efic, NumLiberacoes, `Data`) values(true, 100, 2, '01/01/01'), (true, 50, 3, '01/01/01');

insert into tbPragaOrDoenca(Nome, `P/D`) values('Praga do Mal', true), ('Doença Nem Tão do Mal', false);
   
insert into tbControlePD values(1, 1), (2,2); 
   
insert into tbAreaPD values(true, 2, 1), (true, 3, 2);

insert into tbItensControle(QtdUsada, IdControle, IdEstoque) values(0.25, 1, 5), (0.25, 2, 5);
select * from vwArea;select * from vwpragaordoenca; select * from vwcontrole;
select * from vwItems; select * from vwHistorico; 
