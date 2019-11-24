use dbOrgan;
insert into tbTipoTel(Tipo) values("Fixo"), ("Celular");

insert into tbDDD(DDD) values(11), (12), (13), (14), (15), (16), (17), (18), (19), (21), (22), (24), (27), (28), (31), (32), (33), (34),
							 (35), (37), (38), (41), (42), (43), (44), (45), (46), (47), (48), (49), (51), (53), (54), (55), (61), (62),
							 (63), (64), (65), (66), (67), (68), (69), (71), (73), (74), (75), (77), (79), (81), (82), (83), (84), (85),
							 (86), (87), (88), (89), (91), (92), (93), (94), (95), (96), (97), (98), (99); 

insert into tbTelefone(Numero, IdTipo, IdDDD) values(989896912, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 11)),
													(989896913, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 11)),
													(89896912, (select Id from tbTipoTel where Tipo = "Fixo"), (select DDD from tbDDD where DDD = 64));


insert into tbFuncionario(Nome, Funcao, Email) value('Mirena', 1, 'milenamonteiro@gmail.com');

insert into tbTelFunc value(1,1);

insert into tbFornecedor(Nome, Email) value('Expereince Systems', 'moreexpsystems@gmail.com');

insert into tbTelForn value(1,2);

insert into tbUM value('a', 'A');

insert into tbEstoque(Qtd, UM, IdFornecedor) values(5, 'a', 1);
insert into tbSemente(IdEstoque, Nome) values(1, "Semente de Soja");

insert into tbEstoque(Qtd, UM, IdFornecedor) values(1, 'a', 1), (2, 'a', 1), (5, 'a', 1);
insert into tbInsumo(IdEstoque, Nome, Categoria) values(2, "CresceForte", 1), (3, "Pá", 2), (4, "MataBichoEPlanta", 3);

insert into tbEstoque(Qtd,UM, IdFornecedor) values(5, 'a', 1), (6, 'a', 1); -- Id 5 e 6
insert into tbMaquina(IdEstoque, Nome, Tipo, Montadora) values(5,'TratorX', 1, 'MaquinasBoas'), (6,'ColhedeiraY', 2, 'MaquinasRuins e Caras');
  
insert into tbPlantio(Nome, Sistema, DataColheita, DataInicio, TipoPlantio) values('Plantio de Soja', 1, '01/01/01', '01/01/01', 1);
  
insert into tbEstoque(Qtd, UM, IdFornecedor) values(3, 'a', 1);
insert into tbProduto(IdEstoque, Nome) value(7, 'Soja');

insert into tbColheita(`Data`, QtdPerdas, QtdTotal, IdPlantio, IdProd) values('01/01/01',  1, 4, 1, 7);

insert into tbItensPlantio values(1, 1, 1);

insert into tbSolo(Nome, Tipo) values('Arenoso', 1), ('Vermelho', 1);

insert into tbArea(Nome,  IdSolo) values('Area1', 1), ('Area2', 1), ('Area3', 2), ('Area4', 2), ('Area5', 2);  

insert into tbAreaPlantio values(1, 1, 1), (1, 2, 1);

insert into tbControle(`Status`, Efic, NumLiberacoes, `Data`) values(true, 100, 2, '01/01/01'), (true, 50, 3, '01/01/01');

insert into tbPragaOrDoenca(Nome, `P/D`) values('Praga do Mal', true), ('Doença Nem Tão do Mal', false);
   
insert into tbControlePD values(1, 1), (2,2); 
   
insert into tbAreaPD values(true, 2, 1), (true, 3, 2);

insert into tbItensControle values(0.25, 1, 4), (0.25, 2, 4);

select*from tbcolheita