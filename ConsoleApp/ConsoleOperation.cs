using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;
using TestSoundcloudApi.ServiceModels;
using TestSoundcloudApi.ServicesInterface;

namespace TestSoundcloudApi
{
    internal delegate void Presentative(string message);
    internal delegate string Writer();
    internal class ConsoleOperation
    {
        internal readonly Presentative Presenter;
        internal readonly Writer Writer;
        internal ConsoleOperation(Presentative presenter, Writer writer)
        {
            this.Presenter = presenter;
            this.Writer = writer;
        }
        internal int ChooseOperation()
        {
            Presenter("Choose variant: ");
            var actions = new Dictionary<int, string>()
            {
                {1,"Download"},
                {2,"My musics" },
                {3, "Delete music" },
                {4, "Open music" }
            };
            foreach (var action in actions)
            {
                Presenter(action.Key + "." + action.Value);
            }

            string? inp = Writer();
            int inpInt = int.Parse(inp!);
            return inpInt;
        }
        internal List<Collection> SearchTracks(ISearch searcher)
        {
            Presenter("Input track: ");
            string? input = Writer();
            if (string.IsNullOrWhiteSpace(input)) throw new NullReferenceException(nameof(input));
            Presenter("Input count answers: ");
            string? input2 = Writer();
            if (string.IsNullOrWhiteSpace(input2)) throw new NullReferenceException(nameof(input));
            uint countAnswers = uint.Parse(input2);
            var task = searcher.SearchAsync(input, countAnswers);
            task.Wait();
            return task.Result.collection;
        }
        internal Collection ChooseTrack(List<Collection> collections)
        {
            Presenter("Please choose from below: ");
            Thread.Sleep(3000);
            int counter = 0;
            collections.ForEach(x =>
            {
                Presenter(counter + "." + x.title);
                counter++;
            });
            Presenter(Environment.NewLine);
            Presenter("Please take a id: ");
            string? input = Writer();
            if (string.IsNullOrWhiteSpace(input)) throw new NullReferenceException(nameof(input));
            if (!int.TryParse(input, out int index)) throw new InvalidCastException(nameof(input));
            return collections[index];
        }
        internal FileInfo ChooseFile(List<FileInfo> list)
        {
            int counter = 0;
            list.ForEach(x =>
            {
                Presenter(counter + "." + x.Name);
                counter++;
            });
            Presenter("Please take a id: ");
            string? input = Writer();
            if (string.IsNullOrWhiteSpace(input)) throw new NullReferenceException(nameof(input));
            if (!int.TryParse(input, out int index)) throw new InvalidCastException(nameof(input));
            return list[index];
        }
        internal MusicInfoModel Load(ILoad loader, Collection collection)
        {
            return loader.Load(collection);
        }
        internal void AddFile(FileManager fileManager, MusicInfoModel musicInfoModel)
        {
            var task = fileManager.AddFile(musicInfoModel.Url.url, musicInfoModel.Name);
            task.Wait();
            Presenter("Correct");
        }
        internal IList<FileInfo> WriteAll(FileManager fileManager)
        {
            return fileManager.AllFiles();
        }
        internal void Delete(FileManager fileManager, FileInfo file)
        {
            fileManager.RemoveFile(file.Name);
            Presenter("Correct");
        }
        internal void Open(FileManager fileManager, FileInfo file)
        {
            fileManager.OpenFile(file.Name);
            Presenter("Correct");
        }
    }
}
