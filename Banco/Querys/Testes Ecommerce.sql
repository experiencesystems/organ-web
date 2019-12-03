use dbEcommerce;

insert into tbEstado(Estado, UF) values("São Paulo", "SP"),
									("Acre", "AC"),
									("Alagoas", "AL"),
									("Amapá", "AP"),
									("Amazonas", "AM"),
									("Bahia", "BA"),
									("Ceará", "CE"),
									("Distrito Federal(Brasília)", "DF"),
									("Espiríto Santo", "ES"),
									("Goiás", "GO"),
									("Maranhão", "MA"),
									("Mato Grosso", "MT"),
									("Mato Grosso do Sul", "MS"),
									("Minas Gerais", "MG"),
									("Pará", "PA"),
									("Paraíba", "PB"),
									("Paraná", "PR"),
									("Pernambuco", "PE"),
									("Piauí", "PI"),
									("Rio Grande do Norte", "RN"),
									("Rio Grande do Sul", "RS"),
									("Rio de Janeiro", "RJ"),
									("Rondônia", "RO"),
									("Roraima", "RR"),
									("Santa Catarina", "SC"),
									("Sergipe", "SE"),
									("Tocantins", "TO");

insert into tbCidade(Cidade, IdEstado) values("Cuiabá", 12);
insert into tbBairro(Bairro, IdCidade) values("Baú", 1);
insert into tbEndereco(CEP) values("78008105"),
								  ("78008110");
										 
insert into tbLogradouro(Logradouro, IdBairro, CEP) values("Avenida Bosque da Saúde - até 161/162", 1, "78008105"),
														  ("Avenida Historiador Rubens de Mendonça - até 1250 - lado par", 1, "78008110");
                                         
insert into tbUsuario(`Id`, `Email`, `ConfirmacaoEmail`, `SenhaHash`, `CarimboSeguranca`, `UserName`, Foto, CPF, Assinatura)
values('02719894-e4a9-46c8-999e-ba942abd5f8f', 'jessica@gmail.com', 0, 
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9fd6-39f7c6a99a52', 'Jéssica',  load_file('C:\Jessica.png'), 11111111111, 4),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8g', 'expfarms@gmail.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Experience Farms', load_file('C:\farm.png'), 11111111112, 1),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8h', 'fazendinha@a.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9850-5f0fb0024c9a', 'Fazendinha Feliz', load_file('C:\farm.png'), 11111111113, 3),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8i', 'fazenda@a.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9850-5f0fb0024c9a', 'Fazenda da Serra', load_file('C:\farm.png'), 11111111114, 3);
       
insert into `AspNetRoles` (`Id`, `Name`) value ('02719894-e4a9-46c8-999e-ba942abd5f8u', 'Admin');

insert into `AspNetUserRoles` (`UserId`, `RoleId`) values ('02719894-e4a9-46c8-999e-ba942abd5f8g', '02719894-e4a9-46c8-999e-ba942abd5f8u'),
														  ('02719894-e4a9-46c8-999e-ba942abd5f8h', '02719894-e4a9-46c8-999e-ba942abd5f8u'),
														  ('02719894-e4a9-46c8-999e-ba942abd5f8i', '02719894-e4a9-46c8-999e-ba942abd5f8u');

insert into tbDadosBancarios(NomeTitular, CVV, Banco, NumCartao, Validade, IdUsuario) values("João Meu Pai", 1111, 1, 11111111111111111, '01/01/01', '02719894-e4a9-46c8-999e-ba942abd5f8f');

       
insert into tbAnunciante(IdUsuario, NomeFazenda, NumEnd, CEP, NomeBanco) values('02719894-e4a9-46c8-999e-ba942abd5f8g', 'Experience Farms', 1, "78008105", 'dbOrgan'),
																			   ('02719894-e4a9-46c8-999e-ba942abd5f8h', 'Fazenda Triste', 2, "78008110", 'dbOrgan 1'),
																			   ('02719894-e4a9-46c8-999e-ba942abd5f8i', 'Fazenda Triste', 2, "78008110", 'dbOrgan 2');
