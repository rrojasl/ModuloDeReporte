﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSAM.Models
{
    public class FormatoPermisoAduana
    {
        public Nullable<System.DateTime> FechaRecepcion { get; set; }
        public string NombreTransportista { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreChofer { get; set; }
        public string PlacasCamion { get; set; }
        public string PlacasPlana { get; set; }
        public string NombreProyecto { get; set; }
    }
}