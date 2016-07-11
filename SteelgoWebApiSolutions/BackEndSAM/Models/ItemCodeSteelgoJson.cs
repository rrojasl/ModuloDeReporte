﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSAM.Models
{
    public class ItemCodeSteelgoJson
    {
        public int ItemCodeSteelgoID { get; set; }
        public string DescripcionEspanol { get; set; }
        public string DescripcionIngles { get; set; }
        public decimal Peso { get; set; }
        public decimal Diametro1 { get; set; }
        public decimal Diametro2 { get; set; }
        public int FamiliaAceroID { get; set; }
        public string Familia { get; set; }
        public decimal Area { get; set; }
        public string Cedula { get; set; }
        public int CedulaID { get; set; }
        public string Codigo { get; set; }
        public string TipoAcero { get; set; }
        public int? Cantidad { get; set; }
        public string ColadaNombre { get; set; }
    }

    public class ICSDatosAsociacion
    {
        public string ItemCodeSteelgoID { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionLarga { get; set; }
        public string DescripcionIngles { get; set; }
        public string DescripcionLargaIngles { get; set; }
        public string Rel_ICS_DiametroID { get; set; }
        public string Diametro1ID { get; set; }
        public string Diametro1 { get; set; }
        public string Diametro2ID { get; set; }
        public string Diametro2 { get; set; }
        public string Grupo { get; set; }
        public string GrupoID { get; set; }
        public string FamiliaMaterialID { get; set; }
        public string FamiliaMaterial { get; set; }
        public string Acero { get; set; }
        public string AceroID { get; set; }
        public string CedulaID { get; set; }
        public string CedulaA { get; set; }
        public string CedulaB { get; set; }
        public string Libra { get; set; }
        public string Inch { get; set; }
        public string InchID { get; set; }
        public string MM { get; set; }
        public string Espesor { get; set; }
        public string Peso { get; set; }
        public string Area { get; set; }
        public string TipoMaterial { get; set; }
        public string TieneD2 { get; set; }
        public Boolean Asociado { get; set; }
    }
}