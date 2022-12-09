namespace AdventOfCode.FileSystem;

public class File
{
    public string Name { get; set; }
    public int Size { get; set; }

    public File(string name, string size)
    {
        Name = name;
        Size = Int32.Parse(size);
    }
}