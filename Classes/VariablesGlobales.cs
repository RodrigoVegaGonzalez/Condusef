using Condusef_DLL.Clases.Generales;

namespace Condusef.Classes
{
    public class VariablesGlobales
    {
        public string Llave {  get; set; }
        public string IV {  get; set; }
        public string RutaPlantillasCorreo { get; set; }
        public string RutaPlantillasLayout { get; set; }
        public string RutaReune { get; set; }
        public ClsUrl Url { get; set; }

        public VariablesGlobales()
        {
            Llave = string.Empty;
            IV = string.Empty;
            RutaPlantillasCorreo = string.Empty;
            RutaPlantillasLayout = string.Empty;
            RutaReune = string.Empty;
            Url = new();
        }
    }
}
