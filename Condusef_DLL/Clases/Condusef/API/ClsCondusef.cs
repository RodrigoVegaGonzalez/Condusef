using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsCondusef
    {
    }

    public class ErrorEnvioCondusef
    {
        public Dictionary<string, List<string>> errors { get; set; }
        public string error { get; set; }

        public string message { get; set; }

        public ErrorEnvioCondusef()
        {
            errors = new Dictionary<string, List<string>>();
            message = string.Empty;
            error = string.Empty;
        }
    }
}
