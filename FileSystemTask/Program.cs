using FileSystemTask;

internal class Program
{
    private static void Main(string[] args)
    {
        var fileSystemVisitor = new FileSystemVisitor(args[0]);

        foreach (var fileSystemInfo in fileSystemVisitor.Traverse())
        {
            Console.WriteLine(fileSystemInfo.FullName);
        }
    }
}