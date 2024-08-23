using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi.Models
{
    public class Transcoding
    {
        public string url { get; set; }
        public string preset { get; set; }
        public int duration { get; set; }
        public bool snipped { get; set; }
        public Format format { get; set; }
        public string quality { get; set; }
    }
}
