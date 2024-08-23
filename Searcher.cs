using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;

namespace TestSoundcloudApi
{
    public class Searcher : ISearch
    {
        private readonly SoundCloudConfiguration _SoundCloudConfiguration;
        public Searcher(SoundCloudConfiguration cloudConfiguration)
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
    }
}
