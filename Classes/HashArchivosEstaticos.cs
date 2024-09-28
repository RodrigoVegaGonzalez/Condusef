//using System.IO;
//using System.Security.Cryptography;
//using System.Web;
//using System.Web.Hosting;
//using System.Web.Optimization;

//public class HashArchivosEstaticos : IBundleTransform
//{
//    public void Process(BundleContext context, BundleResponse response)
//    {
//        foreach (var archivo in response.Files)
//        {
//            using (FileStream fs = File.OpenRead(HostingEnvironment.MapPath(archivo.IncludedVirtualPath)))
//            {
//                byte[] hashArchivo = new SHA256Managed().ComputeHash(fs);
//                string version = HttpServerUtility.UrlTokenEncode(hashArchivo);
//                archivo.IncludedVirtualPath = $"{version}?v={version}";
//            }
//        }
//    }
//}