using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;

namespace TestSoundcloudApi
{
    public class DownloaderAudio : IFileUrl
    {
        private readonly SoundCloudConfiguration _SoundCloudConfiguration;
        public DownloaderAudio(SoundCloudConfiguration cloudConfiguration)
        {
            if(cloudConfiguration is null) throw new ArgumentNullException(nameof(cloudConfiguration));
            _SoundCloudConfiguration = cloudConfiguration;
        }
        /// <param name="transcoding">This parameter comes in response to a search request.Please check your json and chooose one version transcoding object</param>
        public async Task<UrlToDownload> GetUrlForDownloadFileAsync(HttpClient client,Transcoding transcoding)
        {
            if (client is null) throw new NullReferenceException(nameof(client));
            var request = new HttpRequestMessage(HttpMethod.Get, transcoding.url);
            request.Headers.Add("Accept", "application/json;charset=utf-8");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _SoundCloudConfiguration.OAuthToken);
            var response = await client.SendAsync(request);
            JsonCorrector.TryJson(response, out string json);
            var model = JsonConvert.DeserializeObject<UrlToDownload>(json);
            if (model is null) throw new NullReferenceException(nameof(model) + " " + nameof(GetUrlForDownloadFileAsync));
            return model;
        }
        public async Task<HttpContent> GetFile(HttpClient client,UrlToDownload urlObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, urlObj.url);
            request.Headers.Add("Accept", "application/json;charset=utf-8");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _SoundCloudConfiguration.OAuthToken);
            var response = await client.SendAsync(request);
            var stream = response.Content;
            if (stream is null) throw new NullReferenceException(nameof(stream));
            return stream;
        }
    
    }
}