select * from vwDadosBancarios;
select * from vwAnunciante;

insert into tbUM value('SC', 'Saca(s) - 1Kg');
insert into tbUM value('UN', 'Unidade(s)');
insert into tbUM value('DZ', 'Dúzia(s)');
insert into tbUM value('CX', 'Caixa(s)');
insert into tbProduto(ValorUnit, UM, Nome, Categoria) values(10.00, 'SC', 'Semente de Soja', 10), 
															(15.00, 'SC', 'Semente de Milho', 10), 
															(15.00, 'SC', 'Soja', 3), 
															(4.00, 'UN', 'Melância', 7), 
															(17.50, 'DZ', 'Rabanete', 5), 
															(3000.00, 'UN', 'Adubadora', 12), 
															(5000.00, 'UN', 'Adubadora500x', 12), 
															(700.00, 'UN', 'Colheitadeira', 15), 
															(28.90, 'SC', 'Cenoura', 6), 
															(14.00, 'DZ', 'Milho', 9), 
															(25.00, 'UN', 'Pá', 16), 
															(27.50, 'SC', 'Rabanete', 5),
															(27.50, 'SC', 'Semente de Soja Orgânica', 5); 
                                                                        
insert into tbAnuncio(Nome, `Desc`, `Status`, Foto, IdProduto, IdAnunciante, Quantidade, Desconto, DuracaoDesc, DataDesc)
	values('Sacas de Soja', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'),3, '02719894-e4a9-46c8-999e-ba942abd5f8g', 3, null, null, null),
		  ('Sacas de Soja 1', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 3, '02719894-e4a9-46c8-999e-ba942abd5f8h', 5, 10, 1, NOW()),
		  ('Sacas de Soja 2', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 3, '02719894-e4a9-46c8-999e-ba942abd5f8i', 2, 50, 4, NOW()),
		  ('Sacas de Soja 3', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 3, '02719894-e4a9-46c8-999e-ba942abd5f8g', 4, null, null, null),
		  ('Milhão Bão', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8h', 5, null, null, null),
		  ('Pá da Boa', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 11, '02719894-e4a9-46c8-999e-ba942abd5f8i', 6, null, null, null),
          ('Semente de Milho', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 2, '02719894-e4a9-46c8-999e-ba942abd5f8g', 2, null, null, null),
          ('Adubadoras Incriveís', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 6, '02719894-e4a9-46c8-999e-ba942abd5f8h', 2, 5, 15, NOW()),
          ('12 Sacas Rabanetes Frescos', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 12, '02719894-e4a9-46c8-999e-ba942abd5f8i', 12, 10, 3, NOW()),
          ('2 Dúzias de Milho', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 10, '02719894-e4a9-46c8-999e-ba942abd5f8g', 2, null, null, null),
          ('Sementes de Soja Transgênica', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 1, '02719894-e4a9-46c8-999e-ba942abd5f8h', 2, null, null, null),
          ('Sementes de Soja Orgânica', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 13, '02719894-e4a9-46c8-999e-ba942abd5f8i', 2, 15, 5, NOW()),
          ('Cenouras Da Estação', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8g', 12, null, null, null),
          ('SACÃO DE CENOURA', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8h', 21, 30, 5, NOW()),
          ('SACÃO DE CENOURA 2', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8i', 11, 15, 4, NOW()),
          ('SACÃO DE CENOURA 3', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8g', 23, null, null, null),
          ('SACÃO DE CENOURA 4', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8h', 15, 10, 7, NOW()),
          ('Adubadora5000X', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 7, '02719894-e4a9-46c8-999e-ba942abd5f8i', 1, 25, 5, NOW()),
          ('Colheitadeira 2001 Semi-Nova', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 8, '02719894-e4a9-46c8-999e-ba942abd5f8g', 1, null, null, null),
          ('Milho Transgênico', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 10, '02719894-e4a9-46c8-999e-ba942abd5f8h', 7, 25, 7, NOW()),
          ('Melancias Orgânicas', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 4, '02719894-e4a9-46c8-999e-ba942abd5f8i', 8, null, null, null),
          ('Melancias Orgânicas 1', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 4, '02719894-e4a9-46c8-999e-ba942abd5f8g', 17, 40, 7, NOW()),
          ('Melancias Orgânicas 2', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 4, '02719894-e4a9-46c8-999e-ba942abd5f8h', 9, null, null, null),
          ('Melancias Orgânicas 3', 'Se puder, compre!', true, load_file('C:\farm.jpg'), 4, '02719894-e4a9-46c8-999e-ba942abd5f8i', 8, null, null, null),
          ('2  Cenoura pelo preço de 1', 'Compra isso, é bom!', true, load_file('C:\farm.jpg'), 9, '02719894-e4a9-46c8-999e-ba942abd5f8g', 24, 50, 15, NOW()),
          ('Black Friday do Milharal', 'Compra isso, é melhor!', true, load_file('C:\farm.jpg'), 2, '02719894-e4a9-46c8-999e-ba942abd5f8h', 4, 70, 1, NOW());

insert into tbAnuncio(Nome, `Desc`, `Status`, Foto, IdProduto, IdAnunciante, Quantidade, Desconto, DuracaoDesc, DataDesc)
	values('Soja', 'É assim!', true, load_file("C:\farm.jpg"), 3, '02719894-e4a9-46c8-999e-ba942abd5f8g', 2, 10, 1, '19/11/27 17:00');          
          
insert into tbWishList value('02719894-e4a9-46c8-999e-ba942abd5f8f', 4);

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Espero poder comprar essas pás', 6, '02719894-e4a9-46c8-999e-ba942abd5f8f');

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Também!', 6, '02719894-e4a9-46c8-999e-ba942abd5f8h');

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Então compra!', 6, '02719894-e4a9-46c8-999e-ba942abd5f8f');

select * from vwComentario;

insert into tbCarrinho(IdUsuario, IdAnuncio, Qtd) values('02719894-e4a9-46c8-999e-ba942abd5f8f', 1, 1),
														('02719894-e4a9-46c8-999e-ba942abd5f8f', 2, 2),
														('02719894-e4a9-46c8-999e-ba942abd5f8f', 3, 1);

select * from vwCarrinho;

insert into tbPagamento(QtdParcelas, VlParcela, Tipo) value(2, 1.00, 1);     
                        
insert into tbPedido (IdUsuario, ValFrete, CEPEntrega, NumEntrega, IdPagamento) values('02719894-e4a9-46c8-999e-ba942abd5f8f', 3.00, "78008110", 1, 1),
																					  ('02719894-e4a9-46c8-999e-ba942abd5f8f', 3.25, "78008110", 7, 1);
                                                                                                                                                                  
insert into tbPedidoAnuncio(IdPedido, IdAnuncio, Qtd) values(1, 1, 1);
insert into tbPedidoAnuncio(IdPedido, IdAnuncio, Qtd) values(1, 2, 2);
insert into tbPedidoAnuncio(IdPedido, IdAnuncio, Qtd) values(2, 3, 1);

select * from vwPedido;
select * from vwCarrinho; 
                                  
update tbPedido set `Status` = 2 where Id = 1;
select * from vwPedido;
select * from vwVenda;

insert into tbAvaliacao value(1, '02719894-e4a9-46c8-999e-ba942abd5f8f', 4);

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Adorei as Sojas, super veganaaaas!', 1, '02719894-e4a9-46c8-999e-ba942abd5f8f');
select * from vwAnuncio;
select * from vwComentario;

