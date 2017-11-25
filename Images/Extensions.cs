using System.IO;
using System.Linq;
using System.Web;

namespace Images
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this HttpPostedFileBase httpPostedFileBase)
        {
            byte[] bytes;
            using (Stream inputStream = httpPostedFileBase.InputStream)
            {
                var memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }

                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
    }
}
