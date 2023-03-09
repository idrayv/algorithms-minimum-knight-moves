using System.Text;

namespace KnightMoveCalculator
{
    public class Calculator
    {
        /// <summary>
        /// Represent all kind of knight moves possible on a board
        /// </summary>
        private static readonly Cell[] PossibleKnightMoves = new Cell[] {
            new Cell(2, 1), new Cell(2, -1), new Cell(-2, 1), new Cell(-2, -1), new Cell(1, 2), new Cell(1, -2), new Cell(-1, 2), new Cell(-1, -2)
        };

        /// <summary>
        /// Checking if the provided move coordinates is going to lead outside the board
        /// </summary>
        /// <param name="position">Current position cell</param>
        /// <param name="moveX">Potential move vertical coordinate</param>
        /// <param name="moveY">Potential move horisontal coordinate</param>
        private static bool IsOutsideTheBoard(Cell position, int moveV, int moveH)
        {
            if (position.V + moveV < 1 || position.H + moveH < 1 || position.V + moveV > 8 || position.H + moveH > 8)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds one layer of possible moves for a current position. Each child will be a possible and allowed knight move on a board starting from current position.
        /// </summary>
        /// <param name="treeNode">Current position in tree</param>
        private static void PopulatePossibleMoves(MovementTree treeNode)
        {
            foreach (var possibleMove in PossibleKnightMoves)
            {
                if (IsOutsideTheBoard(treeNode.CurrentKnightPosition, possibleMove.V, possibleMove.H))
                {
                    continue;
                }

                treeNode.AddPossibleMove(new Cell(treeNode.CurrentKnightPosition.V + possibleMove.V, treeNode.CurrentKnightPosition.H + possibleMove.H));
            }
        }

        /// <summary>
        /// Checks if any of childs of current position in tree might beat the queen, returns victory combination in case there is such
        /// </summary>
        /// <param name="currentPosition">The current position in tree</param>
        /// <param name="queenPosition">Queen coordinates</param>
        private static MovementTree? PossibleChildMovesWin(MovementTree currentPosition, Cell queenPosition)
        {
            foreach (var move in currentPosition.PossibleNextMove)
            {
                if (move.CurrentKnightPosition.V == queenPosition.V && move.CurrentKnightPosition.H == queenPosition.H)
                {
                    return move;
                }
            }

            return null;
        }

        /// <summary>
        /// Calculates the minimum number of moves needed for a knight to beat the queen
        /// </summary>
        /// <param name="knightPosition">Initial knight position</param>
        /// <param name="queenPosition">Initial queen position</param>
        public static int CalculateMinimumMoves(Cell knightPosition, Cell queenPosition)
        {
            if (IsOutsideTheBoard(knightPosition, 0, 0) || IsOutsideTheBoard(queenPosition, 0, 0))
            {
                throw new ArgumentException("Coordinates should be within a board!");
            }

            var currentMoveCounter = 1;
            var chackmate = false;

            var pointTree = new MovementTree(knightPosition);
            PopulatePossibleMoves(pointTree);

            var firstTreeLayer = new List<MovementTree>();
            firstTreeLayer.AddRange(pointTree.PossibleNextMove);

            var finalMove = PossibleChildMovesWin(pointTree, queenPosition);
            if (finalMove != null)
            {
                PrintPathToRoot(finalMove);
                return currentMoveCounter;
            }

            // In case no one moves from first layer is victory one, let's continue search recursively
            CalculateMinimumMoves(firstTreeLayer, queenPosition, ref currentMoveCounter, ref chackmate);

            return currentMoveCounter;
        }

        /// <summary>
        /// Internal recursive calculations for minimum number of moves needed to beat the queen,
        /// implements BFS, iterates childs in tree layer by layer
        /// </summary>
        /// <param name="listOfLatestSteps">List of all items of current layer of tree</param>
        /// <param name="queenPosition">Queen coordinates</param>
        /// <param name="currentMoveCounter">Move counter</param>
        /// <param name="chackmate">Trigger to exit the recursion, true means we have found the victory step</param>
        private static void CalculateMinimumMoves(List<MovementTree> listOfLatestSteps, Cell queenPosition, ref int currentMoveCounter, ref bool chackmate)
        {
            currentMoveCounter++;
            var nextTreeLayer = new List<MovementTree>();
            MovementTree? finalMove = null;

            foreach (var step in listOfLatestSteps)
            {
                PopulatePossibleMoves(step);
                nextTreeLayer.AddRange(step.PossibleNextMove);

                finalMove = PossibleChildMovesWin(step, queenPosition);
                if (finalMove != null)
                {
                    chackmate = true;
                    break;
                }
            }

            if (chackmate)
            {
                PrintPathToRoot(finalMove);
            }
            else
            {
                CalculateMinimumMoves(nextTreeLayer, queenPosition, ref currentMoveCounter, ref chackmate);
            }
        }

        /// <summary>
        /// Writing path representation from the current position to the very start of the tree to the console
        /// </summary>
        /// <param name="position">Current position we should start from</param>
        private static void PrintPathToRoot(MovementTree? position)
        {
            Console.WriteLine("Move combination: ");
            var sb = new StringBuilder();
            sb.AppendLine($"V:{position?.CurrentKnightPosition.V} H:{position?.CurrentKnightPosition.H}");

            while (position?.Parent != null)
            {
                position = position.Parent;
                sb.AppendLine($"V:{position?.CurrentKnightPosition.V} H:{position?.CurrentKnightPosition.H}");
            }

            Console.Write(sb);
        }
    }
}
