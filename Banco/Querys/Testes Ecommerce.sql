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
								  ("78008000");
										 
insert into tbLogradouro(Logradouro, IdBairro, CEP) values("Avenida Bosque da Saúde - até 161/162", 1, "78008105"),
														  ("Avenida Historiador Rubens de Mendonça - até 1250 - lado par", 1, "78008000");
                                         
insert into tbUsuario(`Id`, `Email`, `ConfirmacaoEmail`, `SenhaHash`, `CarimboSeguranca`, `UserName`, Foto, CPF, Assinatura)
values('02719894-e4a9-46c8-999e-ba942abd5f8f', 'milenamonteiro@gmail.com', 0, 
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9fd6-39f7c6a99a52', 'Mirena',  LOAD_FILE("/error.gif"), 11111111111, 4),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8g', 'moreexpsystems@gmail.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Empresinha', LOAD_FILE("/error.gif"), 11111111112, 1),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8h', 'anuncio@a.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9850-5f0fb0024c9a', 'Fazendinha', LOAD_FILE("/error.gif"), 11111111113, 1);

insert into tbDadosBancarios(NomeTitular, CVV, Banco, NumCartao, Validade, IdUsuario) values("João Meu Pai", 1111, 1, 11111111111111111, '01/01/01', '02719894-e4a9-46c8-999e-ba942abd5f8f');

       
insert into tbAnunciante(IdUsuario, NomeFazenda, NumEnd, CEP) values('02719894-e4a9-46c8-999e-ba942abd5f8g', 'Experience Farms', 1, "78008105"),
																	('02719894-e4a9-46c8-999e-ba942abd5f8h', 'Fazenda Triste', 2, "78008000");

insert into tbUM value('a', 'A');
insert into tbProduto(ValorUnit, UM, Nome, Categoria) values(5.25, 'a', 'Semente de Soja', 1),
															(5.75, 'a', 'Semente de Milho', 1),
															(25.00, 'a', 'Pá', 2);
                                                                        
insert into tbAnuncio(Nome, `Desc`, `Status`, Foto, IdProduto, IdAnunciante, Quantidade)
	values('Soja da Boa', 'É UMAS SOJA NÃO TRANGÊNICA!', true, load_file("/error.gif"), 1, '02719894-e4a9-46c8-999e-ba942abd5f8g', 2),
		  ('Milhão Bão', 'ESSE MILHO É TRANSGêNICO, MAS IDAÍ, COMPRA!', true, load_file("/error.gif"), 1, '02719894-e4a9-46c8-999e-ba942abd5f8g', 1),
		  ('Pá da Boa', 'PENSA NUMA PÁ BOA, É ESSA, COMPRA!', true, load_file("/error.gif"), 2, '02719894-e4a9-46c8-999e-ba942abd5f8h', 1),
          ('Pá da Boa', 'PENSA DUAS PÁ BOA, SÃO ESSAS, COMPRA!', true, load_file("/error.gif"), 2, '02719894-e4a9-46c8-999e-ba942abd5f8h', 2);
          
insert into tbWishList value('02719894-e4a9-46c8-999e-ba942abd5f8f', 4);

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Espero poder comprar essas pás', 4, '02719894-e4a9-46c8-999e-ba942abd5f8f');

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Também!', 4, '02719894-e4a9-46c8-999e-ba942abd5f8h');

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Então compra!', 1, '02719894-e4a9-46c8-999e-ba942abd5f8f');

insert into tbCarrinho values('02719894-e4a9-46c8-999e-ba942abd5f8f', 1, 1),
							 ('02719894-e4a9-46c8-999e-ba942abd5f8f', 2, 2),
							 ('02719894-e4a9-46c8-999e-ba942abd5f8f', 3, 1);


insert into tbPagamento(QtdParcelas, VlParcela, Tipo) value(2, 11.00, 1);     
                        
insert into tbPedido (IdUsuario, ValFrete, CEPEntrega, NumEntrega, IdPagamento) values('02719894-e4a9-46c8-999e-ba942abd5f8f', 3.00, "78008000", 1, 1),
																					  ('02719894-e4a9-46c8-999e-ba942abd5f8f', 3.25, "78008000", 7, 1);
                                                                                                                                                                  

insert into tbPedidoAnuncio(IdPedido, IdAnuncio, Qtd) values(1, 1, 1), (1, 2, 2), (2, 3, 1);
                                  
update tbPedido set `Status` = 1 where Id = 1;

insert into tbVenda(IdPedido) value(1);

insert into tbAvaliacao value(1, '02719894-e4a9-46c8-999e-ba942abd5f8f', true, 4);

insert into tbComentario(Comentario, IdAnuncio, IdUsuario) values('Adorei a Soja, super veganaaaa!', 1, '02719894-e4a9-46c8-999e-ba942abd5f8f');


       

