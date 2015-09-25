using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Drawing;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.IO;
using System.Net.Http.Headers;

namespace SelfieServer
{
    public class ImagesController : IHttpController
    {
        public Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {
            Bitmap image = Webcam.Instance.TakePicture();
            return new ImageResult(image).ExecuteAsync(cancellationToken);
        }
    }

    public class ImageResult : IHttpActionResult
    {
        private Bitmap _image;
        public ImageResult(Bitmap image)
        {
            _image = image;
        }
        public async Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            MemoryStream responseStream = new MemoryStream(1024*1024*10);
            _image.Save(responseStream, System.Drawing.Imaging.ImageFormat.Png);
            response.Content = new ByteArrayContent(responseStream.ToArray());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return response;
            
        }
    }
}
