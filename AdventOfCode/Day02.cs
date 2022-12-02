namespace AdventOfCode;

public sealed class Day02 : BaseDay
{
    private readonly string[] _lines;
    private readonly Dictionary<string, int> _shapeSelectedPointValues = new()
    {
        { "A", 1 },
        { "B", 2 },
        { "C", 3 },
        { "X", 1 },
        { "Y", 2 },
        { "Z", 3 },
    };
    private readonly Dictionary<string, int> _outcomePointValues = new()
    {
        { "AX", 3 },
        { "AY", 6 },
        { "AZ", 0 },
        { "BX", 0 },
        { "BY", 3 },
        { "BZ", 6 },
        { "CX", 6 },
        { "CY", 0 },
        { "CZ", 3 }
    };
    private readonly Dictionary<string, string> _shapeNeeded = new()
    {
        { "AX", "Z" },
        { "AY", "X" },
        { "AZ", "Y" },
        { "BX", "X" },
        { "BY", "Y" },
        { "BZ", "Z" },
        { "CX", "Y" },
        { "CY", "Z" },
        { "CZ", "X" }
    };

    public Day02()
    {
        _lines = File.ReadAllText(InputFilePath).Split("\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var points = _lines.Sum(line =>
        {
            var characters = line.Split(' ');
            var opponent = characters[0];
            var player = characters[1];
            return EvalMatchPoints(opponent, player);
        });
        return new ValueTask<string>(points.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var points = _lines.Sum(line =>
        {
            var characters = line.Split(' ');
            var opponent = characters[0];
            var player = _shapeNeeded[$"{opponent}{characters[1]}"];
            return EvalMatchPoints(opponent, player);
        });
        return new ValueTask<string>(points.ToString());
    }

    private int EvalMatchPoints(string opponentCode, string playerCode)
    {
        var shapeSelectedPts = _shapeSelectedPointValues[playerCode];
        var outcomePts = _outcomePointValues[$"{opponentCode}{playerCode}"];
        return shapeSelectedPts + outcomePts;
    }
}