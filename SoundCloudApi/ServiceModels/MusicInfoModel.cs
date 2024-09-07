using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;

namespace TestSoundcloudApi.ServiceModels
{
    public record MusicInfoModel(string Name, UrlToDownload Url);

}
