using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsRespuestasReune
    {
        
    }
    public class ClsRespuestaConsultas
    {
        [JsonProperty("Número total de envios")]
        public int NumeroTotalEnvios { get; set; }

        [JsonProperty("Consultas enviadas")]
        public List<string> ConsultasEnviadas { get; set; }

        [JsonProperty("Aclaraciones enviadas")]
        public List<string> AclaracionesEnviadas { get; set; }

        [JsonProperty("Reclamaciones enviadas")]
        public List<string> ReclamacionesEnviadas { get; set; }
        public ClsRespuestaConsultas()
        {
            NumeroTotalEnvios = 0;
            ConsultasEnviadas = new List<string>();
            AclaracionesEnviadas = new List<string>();
            ReclamacionesEnviadas = new List<string>();
        }
    }
}
