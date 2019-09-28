use sys;
drop database if exists dbOrgan;
create database dbOrgan;
use dbOrgan;

-- =================================================================== USUÁRIO ============================================     
   create table if not exists `AspNetUsers` (
		`Id` nvarchar(128)  not null ,
			DataCadastro datetime default current_timestamp(),
			Confirmacao bool not null,
			Ativacao bool not null default true,
			Assinatura bool not null,
            IdPessoa int not null,
             constraint UQUsuarioIdPessoa unique(IdPessoa),
		`Email` varchar(100) ,-- !
		`EmailConfirmed` bool not null ,
		`PasswordHash` longtext,
		`SecurityStamp` longtext,
		`UserName` varchar(50)  not null ,-- !
	      constraint PKAspNetUsers primary key ( `Id`)
	);
        
	alter table `AspNetUsers` add constraint FKAspeNetUsersPessoa foreign key(IdPessoa) references tbPessoa(Id);        
	/*Id: 02719894-e4a9-46c8-999e-ba942abd5f8f
Confirmacao: 0
Ativacao: 1
Assinatura: 0
CLI/FUNC: 0
Email: milenamonteiro@gmail.com
EmailConfirmed: 0
PasswordHash: ABecbdkGhzyTR1/t+F8FpUnN+AHXhiXYu4qPCVc4SroxOyzj3p0R+TnWK0p1o6q3Rw==
SecurityStamp: e7aac8f8-7c92-44fb-9850-5f0fb0024c9a
UserName: Milena*/
-- =======================================================================================================================

-- =================================================================== ENDEREÇO ==========================================
	create table if not exists tbEndereco(
		CEP char(8),
		 constraint PKLocalizacao primary key (CEP),
		IdRua int not null
	);
    
    create table if not exists tbLogradouro(
		Id int auto_increment,
		 constraint PKRua primary key (Id),
		Logradouro varchar(50) not null,
		IdBairro int not null
	);
    
	create table if not exists tbBairro(
		Id int auto_increment,
		 constraint PKBairro primary key (Id),
		Bairro varchar(30) not null,
		IdCidade int not null
	);
    
    create table if not exists tbCidade(
		Id int auto_increment,
		 constraint PKCidade primary key (Id),
		Cidade varchar(30) not null,
		IdEstado tinyint not null
	);
    
    create table if not exists tbEstado(
		Id tinyint auto_increment,
		 constraint PKEstado primary key (Id),
		Estado varchar(30) not null,
		UF char(2) not null
	);
    
	alter table tbCidade add constraint FKCidadeEstado foreign key(IdEstado) references tbEstado(Id);
	alter table tbBairro add constraint FKBairroCidade foreign key(IdCidade) references tbCidade(Id);
	alter table tbLogradouro add constraint FKRuaBairro foreign key(IdBairro) references tbBairro(Id);
	alter table tbEndereco add constraint FKEnderecoRua foreign key(IdRua) references tbLogradouro(Id);
    
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
-- =======================================================================================================================

-- =================================================================== TELEFONE ========================================== 
	create table if not exists tbTelefone(
		Id int auto_increment,
		 constraint PKTelefone primary key(Id),
		Numero numeric(9),
		IdTipo int not null,
		IdDDD int not null
	);
    
    create table if not exists tbTipoTel(
		Id int auto_increment,
         constraint PKTipoTel primary key(Id),
		Tipo varchar(20) not null
    );
    
    insert into tbTipoTel(Tipo) values("Fixo"),
									  ("Celular");
        
    create table if not exists tbDDD(
		DDD numeric(2) not null,
         constraint PKDDD primary key(DDD)
    );
    
    insert into tbDDD(DDD) values(11), (12), (13), (14), (15), (16), (17), (18), (19), (21), (22), (24), (27), (28), (31), (32), (33), (34),
								 (35), (37), (38), (41), (42), (43), (44), (45), (46), (47), (48), (49), (51), (53), (54), (55), (61), (62),
                                 (63), (64), (65), (66), (67), (68), (69), (71), (73), (74), (75), (77), (79), (81), (82), (83), (84), (85),
                                 (86), (87), (88), (89), (91), (92), (93), (94), (95), (96), (97), (98), (99); 
    
    alter table tbTelefone add constraint FKTelefoneTipo foreign key(IdTipo) references tbTipoTel(Id),
						   add constraint FKTelefoneDDD foreign key(IdDDD) references tbDDD(Id);
                           
	insert into tbTelefone(Numero, IdTipo, IdDDD) values(989896912, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 11)),
														(989896913, (select Id from tbTipoTel where Tipo = "Celular"), (select DDD from tbDDD where DDD = 11)),
                                                        (89896912, (select Id from tbTipoTel where Tipo = "Fixo"), (select DDD from tbDDD where DDD = 64));
