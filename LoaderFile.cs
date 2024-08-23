using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;

namespace TestSoundcloudApi
{
    public delegate void LoggingError(Exception ex);
    public class LoaderFile : ILoad
    {
        private readonly FileWork _fileWork;
        private readonly IFileUrl _DownloaderAudio;
        public LoaderFile(IFileUrl downloaderAudio, FileWork fileWork)
        {
            _DownloaderAudio = downloaderAudio;
            _fileWork = fileWork;
        }
        public void Load(Collection musicobject, LoggingError logging, uint versionLoaderindex = 0)
        {
            using(HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var getUrlFromFileTask = _DownloaderAudio.GetUrlForDownloadFileAsync(httpClient, musicobject.media.transcodings[(int)versionLoaderindex]);
                    getUrlFromFileTask.Wait();
                    var res = getUrlFromFileTask.Result;
                    if (res is null) throw new NullReferenceException(nameof(res));
                    var getfileTask = _DownloaderAudio.GetFile(httpClient, res);
                    getfileTask.Wait();
                    var res2 = getfileTask.Result;
                    _fileWork.AddFile(res2, musicobject.title);

                }
                catch (Exception ex)
                {
                   logging(ex);
                }
                finally
                {
                    httpClient.Dispose();
                }
                
            }
        }
    }
}
