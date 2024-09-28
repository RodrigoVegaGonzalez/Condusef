//using BundleTransformer.Core.Bundles;
//using BundleTransformer.Core.Resolvers;
//using System.Web.Optimization;

//public class BundleConfig
//{
//    public static void RegisterBundles(BundleCollection bundles)
//    {
//        // Agregue aquí los paquetes que desea incluir en el paquete
//        // Ejemplo: bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

//        BundleResolver.Current = new CustomBundleResolver();




//        //SCRIPTS
//        //Bundle bundle = new CustomScriptBundle("~/scripts/js").Include(
//        //    "~/scripts/umd/popper-utils.js",
//        //    "~/scripts/umd/popper.js",
//        //    "~/scripts/bootstrap.js",
//        //    "~/scripts/jquery-3.3.1.js",
//        //    "~/scripts/jquery.dataTables.js",
//        //    "~/scripts/modernizr-2.6.2.js",
//        //    "~/scripts/SweetAlert11/sweetalert2.all.js",
//        //    //"~/scripts/sweetalert2.all.js",
//        //    "~/scripts/toastr.js",
//        //    "~/scripts/jquery.formatCurrency-1.4.0.js",
//        //    "~/scripts/dataTables.bootstrap4.js",
//        //    "~/scripts/bootstrap-select.js",
//        //    "~/scripts/ajax-bootstrap-select.js",
//        //    "~/scripts/dataTables.select.js",
//        //    "~/Scripts/jquery.tabletojson.js",
//        //    "~/Scripts/Site.js",
//        //    "~/Scripts/jquery.mCustomScrollbar.concat.js",
//        //    "~/scripts/jquery.validate.js",
//        //    "~/scripts/Otros/ReloadPage.js",
//        //    "~/scripts/date-eu.js",
//        //    "~/scripts/moment-with-locales.js",
//        //    "~/scripts/Buttons/dataTables.buttons.js",
//        //    "~/scripts/Buttons/buttons.bootstrap4.js",
//        //    "~/scripts/Buttons/jszip.js",
//        //    "~/scripts/Buttons/pdfmake.js",
//        //    "~/scripts/Buttons/vfs_fonts.js",
//        //    "~/scripts/Buttons/buttons.html5.js",
//        //    "~/scripts/Buttons/buttons.print.js",
//        //    "~/scripts/Buttons/buttons.colVis.js",
//        //    "~/scripts/jquery.signalR-2.4.3.js",
//        //    "~/signalr/hubs",
//        //    "~/scripts/dataTables.searchPanes.js",
//        //    "~/scripts/searchPanes.bootstrap4.js"
//        //    );
//        //bundle.Transforms.Add(new HashArchivosEstaticos());
//        //bundles.Add(bundle);

//        //Bundle bundle = new CustomScriptBundle("~/scripts/js-val").Include(
//        //    "~/scripts/jquery.validate.js");
//        //bundle.Transforms.Add(new HashArchivosEstaticos());
//        //bundles.Add(bundle);

//        //bundles.Add(new ScriptBundle("~/bundles/redeco").Include(
//        //    "~/js/constantes.js",
//        //    "~/js/Condusef/EnviarReune.js"
//        //));
//        //var scriptBundle = new ScriptBundle("~/bundles/enviarRedeco");
//        //scriptBundle.Include(
//        //    "~/js/constantes.js",
//        //    "~/js/Condusef/EnviarReune.js"
//        //);
//        //bundles.Add(scriptBundle);
//    }
//}





////using BundleTransformer.Core.Bundles;
////using BundleTransformer.Core.Resolvers;
////using System.Web.Optimization;

