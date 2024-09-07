using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace TestSoundcloudApi
{
    public class FileManager
    {
        private readonly string _filesPath;
        public FileManager(string filesPath)
        {
            _filesPath = filesPath;
        }
        public async Task AddFile(string pathToFile, string name)
        {
            var mediaInfo = await FFmpeg.GetMediaInfo(pathToFile);
            var conversion = await FFmpeg.Conversions.FromSnippet.ExtractAudio(mediaInfo.Path, _filesPath + $"\\{name}.mp3");
            conversion.OnProgress += async (sender, args) =>
            {
                await Console.Out.WriteLineAsync($"[{args.Duration}/{args.TotalLength}][{args.Percent}%]");
            };
            var task = conversion.SetOutputFormat(Format.mp3).Start();
            task.Wait();
        }
        public void RemoveFile(string name)
        {
            name = _filesPath + name;
            if (!File.Exists(name)) throw new NullReferenceException($"file {name} is not exist");
            File.Delete(name);
        }
        public IList<FileInfo> AllFiles() => new DirectoryInfo(_filesPath).GetFiles().ToList();
        public void OpenFile(string name)
        {
            name = _filesPath + name;
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(name)
            {
                UseShellExecute = true
            };
            p.Start();
        }

    }
}
