using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganWeb.Areas.Sistema.Models.Semente
{
    interface ISementeRepository : IDisposable
    {
        IEnumerable<Semente> GetSementes();
        Semente GetSementeByID(int sementeId);
        void InsertSemente(Semente semente);
        void DeleteSemente(int sementeID);
        void UpdateSemente(Semente semente);
        void Save();
    }
}
