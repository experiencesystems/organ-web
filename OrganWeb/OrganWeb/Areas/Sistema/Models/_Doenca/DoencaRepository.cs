using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models._Doenca
{
    public class DoencaRepository
    {
        private BancoContext _context;

        public DoencaRepository(BancoContext sementeContext)
        {
            this._context = sementeContext;
        }
    }
}