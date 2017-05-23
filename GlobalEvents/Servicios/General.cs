using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Servicios
{
    public class Email
    {
        public bool enviarToken(String email, String token)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("informacion.globalevents@gmail.com", "uccnpwqadocomhvy"),
                    EnableSsl = true
                };

                client.Send("informacion.globalevents@gmail.com", email, "Confirma tu cuenta", "Ingresa a la siguiente URL para verificar su cuenta: " +
                   "http://" + (HttpContext.Current.Request.Url.Authority.Contains("localhost") ? HttpContext.Current.Request.Url.Authority : HttpContext.Current.Request.Url.Host) + "/Home/ValidarToken?Token=" + token);
                return true;
            }
            catch
            { return false; }
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
        public ImageUploadResult subirImagen(String Path)
        {
            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.Actions.FileDescription(@Path)
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            ImageUploadResult result = new ImageUploadResult();
            result.Status = uploadResult.StatusCode.ToString();
            result.Uri= uploadResult.Uri.ToString();
            return result;
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

    public class ImageUploadResult
    {
        public string Status { get; set; }
        public string Uri { get; set; }
    }
}
