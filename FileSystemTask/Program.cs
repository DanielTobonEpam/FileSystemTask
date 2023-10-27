using FileSystemTask;
using SimpleInjector;

internal class Program
{
    private static void Main(string[] args)
    {
        //var fileSystemVisitor = new FileSystemVisitor(args[0]);

        //foreach (var fileSystemInfo in fileSystemVisitor.Traverse())
        //{
        //    Console.WriteLine(fileSystemInfo.FullName);
        //}
        var container = new Container();

        container.Register<FileSystemVisitor>(() => new FileSystemVisitor(""));

        var fileSystemVisitor = container.GetInstance<FileSystemVisitor>();

        fileSystemVisitor.RootDirectory = args[0];

        foreach (var fileSystemInfo in fileSystemVisitor.Traverse())
        {
            Console.WriteLine(fileSystemInfo.FullName);
        }
    }
}