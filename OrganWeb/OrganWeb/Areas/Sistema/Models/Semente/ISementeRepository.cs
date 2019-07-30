using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganWeb.Areas.Sistema.Models.Semente
{
    interface ISementeRepository : IDisposable
    {
        IEnumerable<tbsemente> GetSementes();
        tbsemente GetSementeByID(int sementeId);
        void InsertSemente(tbsemente semente);
        void DeleteSemente(int sementeID);
        void UpdateSemente(tbsemente semente);
        void Save();
    }
}
