using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi.ServiceModels
{
    public record SoundCloudConfigurationModel(string OAuthToken, string Protocol = "https", string DomenName = "soundcloud", string DomenRegion = "com", string ApiVersion = "api-v2");
}
