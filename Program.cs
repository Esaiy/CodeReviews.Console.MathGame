// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
List<int[]> History = [];
List<int> NonPrimeNumbers = GenerateNonPrimes();
List<int> dividends = GenerateDividends();

while (true)
{
    Console.WriteLine("1. add\n2. sub\n3. mul\n4. div\n5. history\n6. random");

    string? input = Console.ReadLine();
    if (!int.TryParse(input, out int mode))
    {
        Console.WriteLine("not number");
        continue;
    }

    if (mode == 5)
    {
        PrintHistory();
        continue;
    }

    Game(mode);

}

void Game(int mode)
{
    // start timer
    System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
    string[] operation = ["+", "-", "*", "/"];
    Random r = new();
    if (mode == 6)
    {
        mode = r.Next(1, 5);
    }

    if (mode is < 0 or > 4)
    {
        Console.WriteLine("invalid mode");
        return;
    }

    int difficulty;
    do
    {
        Console.WriteLine("input diff:");
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out difficulty))
        {
            Console.WriteLine("invalid");
            continue;
        }
        if (difficulty is < 1 or > 3)
        {
            Console.WriteLine("invalid diff");
        }
    } while (difficulty is < 1 or > 3);

    bool isHard = false;
    bool isTimed = false;
    if (difficulty == 2)
    {
        isHard = true;
    }

    if (difficulty == 3)
    {

        isHard = true;
        isTimed = true;
    }

    int[] operand = GenerateOperand(mode, isHard);
    int a = operand[0];
    int b = operand[1];

    Console.Write($"{a} {operation[mode - 1]} {b} = ");
    int answer;
    while (true)
    {
        string? input = Console.ReadLine();
        if (int.TryParse(input, out answer))
        {
            break;
        }
        Console.WriteLine("not number, try again");
    }

    // end timer
    sw.Stop();
    if (Validate(a, b, mode, answer))
    {
        Console.Write("correct");
        if (isTimed && sw.ElapsedMilliseconds > 10 * 1000)
        {
            Console.Write(", but timeout");
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("false");
    }
    History.Add([a, b, mode, answer]);
    Console.WriteLine($"{(float)sw.ElapsedMilliseconds / 1000} seconds");
    Console.WriteLine("enter to continue");
    _ = Console.ReadLine();
}

List<int> Possible(int a, bool isHard)
{
    List<int> result = [];
    for (int i = a; i >= 1; i--)
    {
        if (a % i == 0)
        {
            Console.WriteLine(i);
            result.Add(i);
        }
    }
    if (isHard)
    {
        result.RemoveAt(0);
        result.RemoveAt(result.Count - 1);
    }
    return result;
}

bool Validate(int a, int b, int mode, int answer)
{
    return mode switch
    {
        1 => (a + b) == answer,
        2 => (a - b) == answer,
        3 => (a * b) == answer,
        4 => (a / b) == answer,
        _ => false,
    };
}

void PrintHistory()
{
    if (History.Count == 0)
    {
        Console.WriteLine("no history");
        return;
    }

    foreach (int[] item in History)
    {
        string[] operation = ["+", "-", "*", "/"];
        Console.WriteLine($"{item[0]} {operation[item[2] - 1]} {item[1]} = {item[3]}");
    }
}

int[] GenerateOperand(int mode, bool isHard = false)
{
    return mode switch
    {
        1 or 2 => GenerateArithmetic(isHard),
        3 => GenerateMultiplication(isHard),
        4 => GenerateDivision(isHard),
        _ => [0, 0],
    };
}

int[] GenerateArithmetic(bool isHard)
{
    int lowerBound = 0;
    int upperBound = 100;
    if (isHard)
    {
        lowerBound = 100;
        upperBound = 1000;
    }

    Random r = new();
    int a = r.Next(lowerBound, upperBound);
    int b = r.Next(lowerBound, upperBound);

    return [a, b];
}

int[] GenerateMultiplication(bool isHard)
{
    int lowerBound = 0;
    int upperBound = 11;
    if (isHard)
    {
        lowerBound = 11;
        upperBound = 21;
    }

    Random r = new();
    int a = r.Next(lowerBound, upperBound);
    int b = r.Next(lowerBound, upperBound);

    return [a, b];
}

int[] GenerateDivision(bool isHard)
{

    int lowerBound = 0;
    List<int> dividendList = dividends;
    if (isHard)
    {
        dividendList = NonPrimeNumbers;
    }
    int upperBound = dividendList.Count + 1;

    Random r = new();
    int a = dividendList[r.Next(lowerBound, upperBound)];
    int b;

    List<int> possible = Possible(a, isHard);
    b = r.Next(0, possible.Count);
    b = possible[b];

    return [a, b];
}

List<int> GenerateNonPrimes()
{
    int limit = 100;
    bool[] isPrime = new bool[limit + 1];

    for (int i = 2; i <= limit; i++)
    {
        isPrime[i] = true;
    }

    for (int i = 2; i * i <= limit; i++)
    {
        if (isPrime[i])
        {
            for (int j = i * i; j <= limit; j += i)
            {
                isPrime[j] = false;
            }
        }
    }

    List<int> nonPrimes = [];
    for (int i = 2; i <= limit; i++)
    {
        if (!isPrime[i])
        {
            nonPrimes.Add(i);
        }
    }

    return nonPrimes;
}

List<int> GenerateDividends()
{
    List<int> result = [];
    for (int i = 0; i <= 100; i++)
    {
        result.Add(i);
    }

    return result;
}
