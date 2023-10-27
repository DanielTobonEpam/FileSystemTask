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

        //The next code is only to test the events... need to correct later
        var visitor = new FileSystemVisitor(args[0]);
        //Events
        visitor.Start += (sender, path) => Console.WriteLine($"Started searching at: {path}");
        visitor.Finish += (sender, path) => Console.WriteLine($"Finished searching at: {path}");
        visitor.FileFound += (sender, file) => Console.WriteLine($"File found: {file}");
        visitor.DirectoryFound += (sender, folder) => Console.WriteLine($"Directory found: {folder}");
        visitor.FilteredFileFound += (sender, file) => Console.WriteLine($"Filtered file found: {file}");
        visitor.FilteredDirectoryFound += (sender, folder) => Console.WriteLine($"Filtered directory found: {folder}");

        foreach (var fileSystemInfo in fileSystemVisitor.Traverse())
        {
            Console.WriteLine(fileSystemInfo.FullName);
        }
    }
}