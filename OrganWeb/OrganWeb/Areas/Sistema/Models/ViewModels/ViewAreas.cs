using OrganWeb.Areas.Sistema.Models.Administrativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewAreas
    {
        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<Solo> Solos { get; set; }
    }
}