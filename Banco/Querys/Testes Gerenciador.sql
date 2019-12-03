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
insert into tbUM value('SC', 'Saca - 1Kg');
insert into tbUM value('DZ', 'Dúzia');
insert into tbUM value('GL', 'Galão - 1L');

insert into tbEstoque(Qtd, UM, IdFornecedor) values(100, 'SC', 1);
insert into tbSemente(IdEstoque, Nome) values(1, "Semente de Soja");
update tbEstoque set Qtd = 5 where Id = 1;

insert into tbEstoque(Qtd, UM, IdFornecedor) values(50, 'SC', 1); 
insert into tbSemente(IdEstoque, Nome) values(2, "Semente de Milho");

update tbEstoque set Qtd = 4 where Id = 2;
insert into tbEstoque(Qtd, UM, IdFornecedor) values(20, 'SC', 1); 
insert into tbSemente(IdEstoque, Nome) values(3, "Semente de Abóbora");
update tbEstoque set Qtd = 4 where Id = 2;

insert into tbEstoque(Qtd, UM, IdFornecedor) values(27, 'SC', 1); 
insert into tbSemente(IdEstoque, Nome) values(4, "Semente de Melancia");
update tbEstoque set Qtd = 4 where Id = 2;

insert into tbEstoque(Qtd, UM, IdFornecedor) values(5, 'GL', 3), (2, 'UN', 1), (3, 'GL', 3);
insert into tbInsumo(IdEstoque, Nome, Categoria) values(5, "CresceForte", 6), (6, "Pá", 2), (7, "Inseticida", 9);

insert into tbEstoque(Qtd,UM, IdFornecedor) values(5, 'UN', 2), (6, 'UN', 2);
insert into tbMaquina(IdEstoque, Nome, Tipo, Montadora) values(8,'AdubeX300', 9, 'AdubadoraX'), (9,'SemeadoraX170', 1, 'Semotors');

update tbEstoque set Qtd = 0 where Id = 9;
delete from tbMaquina where IdEstoque = 9;
delete from tbEstoque where Id = 9;

insert into tbSolo(Nome, Tipo, IncVento, IncSolar) values('Arenoso', 1, 0.01, 0.50), ('Vermelho', 2, null, null), ('Bom sol', 3, 0.25, 0.90);

insert into tbArea(Nome,  IdSolo) values('Área 1', 3), ('Área 2', 1), ('Área 3', 2), ('Área 4', 3), ('Área 5', 2), ('Área 6', 1), ('Área 7', 3), ('Área 8', 2);

insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Soja', 1, str_to_date('02/11/2019', '%d/%m/%Y'), str_to_date('01/10/2019', '%d/%m/%Y'), 1);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 1, 1);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(1, 1, 6);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(1, 1, 1);

insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Melancia', 1, str_to_date('02/11/2019', '%d/%m/%Y'), str_to_date('01/10/2019', '%d/%m/%Y'), 1);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 1, 4);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(0.75, 1, 3);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(1, 1, 6);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(2, 1, 1);

insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Milho', 2, str_to_date('29/11/2019', '%d/%m/%Y'), str_to_date('04/12/2019', '%d/%m/%Y'), 2);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 3, 2);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(0.05, 3, 5);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(1, 3, 8);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(3, 4, 1), (3, 5, 1);

insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Abóbora ', 3, str_to_date('01/12/2019', '%d/%m/%Y'), str_to_date('05/12/2019', '%d/%m/%Y'), 2);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) values(1, 3, 3);
insert into tbItensPlantio(QtdUsada, IdPlantio, IdEstoque) value(1, 3, 8);
insert into tbAreaPlantio(IdPlantio, IdArea, Densidade) values(4, 4, 1), (4, 5, 1);

-- -				 (datacolehita, qtdperda, qtdtotal, unidade de medida, nome do produto, descricão do produto, idplantio, status colheita, idproduto, Descr)
call spInsertColheita('01/01/01', 1, 4, 'UN', 'Soja', null, 1, true, null, null);
call spInsertColheita('19/02/01', 1, 8, 'UN', 'Milho', 'Milho Transgênico', 3, true, null, 'Barro');
call spInsertColheita('19/02/01', 0, 4, 'UN', 'Soja', null, 2, false, 10, null);

insert into tbControle(`Status`, Efic, NumLiberacoes, `Data`) values(true, 100, 2, str_to_date('02/11/2019', '%d/%m/%Y')), (true, 50, 3, str_to_date('01/12/2019', '%d/%m/%Y'));

insert into tbPragaOrDoenca(Nome, `P/D`) values('Praga do Mal', true), ('Doença Nem Tão do Mal', false);
   
insert into tbControlePD values(1, 1), (2, 2);
   
insert into tbAreaPD values(true, 7, 1), (true, 7, 2);

insert into tbItensControle(QtdUsada, IdControle, IdEstoque) values(0.25, 1, 7), (0.35, 2, 7);