-- =======================================================================================================================

-- =================================================================== PESSOA ============================================   
    create table if not exists tbPessoa(
		Id int auto_increment,
         constraint PKPessoa primary key (Id),
		Nome varchar(100) not null,
        Email varchar(100) not null,
        NumeroEndereco int not null,
        CompEndereco varchar(30),
        CEP char(8) not null
    );
    alter table tbPessoa add constraint FKPessoaEndereco foreign key(CEP) references tbEndereco(CEP);
    
    select * from tbEndereco;
    insert into tbPessoa (Nome, Email, NumeroEndereco, CompEndereco, CEP) values("Mileninha GamePlays", 'milenamonteiro@gmail.com', 12, "AP. 24 Bloco B", 00000000),
																				("Systems Experience", 'moreexpsystems@gmail.com', 13, null, 11111111);
    
    insert into `AspNetUsers` (Id, Confirmacao, Assinatura, Email, EmailConfirmed,
    PasswordHash, SecurityStamp, UserName, IdPessoa) values('02719894-e4a9-46c8-999e-ba942abd5f8f', 0, 0,  'milenamonteiro@gmail.com', 0,
												 'ABecbdkGhzyTR1/t+F8FpUnN+AHXhiXYu4qPCVc4SroxOyzj3p0R+TnWK0p1o6q3Rw==',
                                                 'e7aac8f8-7c92-44fb-9850-5f0fb0024c9a', 'Mirena', 1),
                                                 ('02719894-e4a9-46c8-999e-ba942abd5f8g', 0, 0, 'moreexpsystems@gmail.com', 0,
												 'ABecbdkGhzyTR1/t+F8FpUnN+AHXhiXYu4qPCVc4SroxOyzj3p0R+TnWK0p1o6q3Rw=+',
                                                 'e7aac8f8-7c92-44fb-9850-5f0fb0024c9b', 'Empresinha', 2);
    
    create table if not exists tbTelefonePessoa(
		IdPessoa int,
        IdTelefone int,
         constraint PKTelPessoa primary key (IdPessoa, IdTelefone)
    );
	alter table tbTelefonePessoa add constraint FKTelPessoa foreign key(IdTelefone) references tbTelefone(Id),
								 add constraint FKPessoaTel foreign key(IdPessoa) references tbPessoa(Id);
    
    insert into tbTelefonePessoa values(1, 1),
									   (2, 2),
                                       (1, 3),
                                       (2, 3);
    
    create table if not exists tbDadosBancarios(
		Id int auto_increment,
         constraint PKDadosBancarios primary key(Id),
		CVV numeric(4) not null,
        Banco int, -- Listinha dos Banco s2 s2
        NumCartao numeric(19) not null,
        Validade date not null, 
        IdPessoa int not null,
         constraint UQDadosBancariosIdPessoa unique(IdPessoa)
	);
    alter table tbDadosBancarios add constraint FKDBPessoa foreign key(IdPessoa) references tbPessoa(Id);
	
    insert into tbDadosBancarios(CVV, Banco, NumCartao, Validade, IdPessoa) values(1111, 1, 11111111111111111, "01/01/01", 1);
    
    create table if not exists tbPessoaFisica(
		IdPessoa int not null,
         constraint PKFisica primary key(IdPessoa),
		CPF numeric(11) not null,
         constraint UQCPF unique(CPF),
        RG char(9) not null,
        DataNasc date not null,
        Foto varchar(100) not null
    );
    alter table tbPessoaFisica add constraint FKPessoaFisica foreign key(IdPessoa) references tbPessoa(Id);
   
   insert into tbPessoaFisica (IdPessoa, CPF, RG, DataNasc, Foto) values(1, 12345678910, '123456789', "01/01/01", "NoPhotosPlox");
   
    create table if not exists tbPessoaJuridica(
		IdPessoa int,
         constraint PKJuridica primary key(IdPessoa),
		RazaoSocial varchar(100) not null,
        CNPJ numeric(14) not null,
         constraint UQCNPJ unique(CNPJ),
		IE numeric(12) not null
    );
    alter table tbPessoaJuridica add constraint FKPessoaJuridica foreign key(IdPessoa) references tbPessoa(Id);
	
    insert into tbPessoaJuridica(IdPessoa, RazaoSocial, CNPJ, IE) values(2, 'Exp Sisteminhas Muito Bons', 12345678910112, 123456789101);
    
