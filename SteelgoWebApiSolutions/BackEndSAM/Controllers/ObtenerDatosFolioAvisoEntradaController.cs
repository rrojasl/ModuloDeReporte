﻿using BackEndSAM.DataAcces;
using DatabaseManager.Sam3;
using SecurityManager.Api.Models;
using SecurityManager.TokenHandler;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using BackEndSAM.Models;
using System.Collections.Generic;
using System;

namespace BackEndSAM.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ObtenerDatosFolioAvisoEntradaController : ApiController
    {
        /// <summary>
        /// Obtener los datos de un folio Aviso de Entrada
        /// </summary>
        /// <param name="FolioAvisoEntradaID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public object Get(int folioAvisoLlegadaID, string token)
        {
            string payload = "";
            string newToken = "";
            bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
            if (tokenValido)
            {
                return FoliosCuantificacionBd.Instance.obtenerDatosFolioEntrada(folioAvisoLlegadaID);
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

        public object Get(string folioAvisoLlegadaID, string folioCuantificacionID, string token)
        {
            string payload = "";
            string newToken = "";
            bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
            if (tokenValido)
            {
                InfoFolioAvisoEntrada infoEntrada = (InfoFolioAvisoEntrada)FoliosCuantificacionBd.Instance.obtenerDatosFolioEntrada(Convert.ToInt32(folioAvisoLlegadaID));
                List<CuantificacionListado> listado = (List<CuantificacionListado>)CuantificacionBd.Instance.gridCuantificacionInfo(Convert.ToInt32(folioCuantificacionID));
                List<object> resultado = new List<object>();
                resultado.Add(infoEntrada);
                resultado.Add(listado);
                return resultado;
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


        public object Get(string token)
        {
            string payload = "";
            string newToken = "";
            bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
            if (tokenValido)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Sam3_Usuario usuario = serializer.Deserialize<Sam3_Usuario>(payload);
                return FoliosCuantificacionBd.Instance.ObtenerListadoProyectosCuantificacion(usuario);
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

        public object Get(string proyectoID, string token)
        {
            string payload = "";
            string newToken = "";
            bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
            if (tokenValido)
            {
                return FoliosCuantificacionBd.Instance.ObtenerTipoPackingListConfiguracion(proyectoID);
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
        public object Get(int folioAvisoLlegadaID, int proyectoID, string token)
        {
            string payload = "";
            string newToken = "";
            bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
            if (tokenValido)
            {
                return FoliosCuantificacionBd.Instance.ObtenerTipoUsoPorProyecto(proyectoID);
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

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}