using System.Web;
using System.Web.Optimization;

namespace OrganWeb
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include(
                      "~/Scripts/fontawesome.js"));

            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                      "~/Scripts/modal.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapa").Include(
                      "~/Scripts/src/leaflet-src.js",
                      "~/Scripts/src/Leaflet.draw.js",
                      "~/Scripts/src/Leaflet.Draw.Event.js",
                      "~/Scripts/src/Toolbar.js",
                      "~/Scripts/src/Tooltip.js",
                      "~/Scripts/src/ext/GeometryUtil.js",
                      "~/Scripts/src/ext/LatLngUtil.js",
                      "~/Scripts/src/ext/LineUtil.Intersect.js",
                      "~/Scripts/src/ext/Polygon.Intersect.js",
                      "~/Scripts/src/ext/Polyline.Intersect.js",
                      "~/Scripts/src/ext/TouchEvents.js",
                      "~/Scripts/src/draw/DrawToolbar.js",
                      "~/Scripts/src/draw/handler/Draw.Feature.js",
                      "~/Scripts/src/draw/handler/Draw.SimpleShape.js",
                      "~/Scripts/src/draw/handler/Draw.Polyline.js",
                      "~/Scripts/src/draw/handler/Draw.Marker.js",
                      "~/Scripts/src/draw/handler/Draw.CircleMarker.js",
                      "~/Scripts/src/draw/handler/Draw.Circle.js",
                      "~/Scripts/src/draw/handler/Draw.Polygon.js",
                      "~/Scripts/src/draw/handler/Draw.Rectangle.js",
                      "~/Scripts/src/src/edit/EditToolbar.js",
                      "~/Scripts/src/edit/handler/EditToolbar.Edit.js",
                      "~/Scripts/src/edit/handler/EditToolbar.Delete.js",
                      "~/Scripts/src/Control.Draw.js",
                      "~/Scripts/src/edit/handler/Edit.Poly.js",
                      "~/Scripts/src/edit/handler/Edit.SimpleShape.js",
                      "~/Scripts/src/edit/handler/Edit.Marker.js",
                      "~/Scripts/src/edit/handler/Edit.CircleMarker.js",
                      "~/Scripts/src/edit/handler/Edit.Circle.js",
                      "~/Scripts/src/edit/handler/Edit.Rectangle.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/mapatela").Include(
                      "~/Scripts/off-canvas.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/menu").Include(
                      "~/Scripts/off-canvas.js",
                      "~/Scripts/hoverable-collapse.js",
                      "~/Scripts/template.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/leaflet.css",
                      "~/Scripts/src/leaflet.draw.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/csslog").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/login.css"));
        }
    }
}
