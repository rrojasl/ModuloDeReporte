﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManager.Sam3;

namespace BackEndSAM.Models
{
    public class FolioAvisoEntradaJson
    {
        public int FolioAvisollegadaId { get; set; }
        public string Factura { get; set; }
        public string OrdenCompra { get; set; }
        public int ProveedorID { get; set; }
        public int ClienteId { get; set; }
        public int PatioID { get; set; }
        public string Estatus { get; set; }
        public int ProyectoID { get; set; }
    }

    public class DetalleAvisoEntradaJson
    {
        public int FolioAvisollegadaId { get; set; }
        public string Factura { get; set; }
        public string OrdenCompra { get; set; }
        public Proveedor Proveedor { get; set; }
        public Cliente Cliente { get; set; }
        public Patio Patio { get; set; }
        public string Estatus { get; set; }
        public ProyectosAV Proyectos { get; set; }
        public List<ListaDocumentos> Documentos { get; set; }

        public DetalleAvisoEntradaJson()
        {
            Proveedor = new Proveedor();
            Cliente = new Cliente();
            Patio = new Patio();
            Proyectos = new ProyectosAV();
            Documentos = new List<ListaDocumentos>();
        }
    }
}