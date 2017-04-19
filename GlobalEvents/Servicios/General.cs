using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using System.Net;
using RestSharp.Authenticators;
using RestSharp;

namespace Servicios
{
    public class Email
    {
        public bool enviarToken(String email, String token)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", "key-44d44a4871449b214a251af55b840e68");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "app6cc9b7ab089145f5b8736732808aba66.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "GlobalEvents <maximiliano.talenti@gmail.com>");
            request.AddParameter("to", email);
            //request.AddParameter("to", "YOU@YOUR_DOMAIN_NAME");
            request.AddParameter("subject", "Confirma tu cuenta en GlobalEvents.");
            request.AddParameter("text", "Hola, tu token es: " + token);
            request.Method = Method.POST;
            return client.Execute(request).StatusCode == HttpStatusCode.OK;
            //return client.Execute(request);
        }
    }

    /// <summary>
    /// Clase para manejar imagenes en Cludinary.
    /// </summary>
    public class Imagenes
    {
        static Account account = new Account("hrr6lj3xm", "857819398585847", "R4Vf2S8YjWh9bFo-YAl1UPAkgqk");
        static Cloudinary cloudinary = new Cloudinary(account);

        /// <summary>
        /// Sube una imagen a Cludinary.
        /// </summary>
        /// <param name="Path">Ruta de imagen</param>
        /// <param name="GenerateId">Id con el que se identificará la imagen</param>
        /// <returns></returns>
        public bool subirImagen(String Path, String GenerateId)
        {
            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.Actions.FileDescription(Path),
                PublicId = GenerateId
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Sube imagen sin id específico, se genera automático.
        /// </summary>
        /// <param name="Path">Ruta de imagen</param>
        /// <returns>Diccionario con código de respuesta, si es 200 es ok y si es 200, el Id de la imagen generada</returns>
        public Dictionary<String, String> subirImagen(String Path)
        {
            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.Actions.FileDescription(@Path)
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            Dictionary<String, String> Diccionario = new Dictionary<String, String>();
            Diccionario.Add("StatusCode", uploadResult.StatusCode.ToString());
            Diccionario.Add("RandomId", uploadResult.PublicId);
            return Diccionario;
        }

        /// <summary>
        /// Obtiene url de la imagen a partir de su nombre completo (Con extension).
        /// </summary>
        /// <param name="NombreImagen">El nombre con el que se guardo la imagen</param>
        /// <returns>URL dónde se encuentra la imagen</returns>
        public string getUrl(String NombreImagen)
        {
            //return cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", NombreImagen, Format));
            return cloudinary.Api.UrlImgUp.BuildUrl(NombreImagen);
        }
    }
}
