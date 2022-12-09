namespace AdventOfCode;

public sealed class Day07 : BaseDay
{
    private readonly string[] _terminalOutput;

    public Day07()
    {
        _terminalOutput = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var fileSystem = new FileSystem.FileSystem(_terminalOutput);
        var folders = fileSystem.FindFoldersWithSizeLessThan(100001);
        return new ValueTask<string>(folders.Sum(x => x.Size).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var fileSystem = new FileSystem.FileSystem(_terminalOutput);
        var spaceToFreeUp = 30000000 - fileSystem.SpaceAvailable;
        var folder = fileSystem.FindFolderToFreeUp(spaceToFreeUp);
        return new ValueTask<string>(folder.Size.ToString());
    }
}