// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
List<int[]> History = [];

while (true)
{
    Console.WriteLine("1. add\n2. sub\n3. mul\n4. div\n5. history:");

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

void Game(int mode, bool randomMode = false)
{
    string[] operation = ["+", "-", "*", "/"];
    Random r = new();
    if (randomMode)
    {
        mode = r.Next(0, 4);
    }

    if (mode is < 0 or > 4)
    {
        Console.WriteLine("invalid mode");
        return;
    }

    int a = r.Next(101);
    int b = r.Next(101);
    if (mode == 4)
    {
        List<int> possible = Possible(a);
        b = r.Next(0, possible.Count);
        b = possible[b];
    }

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

    if (Validate(a, b, mode, answer))
    {
        Console.WriteLine("correct");
    }
    else
    {
        Console.WriteLine("false");
    }
    History.Add([a, b, mode, answer]);
    Console.WriteLine("enter to continue");
    _ = Console.ReadLine();
}

List<int> Possible(int a)
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

