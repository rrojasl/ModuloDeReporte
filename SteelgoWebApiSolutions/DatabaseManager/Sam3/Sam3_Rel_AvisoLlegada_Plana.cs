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
    
    public partial class Sam3_Rel_AvisoLlegada_Plana
    {
        public int Rel_AvisoPlanaID { get; set; }
        public int FolioAvisoLlegadaID { get; set; }
        public int PlanaID { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual Sam3_FolioAvisoLlegada Sam3_FolioAvisoLlegada { get; set; }
        public virtual Sam3_Plana Sam3_Plana { get; set; }
    }
}
