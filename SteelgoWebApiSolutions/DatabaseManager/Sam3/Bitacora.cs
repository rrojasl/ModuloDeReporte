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
    
    public partial class Bitacora
    {
        public int BitacoraId { get; set; }
        public int UsuarioId { get; set; }
        public int TipoActividadID { get; set; }
        public string Mensaje { get; set; }
        public System.DateTime Fecha { get; set; }
        public int EntidadId { get; set; }
    
        public virtual Entidad Entidad { get; set; }
        public virtual TipoActividad TipoActividad { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
