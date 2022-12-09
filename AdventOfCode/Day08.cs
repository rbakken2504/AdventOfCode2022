namespace AdventOfCode;

public sealed class Day08 : BaseDay
{
    private readonly string[] _lines;

    public Day08()
    {
        _lines = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var grid = new TreeGrid(_lines);
        return new ValueTask<string>(grid.GetNumOfTreesVisible().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var grid = new TreeGrid(_lines);
        return new ValueTask<string>(grid.GetHighestScenicScore().ToString());
    }
}

public class TreeGrid
{
    public int XLength { get; set; }
    public int YLength { get; set; }
    public double[,] Rows { get; set; }

    public TreeGrid(string[] rows)
    {
        XLength = rows.First().Length;
        YLength = rows.Length;
        Rows = new double[XLength, YLength];
        for (var rowNum = 0; rowNum < rows.Length; rowNum++)
        {
            var row = rows[rowNum];
            for (var colNum = 0; colNum < row.Length; colNum++)
            {
                var height = Char.GetNumericValue(row[colNum]);
                Rows[rowNum, colNum] = height;
            }
        }
    }

    public int GetNumOfTreesVisible()
    {
        var count = 0;
        for (var x = 0; x < XLength; x++)
        {
            for (var y = 0; y < YLength; y++)
            {
                if (IsTreeVisible(x, y))
                {
                    count++;
                }
            }
        }

        return count;
    }

    public int GetHighestScenicScore()
    {
        var score = 0;
        for (var x = 0; x < XLength; x++)
        {
            for (var y = 0; y < YLength; y++)
            {
                var curTreeScore = GetScenicScore(x, y);
                if (curTreeScore > score)
                {
                    score = curTreeScore;
                }
            }
        }

        return score;
    }

    private bool IsTreeVisible(int x, int y)
    {
        var treeHeight = Rows[x, y];
        return IsExteriorTree(x,y) || IsVisibleNorth(x,y,treeHeight) || IsVisibleEast(x,y,treeHeight) || IsVisibleWest(x,y,treeHeight) || IsVisibleSouth(x,y,treeHeight);
    }

    private bool IsExteriorTree(int x, int y)
    {
        return x == 0 || x == XLength - 1 || y == 0 || y == YLength - 1;
    }

    private bool IsVisibleNorth(int x, int y, double treeHeight)
    {
        var northTrees = new List<double>();
        for (var y2 = y - 1; y2 >= 0; y2--)
        {
            northTrees.Add(Rows[x,y2]);
        }

        return northTrees.All(ht => ht < treeHeight);
    }
    
    private bool IsVisibleEast(int x, int y, double treeHeight)
    {
        var eastTrees = new List<double>();
        for (var x2 = x + 1; x2 < XLength; x2++)
        {
            eastTrees.Add(Rows[x2,y]);
        }

        return eastTrees.All(ht => ht < treeHeight);
    }
    
    private bool IsVisibleWest(int x, int y, double treeHeight)
    {
        var westTrees = new List<double>();
        for (var x2 = x - 1; x2 >= 0; x2--)
        {
            westTrees.Add(Rows[x2,y]);
        }

        return westTrees.All(ht => ht < treeHeight);
    }
    
    private bool IsVisibleSouth(int x, int y, double treeHeight)
    {
        var southTrees = new List<double>();
        for (var y2 = y + 1; y2 < YLength; y2++)
        {
            southTrees.Add(Rows[x,y2]);
        }

        return southTrees.All(ht => ht < treeHeight);
    }

    private int GetScenicScore(int x, int y)
    {
        var treeHeight = Rows[x, y];
        return GetScenicScoreEast(x, y, treeHeight) *
               GetScenicScoreNorth(x, y, treeHeight) *
               GetScenicScoreSouth(x, y, treeHeight) *
               GetScenicScoreWest(x, y, treeHeight);
    }
    
    private int GetScenicScoreNorth(int x, int y, double treeHeight)
    {
        if (y == 0)
        {
            return 0;
        }
        
        var northTrees = new List<double>();
        for (var y2 = y - 1; y2 >= 0; y2--)
        {
            northTrees.Add(Rows[x,y2]);
        }

        var shorterTrees = northTrees.TakeWhile(ht => treeHeight > ht);
        var count = shorterTrees.Count();
        // Adjust if we have not hit an edge
        if (count != y)
        {
            return count + 1;
        }

        return count;
    }
    
    private int GetScenicScoreEast(int x, int y, double treeHeight)
    {
        if (x == XLength - 1)
        {
            return 0;
        }
        
        var eastTrees = new List<double>();
        for (var x2 = x + 1; x2 < XLength; x2++)
        {
            eastTrees.Add(Rows[x2,y]);
        }

        var shorterTrees = eastTrees.TakeWhile(ht => treeHeight > ht);
        var count = shorterTrees.Count();
        // Adjust if we have not hit an edge
        if (count != (XLength - 1) - x)
        {
            return count + 1;
        }

        return count;
    }
    
    private int GetScenicScoreWest(int x, int y, double treeHeight)
    {
        if (x == 0)
        {
            return 0;
        }
        
        var westTrees = new List<double>();
        for (var x2 = x - 1; x2 >= 0; x2--)
        {
            westTrees.Add(Rows[x2,y]);
        }

        var shorterTrees = westTrees.TakeWhile(ht => treeHeight > ht);
        var count = shorterTrees.Count();
        // Adjust if we have not hit an edge
        if (count != x)
        {
            return count + 1;
        }

        return count;
    }
    
    private int GetScenicScoreSouth(int x, int y, double treeHeight)
    {
        if (y == YLength - 1)
        {
            return 0;
        }
        
        var southTrees = new List<double>();
        for (var y2 = y + 1; y2 < YLength; y2++)
        {
            southTrees.Add(Rows[x,y2]);
        }

        var shorterTrees = southTrees.TakeWhile(ht => treeHeight > ht);
        var count = shorterTrees.Count();
        // Adjust if we have not hit an edge
        if (count != (YLength - 1) - y)
        {
            return count + 1;
        }

        return count;
    }
}