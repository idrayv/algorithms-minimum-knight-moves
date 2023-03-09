using KnightMoveCalculator;

Console.WriteLine("Hello, Knight!");
Console.WriteLine("Chess board is 8x8, V means vertical, H means horisontal, bottom left position is V1H1, the top right position is V8H8");
Console.WriteLine();
Console.WriteLine("Let's start our tests. Knight V1H1, Queen V3H2");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Point { V = 1, H = 1 }, new Point { V = 3, H = 2 }));
Console.WriteLine();
Console.WriteLine("Knight V1H1, Queen V8H8");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Point { V = 1, H = 1 }, new Point { V = 8, H = 8 }));
Console.WriteLine();
Console.WriteLine("Knight V7H8, Queen V8H8");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Point { V = 7, H = 8 }, new Point { V = 8, H = 8 }));
Console.WriteLine();
Console.WriteLine("Knight V6H6, Queen V8H8");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Point { V = 6, H = 6 }, new Point { V = 8, H = 8 }));
Console.WriteLine();

Console.WriteLine("Now is your turn, enter knight V:");
_ = int.TryParse(Console.ReadLine(), out var knightV);
Console.WriteLine("Enter knight H:");
_ = int.TryParse(Console.ReadLine(), out var knightH);
Console.WriteLine("Enter queen V:");
_ = int.TryParse(Console.ReadLine(), out var queenV);
Console.WriteLine("Enter queen H:");
_ = int.TryParse(Console.ReadLine(), out var queenH);

Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Point { V = knightV, H = knightH }, new Point { V = queenV, H = queenH }));

Console.ReadLine();
