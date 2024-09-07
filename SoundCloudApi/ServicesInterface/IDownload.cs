using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;

namespace TestSoundcloudApi.ServicesInterface
{
    public interface IDownload
    {
        public Task<UrlToDownload> GetUrlForDownloadFileAsync(HttpClient client, Transcoding transcoding);
        public Task<HttpContent> GetFile(HttpClient client, UrlToDownload urlObj);
    }
}
