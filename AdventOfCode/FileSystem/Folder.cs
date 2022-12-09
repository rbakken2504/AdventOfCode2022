namespace AdventOfCode.FileSystem;

public class Folder
{
    public string Name { get; set; }
    public List<File> Files { get; set; } = new();
    public List<Folder> Folders { get; set; } = new();
    public Folder Parent { get; set; }

    public int Size => Files.Sum(x => x.Size) + Folders.Sum(x => x.Size);

    public Folder(string name, Folder parent)
    {
        Name = name;
        Parent = parent;
    }

    public Folder GetFolder(string name)
    {
        return Folders.First(x => x.Name == name);
    }

    public IEnumerable<Folder> GetAllFolders()
    {
        return Folders.SelectMany(x => x.GetAllFolders()).Append(this).ToList();
    }
}