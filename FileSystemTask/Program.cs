using FileSystemTask;
using SimpleInjector;

internal class Program
{
    private static void Main(string[] args)
    {
        var container = new Container();

        container.Register<IFilesystemVisitor>(() => new FileSystemVisitor(""));

        var fileSystemVisitor = container.GetInstance<IFilesystemVisitor>();

        fileSystemVisitor.RootDirectory = args[0];

        foreach (var fileSystemInfo in fileSystemVisitor.Traverse())
        {
            Console.WriteLine(fileSystemInfo.FullName);
        }
    }
}