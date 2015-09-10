﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSAM.Models
{
    public class ListadoGenerarOrdenAlmacenaje
    {
            public string FolioCuantificacion { get; set; }
            public List<ElementoCuantificacionItemCode> ItemCodes { get; set; }

            public ListadoGenerarOrdenAlmacenaje()
            {
                ItemCodes = new List<ElementoCuantificacionItemCode>();
            }
    }

        public class ElementoCuantificacionItemCode
        {
            public string FolioCuantificacion { get; set; }
            public string ItemCodeID { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public string D1 { get; set; }
            public string D2 { get; set; }
            public string Cantidad { get; set; }
            public List<ElementoNumeroUnico> NumerosUnicos { get; set; }

            public ElementoCuantificacionItemCode()
            {
                NumerosUnicos = new List<ElementoNumeroUnico>();
            }
        }

        public class ElementoNumeroUnico
        {
            public string FolioCuantificacion { get; set; }
            public string ItemCodeID { get; set; }
            public string NumeroUnicoID { get; set; }
            public string NumeroUnico { get; set; }
        }
}