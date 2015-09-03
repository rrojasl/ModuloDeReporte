﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using CommonTools.Libraries.Strings.Security;
using DatabaseManager.Sam3;
using SecurityManager.TokenHandler;
using SecurityManager.Api.Models;
using BackEndSAM.Models;
using BackEndSAM.DataAcces;

namespace BackEndSAM.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ListadoController : ApiController
    {

        public object Get(int tipoListado, string token, string parametroBusqueda = "")
        {
            string payload = "";
            string newToken = "";
            bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
            if (tokenValido)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Sam3_Usuario usuario = serializer.Deserialize<Sam3_Usuario>(payload);

                switch (tipoListado)
                {
                    case 1: //Folios aviso llegada
                        return AvisoLlegadaBd.Instance.ObtenerListadoFoliosParaFiltro();
                    case 2: // Folios de aviso de llegada con permiso de aduana autorizados
                        return AvisoLlegadaBd.Instance.ObtenerListadoFoliosRequierePermiso();
                    case 3: // listado de choferes por transportista
                        return ChoferBd.Instance.ObtenerChoferesProTransportista(Convert.ToInt32(parametroBusqueda), usuario);
                    case 4: //Obtener cantidades para dashboard
                        TransactionalInformation rest = new TransactionalInformation();
                        rest.ReturnMessage.Add("El listado de cantidades de Dashboard requiere de parametros de filtrado");
                        rest.ReturnCode = 500;
                        rest.ReturnStatus = false;
                        rest.IsAuthenicated = false;
                        return rest;
                    case 6: //Obtener listado de folios que ya tienen llegada de material
                        return AvisoLlegadaBd.Instance.ObtenerListadoSinPaseSalida();
                    default:
                        TransactionalInformation result = new TransactionalInformation();
                        result.ReturnMessage.Add("Listado no encontrado");
                        result.ReturnCode = 500;
                        result.ReturnStatus = false;
                        result.IsAuthenicated = false;
                        return result;
                }

            }
            else
            {
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(payload);
                result.ReturnCode = 401;
                result.ReturnStatus = false;
                result.IsAuthenicated = false;
                return result;
            }
        }


        public object Get(string data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            FiltrosJson filtros = serializer.Deserialize<FiltrosJson>(data);
            int tipoListado = Convert.ToInt32(filtros.TipoListado);
            string parametroBusqueda = filtros.ParametroBusqueda;
            string payload = "";
            string newToken = "";
            bool tokenValido = ManageTokens.Instance.ValidateToken(filtros.token, out payload, out newToken);
            if (tokenValido)
            {
                Sam3_Usuario usuario = serializer.Deserialize<Sam3_Usuario>(payload);

                switch (tipoListado)
                {
                    case 1: //Folios aviso llegada
                        return AvisoLlegadaBd.Instance.ObtenerListadoFoliosParaFiltro();
                    case 2: // Folios de aviso de llegada con permiso de aduana autorizados
                        return AvisoLlegadaBd.Instance.ObtenerListadoFoliosRequierePermiso();
                    case 3: // listado de choferes por transportista
                        return ChoferBd.Instance.ObtenerChoferesProTransportista(Convert.ToInt32(parametroBusqueda), usuario);
                    case 4: //Obtener cantidades para dashboard
                        return ListadoBd.Instance.ObtenerCantidadesDashboard(filtros, usuario);
                    case 5: //Obtener cantidades de Dashboard para aviso de entrada
                        return ListadoBd.Instance.ObtenerCantidadesDashboardAvisoEntrada(filtros, usuario);
                    case 7: //Obtener Cantidades dashboard de cuantificacion
                        return ListadoBd.Instance.ObtenerCantidadesDashboardCuantificacion(filtros, usuario);
                    case 8: //Obtener listado para generar ordenes de recepcion
                        return OrdenRecepcionBd.Instance.ObtenerListadoGenerarOrdenRecepcion(filtros, usuario);
                    case 9: //Obtener lista combo de folios de entrada por proyecto, que ya tengan cuantificacion
                        int proyectoID = filtros.ProyectoID != "" ? Convert.ToInt32(filtros.ProyectoID) : 0;
                        return ListadoBd.Instance.ObtenerFoliosEntradaPorProyecto(proyectoID, usuario);
                    case 10: //Obtener lista de combo de itemCodes por folio aviso de llegada
                        int folioAvisoLlegada = filtros.FolioAvisoLlegadaID != "" ? Convert.ToInt32(filtros.FolioAvisoLlegadaID) : 0;
                        return ListadoBd.Instance.ObtenerItemCodesPorFolioLlegada(folioAvisoLlegada, usuario);
                    case 11: //Entradas por cuantificar
                        return ListadoBd.Instance.ListadoMaterialesSinCuantificar(filtros, usuario);
                    case 12: // Packing list por cuantificar
                        return ListadoBd.Instance.ListadoPacknglistPorCuantificar(filtros, usuario);
                    case 13: // Materiales sin itemcode steelgo
                        return ListadoBd.Instance.ListadoMTLSinICS(filtros, usuario);
                    case 14: // Numeros unicos sin complemento de recepcion
                        return ListadoBd.Instance.ListadoNUConRecepcionSinComplemento(filtros, usuario);
                    case 15: // numeros unicos sin rack
                        return ListadoBd.Instance.ListadoNUSinAlmacenaje(filtros, usuario);
                    case 16:
                        return ListadoBd.Instance.ListadoIncidenciasActivas(filtros, usuario);
                    default:
                        TransactionalInformation result = new TransactionalInformation();
                        result.ReturnMessage.Add("Listado no encontrado");
                        result.ReturnCode = 500;
                        result.ReturnStatus = false;
                        result.IsAuthenicated = false;
                        return result;
                }

            }
            else
            {
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnMessage.Add(payload);
                result.ReturnCode = 401;
                result.ReturnStatus = false;
                result.IsAuthenicated = false;
                return result;
            }
        }
    }
}