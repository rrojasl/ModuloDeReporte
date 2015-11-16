﻿using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseManager.Sam2;
using DatabaseManager.Sam3;
using BackEndSAM.Models;
using SecurityManager.Api.Models;
using System.Web.Script.Serialization;

namespace BackEndSAM.DataAcces
{
    public class OrdenTrabajoSpoolBd
    {
        private static readonly object _mutex = new object();
        private static OrdenTrabajoSpoolBd _instance;

        /// <summary>
        /// constructor privado para implementar el patron Singleton
        /// </summary>
        private OrdenTrabajoSpoolBd()
        {
        }

        /// <summary>
        /// crea una instancia de la clase
        /// </summary>
        public static OrdenTrabajoSpoolBd Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new OrdenTrabajoSpoolBd();
                    }
                }
                return _instance;
            }
        }

        public object ListadoNumerosDeControl(int proyectoID, string busqueda, Sam3_Usuario usuario)
        {
            try
            {
                if (busqueda == null)
                {
                    busqueda = "";
                }
                using (Sam2Context ctx2 = new Sam2Context())
                {
                    List<int> proyectos = new List<int>();
                    List<int> patios = new List<int>();
                    using (SamContext ctx = new SamContext())
                    {
                        proyectos = (from p in ctx.Sam3_Rel_Usuario_Proyecto
                                     join eqp in ctx.Sam3_EquivalenciaProyecto on p.ProyectoID equals eqp.Sam3_ProyectoID
                                     where p.Activo && eqp.Activo
                                     && p.UsuarioID == usuario.UsuarioID
                                     && eqp.Sam3_ProyectoID == proyectoID
                                     select eqp.Sam2_ProyectoID).Distinct().AsParallel().ToList();

                       // proyectos.AddRange(ctx.Sam3_Rel_Usuario_Proyecto.Where(x => x.UsuarioID == usuario.UsuarioID)
                       //.Select(x => x.ProyectoID).Distinct().AsParallel().ToList());

                        proyectos = proyectos.Where(x => x > 0).ToList();



                        patios = (from p in ctx.Sam3_Proyecto
                                  join pa in ctx.Sam3_Patio on p.PatioID equals pa.PatioID
                                  join eq in ctx.Sam3_EquivalenciaPatio on pa.PatioID equals eq.Sam2_PatioID
                                  where p.Activo && pa.Activo && eq.Activo
                                  && proyectos.Contains(p.ProyectoID)
                                  select eq.Sam2_PatioID).Distinct().AsParallel().ToList();

                        patios = patios.Where(x => x > 0).ToList();
                    }

                    char[] lstElementoNumeroControl = busqueda.ToCharArray();
                    List<string> elementos = new List<string>();
                    foreach (char i in lstElementoNumeroControl)
                    {
                        elementos.Add(i.ToString());
                    }

                    List<ListaCombos> listado = (from odts in ctx2.OrdenTrabajoSpool
                                                 join odt in ctx2.OrdenTrabajo on odts.OrdenTrabajoID equals odt.OrdenTrabajoID
                                                 where !(from d in ctx2.Despacho
                                                      where d.Cancelado == false
                                                      select d.OrdenTrabajoSpoolID).Contains(odts.OrdenTrabajoSpoolID)
                                                 && !(from sh in ctx2.SpoolHold
                                                      where sh.SpoolID == odts.SpoolID
                                                      && (sh.Confinado || sh.TieneHoldCalidad || sh.TieneHoldIngenieria)
                                                      select sh).Any()
                                                 && proyectos.Contains(odt.ProyectoID)
                                                 && elementos.Any(x => odts.NumeroControl.Contains(x))
                                                 select new ListaCombos
                                                 {
                                                     id = odts.OrdenTrabajoSpoolID.ToString(),
                                                     value = odts.NumeroControl
                                                 }).Distinct().AsParallel().ToList();

                    listado = listado.OrderBy(x => x.value).ToList();

#if DEBUG
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(listado);
#endif

                    return listado;

                }
            }
            catch (Exception ex)
            {
                //-----------------Agregar mensaje al Log -----------------------------------------------
                LoggerBd.Instance.EscribirLog(ex);
                //-----------------Agregar mensaje al Log -----------------------------------------------
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(ex.Message);
                result.ReturnCode = 500;
                result.ReturnStatus = false;
                result.IsAuthenicated = true;

                return result;
            }
        }

        public object ListadoNumerosDeControl(string busqueda, int proyectoID, Sam3_Usuario usuario)
        {
            try
            {
                if (busqueda == null)
                {
                    busqueda = "";
                }
                using (Sam2Context ctx2 = new Sam2Context())
                {
                    List<int> proyectos = new List<int>();
                    List<int> patios = new List<int>();
                    using (SamContext ctx = new SamContext())
                    {
                        proyectos = (from p in ctx.Sam3_Rel_Usuario_Proyecto
                                     join eqp in ctx.Sam3_EquivalenciaProyecto on p.ProyectoID equals eqp.Sam3_ProyectoID
                                     where p.Activo && eqp.Activo
                                     && p.UsuarioID == usuario.UsuarioID
                                     && p.ProyectoID == proyectoID
                                     select eqp.Sam2_ProyectoID).Distinct().AsParallel().ToList();

                        // proyectos.AddRange(ctx.Sam3_Rel_Usuario_Proyecto.Where(x => x.UsuarioID == usuario.UsuarioID)
                        //.Select(x => x.ProyectoID).Distinct().AsParallel().ToList());

                        proyectos = proyectos.Where(x => x > 0).ToList();



                        patios = (from p in ctx.Sam3_Proyecto
                                  join pa in ctx.Sam3_Patio on p.PatioID equals pa.PatioID
                                  join eq in ctx.Sam3_EquivalenciaPatio on pa.PatioID equals eq.Sam2_PatioID
                                  where p.Activo && pa.Activo && eq.Activo
                                  && proyectos.Contains(p.ProyectoID)
                                  select eq.Sam2_PatioID).Distinct().AsParallel().ToList();

                        patios = patios.Where(x => x > 0).ToList();
                    }

                    char[] lstElementoNumeroControl = busqueda.ToCharArray();
                    List<string> elementos = new List<string>();
                    foreach (char i in lstElementoNumeroControl)
                    {
                        elementos.Add(i.ToString());
                    }

                    List<ComboNumeroControl> listado = (from odts in ctx2.OrdenTrabajoSpool
                                                 join odt in ctx2.OrdenTrabajo on odts.OrdenTrabajoID equals odt.OrdenTrabajoID
                                                 where !(from d in ctx2.Despacho
                                                         where d.Cancelado == false
                                                         select d.OrdenTrabajoSpoolID).Contains(odts.OrdenTrabajoSpoolID)
                                                 && !(from sh in ctx2.SpoolHold
                                                      where sh.SpoolID == odts.SpoolID
                                                      && (sh.Confinado || sh.TieneHoldCalidad || sh.TieneHoldIngenieria)
                                                      select sh).Any()
                                                 && proyectos.Contains(odt.ProyectoID)
                                                 && elementos.Any(x => odts.NumeroControl.Contains(x))
                                                        select new ComboNumeroControl
                                                 {
                                                      NumeroControl = odts.NumeroControl, 
                                                      NumeroControlID = odts.OrdenTrabajoSpoolID.ToString()
                                                 }).Distinct().AsParallel().ToList();

                    listado = listado.OrderBy(x => x.NumeroControl).ToList();

#if DEBUG
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(listado);
#endif
                    return listado;

                }
            }
            catch (Exception ex)
            {
                //-----------------Agregar mensaje al Log -----------------------------------------------
                LoggerBd.Instance.EscribirLog(ex);
                //-----------------Agregar mensaje al Log -----------------------------------------------
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(ex.Message);
                result.ReturnCode = 500;
                result.ReturnStatus = false;
                result.IsAuthenicated = true;

                return result;
            }
        }

        public object ListadoNumerosDeControlImpresionDocumental(string busqueda, int proyectoID, Sam3_Usuario usuario)
        {
            try
            {
                if (busqueda == null)
                {
                    busqueda = "";
                }
                using (Sam2Context ctx2 = new Sam2Context())
                {
                    List<int> proyectos = new List<int>();
                    List<int> patios = new List<int>();
                    using (SamContext ctx = new SamContext())
                    {
                        proyectos = (from p in ctx.Sam3_Rel_Usuario_Proyecto
                                     join eqp in ctx.Sam3_EquivalenciaProyecto on p.ProyectoID equals eqp.Sam3_ProyectoID
                                     where p.Activo && eqp.Activo
                                     && p.UsuarioID == usuario.UsuarioID
                                     && p.ProyectoID == proyectoID
                                     select eqp.Sam2_ProyectoID).Distinct().AsParallel().ToList();

                        // proyectos.AddRange(ctx.Sam3_Rel_Usuario_Proyecto.Where(x => x.UsuarioID == usuario.UsuarioID)
                        //.Select(x => x.ProyectoID).Distinct().AsParallel().ToList());

                        proyectos = proyectos.Where(x => x > 0).ToList();



                        patios = (from p in ctx.Sam3_Proyecto
                                  join pa in ctx.Sam3_Patio on p.PatioID equals pa.PatioID
                                  join eq in ctx.Sam3_EquivalenciaPatio on pa.PatioID equals eq.Sam2_PatioID
                                  where p.Activo && pa.Activo && eq.Activo
                                  && proyectos.Contains(p.ProyectoID)
                                  select eq.Sam2_PatioID).Distinct().AsParallel().ToList();

                        patios = patios.Where(x => x > 0).ToList();
                    }

                    char[] lstElementoNumeroControl = busqueda.ToCharArray();
                    List<string> elementos = new List<string>();
                    foreach (char i in lstElementoNumeroControl)
                    {
                        elementos.Add(i.ToString());
                    }

                    List<ComboNumeroControl> listado = (from odts in ctx2.OrdenTrabajoSpool
                                                        join odt in ctx2.OrdenTrabajo on odts.OrdenTrabajoID equals odt.OrdenTrabajoID
                                                        where !(from sh in ctx2.SpoolHold
                                                             where sh.SpoolID == odts.SpoolID
                                                             && (sh.Confinado || sh.TieneHoldCalidad || sh.TieneHoldIngenieria)
                                                             select sh).Any()
                                                        && proyectos.Contains(odt.ProyectoID)
                                                        && elementos.Any(x => odts.NumeroControl.Contains(x))
                                                        select new ComboNumeroControl
                                                        {
                                                            NumeroControl = odts.NumeroControl,
                                                            NumeroControlID = odts.OrdenTrabajoSpoolID.ToString()
                                                        }).Distinct().AsParallel().ToList();


                    listado = listado.OrderBy(x => x.NumeroControl).ToList();

#if DEBUG
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(listado);
#endif
                    return listado;

                }
            }
            catch (Exception ex)
            {
                //-----------------Agregar mensaje al Log -----------------------------------------------
                LoggerBd.Instance.EscribirLog(ex);
                //-----------------Agregar mensaje al Log -----------------------------------------------
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(ex.Message);
                result.ReturnCode = 500;
                result.ReturnStatus = false;
                result.IsAuthenicated = true;

                return result;
            }
        }

        public object ListadoNumeroControlConvertirAGranel(Sam3_Usuario usuario)
        {
            try
            {
                using (SamContext ctx = new SamContext())
                {
                    using (Sam2Context ctx2 = new Sam2Context())
                    {
                        List<int> proyectos = new List<int>();
                        List<int> patios = new List<int>();

                        UsuarioBd.Instance.ObtenerPatiosYProyectosDeUsuario(usuario.UsuarioID, out proyectos, out patios);

                        List<int> proyectos_sam2 = (from eqp in ctx.Sam3_EquivalenciaProyecto
                                                    where eqp.Activo
                                                    && proyectos.Contains(eqp.Sam3_ProyectoID)
                                                    select eqp.Sam2_ProyectoID).AsParallel().ToList();

                        List<int> patios_sam2 = (from eqp in ctx.Sam3_EquivalenciaPatio
                                                 where eqp.Activo
                                                 && patios.Contains(eqp.Sam3_PatioID)
                                                 select eqp.Sam2_PatioID).AsParallel().ToList();

                        List<ListaCombos> listado = (from odts in ctx2.OrdenTrabajoSpool
                                                     join odt in ctx2.OrdenTrabajo on odts.OrdenTrabajoID equals odt.OrdenTrabajoID
                                                     where proyectos_sam2.Contains(odt.ProyectoID)
                                                     select new ListaCombos
                                                     {
                                                         id = odts.OrdenTrabajoSpoolID.ToString(),
                                                         value = odts.NumeroControl
                                                     }).AsParallel().Distinct().ToList();

                        listado = listado.GroupBy(x => x.id).Select(x => x.First()).OrderBy(x => x.value).ToList();

                        return listado;
                    }
                }
 
            }
            catch (Exception ex)
            {
                //-----------------Agregar mensaje al Log -----------------------------------------------
                LoggerBd.Instance.EscribirLog(ex);
                //-----------------Agregar mensaje al Log -----------------------------------------------
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(ex.Message);
                result.ReturnCode = 500;
                result.ReturnStatus = false;
                result.IsAuthenicated = true;

                return result;
            }
        }

        public object ObtenerJuntasPorODTS(int odtsID, Sam3_Usuario usuario)
        {
            try
            {
                ItemsSpoolAGranel lista = new ItemsSpoolAGranel();
                using (Sam2Context ctx2 = new Sam2Context())
                {
                    lista.Items.AddRange((from js in ctx2.JuntaSpool
                                          join odts in ctx2.OrdenTrabajoSpool on js.SpoolID equals odts.SpoolID
                                          join tj in ctx2.TipoJunta on js.TipoJuntaID equals tj.TipoJuntaID
                                          where odts.OrdenTrabajoSpoolID == odtsID
                                          select new ListadoConvertirSpoolAGranel
                                          {
                                              Spool = odts.NumeroControl,
                                              Junta = js.Etiqueta,
                                              Status = (from jwks in ctx2.JuntaWorkstatus
                                                        join up in ctx2.UltimoProceso on jwks.UltimoProcesoID equals up.UltimoProcesoID
                                                        where jwks.JuntaSpoolID == js.JuntaSpoolID
                                                        select up.Nombre).FirstOrDefault(),
                                              TipoJunta = tj.Nombre
                                          }).AsParallel().Distinct().ToList());
                }
                return lista;
            }
            catch (Exception ex)
            {
                //-----------------Agregar mensaje al Log -----------------------------------------------
                LoggerBd.Instance.EscribirLog(ex);
                //-----------------Agregar mensaje al Log -----------------------------------------------
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(ex.Message);
                result.ReturnCode = 500;
                result.ReturnStatus = false;
                result.IsAuthenicated = true;

                return result;
            }
        }

        public object ConvertirJuntasPorODTS(int odtsID, Sam3_Usuario usuario)
        {
            try
            {
                using (SamContext ctx = new SamContext())
                {
                    using (var ctx_tran = ctx.Database.BeginTransaction())
                    {
                        using (Sam2Context ctx2 = new Sam2Context())
                        {
                            using (var ctx2_tran = ctx2.Database.BeginTransaction())
                            {
                                
                                ctx2_tran.Commit();
                            }
                        }
                        ctx_tran.Commit();
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
                //-----------------Agregar mensaje al Log -----------------------------------------------
                LoggerBd.Instance.EscribirLog(ex);
                //-----------------Agregar mensaje al Log -----------------------------------------------
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(ex.Message);
                result.ReturnCode = 500;
                result.ReturnStatus = false;
                result.IsAuthenicated = true;

                return result;
            }
        }
    }
}