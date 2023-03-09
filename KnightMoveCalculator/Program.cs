using KnightMoveCalculator;

Console.WriteLine("Hello, Knight!");
Console.WriteLine("Chess board is 8x8, V means vertical, H means horisontal, bottom left position is V1H1, the top right position is V8H8");
Console.WriteLine();
Console.WriteLine("Let's start our tests. Knight V1H1, Queen V3H2");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Cell(1, 1), new Cell(3, 2)));
Console.WriteLine();
Console.WriteLine("Knight V1H1, Queen V8H8");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Cell(1, 1), new Cell(8, 8)));
Console.WriteLine();
Console.WriteLine("Knight V7H8, Queen V8H8");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Cell(7, 8), new Cell(8, 8)));
Console.WriteLine();
Console.WriteLine("Knight V6H6, Queen V8H8");
Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Cell(6, 6), new Cell(8, 8)));
Console.WriteLine();

Console.WriteLine("Now is your turn, enter knight V:");
_ = int.TryParse(Console.ReadLine(), out var knightV);
Console.WriteLine("Enter knight H:");
_ = int.TryParse(Console.ReadLine(), out var knightH);
Console.WriteLine("Enter queen V:");
_ = int.TryParse(Console.ReadLine(), out var queenV);
Console.WriteLine("Enter queen H:");
_ = int.TryParse(Console.ReadLine(), out var queenH);

Console.WriteLine("Result: " + Calculator.CalculateMinimumMoves(new Cell(knightV, knightH), new Cell(queenV, queenH)));

Console.ReadLine();
