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
    
    public partial class Chofer
    {
        public Chofer()
        {
            this.Camion = new HashSet<Camion>();
        }
    
        public int ChoferID { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Camion> Camion { get; set; }
    }
}
