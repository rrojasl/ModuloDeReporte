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
    
    public partial class Sam3_Entidad
    {
        public Sam3_Entidad()
        {
            this.Sam3_Rel_Perfil_Entidad_Pagina = new HashSet<Sam3_Rel_Perfil_Entidad_Pagina>();
            this.Sam3_Rel_Documento_Entidad = new HashSet<Sam3_Rel_Documento_Entidad>();
            this.Sam3_Rel_Incidencia_Entidad = new HashSet<Sam3_Rel_Incidencia_Entidad>();
            this.Sam3_FolioAvisoLlegada = new HashSet<Sam3_FolioAvisoLlegada>();
        }
    
        public int EntidadID { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual ICollection<Sam3_Rel_Perfil_Entidad_Pagina> Sam3_Rel_Perfil_Entidad_Pagina { get; set; }
        public virtual ICollection<Sam3_Rel_Documento_Entidad> Sam3_Rel_Documento_Entidad { get; set; }
        public virtual ICollection<Sam3_Rel_Incidencia_Entidad> Sam3_Rel_Incidencia_Entidad { get; set; }
        public virtual Sam3_Rel_Proyecto_Entidad_Configuracion Sam3_Rel_Proyecto_Entidad_Configuracion { get; set; }
        public virtual ICollection<Sam3_FolioAvisoLlegada> Sam3_FolioAvisoLlegada { get; set; }
    }
}
