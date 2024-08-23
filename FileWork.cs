using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi
{
    public class FileWork
    {
        private readonly string _filesPath;
        public FileWork(string filesPath)
        {
            _filesPath = filesPath;
        }
        public void AddFile(HttpContent stream, string name)
        {
            name = _filesPath + name;
            if (File.Exists(name)) throw new InvalidOperationException($"file {name} is exist");
            using (var fileStream = new FileStream(name + ".m3u8", FileMode.CreateNew))
            {
               var task = stream.CopyToAsync(fileStream);
               task.Wait();
            }
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
