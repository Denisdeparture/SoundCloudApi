using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;
using TestSoundcloudApi.ServiceModels;

namespace TestSoundcloudApi.ServicesInterface
{
    public interface ILoad
    {
        public MusicInfoModel Load(Collection musicobject, uint versionLoaderindex = 0);
    }
}
