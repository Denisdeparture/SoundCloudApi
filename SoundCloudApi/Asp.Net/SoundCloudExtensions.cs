using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.ServiceModels;

namespace TestSoundcloudApi.Asp.Net
{
    public static class SoundCloudExtensions
    {
        public static IServiceCollection AddSoundCloudClient(this IServiceCollection services, SoundCloudConfigurationModel model) => services.AddTransient(x => new SoundCloudClient(model));
    }
}
