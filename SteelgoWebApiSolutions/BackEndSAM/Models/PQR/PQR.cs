﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEndSAM.Models
{
    public class PQR : ApiController
    {

        //Grid PQR
        public string PQRID { get; set; }
        public string Nombre { get; set; }
        public bool PREHEAT { get; set; }
        public bool PWHT { get; set; }
        public string Espesor { get; set; }      
        public string Codigo { get; set; }
        public string NumeroP { get; set; }
        public string GrupoP { get; set; }
        public string Aporte { get; set; }
        public string Mezcla { get; set; }
        public string Respaldo { get; set; }
        public string GrupoF { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }

        //NumeroP

        public int NumeroPID { get; set; }

        //ProcesoSoldadura

        public int ProcesoSoldaduraID { get; set; }

        //Grupop

        public int GrupoPID { get; set; }

        //Aporte
        public int AporteID { get; set; }

        //Mezcla
        public int MezclaID { get; set; }

        //Respaldo
        public int RespaldoID { get; set; }

        //GrupoF

        public int GrupoFID { get; set; }




    }
}
