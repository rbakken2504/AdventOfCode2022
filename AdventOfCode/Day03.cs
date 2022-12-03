namespace AdventOfCode;

public sealed class Day03 : BaseDay
{
    private readonly string[] _rucksacks;
    private const int LowerOffset = 96;
    private const int UpperOffset = 38;

    public Day03()
    {
        _rucksacks = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var priority = _rucksacks.Sum(rucksack =>
        {
            var midpoint = rucksack.Length / 2;
            var compartment1 = rucksack[..midpoint];
            var compartment2 = rucksack[(midpoint)..];
            var intersection = compartment1.Intersect(compartment2);
            return intersection.Sum(x => Char.IsUpper(x) ? x - UpperOffset : x - LowerOffset);
        });
        return new ValueTask<string>(priority.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var groupRucksacks = _rucksacks.Chunk(3);
        var priority = groupRucksacks.Sum(group =>
        {
            var intersection = group[0].Intersect(group[1]).Intersect(group[2]);
            return intersection.Sum(x => Char.IsUpper(x) ? x - UpperOffset : x - LowerOffset);;
        });
        return new ValueTask<string>(priority.ToString());
    }
}