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
    
    public partial class Sam3_Proyecto
    {
        public Sam3_Proyecto()
        {
            this.Sam3_Colada = new HashSet<Sam3_Colada>();
            this.Sam3_Corte = new HashSet<Sam3_Corte>();
            this.Sam3_ItemCode = new HashSet<Sam3_ItemCode>();
            this.Sam3_NumeroUnico = new HashSet<Sam3_NumeroUnico>();
            this.Sam3_NumeroUnicoCorte = new HashSet<Sam3_NumeroUnicoCorte>();
            this.Sam3_NumeroUnicoInventario = new HashSet<Sam3_NumeroUnicoInventario>();
            this.Sam3_NumeroUnicoMovimiento = new HashSet<Sam3_NumeroUnicoMovimiento>();
            this.Sam3_NumeroUnicoSegmento = new HashSet<Sam3_NumeroUnicoSegmento>();
            this.Sam3_OrdenTrabajo = new HashSet<Sam3_OrdenTrabajo>();
            this.Sam3_PinturaNumeroUnico = new HashSet<Sam3_PinturaNumeroUnico>();
            this.Sam3_Rel_Usuario_Proyecto = new HashSet<Sam3_Rel_Usuario_Proyecto>();
            this.Sam3_Rel_FolioAvisoLlegada_Proyecto = new HashSet<Sam3_Rel_FolioAvisoLlegada_Proyecto>();
            this.Sam3_RequisicionNumeroUnico = new HashSet<Sam3_RequisicionNumeroUnico>();
            this.Sam3_Spool = new HashSet<Sam3_Spool>();
            this.Sam3_FolioCuantificacion = new HashSet<Sam3_FolioCuantificacion>();
        }
    
        public int ProyectoID { get; set; }
        public int PatioID { get; set; }
        public int ClienteID { get; set; }
        public int ContactoID { get; set; }
        public int ColorID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual Sam3_Cliente Sam3_Cliente { get; set; }
        public virtual ICollection<Sam3_Colada> Sam3_Colada { get; set; }
        public virtual Sam3_Color Sam3_Color { get; set; }
        public virtual Sam3_Contacto Sam3_Contacto { get; set; }
        public virtual ICollection<Sam3_Corte> Sam3_Corte { get; set; }
        public virtual ICollection<Sam3_ItemCode> Sam3_ItemCode { get; set; }
        public virtual ICollection<Sam3_NumeroUnico> Sam3_NumeroUnico { get; set; }
        public virtual ICollection<Sam3_NumeroUnicoCorte> Sam3_NumeroUnicoCorte { get; set; }
        public virtual ICollection<Sam3_NumeroUnicoInventario> Sam3_NumeroUnicoInventario { get; set; }
        public virtual ICollection<Sam3_NumeroUnicoMovimiento> Sam3_NumeroUnicoMovimiento { get; set; }
        public virtual ICollection<Sam3_NumeroUnicoSegmento> Sam3_NumeroUnicoSegmento { get; set; }
        public virtual ICollection<Sam3_OrdenTrabajo> Sam3_OrdenTrabajo { get; set; }
        public virtual Sam3_Patio Sam3_Patio { get; set; }
        public virtual ICollection<Sam3_PinturaNumeroUnico> Sam3_PinturaNumeroUnico { get; set; }
        public virtual ICollection<Sam3_Rel_Usuario_Proyecto> Sam3_Rel_Usuario_Proyecto { get; set; }
        public virtual ICollection<Sam3_Rel_FolioAvisoLlegada_Proyecto> Sam3_Rel_FolioAvisoLlegada_Proyecto { get; set; }
        public virtual ICollection<Sam3_RequisicionNumeroUnico> Sam3_RequisicionNumeroUnico { get; set; }
        public virtual ICollection<Sam3_Spool> Sam3_Spool { get; set; }
        public virtual Sam3_ProyectoConsecutivo Sam3_ProyectoConsecutivo { get; set; }
        public virtual ICollection<Sam3_FolioCuantificacion> Sam3_FolioCuantificacion { get; set; }
    }
}
