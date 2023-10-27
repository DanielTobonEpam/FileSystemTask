using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemTask
{
    //public class FileSystemVisitor
    //{
    //    private readonly string _rootDirectory;
    //    private readonly Func<FileSystemInfo, bool> _filter;

    //    public FileSystemVisitor(string rootDirectory)
    //    {
    //        _rootDirectory = rootDirectory;
    //        _filter = null;
    //    }

    //    public FileSystemVisitor(string rootDirectory, Func<FileSystemInfo, bool> filter)
    //    {
    //        _rootDirectory = rootDirectory;
    //        _filter = filter;
    //    }

    //    public IEnumerable<FileSystemInfo> Traverse()
    //    {
    //        var queue = new Queue<DirectoryInfo>();
    //        queue.Enqueue(new DirectoryInfo(_rootDirectory));

    //        while (queue.Count > 0)
    //        {
    //            var directory = queue.Dequeue();

    //            foreach (var file in directory.GetFiles())
    //            {
    //                if (_filter != null && !_filter(file))
    //                {
    //                    continue;
    //                }

    //                yield return file;
    //            }

    //            foreach (var subdirectory in directory.GetDirectories())
    //            {
    //                if (_filter != null && !_filter(subdirectory))
    //                {
    //                    continue;
    //                }

    //                yield return subdirectory;

    //                queue.Enqueue(subdirectory);
    //            }
    //        }
    //    }
    //}
    public class FileSystemVisitor
    {
        private readonly string _rootDirectory;
        private readonly Func<FileSystemInfo, bool> _filter;

        public FileSystemVisitor(string rootDirectory)
        {
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
            var queue = new Queue<DirectoryInfo>();
            queue.Enqueue(new DirectoryInfo(RootDirectory));

            while (queue.Count > 0)
            {
                var directory = queue.Dequeue();

                foreach (var file in directory.GetFiles())
                {
                    if (_filter != null && !_filter(file))
                    {
                        continue;
                    }

                    yield return file;
                }

                foreach (var subdirectory in directory.GetDirectories())
                {
                    if (_filter != null && !_filter(subdirectory))
                    {
                        continue;
                    }

                    yield return subdirectory;

                    queue.Enqueue(subdirectory);
                }
            }
        }
    }
}

