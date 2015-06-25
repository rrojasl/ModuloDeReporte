﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using CommonTools.Libraries.Strings.Security;
using DatabaseManager.EntidadesPersonalizadas;
using DatabaseManager.Sam3;
using SecurityManager.TokenHandler;
using SecurityManager.Api.Models;
using BackEndSAM.DataAcces;
using BackEndSAM.Models;
using System.Diagnostics;
using System.IO;

namespace BackEndSAM.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DocumentoAvisoLlegadaController : ApiController
    {

        //public object Post(string token)
        //{
        //    try
        //    {
        //        string newToken = "";
        //        string payload = "";
        //        bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
        //        if (tokenValido)
        //        {
        //            JavaScriptSerializer serializer = new JavaScriptSerializer();
        //            Sam3_Usuario usuario = serializer.Deserialize<Sam3_Usuario>(payload);

        //            HttpRequestMessage request = this.Request;
        //            //verificamos que el tipo de contenido del request sea multipart
        //            if (!request.Content.IsMimeMultipartContent())
        //            {
        //                throw new Exception("Contenido no soportado");
        //            }

        //            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
        //            var provider = new MultipartFormDataStreamProvider(root);

        //            var task = request.Content.ReadAsMultipartAsync(provider).
        //                ContinueWith<HttpResponseMessage>(o =>
        //                {
        //                    FileInfo finfo = new FileInfo(provider.FileData.First().LocalFileName);

        //                    string guid = Guid.NewGuid().ToString();

        //                    File.Move(finfo.FullName, Path.Combine(root, guid + "_" + provider.FileData.First().Headers.ContentDisposition.FileName.Replace("\"", "")));

        //                    return new HttpResponseMessage()
        //                    {
        //                        Content = new StringContent("File uploaded")
        //                    };
        //                }
        //            );

        //            return task;
        //        }
        //        else
        //        {
        //            TransactionalInformation result = new TransactionalInformation();
        //            result.ReturnCode = 401;
        //            result.ReturnStatus = false;
        //            result.ReturnMessage.Add(payload);
        //            result.IsAuthenicated = false;
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TransactionalInformation result = new TransactionalInformation();
        //        result.ReturnCode = 500;
        //        result.ReturnStatus = false;
        //        result.ReturnMessage.Add(ex.Message);
        //        result.IsAuthenicated = false;
        //        return result;
        //    }
        //}

        public object Post(int folioAvisoLlegadaID, int TipoArchivoID, string token)
        {
            try
            {
                string newToken = "";
                string payload = "";
                bool tokenValido = ManageTokens.Instance.ValidateToken(token, out payload, out newToken);
                if (tokenValido)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Sam3_Usuario usuario = serializer.Deserialize<Sam3_Usuario>(payload);

                    HttpResponseMessage result = null;

                    var httpRequest = HttpContext.Current.Request;

                    if (httpRequest.Files.Count > 0)
                    {

                        var docfiles = new List<string>();
                        HttpPostedFile postedFile;
                        List<DocumentoPosteado> lstArchivos = new List<DocumentoPosteado>();
                        foreach (string file in httpRequest.Files)
                        {
                            Guid docguID = Guid.NewGuid();
                            postedFile = httpRequest.Files[file];
                            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/uploads/" + docguID + "." + postedFile.FileName);
                            string[] st = postedFile.FileName.Split('.');
                            string extencion = "." + st[1]; 
                            lstArchivos.Add(new DocumentoPosteado
                            {
                                FileName = postedFile.FileName,
                                ContentType = postedFile.ContentType,
                                Size = postedFile.ContentLength,
                                Path = filePath,
                                DocGuid = docguID,
                                FolioAvisoLlegadaID = folioAvisoLlegadaID,
                                UserId = usuario.UsuarioID,
                                TipoArchivoID = TipoArchivoID,
                                Extencion = extencion
                            });

                            postedFile.SaveAs(filePath);
                            docfiles.Add(filePath);
                        }

                        if (DocumentosBd.Instance.GuardarArchivosFolioAvisoLlegada(lstArchivos))
                        {
                            return "";
                        }
                        else
                        {
                            foreach (string path in docfiles)
                            {
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                }
                            }
                            result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                        }
                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.BadRequest);
                    }

                    return result;
                }
                else
                {
                    TransactionalInformation result = new TransactionalInformation();
                    result.ReturnCode = 401;
                    result.ReturnStatus = false;
                    result.ReturnMessage.Add(payload);
                    result.IsAuthenicated = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                TransactionalInformation result = new TransactionalInformation();
                result.ReturnCode = 500;
                result.ReturnStatus = false;
                result.ReturnMessage.Add(ex.Message);
                result.IsAuthenicated = false;
                return result;
            }
        }

    }//fin clase
}