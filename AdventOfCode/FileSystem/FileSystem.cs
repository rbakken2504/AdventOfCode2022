namespace AdventOfCode.FileSystem;

public class FileSystem
{
    public Folder RootFolder { get; set; } = new("/", null);
    public int TotalDiskSpace = 70000000;
    public int SpaceAvailable => TotalDiskSpace - RootFolder.Size;

    public FileSystem(IEnumerable<string> terminalOutput)
    {
        BuildFileSystemFromTerminalOutput(terminalOutput);
    }

    public IEnumerable<Folder> FindFoldersWithSizeLessThan(int size)
    {
        return RootFolder.GetAllFolders().Where(x => x.Size < size).ToList();
    }

    public Folder FindFolderToFreeUp(int size)
    {
        return RootFolder.GetAllFolders().Where(x => x.Size > size).MinBy(x => x.Size);
    }

    private void BuildFileSystemFromTerminalOutput(IEnumerable<string> terminalOutput)
    {
        var curDir = RootFolder;
        foreach (var output in terminalOutput)
        {
            if (output.StartsWith("$"))
            {
                var command = output.Trim('$').Trim();
                if (!command.StartsWith("cd"))
                {
                    // If LS we skip doing anything
                    continue;
                }
                
                var target = command.Split(' ').Last();
                curDir = target switch
                {
                    "/" => RootFolder,
                    ".." => curDir.Parent,
                    _ => curDir.GetFolder(target)
                };
            }
            else
            {
                if (output.StartsWith("dir"))
                {
                    var folder = new Folder(output.Split(' ').Last(), curDir);
                    curDir.Folders.Add(folder);
                }
                else
                {
                    var parts = output.Split(' ');
                    var file = new File(parts.Last(), parts.First());
                    curDir.Files.Add(file);
                }
            }
        }
    }
}