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
    public class CuantificacionBd
    {
        private static readonly object _mutex = new object();
        private static CuantificacionBd _instance;

        /// <summary>
        /// constructor privado para implementar el patron Singleton
        /// </summary>
        private CuantificacionBd()
        {
        }

        /// <summary>
        /// crea una instancia de la clase
        /// </summary>
        public static CuantificacionBd Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new CuantificacionBd();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Obtener la informacion del grid de Cuantificacion
        /// </summary>
        /// <param name="avisoEntrada">folio de aviso de entrada</param>
        /// <param name="folioCuantificacion">folio cuantificacion</param>
        /// <param name="bultoID">id del bulto</param>
        /// <returns>informacion</returns>
        public object gridCuantificacionInfo(int folioCuantificacion, int bultoID = 0)
        {
            try
            {
                List<CuantificacionListado> listado = new List<CuantificacionListado>();
                List<CuantificacionListado> listadoBultos = new List<CuantificacionListado>();

                using (SamContext ctx = new SamContext())
                {
                    //Para cuando no es la pantalla de Bulto
                    if (bultoID == 0)
                    {
                        listado = (from fc in ctx.Sam3_Rel_FolioCuantificacion_ItemCode
                                   join rid in ctx.Sam3_Rel_ItemCode_Diametro on fc.Rel_ItemCode_Diametro_ID equals rid.Rel_ItemCode_Diametro_ID
                                   join ic in ctx.Sam3_ItemCode on rid.ItemCodeID equals ic.ItemCodeID
                                   join d1 in ctx.Sam3_Diametro on rid.Diametro1ID equals d1.DiametroID
                                   join d2 in ctx.Sam3_Diametro on rid.Diametro2ID equals d2.DiametroID
                                   where fc.FolioCuantificacionID == folioCuantificacion && ic.Activo && fc.Activo
                                   select new CuantificacionListado
                                   {
                                       ItemCode = ic.Codigo,
                                       ItemCodeID = rid.Rel_ItemCode_Diametro_ID.ToString(),//ic.ItemCodeID.ToString(),
                                       Detallar = ctx.Sam3_Rel_Bulto_ItemCode.Where(c => c.ItemCodeID == ic.ItemCodeID && c.Activo && ic.Activo).Any() ? "Si" : "No",
                                       BultoID = ctx.Sam3_Rel_Bulto_ItemCode.Where(c => c.ItemCodeID == ic.ItemCodeID && c.Activo && ic.Activo).Any() ?
                                            ctx.Sam3_Rel_Bulto_ItemCode.Select(b => b.BultoID.ToString()).FirstOrDefault() : "",
                                       Descripcion = ic.DescripcionEspanol,
                                       D1 = d1.Valor,
                                       D2 = d2.Valor,
                                       Cantidad = fc.Cantidad,
                                       MM = fc.MM,
                                       ItemCodeSteelgo = (from rdi in ctx.Sam3_Rel_ItemCode_Diametro
                                                          join rics in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo on rdi.Rel_ItemCode_Diametro_ID equals rics.Rel_ItemCode_Diametro_ID
                                                          join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rics.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                          join itcs in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals itcs.ItemCodeSteelgoID
                                                          where rdi.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                          select itcs.Codigo).FirstOrDefault(),

                                       ItemCodeSteelgoID = (from rdi in ctx.Sam3_Rel_ItemCode_Diametro
                                                            join rics in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo on rdi.Rel_ItemCode_Diametro_ID equals rics.Rel_ItemCode_Diametro_ID
                                                            join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rics.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                            join itcs in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals itcs.ItemCodeSteelgoID
                                                            where rdi.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                            select rids.Rel_ItemCodeSteelgo_Diametro_ID).FirstOrDefault().ToString(),

                                       Familia = (from rdi in ctx.Sam3_Rel_ItemCode_Diametro
                                                  join rics in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo on rdi.Rel_ItemCode_Diametro_ID equals rics.Rel_ItemCode_Diametro_ID
                                                  join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rics.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                  join itcs in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals itcs.ItemCodeSteelgoID
                                                  join fa in ctx.Sam3_FamiliaAcero on itcs.FamiliaAceroID equals fa.FamiliaAceroID
                                                  where rdi.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                  select fa.Nombre).FirstOrDefault().ToString(),

                                       Cedula = (from rdi in ctx.Sam3_Rel_ItemCode_Diametro
                                                 join rics in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo on rdi.Rel_ItemCode_Diametro_ID equals rics.Rel_ItemCode_Diametro_ID
                                                 join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rics.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                 join itcs in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals itcs.ItemCodeSteelgoID
                                                 join ced in ctx.Sam3_Cedula on itcs.CedulaID equals ced.CedulaID
                                                 join d in ctx.Sam3_Diametro on rids.Diametro1ID equals d.DiametroID
                                                 where rdi.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                 select d1.Valor.ToString() + "-" + ced.CedulaA + "-" + ced.CedulaB + "-" + ced.CedulaC).FirstOrDefault().ToString(),

                                       Colada = (from ric in ctx.Sam3_Rel_Itemcode_Colada
                                                 join c in ctx.Sam3_Colada on ric.ColadaID equals c.ColadaID
                                                 where ric.Activo 
                                                 && ric.ColadaID == fc.ColadaID 
                                                 && ric.ItemCodeID == ic.ItemCodeID
                                                 select c.NumeroColada).FirstOrDefault(),

                                       ColadaID = (from ric in ctx.Sam3_Rel_Itemcode_Colada
                                                 join c in ctx.Sam3_Colada on ric.ColadaID equals c.ColadaID
                                                 where ric.Activo
                                                 && ric.ColadaID == fc.ColadaID
                                                 && ric.ItemCodeID == ic.ItemCodeID
                                                 select c.ColadaID).FirstOrDefault(),

                                       TipoAcero = (from rdi in ctx.Sam3_Rel_ItemCode_Diametro
                                                    join rics in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo on rdi.Rel_ItemCode_Diametro_ID equals rics.Rel_ItemCode_Diametro_ID
                                                    join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rics.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                    join itcs in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals itcs.ItemCodeSteelgoID
                                                    join fa in ctx.Sam3_FamiliaAcero on itcs.FamiliaAceroID equals fa.FamiliaAceroID
                                                    join fm in ctx.Sam3_FamiliaMaterial on fa.FamiliaMaterialID equals fm.FamiliaMaterialID
                                                    where rdi.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                    select fm.Nombre).FirstOrDefault().ToString(),

                                       TieneNU = ctx.Sam3_NumeroUnico.Where(n => n.ItemCodeID == ic.ItemCodeID && n.Activo && ic.Activo).Count() == fc.Cantidad ? "Si" :
                                            ctx.Sam3_NumeroUnico.Where(n => n.ItemCodeID == fc.ItemCodeID && n.Activo && ic.Activo).Count() == 0 ? "No" : "Parcial",
                                       RelFCId = fc.Rel_FolioCuantificacion_ItemCode_ID.ToString(),
                                       ItemCodeOrigenID = ic.ItemCodeID.ToString()
                                   }).AsParallel().ToList();

                        listadoBultos = (from b in ctx.Sam3_Bulto
                                         where b.FolioCuantificacionID == folioCuantificacion && b.Activo
                                         select new CuantificacionListado
                                         {
                                             ItemCode = "Bulto No. " + b.BultoID.ToString(),
                                             Detallar = "Si",
                                             Cantidad = 1,
                                             BultoID = b.BultoID.ToString()
                                         }).AsParallel().ToList();

                        listado.AddRange(listadoBultos);



                    }
                    else //Cuando es la pantalla de Bulto
                    {
                        listado = (from rbic in ctx.Sam3_Rel_Bulto_ItemCode
                                   join rid in ctx.Sam3_Rel_ItemCode_Diametro on rbic.Rel_ItemCode_Diametro_ID equals rid.Rel_ItemCode_Diametro_ID
                                   join ic in ctx.Sam3_ItemCode on rid.ItemCodeID equals ic.ItemCodeID
                                   join d1 in ctx.Sam3_Diametro on rid.Diametro1ID equals d1.DiametroID
                                   join d2 in ctx.Sam3_Diametro on rid.Diametro2ID equals d2.DiametroID
                                   where rbic.BultoID == bultoID 
                                   && rbic.Activo && rid.Activo && ic.Activo && d1.Activo && d2.Activo
                                   select new CuantificacionListado
                                   {
                                       ItemCode = ic.Codigo,
                                       ItemCodeID = rid.Rel_ItemCode_Diametro_ID.ToString(),
                                       Detallar = "No",
                                       BultoID = rbic.BultoID.ToString(),
                                       Descripcion = ic.DescripcionEspanol,
                                       D1 = d1.Valor,
                                       D2 = d2.Valor,
                                       Cantidad = rbic.Cantidad,
                                       MM = rbic.MM,
                                       ItemCodeSteelgo = (from rii in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo
                                                          join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rii.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                          join ics in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals ics.ItemCodeSteelgoID
                                                          where rii.Activo && rids.Activo && ics.Activo
                                                          && rii.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                          select ics.Codigo).FirstOrDefault(),

                                       ItemCodeSteelgoID = (from rii in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo
                                                            join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rii.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                            join ics in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals ics.ItemCodeSteelgoID
                                                            where rii.Activo && rids.Activo && ics.Activo
                                                            && rii.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                            select rii.Rel_ItemCodeSteelgo_Diametro_ID).FirstOrDefault().ToString(),
                                       Familia = (from rii in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo
                                                          join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rii.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                          join ics in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals ics.ItemCodeSteelgoID
                                                          join fa in ctx.Sam3_FamiliaAcero on ics.FamiliaAceroID equals fa.FamiliaAceroID
                                                          where rii.Activo && rids.Activo && ics.Activo && fa.Activo
                                                          && rii.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                          select fa.Nombre).FirstOrDefault(),

                                       Cedula = (from rii in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo
                                                          join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rii.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                          join ics in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals ics.ItemCodeSteelgoID
                                                          join c in ctx.Sam3_Cedula on ics.CedulaID equals c.CedulaID
                                                          where rii.Activo && rids.Activo && ics.Activo && c.Activo
                                                          && rii.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                          select d1.Valor.ToString() + "-" + c.CedulaA + "-" + c.CedulaB + "-" + c.CedulaC).FirstOrDefault(),

                                       Colada = (from ric in ctx.Sam3_Rel_Itemcode_Colada
                                                 join co in ctx.Sam3_Colada on ric.ColadaID equals co.ColadaID
                                                 where ric.Activo 
                                                 && ric.ItemCodeID == rbic.ColadaID
                                                 && ric.ItemCodeID == ic.ItemCodeID
                                                 select co.NumeroColada).FirstOrDefault(),

                                       ColadaID = (from ric in ctx.Sam3_Rel_Itemcode_Colada
                                                   join co in ctx.Sam3_Colada on ric.ColadaID equals co.ColadaID
                                                   where ric.Activo
                                                   && ric.ColadaID == rbic.ColadaID
                                                   && ric.ItemCodeID == ic.ItemCodeID
                                                   select co.ColadaID).FirstOrDefault(),

                                       TipoAcero = (from rii in ctx.Sam3_Rel_ItemCode_ItemCodeSteelgo
                                                          join rids in ctx.Sam3_Rel_ItemCodeSteelgo_Diametro on rii.Rel_ItemCodeSteelgo_Diametro_ID equals rids.Rel_ItemCodeSteelgo_Diametro_ID
                                                          join ics in ctx.Sam3_ItemCodeSteelgo on rids.ItemCodeSteelgoID equals ics.ItemCodeSteelgoID
                                                          join fa in ctx.Sam3_FamiliaAcero on ics.FamiliaAceroID equals fa.FamiliaAceroID
                                                          join fm in ctx.Sam3_FamiliaMaterial on fa.FamiliaMaterialID equals fm.FamiliaMaterialID
                                                          where rii.Activo && rids.Activo && ics.Activo && fa.Activo && fm.Activo
                                                          && rii.Rel_ItemCode_Diametro_ID == rid.Rel_ItemCode_Diametro_ID
                                                          select fm.Nombre).FirstOrDefault(),

                                       TieneNU = ctx.Sam3_NumeroUnico.Where(n => n.ItemCodeID == rbic.ItemCodeID && n.Activo && ic.Activo).Count() == rbic.Cantidad  ? "Si" :
                                            ctx.Sam3_NumeroUnico.Where(n => n.ItemCodeID == ic.ItemCodeID && n.Activo && ic.Activo).Count() == 0 ? "No" : "Parcial",
                                       RelBID = rbic.Rel_Bulto_ItemCode_ID.ToString(),
                                       ItemCodeOrigenID = ic.ItemCodeID.ToString()
                                   }).AsParallel().ToList();
                    }
                }

#if DEBUG
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(listado);
#endif
                //listado = listado.GroupBy(x => x.ItemCodeID).Select(x => x.First()).ToList();

                foreach (CuantificacionListado lst in listado)
                {
                    lst.ItemCode = lst.ItemCode + "(" + lst.D1.ToString() + ", " + lst.D2.ToString() + ")";
                }

                return listado;
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