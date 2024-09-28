using System.ComponentModel.DataAnnotations;
namespace Condusef.Models
{
    public class SharedLayoutViewModel
    {
        public string LangValue { get; set; }
        public SharedLayoutViewModel() {
            LangValue = string.Empty;
        }
    }
}
