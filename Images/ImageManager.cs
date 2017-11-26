using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;

namespace Images
{
    public class ImageManager : IImageManager
    {
        public string Download(string url)
        {
            try
            {
                using (var web = new WebClient())
                {
                    var fileName = Guid.NewGuid() + ".png";
                    var path = Path.Combine("../../../WebTemplate.MVC/Data", "Images", fileName);
                    //var fullPath = HttpContext.Current.Server.MapPath("~/" + path);

                    web.DownloadFile(url, path);
                    return fileName;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public string Save(HttpPostedFileBase httpPostedFilebase)
        {
            try
            {
                if (httpPostedFilebase == null)
                    return string.Empty;

                var fileName = Guid.NewGuid() + ".png";
                var path = Path.Combine("Data", "Images", fileName);

                var fullPath = HttpContext.Current.Server.MapPath("~/" + path);

                using (var image = Image.FromStream(httpPostedFilebase.InputStream))
                {
                    using (var fileWriteStream = new FileStream(fullPath, FileMode.Create))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, ImageFormat.Png);
                            memoryStream.WriteTo(fileWriteStream);
                        }
                    }
                }

                return fileName;
            }

            catch
            {
                return null;
            }
        }
    }
}