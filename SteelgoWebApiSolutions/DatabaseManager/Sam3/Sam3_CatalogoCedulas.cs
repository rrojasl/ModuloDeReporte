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
    
    public partial class Sam3_CatalogoCedulas
    {
        public Sam3_CatalogoCedulas()
        {
            this.Sam3_ItemCodeSteelgo = new HashSet<Sam3_ItemCodeSteelgo>();
        }
    
        public int CatalogoCedulasID { get; set; }
        public Nullable<int> DiametroID { get; set; }
        public Nullable<int> CedulaA { get; set; }
        public Nullable<int> CedulaB { get; set; }
        public Nullable<int> CedulaC { get; set; }
        public decimal EspesorIn { get; set; }
        public decimal EspesorMM { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual Sam3_Diametro Sam3_Diametro { get; set; }
        public virtual ICollection<Sam3_ItemCodeSteelgo> Sam3_ItemCodeSteelgo { get; set; }
    }
}
