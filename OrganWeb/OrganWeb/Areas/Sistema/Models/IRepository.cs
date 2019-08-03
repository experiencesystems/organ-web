using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganWeb.Areas.Sistema.Models
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllItems();
        T GetByID(int? Id);
        void Insert(T t);
        void Delete(int Id);
        void Update(T t);
        void Save();
    }
}
