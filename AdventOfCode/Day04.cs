namespace AdventOfCode;

public sealed class Day04 : BaseDay
{
    private readonly string[] _pairs;

    public Day04()
    {
        _pairs = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var completeOverlaps = _pairs.Sum(pair =>
        {
            var (elf1, elf2) = BuildAssignmentPair(pair);
            if (elf1.Contains(elf2) || elf2.Contains(elf1))
            {
                return 1;
            }

            return 0;
        });
        return new ValueTask<string>(completeOverlaps.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var partialOverlaps = _pairs.Sum(pair =>
        {
            var (elf1, elf2) = BuildAssignmentPair(pair);
            if (elf1.Overlaps(elf2) || elf2.Overlaps(elf1))
            {
                return 1;
            }

            return 0;
        });
        return new ValueTask<string>(partialOverlaps.ToString());
    }

    private static Tuple<SectionAssignment, SectionAssignment> BuildAssignmentPair(string pair)
    {
        var elfAssignments = pair.Split(',');
        var elf1 = new SectionAssignment(elfAssignments[0]);
        var elf2 = new SectionAssignment(elfAssignments[1]);
        return new Tuple<SectionAssignment, SectionAssignment>(elf1, elf2);
    }
}

internal class SectionAssignment
{
    public int From { get; set; }
    public int To { get; set; }

    public SectionAssignment(string sections)
    {
        var sectionSplit = sections.Split('-');
        From = Int32.Parse(sectionSplit[0]);
        To = Int32.Parse(sectionSplit[1]);
    }

    public bool Contains(SectionAssignment other)
    {
        return other.From >= From && other.To <= To;
    }

    public bool Overlaps(SectionAssignment other)
    {
        return other.From <= To && other.From >= From || other.To >= From && other.To <= To;
    }
}