using Condusef_DLL.Clases.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    

    


    public class ClsErrorQuejas : ClsQuejas
    {
        public List<string> errors { get; set; }
        public ClsErrorQuejas() {
            errors = new List<string>();
        }
    }
    public class ErrorQuejas
    {
        public ClsErrorQuejas queja { get; set; }
        public ErrorQuejas()
        {
            queja = new ClsErrorQuejas();
        }
    }
    public class AddedQuejas : ClsQuejas
    {
        public int institucionid { get; set; }
        public string denominacion_social { get; set; }
        public int sectorid { get; set; }
        public string sector {  get; set; }
        public int Prod1Id { get; set; }
        public int Prod2Id { get; set; }
        public int Prod3Id { get; set; }
        public string QuejasMunDsc { get; set; }
        public string QuejasColDsc { get; set; }
        public string QuejasLocDsc { get; set; }
        public AddedQuejas()
        {
            institucionid = 0;
            denominacion_social = string.Empty;
            sectorid = 0;
            sector = string.Empty;
            Prod1Id = 0;
            Prod2Id = 0;
            Prod3Id = 0;
            QuejasColDsc = string.Empty;
            QuejasLocDsc = string.Empty;
            QuejasMunDsc = string.Empty;
        }
    }
    public class RespuestaEnvioQuejas
    {
        public List<AddedQuejas> addedRows { get; set; }

        public List<ErrorQuejas> errors { get; set; }
        public RespuestaEnvioQuejas()
        {
            addedRows = new List<AddedQuejas>();
            errors = new List<ErrorQuejas>();
        }
    }
}
