using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;

namespace TestSoundcloudApi
{
    public class Program
    {
        // сразу извиняюсь за жесть написанную ниже 
        // это всего лишь тестовая обёртка над api ввиде консли :(
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory() + "\\Music\\";
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
            SoundCloudConfiguration config = new SoundCloudConfiguration("");
            FileWork fileManager = new FileWork(path);
            IFileUrl downloader = new DownloaderAudio(config);
            ISearch searcher = new Searcher(config);
            ILoad loader = new LoaderFile(downloader, fileManager);
            Work(searcher,loader, fileManager);
        }
        static void Work(ISearch searcher, ILoad loader, FileWork fileManager)
        {
            try
            {
                while (true)
                {

                    Console.WriteLine("Choose variant: ");
                    var actions = new Dictionary<int, string>()
                    {
                        {1,"Download"},
                        {2,"My musics" },
                        {3, "Delete music" },
                        {4, "Open music" }
                    };
                    foreach (var action in actions)
                    {
                        Console.WriteLine(action.Key + "." + action.Value);
                    }
                    var all = fileManager.AllFiles();
                    int inp = int.Parse(Console.ReadLine()!);
                    switch (inp)
                    {
                        case 1:
                            Console.Write("Input track: ");
                            string? input = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(input)) continue;
                            var task = searcher.SearchAsync(input);
                            task.Wait();
                            var res = task.Result;
                            Console.WriteLine("Please choose from below: ");
                            Thread.Sleep(3000);
                            int counter = 0;
                            res.collection.ForEach(x =>
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(counter + "." + x.title);
                                Console.ForegroundColor = ConsoleColor.White;
                                counter++;
                            });
                            Console.WriteLine();
                            Console.Write("Please take a id: ");
                            string? input_second = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(input_second)) continue;
                            var collection = res.collection[int.Parse(input_second)];
                            if (collection is null) continue;
                            loader.Load(collection, (ex) => Console.WriteLine(ex.Message));
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Correct");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case 2:
                            if (all.Count <= 0) Console.WriteLine("None");
                            foreach (var file in all)
                            {
                                Console.WriteLine(file.Name);
                            }
                        break;
                        case 3:
                            if (all.Count <= 0)
                            {
                                Console.WriteLine("None");
                                continue;
                            }
                            int count = 0;
                            
                            foreach (var x in all)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(count + "." + x.Name);
                                Console.ForegroundColor = ConsoleColor.White;
                                count++;
                            }
                            Console.Write("Please take a id deleted file: ");
                            string? inputDel = Console.ReadLine();
                            fileManager.RemoveFile(all[int.Parse(inputDel!)].Name);
                        break;
                        case 4:
                            if (all.Count <= 0)
                            {
                                Console.WriteLine("None");
                                continue;
                            }
                            int j = 0;
                            foreach (var x in all)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(j + "." + x.Name);
                                Console.ForegroundColor = ConsoleColor.White;
                                j++;
                            }
                            Console.Write("Please take a id opened file: ");
                            string? inputOp = Console.ReadLine();
                            fileManager.OpenFile(all[int.Parse(inputOp!)].Name);
                        break;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
