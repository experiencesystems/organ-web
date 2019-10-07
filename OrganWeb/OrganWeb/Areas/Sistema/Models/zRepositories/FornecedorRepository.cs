using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class FornecedorRepository : Repository<Fornecedor>
    {
        public List<Fornecedor> GetNomesFornecedor()
        {
            var pessoa = new Pessoa();
            var pessoas = pessoa.GetAll();
            var select = _context.Fornecedors
                        .AsEnumerable()
                        .Select(f => new Fornecedor
                        {
                            Id = f.Id,
                            Nome = pessoas.Where(p => p.Id == f.IdPessoa).First().Nome
                        })
                        .ToList();
            return select;
        }
    }
}