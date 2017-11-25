using System.Web;

namespace Images
{
    public interface IImageManager
    {
        string Save(HttpPostedFileBase httpPostedFilebase);
    }
}