-- =======================================================================================================================   
  
-- =================================================================== Estoque ============================================  
	create table if not exists tbEstoque(
		Id int auto_increment,
         constraint PKEstoque primary key(Id),
		Qtd double not null default 0.00,
        UM int not null,
        ValorUnit double not null default 0.00
    );
    
    create table if not exists tbHistEstoque(
		Id int auto_increment,
         constraint PKHistEstoque primary key(Id),
		QtdAlterada double not null,
        QtdAntiga double not null default 0.00,
        DataAlteracao datetime not null default current_timestamp,
        `Desc` varchar(300) not null,
        IdEstoque int not null
    );
    alter table tbHistEstoque add constraint FKHistEstoque foreign key(IdEstoque) references tbEstoque(Id);
     
                      -- ------------------------------- Semente ------------------------------------
    create table if not exists tbSemente(
		IdEstoque int not null,
         constraint PKSemente primary key(IdEstoque),
		Nome varchar(50) not null,
        Solo varchar(50) not null default "Não Registrado",
        IncSol decimal(5,2) not null default 0.00,
        IncVento decimal(5,2) not null default 0.00,
        Acidez decimal(5,2) not null default 0.00        
    );
    alter table tbSemente add constraint FKSementeEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    insert into tbEstoque(Qtd, UM, ValorUnit) values(5, 1, 2.50);
    insert into tbSemente(IdEstoque, Nome) values(1, "Semente de Soja");
    
    create table tbSolo(
		Id int auto_increment,
		 constraint PKSolo primary key(Id),
		Nome varchar(50) not null,
        Tipo int not null,
        IncSolar decimal(5,2) not null default 0.00,
        IncVento decimal(5,2) not null default 0.00,
        Acidez decimal(5,2) not null default 0.00
    );
    
                         -- ------------------------------- Insumo ------------------------------------ 
    create table if not exists tbInsumo(		
        IdEstoque int not null,
         constraint PKInsumo primary key(IdEstoque),
		Nome varchar(50) not null,
        `Desc` varchar(300),
        IdCategoria int not null
    );
    alter table tbInsumo add constraint FKInsumoEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    create table if not exists tbCategoria(
		Id int auto_increment,
         constraint PKCategoria primary key(Id),
		Categoria varchar(30) not null
    );
    alter table tbInsumo add constraint FKInsumoCategoria foreign key(IdCategoria) references tbCategoriaInsumo(Id);
    
    
    insert into tbEstoque(Qtd, UM, ValorUnit) values(1, 2, 0.50), -- UM 2- L, 1 - Kg, 3 - Unidade
													(2, 3, 23.50),
                                                    (5, 2, 1.50);
    insert into tbCategoria(Categoria) values("Fertilizante"),
											 ("Ferramenta"),
                                             ("Pesticida");
	insert into tbInsumo(IdEstoque, Nome, IdCategoria) values(2, "CresceForte", 1),
															 (3, "Pá", 2),
                                                             (4, "MataBichoEPlanta", 3);
                          -- ------------------------------- Maquina ------------------------------------ 
    create table if not exists tbMaquina(
		IdEstoque int not null,
         constraint PKMaquina primary key(IdEstoque),
		Montadora varchar(75),
        `Desc` varchar(300),
        VidaUtil int,
        ValorInicial double not null,
        DeprMes double,
        DeprAno double,
        DataCadastro datetime not null default current_timestamp
    );
    alter table tbMaquina add constraint FKMaquinaEstoque foreign key(IdEstoque) references tbEstoque(Id);
    
    insert into tbEstoque(Qtd, UM, ValorUnit) values(2, 3, 5000.00),
													(1, 3, 10000.00); -- Id 5 e 6
	insert into tbMaquina(IdEstoque, Montadora, VidaUtil, ValorInicial, DeprMes, DeprAno) values(5, 'MaquinasBoas', 5, 7000.00, 10.00, 120.00),
																								(6, 'MaquinasRuins e Caras', 1, 20000.00, 500.00, 6000.00);
 
	create table if not exists tbManutencao(
		Id int auto_increment,
		 constraint PKManutencao primary key(Id),
		Nome varchar(30),
        Detalhes varchar(300),
        `Data` date not null,
        ValorPago double not null
    );
	
    insert into tbManutencao(Nome, `Data`, ValorPago) value("Revisão Anual", '01/01/01', 5000.00);
    
    create table if not exists tbMaquinaManutencao(
		IdMaquina int not null,
        IdManutencao int not null,
		 constraint PKMaquinaManutencao primary key(IdMaquina, IdManutencao)
    );
    alter table tbMaquinaManutencao add constraint FKMaquinaManutencao foreign key(IdMaquina) references tbMaquina(IdEstoque),
									add constraint FKManutencaoMaquina foreign key(IdManutencao) references tbManutencao(Id);
    
    insert into tbMaquinaManutencao value(2,1);    
