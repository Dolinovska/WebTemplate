using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Images
{
    public class ImageManager : IImageManager
    {
        public string Save(HttpPostedFileBase httpPostedFilebase)
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

            //var bytes = httpPostedFilebase.ToByteArray();

            //var fullPath = HttpContext.Current.Server.MapPath(path);

            //File.WriteAllBytes(fullPath, bytes);

            //return fileName;

            //using (var image = Image.FromStream(httpPostedFilebase.InputStream))
            //{
            //image.Save(path, ImageFormat.Png);
            //}
        }
    }
}