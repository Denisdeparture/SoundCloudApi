using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;

namespace TestSoundcloudApi
{
    public interface ILoad
    {
        public void Load(Collection musicobject, LoggingError logging, uint versionLoaderindex = 0);
    }
}