////namespace WebApplication1.App_Start
////{
////    public class BundleConfig
////    {
////        public static void RegisterBundles(BundleCollection bundles)
////        {
////            BundleResolver.Current = new CustomBundleResolver();
////            //CSS
////            Bundle bundle = new CustomStyleBundle("~/Content/css").Include(
////                "~/Content/bootstrap.css",
////                "~/Content/select.dataTables.css",
////                "~/Content/dataTables.bootstrap4.css",
////                "~/Content/toastr.css",
////                "~/Content/Site.css",
////                "~/Content/glyphicon.css",
////                "~/Content/bootstrap-select.css",
////                "~/Content/ajax-bootstrap-select.css",
////                //"~/Content/font-awesome-custom.css",
////                "~/Content/jquery.mCustomScrollbar.css",
////                "~/Content/buttons.dataTables.css",
////                "~/Content/sweetalert2.css",
////                "~/Content/font-awesome-all.css",
////                "~/Content/searchPanes.bootstrap4.css"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomStyleBundle("~/Content/css_inicio").Include(
////                "~/Content/w3_custom.css"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomStyleBundle("~/Content/css_inicio1").Include(
////                "~/Content/w3.css"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomStyleBundle("~/Content/scroll").Include(
////                "~/Content/scroller.dataTables.min.css"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomStyleBundle("~/Content/principal").Include(
////                "~/Content/principal.css"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomStyleBundle("~/Content/scrollNoWrap").Include(
////                "~/Content/fixedColumns.dataTables.min.css",
////                "~/Content/scroller.dataTables.min.css"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomStyleBundle("~/Content/autoComplete").Include(
////                "~/Content/autoComplete.css"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);
////            //SCRIPTS
////            bundle = new CustomScriptBundle("~/scripts/js").Include(
////                "~/scripts/umd/popper-utils.js",
////                "~/scripts/umd/popper.js",
////                "~/scripts/bootstrap.js",
////                "~/scripts/jquery-3.3.1.js",
////                "~/scripts/jquery.dataTables.js",
////                "~/scripts/modernizr-2.6.2.js",
////                "~/scripts/SweetAlert11/sweetalert2.all.js",
////                //"~/scripts/sweetalert2.all.js",
////                "~/scripts/toastr.js",
////                "~/scripts/jquery.formatCurrency-1.4.0.js",
////                "~/scripts/dataTables.bootstrap4.js",
////                "~/scripts/bootstrap-select.js",
////                "~/scripts/ajax-bootstrap-select.js",
////                "~/scripts/dataTables.select.js",
////                "~/Scripts/jquery.tabletojson.js",
////                "~/Scripts/Site.js",
////                "~/Scripts/jquery.mCustomScrollbar.concat.js",
////                "~/scripts/jquery.validate.js",
////                "~/scripts/Otros/ReloadPage.js",
////                "~/scripts/date-eu.js",
////                "~/scripts/moment-with-locales.js",
////                "~/scripts/Buttons/dataTables.buttons.js",
////                "~/scripts/Buttons/buttons.bootstrap4.js",
////                "~/scripts/Buttons/jszip.js",
////                "~/scripts/Buttons/pdfmake.js",
////                "~/scripts/Buttons/vfs_fonts.js",
////                "~/scripts/Buttons/buttons.html5.js",
////                "~/scripts/Buttons/buttons.print.js",
////                "~/scripts/Buttons/buttons.colVis.js",
////                "~/scripts/jquery.signalR-2.4.3.js",
////                "~/signalr/hubs",
////                "~/scripts/dataTables.searchPanes.js",
////                "~/scripts/searchPanes.bootstrap4.js"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomScriptBundle("~/scripts/js-val").Include(
////                "~/scripts/jquery.validate.js");
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomScriptBundle("~/scripts/autocomplete").Include(
////                "~/scripts/autoComplete.js");
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomScriptBundle("~/scripts/scroller").Include(
////                "~/scripts/dataTables.scroller.min.js"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomScriptBundle("~/scripts/scrollerNow").Include(
////                "~/scripts/dataTables.fixedColumns.min.js",
////                "~/scripts/dataTables.scroller.min.js"
////                );
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);

////            bundle = new CustomScriptBundle("~/scripts/funciones").Include(
////                "~/scripts/Otros/Funciones.js");
////            bundle.Transforms.Add(new HashArchivosEstaticos());
////            bundles.Add(bundle);







////        }
////    }
////}