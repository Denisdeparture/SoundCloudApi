using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.ServicesInterface;

namespace TestSoundcloudApi
{
    internal interface ISoundCloudClient : ILoad, IDownload, ISearch
    {
    }
}
