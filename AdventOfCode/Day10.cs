namespace AdventOfCode;

public sealed class Day10 : BaseDay
{
    private readonly string[] _instructions;

    public Day10()
    {
        _instructions = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var clockCircuit = new ClockCircuit(_instructions);
        return new ValueTask<string>(clockCircuit.SumOfSignalStrengthsAtCycles(new List<int> { 20, 60, 100, 140, 180, 220 }).ToString());
    }

    public override ValueTask<string> Solve_2()
    {

        var clockCircuit = new ClockCircuit(_instructions, true);
        return new ValueTask<string>("");
    }
}

public class ClockCircuit
{
    public int Cycle { get; set; }
    public int X { get; set; } = 1;
    public List<(int cycle, int x)> SignalStrengths = new () { (0,1) };

    public ClockCircuit(string[] commands, bool print = false)
    {
        foreach (var command in commands)
        {
            var commandParts = command.Split(' ');
            var cmd = commandParts.First();
            switch (cmd)
            {
                case "noop":
                    Cycle++;
                    SignalStrengths.Add((Cycle, X));
                    if (print)
                    {
                        RenderPixel();    
                    }
                    break;
                case ("addx"):
                    Cycle++;
                    SignalStrengths.Add((Cycle, X));
                    if (print)
                    {
                        RenderPixel();    
                    }
                    Cycle++;
                    SignalStrengths.Add((Cycle, X));
                    if (print)
                    {
                        RenderPixel();    
                    }
                    X += Int32.Parse(commandParts.Last());
                    break;
            }
        }
    }

    public void RenderPixel()
    {
        var cycle = Cycle == 0 ? Cycle : Cycle - 1;
        var pixelDrawn = Cycle % 40;
        
        if (X - 1 == pixelDrawn - 1 || X == pixelDrawn - 1 || X + 1 == pixelDrawn - 1)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }

        if (pixelDrawn % 40 == 0)
        {
            Console.WriteLine();    
        }
    }

    public int SumOfSignalStrengthsAtCycles(IEnumerable<int> cycleNumbers)
    {
        return cycleNumbers.Sum(cycleNum =>
        {
            var element = SignalStrengths.ElementAt(cycleNum);
            return element.x * element.cycle;
        });
    }
}