-- ======================================================================================================================= 
  
-- ========================================================== COMPRA ==============================================  
	create table if not exists tbFornecedor(
		Id int auto_increment,
         constraint PKFornecedor primary key(Id),
		`Status` bool not null default true,
        IdPessoa int not null
    );
    alter table tbFornecedor add constraint FKFornecedorPessoa foreign key(IdPessoa) references tbPessoa(Id);
    
    insert into tbFornecedor(IdPessoa) value(2);
    
    create table if not exists tbPagamento(
		Id int auto_increment,
         constraint PKPagamento primary key(Id),
		QtdParcelas int not null default 1,
        VlParcela double not null,
        Tipo int not null -- 1 A Vista Memo
    );
    
    create table if not exists tbCompra(
		Id int auto_increment,
         constraint PKCompra primary key(Id),
		Desconto decimal(5,2) not null default 0,
        `Data` date not null,
        IdForn int not null,
        IdPagamento int not null
    );
    alter table tbCompra add constraint FKCompraForn foreign key(IdForn) references tbFornecedor(Id),
						 add constraint FKCompraPgmt foreign key(IdPagamento) references tbPagamento(Id);
                         
	insert into tbPagamento(VlParcela, Tipo) value(0.00, 1);
    insert into tbCompra(`Data`, IdForn, IdPagamento) value('01/01/01', 1, 1);
    
    create table if not exists tbItensComprados(
		IdCompra int not null,
        IdEstoque int not null,
         constraint PKEstocaCompra primary key(IdCompra, IdEstoque),
        DescontoProd int not null default 0.00,
        QtdProd double not null
    );
    alter table tbItensComprados add constraint FKItensCompraEstoque foreign key(IdCompra) references tbCompra(Id),
							     add constraint FKItensEstoqueCompra foreign key(IdEstoque) references tbEstoque(IdEstoque);
                                 
	insert into tbItensComprados(IdCompra, IdEstoque, QtdProd) values(1, 1, (select Qtd from tbEstoque where Id = 1)),
																	 (1, 2, (select Qtd from tbEstoque where Id = 2)),
                                                                     (1, 3, (select Qtd from tbEstoque where Id = 3)),
                                                                     (1, 4, (select Qtd from tbEstoque where Id = 4)),
                                                                     (1, 5, (select Qtd from tbEstoque where Id = 5)),
                                                                     (1, 6, (select Qtd from tbEstoque where Id = 6));
-- =======================================================================================================================

