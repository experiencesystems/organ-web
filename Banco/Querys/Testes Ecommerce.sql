use dbEcommerce;
insert into tbDadosBancarios(NomeTitular, CVV, Banco, NumCartao, Validade, IdUsuario) values("João Meu Pai", 1111, 1, 11111111111111111, '01/01/01', '02719894-e4a9-46c8-999e-ba942abd5f8f');

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

insert into tbCidade(Cidade, IdEstado) values("Osasco", 1);
insert into tbBairro(Bairro, IdCidade) values("Vila Yara (Real)", 1);
insert into tbLogradouro(Logradouro, IdBairro) values("Rua das Flores", 1),
													 ("Rua das Árvores", 1);
insert into tbEndereco(CEP, IdRua) values("00000000", 1),
										 ("11111111", 2);
                                         
insert into tbUsuario(`Id`, `Email`, `ConfirmacaoEmail`, `SenhaHash`, `CarimboSeguranca`, `UserName`, Foto, CPF, Assinatura)
values('02719894-e4a9-46c8-999e-ba942abd5f8f', 'milenamonteiro@gmail.com', 0, 
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   '1a38cc85-3bd4-400b-9fd6-39f7c6a99a52', 'Mirena',  LOAD_FILE("/error.gif"), 11111111111, 0),
       
	   ('02719894-e4a9-46c8-999e-ba942abd5f8g', 'moreexpsystems@gmail.com', 0,
	   'AKM33xpM5jcwZ/ojFJuuWBOvPQOiROAQmhfZwupekFSTAGpmW5+O7iPmj7cUuM/r6w==',
	   'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Empresinha', LOAD_FILE("/error.gif"), 11111111112, 1);
