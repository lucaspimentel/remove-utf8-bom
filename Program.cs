if (args.Length == 0)
{
    Console.WriteLine("Usage: <app> <path to directory>");
    return;
}

var path = args[0];

if (!Directory.Exists(path))
{
    Console.WriteLine("Directory does not exist.");
    return;
}

var filenameExtensions = new string[] { "*.cs", "*.cshtml", ".js", ".css", ".html", ".json", ".xml", ".txt", "*.sh", "*.md", "*.csproj", "*.sln" };

var filenames = from filenameExtension in filenameExtensions
                from filename in Directory.GetFiles(path, filenameExtension, SearchOption.AllDirectories)
                where !filename.Contains(".git" + Path.DirectorySeparatorChar)
                select filename;

int totalCount = 0;
int modifiedCount = 0;

foreach (var filename in filenames)
{
    totalCount++;

    // find files that start with bytes: 0xEF, 0xBB, 0xBF
    using var file = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

    if (file.ReadByte() == 0xEF &&
        file.ReadByte() == 0xBB &&
        file.ReadByte() == 0xBF)
    {
        var bytes = new byte[file.Length - 3];
        await file.ReadExactlyAsync(bytes);

        file.Seek(0, SeekOrigin.Begin);
        file.SetLength(file.Length - 3);
        await file.WriteAsync(bytes);

        file.Close();

        modifiedCount++;
        Console.WriteLine($"Removed BOM from {filename}");
    }
}

Console.WriteLine($"Modified {modifiedCount} files out of {totalCount}.");
