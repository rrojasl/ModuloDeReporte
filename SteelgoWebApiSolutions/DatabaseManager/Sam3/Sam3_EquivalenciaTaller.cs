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
    
    public partial class Sam3_EquivalenciaTaller
    {
        public int EquivalenciaTaller_ID { get; set; }
        public int Sam2_TallerID { get; set; }
        public int Sam3_TallerID { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> UsuarioModifica { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    }
}
