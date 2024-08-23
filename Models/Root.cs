using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi.Models
{
    public class Root
    {
        public List<Collection> collection { get; set; }
        public int total_results { get; set; }
        public string next_href { get; set; }
        public string query_urn { get; set; }
    }
}
