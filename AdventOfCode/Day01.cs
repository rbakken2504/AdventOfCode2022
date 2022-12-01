namespace AdventOfCode;

public sealed class Day01 : BaseDay
{
    private readonly string[] _lines;

    public Day01()
    {
        _lines = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var maxCalories = 0;
        var curElfCalories = 0;
        foreach (var line in _lines)
        {
            if (String.IsNullOrEmpty(line))
            {
                if (maxCalories < curElfCalories)
                {
                    maxCalories = curElfCalories;
                }

                curElfCalories = 0;
                continue;
            }
            curElfCalories += Int32.Parse(line!);
        }

        return new ValueTask<string>(maxCalories.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var curCalories = 0;
        var topThreeElves = new SortedList<int, int>
        {
            { 0, 0 }
        };
        foreach (var line in _lines)
        {
            if (String.IsNullOrEmpty(line))
            {
                topThreeElves = UpdateTopThree(topThreeElves, curCalories);
                curCalories = 0;
                continue;
            }
            curCalories += Int32.Parse(line!);
        }

        // Add last elf
        topThreeElves = UpdateTopThree(topThreeElves, curCalories);
        return new ValueTask<string>(topThreeElves.Sum(x => x.Value).ToString());

    }

    private static SortedList<int, int> UpdateTopThree(SortedList<int, int> topThreeElves, int curElf)
    {
        var minElf = topThreeElves.First();
        if (minElf.Value >= curElf)
        {
            return topThreeElves;
        }
        
        if (topThreeElves.Count == 3)
        {
            topThreeElves.Remove(minElf.Key);    
        }
        topThreeElves.Add(curElf, curElf);

        return topThreeElves;
    }
}
