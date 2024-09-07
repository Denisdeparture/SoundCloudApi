using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi.HelpfullServices
{
    internal static class JsonCorrector
    {
        internal static string TryJson(HttpResponseMessage response, out string json)
        {
            if (response is null) throw new NullReferenceException($"Resonse  was null");
            if (!response.IsSuccessStatusCode) throw new Exception($"Resonse  was {response.StatusCode} is incorrect");
            var task = response.Content.ReadAsStringAsync();
            task.Wait();
            json = task.Result;
            if (string.IsNullOrWhiteSpace(json)) throw new NullReferenceException(nameof(json));
            return json;
        }
    }
}
