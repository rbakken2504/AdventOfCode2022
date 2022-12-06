namespace AdventOfCode;

public sealed class Day06 : BaseDay
{
    private readonly string _input;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>(FindStartOfMarkerWithDistinct(4));
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>(FindStartOfMarkerWithDistinct(14));
    }

    private string FindStartOfMarkerWithDistinct(int distinctNumOfChars)
    {
        var startIndex = 0;
        var marker = "";
        for (var i = 0; i < _input.Length; i++)
        {
            var character = _input[i];
            if (!marker.Contains(character))
            {
                marker += character;
            }
            else
            {
                marker = "";
                i = startIndex;
                startIndex++;
            }

            if (marker.Length == distinctNumOfChars)
            {
                return (i + 1).ToString();
            }
        }
        return "NOT FOUND";
    }
}