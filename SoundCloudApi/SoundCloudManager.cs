using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.HelpfullServices;
using TestSoundcloudApi.Models;
using TestSoundcloudApi.ServiceModels;

namespace TestSoundcloudApi
{
    internal class SoundCloudManager : ISoundCloudClient
    {
        private readonly SoundCloudConfigurationModel _SoundCloudConfiguration;
        public SoundCloudManager(SoundCloudConfigurationModel cloudConfiguration)
        {
            if (cloudConfiguration is null) throw new ArgumentNullException(nameof(cloudConfiguration));
            _SoundCloudConfiguration = cloudConfiguration;
        }
        public async Task<Root> SearchAsync(string track, uint limit = 2)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_SoundCloudConfiguration.OAuthToken);
                if (string.IsNullOrWhiteSpace(track)) throw new NullReferenceException(track);
                if (client is null) throw new NullReferenceException(nameof(client));
                string url = string.Format("{0}://{1}.{2}.{3}/search?q={4}&limit={5}", _SoundCloudConfiguration.Protocol, _SoundCloudConfiguration.ApiVersion, _SoundCloudConfiguration.DomenName, _SoundCloudConfiguration.DomenRegion, track, limit);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json;charset=utf-8");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _SoundCloudConfiguration.OAuthToken);
                var response = await client.SendAsync(request);
                JsonCorrector.TryJson(response, out string json);
                var model = JsonConvert.DeserializeObject<Root>(json);
                if (model is null) throw new NullReferenceException(nameof(model) + " " + nameof(SearchAsync));
                return model;
            }

        }
        /// <param name="transcoding">This parameter comes in response to a search request.Please check your json and chooose one version transcoding object</param>
        public async Task<UrlToDownload> GetUrlForDownloadFileAsync(HttpClient client, Transcoding transcoding)
        {
            if (client is null) throw new NullReferenceException(nameof(client));
            var request = new HttpRequestMessage(HttpMethod.Get, transcoding.url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _SoundCloudConfiguration.OAuthToken);
            var response = await client.SendAsync(request);
            JsonCorrector.TryJson(response, out string json);
            var model = JsonConvert.DeserializeObject<UrlToDownload>(json);
            if (model is null) throw new NullReferenceException(nameof(model) + " " + nameof(GetUrlForDownloadFileAsync));
            return model;
        }
        public async Task<HttpContent> GetFile(HttpClient client, UrlToDownload urlObj)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, urlObj.url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _SoundCloudConfiguration.OAuthToken);
            var response = await client.SendAsync(request);
            var stream = response.Content;
            if (stream is null) throw new NullReferenceException(nameof(stream));
            return stream;
        }
        public MusicInfoModel Load(Collection musicobject, uint versionLoaderindex = 0)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var getUrlFromFileTask = GetUrlForDownloadFileAsync(httpClient, musicobject.media.transcodings[(int)versionLoaderindex]);
                getUrlFromFileTask.Wait();
                var urlModel = getUrlFromFileTask.Result;
                if (urlModel is null) throw new NullReferenceException(nameof(urlModel));
                return new MusicInfoModel(musicobject.title, urlModel);
            }
        }
    }
}
