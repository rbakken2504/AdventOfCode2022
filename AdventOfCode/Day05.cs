namespace AdventOfCode;

public sealed class Day05 : BaseDay
{
    private readonly string[] _stackLines;
    private readonly string[] _moveLines;

    public Day05()
    {
        var input = File.ReadAllText(InputFilePath).Split('\n').ToList();
        var emptyIdx = input.IndexOf("");
        _stackLines = input.ToArray()[..emptyIdx];
        _moveLines = input.ToArray()[(emptyIdx + 1)..];
    }

    public override ValueTask<string> Solve_1()
    {
        var stack = new StackArrangement(_stackLines);
        var moves = _moveLines.Select(move => new CrateMove(move));
        foreach (var crateMove in moves)
        {
            stack.ApplyMove(crateMove);
        }
        return new ValueTask<string>(stack.GetTopOfStacks());
    }

    public override ValueTask<string> Solve_2()
    {
        var stack = new StackArrangement(_stackLines);
        var moves = _moveLines.Select(move => new CrateMove(move));
        foreach (var crateMove in moves)
        {
            stack.ApplyMove2(crateMove);
        }
        return new ValueTask<string>(stack.GetTopOfStacks());
    }
}

internal class CrateMove
{
    public int NumOfCrates { get; set; }
    public int From { get; set; }
    public int To { get; set; }

    public CrateMove(string crateMove)
    {
        var parts = crateMove.Split(' ').ToList();
        var moveIdx = parts.IndexOf("move") + 1;
        var fromIdx = parts.IndexOf("from") + 1;
        var toIdx = parts.IndexOf("to") + 1;
        NumOfCrates = Int32.Parse(parts.ElementAt(moveIdx));
        From = Int32.Parse(parts.ElementAt(fromIdx));
        To = Int32.Parse(parts.ElementAt(toIdx));
    }
}

internal class StackArrangement
{
    private readonly Dictionary<int, Stack<char>> _stackArrangement = new();

    public StackArrangement(string[] arrangement)
    {
        CreateStacks(arrangement.Last());
        ApplyCrates(arrangement.Where(x => x != arrangement.Last()));
    }

    public void ApplyMove(CrateMove move)
    {
        for (var i = 0; i < move.NumOfCrates; i++)
        {
            var fromStackIdx = move.From - 1;
            var toStackIdx = move.To - 1;
            var crate = _stackArrangement[fromStackIdx].Pop();
            _stackArrangement[toStackIdx].Push(crate);
        }
    }

    public void ApplyMove2(CrateMove move)
    {
        var fromStackIdx = move.From - 1;
        var toStackIdx = move.To - 1;
        // Pop all the crates off in order and reverse so we put them back on in the correct order
        var cratesToMove = Enumerable.Range(0, move.NumOfCrates).Select(_ => _stackArrangement[fromStackIdx].Pop()).Reverse();
        foreach (var crate in cratesToMove)
        {
            _stackArrangement[toStackIdx].Push(crate);
        }
    }

    public string GetTopOfStacks()
    {
        return String.Join("", _stackArrangement.Select(stack => stack.Value.Peek()));
    }
    
    public void Print()
    {
        var printStacks = new Dictionary<int, Stack<char>>(_stackArrangement);
        foreach (var printStack in printStacks)
        {
            var crates = String.Join(' ', printStack.Value.Select(x => x));
            Console.WriteLine($"{printStack.Key} -> {crates}");
        }
        Console.WriteLine();
    }

    private void CreateStacks(string stackNumbers)
    {
        var numOfStacks = Int32.Parse(stackNumbers.Where(Char.IsDigit).Last().ToString());
        for (var i = 0; i < numOfStacks; i++)
        {
            _stackArrangement.Add(i, new Stack<char>());
        }
    }

    private void ApplyCrates(IEnumerable<string> crateArrangements)
    {
        foreach (var crateArrangement in crateArrangements.Reverse())
        {
            var crateIdx = 1;
            for (var i = 0; i < _stackArrangement.Count; i++)
            {
                var crate = crateArrangement[crateIdx];
                if (Char.IsLetter(crate))
                {
                    _stackArrangement[i].Push(crate);    
                }
                crateIdx += 4;
            }
        }
    }
}