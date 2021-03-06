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
    
    public partial class Proyecto
    {
        public Proyecto()
        {
            this.Colada = new HashSet<Colada>();
            this.Corte = new HashSet<Corte>();
            this.CostoArmado = new HashSet<CostoArmado>();
            this.CostoProcesoRaiz = new HashSet<CostoProcesoRaiz>();
            this.CostoProcesoRelleno = new HashSet<CostoProcesoRelleno>();
            this.Despacho = new HashSet<Despacho>();
            this.DestajoSoldadorDetalle = new HashSet<DestajoSoldadorDetalle>();
            this.DestajoTuberoDetalle = new HashSet<DestajoTuberoDetalle>();
            this.Embarque = new HashSet<Embarque>();
            this.EmbarqueInventario = new HashSet<EmbarqueInventario>();
            this.Estimacion = new HashSet<Estimacion>();
            this.FabricanteProyecto = new HashSet<FabricanteProyecto>();
            this.InspeccionVisual = new HashSet<InspeccionVisual>();
            this.ItemCode = new HashSet<ItemCode>();
            this.NumeroUnico = new HashSet<NumeroUnico>();
            this.NumeroUnicoCorte = new HashSet<NumeroUnicoCorte>();
            this.NumeroUnicoInventario = new HashSet<NumeroUnicoInventario>();
            this.NumeroUnicoMovimiento = new HashSet<NumeroUnicoMovimiento>();
            this.NumeroUnicoSegmento = new HashSet<NumeroUnicoSegmento>();
            this.OrdenTrabajo = new HashSet<OrdenTrabajo>();
            this.Pendiente = new HashSet<Pendiente>();
            this.Peq = new HashSet<Peq>();
            this.PinturaAvance = new HashSet<PinturaAvance>();
            this.PinturaNumeroUnico = new HashSet<PinturaNumeroUnico>();
            this.PinturaSpool = new HashSet<PinturaSpool>();
            this.PreList = new HashSet<PreList>();
            this.ProcesoPinturaDetalle = new HashSet<ProcesoPinturaDetalle>();
            this.ProveedorProyecto = new HashSet<ProveedorProyecto>();
            this.ProyectoPendiente = new HashSet<ProyectoPendiente>();
            this.ProyectoPrograma = new HashSet<ProyectoPrograma>();
            this.ProyectoReporte = new HashSet<ProyectoReporte>();
            this.Recepcion = new HashSet<Recepcion>();
            this.ReporteDimensional = new HashSet<ReporteDimensional>();
            this.ReportePnd = new HashSet<ReportePnd>();
            this.ReporteSpoolPnd = new HashSet<ReporteSpoolPnd>();
            this.ReporteTt = new HashSet<ReporteTt>();
            this.Requisicion = new HashSet<Requisicion>();
            this.RequisicionNumeroUnico = new HashSet<RequisicionNumeroUnico>();
            this.RequisicionPintura = new HashSet<RequisicionPintura>();
            this.RequisicionSpool = new HashSet<RequisicionSpool>();
            this.Spool = new HashSet<Spool>();
            this.SpoolPendiente = new HashSet<SpoolPendiente>();
            this.TransportistaProyecto = new HashSet<TransportistaProyecto>();
            this.UsuarioProyecto = new HashSet<UsuarioProyecto>();
            this.WpsProyecto = new HashSet<WpsProyecto>();
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
        public Nullable<System.Guid> UsuarioModifica { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public byte[] VersionRegistro { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Colada> Colada { get; set; }
        public virtual Color Color { get; set; }
        public virtual Contacto Contacto { get; set; }
        public virtual ICollection<Corte> Corte { get; set; }
        public virtual ICollection<CostoArmado> CostoArmado { get; set; }
        public virtual ICollection<CostoProcesoRaiz> CostoProcesoRaiz { get; set; }
        public virtual ICollection<CostoProcesoRelleno> CostoProcesoRelleno { get; set; }
        public virtual ICollection<Despacho> Despacho { get; set; }
        public virtual ICollection<DestajoSoldadorDetalle> DestajoSoldadorDetalle { get; set; }
        public virtual ICollection<DestajoTuberoDetalle> DestajoTuberoDetalle { get; set; }
        public virtual ICollection<Embarque> Embarque { get; set; }
        public virtual ICollection<EmbarqueInventario> EmbarqueInventario { get; set; }
        public virtual ICollection<Estimacion> Estimacion { get; set; }
        public virtual ICollection<FabricanteProyecto> FabricanteProyecto { get; set; }
        public virtual ICollection<InspeccionVisual> InspeccionVisual { get; set; }
        public virtual ICollection<ItemCode> ItemCode { get; set; }
        public virtual ICollection<NumeroUnico> NumeroUnico { get; set; }
        public virtual ICollection<NumeroUnicoCorte> NumeroUnicoCorte { get; set; }
        public virtual ICollection<NumeroUnicoInventario> NumeroUnicoInventario { get; set; }
        public virtual ICollection<NumeroUnicoMovimiento> NumeroUnicoMovimiento { get; set; }
        public virtual ICollection<NumeroUnicoSegmento> NumeroUnicoSegmento { get; set; }
        public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; set; }
        public virtual Patio Patio { get; set; }
        public virtual ICollection<Pendiente> Pendiente { get; set; }
        public virtual ICollection<Peq> Peq { get; set; }
        public virtual ICollection<PinturaAvance> PinturaAvance { get; set; }
        public virtual ICollection<PinturaNumeroUnico> PinturaNumeroUnico { get; set; }
        public virtual ICollection<PinturaSpool> PinturaSpool { get; set; }
        public virtual ICollection<PreList> PreList { get; set; }
        public virtual ICollection<ProcesoPinturaDetalle> ProcesoPinturaDetalle { get; set; }
        public virtual ICollection<ProveedorProyecto> ProveedorProyecto { get; set; }
        public virtual ProyectoCamposRecepcion ProyectoCamposRecepcion { get; set; }
        public virtual ProyectoConfiguracion ProyectoConfiguracion { get; set; }
        public virtual ProyectoConsecutivo ProyectoConsecutivo { get; set; }
        public virtual ProyectoDossier ProyectoDossier { get; set; }
        public virtual ProyectoNomenclaturaSpool ProyectoNomenclaturaSpool { get; set; }
        public virtual ICollection<ProyectoPendiente> ProyectoPendiente { get; set; }
        public virtual ICollection<ProyectoPrograma> ProyectoPrograma { get; set; }
        public virtual ICollection<ProyectoReporte> ProyectoReporte { get; set; }
        public virtual ICollection<Recepcion> Recepcion { get; set; }
        public virtual ICollection<ReporteDimensional> ReporteDimensional { get; set; }
        public virtual ICollection<ReportePnd> ReportePnd { get; set; }
        public virtual ICollection<ReporteSpoolPnd> ReporteSpoolPnd { get; set; }
        public virtual ICollection<ReporteTt> ReporteTt { get; set; }
        public virtual ICollection<Requisicion> Requisicion { get; set; }
        public virtual ICollection<RequisicionNumeroUnico> RequisicionNumeroUnico { get; set; }
        public virtual ICollection<RequisicionPintura> RequisicionPintura { get; set; }
        public virtual ICollection<RequisicionSpool> RequisicionSpool { get; set; }
        public virtual ICollection<Spool> Spool { get; set; }
        public virtual ICollection<SpoolPendiente> SpoolPendiente { get; set; }
        public virtual ICollection<TransportistaProyecto> TransportistaProyecto { get; set; }
        public virtual ICollection<UsuarioProyecto> UsuarioProyecto { get; set; }
        public virtual ICollection<WpsProyecto> WpsProyecto { get; set; }
    }
}
