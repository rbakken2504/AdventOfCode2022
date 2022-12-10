namespace AdventOfCode;

public sealed class Day09 : BaseDay
{
    private readonly string[] _moves;

    public Day09()
    {
        _moves = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var rope = new Rope();
        foreach (var move in _moves)
        {
            var parts = move.Split(' ');
            var direction = parts[0];
            var numOfSteps = Int32.Parse(parts[1]);
            rope.ApplyMove(direction, numOfSteps);
        }
        /*Console.WriteLine("HEAD");
        rope.Head.Print();
        Console.WriteLine("TAIL");
        rope.Tail.Print();*/
        return new ValueTask<string>(rope.Tail.SpaceVisited.Count(x => x.Value).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var rope = new LongRope(10);
        foreach (var move in _moves)
        {
            var parts = move.Split(' ');
            var direction = parts[0];
            var numOfSteps = Int32.Parse(parts[1]);
            rope.ApplyMove(direction, numOfSteps);
        }
        return new ValueTask<string>(rope.Tail.SpaceVisited.Count(x => x.Value).ToString());
    }
}

public class Rope
{
    public Knot Head { get; set; } = new();
    public Knot Tail { get; set; } = new();
    
    public void ApplyMove(string direction, int numOfSteps)
    {
        for (var i = 1; i <= numOfSteps; i++)
        {
            switch (direction)
            {
                case "R":
                    Head.ApplyMove(Head.X + 1, Head.Y);
                    break;
                case "L":
                    Head.ApplyMove(Head.X - 1, Head.Y);
                    break;
                case "U":
                    Head.ApplyMove(Head.X, Head.Y + 1);
                    break;
                case "D":
                    Head.ApplyMove(Head.X, Head.Y - 1);
                    break;
            }
            Tail.AdjustBasedOn(Head);
        }
    }
}

public class LongRope
{
    public List<Knot> Knots { get; set; }

    public Knot Tail => Knots.Last();

    public LongRope(int knots)
    {
        Knots = Enumerable.Range(0, knots).Select(_ => new Knot()).ToList();
    }

    public void ApplyMove(string direction, int numOfSteps)
    {
        for (var i = 1; i <= numOfSteps; i++)
        {
            var firstKnot = Knots.First();
            var nextKnot = Knots.ElementAt(1);
            switch (direction)
            {
                case "R":
                    firstKnot.ApplyMove(firstKnot.X + 1, firstKnot.Y);
                    break;
                case "L":
                    firstKnot.ApplyMove(firstKnot.X - 1, firstKnot.Y);
                    break;
                case "U":
                    firstKnot.ApplyMove(firstKnot.X, firstKnot.Y + 1);
                    break;
                case "D":
                    firstKnot.ApplyMove(firstKnot.X, firstKnot.Y - 1);
                    break;
            }
            nextKnot?.AdjustBasedOn(firstKnot);
            foreach (var subsequentKnot in Knots.GetRange(2, 8))
            {
                subsequentKnot.AdjustBasedOn(nextKnot);
                nextKnot = subsequentKnot;
            }
        }
    }
}

public class Knot
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    public Dictionary<(int, int), bool> SpaceVisited { get; set; } = new();

    public Knot()
    {
        SpaceVisited.Add((0, 0), true);
    }

    public void AdjustBasedOn(Knot head)
    {
        var yDiff = head.Y - Y;
        var xDiff = head.X - X;
        var newY = yDiff > 0 ? Y + 1 : Y - 1;
        var newX = xDiff > 0 ? X + 1 : X - 1;
        if (Math.Abs(yDiff) == 2 && xDiff == 0)
        {
            ApplyMove(X, newY);
        }

        if (Math.Abs(xDiff) == 2 && yDiff == 0)
        {
            ApplyMove(newX, Y);
        }

        if (IsTouching(head))
        {
            return;
        }
        
        ApplyMove(newX, newY);
    }

    public bool IsTouching(Knot otherKnot)
    {
        var xDiff = X - otherKnot.X;
        var yDiff = Y - otherKnot.Y;
        return xDiff is >= -1 and <= 1 && yDiff is >= -1 and <= 1;
    }

    public void ApplyMove(int x, int y)
    {
        if (!SpaceVisited.ContainsKey((x, y)))
        {
            SpaceVisited.Add((x,y), true);
        }

        X = x;
        Y = y;
    }

    public void Print()
    {
        for (var y = 5; y >= 0; y--)
        {
            for (var x = 0; x < 6; x++)
            {
                if (SpaceVisited.ContainsKey((x, y)) && SpaceVisited[(x, y)])
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
    }
}