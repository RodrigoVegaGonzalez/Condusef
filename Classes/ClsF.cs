using Condusef_DLL.Clases.Generales;

namespace Condusef.Classes
{
    public class ClsF
    {
        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }

        public long Length { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public ClsF(IFormFile formFile)
        {
            ContentType = formFile.ContentType;
            ContentDisposition = formFile.ContentDisposition;
            Length = formFile.Length;
            Name = formFile.Name;
            FileName = formFile.FileName;
        }
    }
}
