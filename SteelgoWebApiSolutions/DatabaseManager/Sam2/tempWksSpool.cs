//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseManager.Sam2
{
    using System;
    using System.Collections.Generic;
    
    public partial class tempWksSpool
    {
        public int WorkstatusSpoolID { get; set; }
        public int OrdenTrabajoSpoolID { get; set; }
        public Nullable<int> UltimoProcesoID { get; set; }
        public bool TieneLiberacionDimensional { get; set; }
        public bool TieneRequisicionPintura { get; set; }
        public bool TienePintura { get; set; }
        public bool LiberadoPintura { get; set; }
        public bool Preparado { get; set; }
        public bool Embarcado { get; set; }
        public bool Certificado { get; set; }
        public string NumeroEtiqueta { get; set; }
        public Nullable<System.DateTime> FechaEtiqueta { get; set; }
        public Nullable<System.DateTime> FechaPreparacion { get; set; }
        public Nullable<System.DateTime> FechaCertificacion { get; set; }
        public Nullable<System.Guid> UsuarioModifica { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public byte[] VersionRegistro { get; set; }
        public Nullable<System.DateTime> FechaLiberacionCalidad { get; set; }
        public Nullable<System.Guid> UsuarioLiberacionCalidad { get; set; }
        public Nullable<System.DateTime> FechaLiberacionMateriales { get; set; }
        public Nullable<System.Guid> UsuarioLiberacionMateriales { get; set; }
        public Nullable<System.DateTime> FechaOkPnd { get; set; }
        public Nullable<System.Guid> UsuarioOkPnd { get; set; }
        public Nullable<int> FolioPreparacion { get; set; }
    }
}
