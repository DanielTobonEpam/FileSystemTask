using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemTask
{
    public interface IFilesystemVisitor
    {
        string GetRootDirectory();
        IEnumerable<FileSystemInfo> Traverse();
    }
}
