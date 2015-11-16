//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseManager.Sam3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sam3_Spool
    {
        public Sam3_Spool()
        {
            this.Sam3_MaterialSpool = new HashSet<Sam3_MaterialSpool>();
            this.Sam3_OrdenTrabajoSpool = new HashSet<Sam3_OrdenTrabajoSpool>();
            this.Sam3_CorteSpool = new HashSet<Sam3_CorteSpool>();
            this.Sam3_JuntaSpool = new HashSet<Sam3_JuntaSpool>();
        }
    
        public int SpoolID { get; set; }
        public int ProyectoID { get; set; }
        public int FamiliaAcero1ID { get; set; }
        public Nullable<int> FamiliaAcero2ID { get; set; }
        public string Nombre { get; set; }
        public string Dibujo { get; set; }
        public string Especificacion { get; set; }
        public string Cedula { get; set; }
        public Nullable<decimal> Pdis { get; set; }
        public Nullable<decimal> DiametroPlano { get; set; }
        public Nullable<decimal> Peso { get; set; }
        public Nullable<decimal> Area { get; set; }
        public Nullable<int> PorcentajePnd { get; set; }
        public bool RequierePwht { get; set; }
        public bool PendienteDocumental { get; set; }
        public bool AprobadoParaCruce { get; set; }
        public Nullable<int> Prioridad { get; set; }
        public string Revision { get; set; }
        public string RevisionCliente { get; set; }
        public string Segmento1 { get; set; }
        public string Segmento2 { get; set; }
        public string Segmento3 { get; set; }
        public string Segmento4 { get; set; }
        public string Segmento5 { get; set; }
        public string Segmento6 { get; set; }
        public string Segmento7 { get; set; }
        public string SistemaPintura { get; set; }
        public string ColorPintura { get; set; }
        public string CodigoPintura { get; set; }
        public Nullable<System.DateTime> FechaEtiqueta { get; set; }
        public string NumeroEtiqueta { get; set; }
        public string Campo1 { get; set; }
        public string Campo2 { get; set; }
        public string Campo3 { get; set; }
        public string Campo4 { get; set; }
        public string Campo5 { get; set; }
        public Nullable<decimal> DiametroMayor { get; set; }
        public Nullable<int> CuadranteID { get; set; }
        public Nullable<System.DateTime> FechaImportacion { get; set; }
        public string RequierePruebaHidrostatica { get; set; }
        public Nullable<System.DateTime> FechaLocalizacion { get; set; }
        public string UltimoProceso { get; set; }
        public string AreaGrupo { get; set; }
        public string KgsGrupo { get; set; }
        public string DiamGrupo { get; set; }
        public string PeqGrupo { get; set; }
        public string SistemaPinturaFinal { get; set; }
        public string Paint { get; set; }
        public string DiametroPromedio { get; set; }
        public string PaintLine { get; set; }
        public string AreaEq { get; set; }
        public string Inox { get; set; }
        public string ClasifInox { get; set; }
        public Nullable<System.DateTime> FechaHomologacion { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual Sam3_FamiliaAcero Sam3_FamiliaAcero { get; set; }
        public virtual Sam3_FamiliaAcero Sam3_FamiliaAcero1 { get; set; }
        public virtual ICollection<Sam3_MaterialSpool> Sam3_MaterialSpool { get; set; }
        public virtual ICollection<Sam3_OrdenTrabajoSpool> Sam3_OrdenTrabajoSpool { get; set; }
        public virtual Sam3_Proyecto Sam3_Proyecto { get; set; }
        public virtual ICollection<Sam3_CorteSpool> Sam3_CorteSpool { get; set; }
        public virtual ICollection<Sam3_JuntaSpool> Sam3_JuntaSpool { get; set; }
    }
}
