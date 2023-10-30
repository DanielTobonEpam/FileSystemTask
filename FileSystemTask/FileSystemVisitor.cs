using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemTask
{
    public class FileSystemVisitor : IFilesystemVisitor
    {
        public event EventHandler<string> Start;
        public event EventHandler<string> Finish;
        public event EventHandler<string> FileFound;
        public event EventHandler<string> DirectoryFound;
        public event EventHandler<string> FilteredFileFound;
        public event EventHandler<string> FilteredDirectoryFound;
    
        private readonly string _rootDirectory;
        private readonly Func<FileSystemInfo, bool> _filter;

            private bool abortSearch;
    private bool excludeItems;

        public FileSystemVisitor(string rootDirectory)
        {
            //Event: start
            Start?.Invoke(this, rootDirectory);
            _rootDirectory = rootDirectory;
            _filter = null;
        }

        public FileSystemVisitor(string rootDirectory, Func<FileSystemInfo, bool> filter)
        {
            _rootDirectory = rootDirectory;
            _filter = filter;
        }

        public string RootDirectory { get; set; }

        public IEnumerable<FileSystemInfo> Traverse()
        {
            if (abortSearch)
            {
                //Event: Finish
                Finish?.Invoke(this, RootDirectory);
                yield break;
            }

            var queue = new Queue<DirectoryInfo>();
            queue.Enqueue(new DirectoryInfo(RootDirectory));

            while (queue.Count > 0)
            {
                var directory = queue.Dequeue();

                foreach (var file in directory.GetFiles())
                {
                    //Event: Filefound
                    FileFound?.Invoke(this, file.Name);

                    if (_filter != null && !_filter(file))
                    {
                        continue;
                    }

                    yield return file;
                }

                foreach (var subdirectory in directory.GetDirectories())
                {
                    //Event: Directory found
                    DirectoryFound?.Invoke(this, subdirectory.Name);
                    if (_filter != null && !_filter(subdirectory))
                    {
                        //Event: Filtered file found
                        FilteredFileFound?.Invoke(this, subdirectory.Name);
                        continue;
                    }

                    yield return subdirectory;

                    queue.Enqueue(subdirectory);
                }
            }
        }
    }
}

