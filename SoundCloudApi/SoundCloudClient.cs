using TestSoundcloudApi.HelpfullServices;
using TestSoundcloudApi.ServiceModels;
using TestSoundcloudApi.ServicesInterface;

namespace TestSoundcloudApi
{
    /// <summary>
    /// It's realization pattern UnitOfWork
    /// </summary>
    public partial class SoundCloudClient : IDisposable
    {
        
        private readonly ISoundCloudClient _client;
        public SoundCloudClient(SoundCloudConfigurationModel model)
        {
            if(!ModelValidator.ModelIsValid(model)) throw new NullReferenceException(nameof(model));
            _client = new SoundCloudManager(model);
        }
        public ISearch Searcher
        {
            get
            {
                return _client;
            }
        }
        public ILoad Loader
        {
            get
            {
                return _client;
            }
        }
        public IDownload Downloader
        {
            get
            {
                return _client;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(_client);
            GC.Collect();
        }
    }
    public partial class SoundCloudClient
    {
        public static SoundCloudClient CreateClient(SoundCloudConfigurationModel model) => new SoundCloudClient(model);
    }
}
