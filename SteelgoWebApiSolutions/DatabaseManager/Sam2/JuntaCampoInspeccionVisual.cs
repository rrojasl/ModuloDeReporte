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
    
    public partial class JuntaCampoInspeccionVisual
    {
        public JuntaCampoInspeccionVisual()
        {
            this.JuntaCampo = new HashSet<JuntaCampo>();
            this.JuntaCampoInspeccionVisualDefecto = new HashSet<JuntaCampoInspeccionVisualDefecto>();
        }
    
        public int JuntaCampoInspeccionVisualID { get; set; }
        public int InspeccionVisualCampoID { get; set; }
        public int JuntaCampoID { get; set; }
        public Nullable<System.DateTime> FechaInspeccion { get; set; }
        public bool Aprobado { get; set; }
        public string Observaciones { get; set; }
        public Nullable<System.Guid> UsuarioModifica { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public byte[] VersionRegistro { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual InspeccionVisualCampo InspeccionVisualCampo { get; set; }
        public virtual ICollection<JuntaCampo> JuntaCampo { get; set; }
        public virtual JuntaCampo JuntaCampo1 { get; set; }
        public virtual ICollection<JuntaCampoInspeccionVisualDefecto> JuntaCampoInspeccionVisualDefecto { get; set; }
    }
}
