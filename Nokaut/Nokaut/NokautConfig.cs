using Microsoft.Extensions.Options;
using Nokaut.Service;

namespace Nokaut
{
    public class NokautConfig 
    {
        public string BaseUrl { get; set; } = String.Empty;

        public NokautConfig()
        {
            
        }
    }
}
