using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using TestSoundcloudApi.Models;
using TestSoundcloudApi.ServiceModels;
using TestSoundcloudApi.ServicesInterface;

namespace TestSoundcloudApi
{
    public class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory() + "\\Music\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            SoundCloudConfigurationModel config = new SoundCloudConfigurationModel("your oauth token");
            FileManager fileManager = new FileManager(path);
            using (var client = SoundCloudClient.CreateClient(config))
            {
                IDownload downloader = client.Downloader;
                ISearch searcher = client.Searcher;
                ILoad loader = client.Loader;
                Work(searcher, loader, fileManager);
            }
        }
        static void Work(ISearch searcher, ILoad loader, FileManager fileManager)
        {
            while (true)
            {
                ConsoleOperation console = new ConsoleOperation(Console.WriteLine, Console.ReadLine!);
                try
                {
                    var inpInt = console.ChooseOperation();
                    switch (inpInt)
                    {
                        case 1:
                            console.AddFile(fileManager, console.Load(loader, console.ChooseTrack(console.SearchTracks(searcher))));
                            break;
                        case 2:
                            console.WriteAll(fileManager);
                            break;
                        case 3:
                            console.Delete(fileManager, console.ChooseFile(console.WriteAll(fileManager).ToList()));
                            break;
                        case 4:
                            console.Open(fileManager, console.ChooseFile(console.WriteAll(fileManager).ToList()));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    continue;
                }

            }
            
        }
    }
}