-- =================================================================== VENDA ============================================ 
    create table if not exists tbCliente(
		Id int auto_increment,
         constraint PKCliente primary key(Id),
        IdPessoa int not null
    );
    alter table tbCliente add constraint FKClientePessoa foreign key(IdPessoa) references tbPessoa(Id);
    
    insert into tbCliente(IdPessoa) value(1);
    
    create table if not exists tbVenda(
		Id int auto_increment,
         constraint PKVenda primary key(Id),
		Desconto decimal(5,2) not null default 0.00,
        `Data` date not null,
        IdCliente int not null,
        IdPagamento int not null
    );
    alter table tbVenda add constraint FKVendaCliente foreign key(IdCliente) references tbCliente(Id),
						add constraint FKCompraPgmt foreign key(IdPagamento) references tbPagamento(Id);
    
    
    insert into tbVenda(`Data`, IdCliente, IdPagamento) value('01/01/01', 1, 1);
    
    create table if not exists tbItensVendidos(
		IdVenda int not null,
        IdEstoque int not null,
         constraint PKVendaEstoque primary key(IdVenda, IdEstoque),
        DescontoProd decimal(5,2)  not null default 0.00,
        QtdVendida double not null
    );
    alter table tbItensVendidos add constraint FKItensVendaEstoque foreign key(IdVenda) references tbVenda(Id),
								add constraint FKItensEstoqueVenda foreign key(IdEstoque) references tbEstoque(IdEstoque);
                                
	insert into tbItensVendidos(IdVenda, IdEstoque, QtdVendida) values(1, 1, 1);
-- ======================================================================================================================= 
 
 
-- =================================================================== FUNCIONARIO ============================================  
	create table if not exists tbFuncionario(
		Id int auto_increment,
         constraint PKFunc primary key(Id),
		`Status` bool not null default true,
        Salario double not null,
        IdPessoa int not null,
        IdCargo int not null
    );
    alter table tbFuncionario add constraint FKFuncPessoa foreign key(IdPessoa) references tbPessoa(Id);
    
    create table if not exists tbCargo(
		Id int auto_increment,
         constraint PKCargo primary key(Id),
		Nivel int,
        Nome varchar(75) not null
    );
    alter table tbFuncionario add constraint FKFuncCargo foreign key(IdCargo) references tbCargo(Id);
    
    insert into tbCargo(Nivel, Nome) value(1, 'Carinha que Planta');
    insert into tbFuncionario(Salario, IdPessoa, IdCargo) value(2.000, 1, 1);
    
    create table if not exists tbEquipe(
		Id int auto_increment,
         constraint FKEquipe primary key(Id),
		`Desc` varchar(300),
        Nome varchar(30) not null
	);
    
    create table if not exists tbFuncEquipe(
		IdFunc int not null,
        IdEquipe int not null,
         constraint PKFuncEquipe primary key(IdFunc, IdEquipe),
		LIDER bool not null default false
    );
    alter table tbFuncEquipe add constraint FKFuncEquipe foreign key(IdFunc) references tbFuncionario(Id),
							 add constraint FKEquipeFunc foreign key(IdEquipe) references tbEquipe(Id);
-- ======================================================================================================================= 


-- =================================================================== FLUXO DE CAIXA ============================================  
	create table if not exists tbDespesa(
		Id int auto_increment,
         constraint PKDespesa primary key(Id),
		ValorPago double not null,
        `Data` date
    );
    
    insert into tbDespesa(ValorPago, `Data`) values(1000.00, '01/01/01'),
												   (700.00, '01/01/01');
    
    create table if not exists tbContas(
		Id int auto_increment,
         constraint PKConta primary key(Id),
		Nome varchar(30) not null
    );
    
    insert into tbContas(Nome) value('Conta de Luz');
    
    
    create table if not exists tbDespesaAdm(
		IdDespesa int not null,
        IdDespAdm int not null,
         constraint PKDespesaAdmin primary key(IdDespesa, IdDespAdm)
    );
    alter table tbDespesaAdm add constraint FKDespesaAdmDespesa foreign key(IdDespesa) references tbDespesa(Id),
							   add constraint FKDespesaAdmConta foreign key(IdDespAdm) references tbContas(Id);
                               
	insert into tbDespesaAdm value(2, 1);
    
	create table if not exists tbDespesaFunc(
		IdDespesa int not null,
        IdFunc int not null,
         constraint PKDespesaFunc primary key(IdDespesa, IdFunc)
    );
    alter table tbDespesaFunc add constraint FKDespesaFunc foreign key(IdDespesa) references tbDespesa(Id),
							   add constraint FKFuncDespesa foreign key(IdFunc) references tbFuncionario(Id);
    
	insert into tbDespesaFunc value(1, 1);
                               
                         -- ------------------------------- Views ------------------------------------ 

    
    

 -- tbItem I ON E.Id = I.IdEstoque
-- =============================================================================================================================== 

-- =================================================================== PESSOA ============================================   
-- ======================================================================================================================= 








