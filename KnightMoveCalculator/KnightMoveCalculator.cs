using System.Text;

namespace KnightMoveCalculator
{
    public class Calculator
    {
        /// <summary>
        /// Possible knight moves on a board, in case he's standing right in the middle and each move will not exceed the board
        /// </summary>
        private static readonly Point[] PossibleKnightMoves = new Point[] {
            new Point(2, 1), new Point(2, -1), new Point(-2, 1), new Point(-2, -1), new Point(1, 2), new Point(1, -2), new Point(-1, 2), new Point(-1, -2)
        };

        /// <summary>
        /// Cheching if the provided move coordinights is going to lead outside the board.
        /// </summary>
        /// <param name="position">Current position point</param>
        /// <param name="moveX">Potential move X coordinate</param>
        /// <param name="moveY">Potential move Y coordinate</param>
        private static bool IsOutsideTheBoard(Point position, int moveV, int moveH)
        {
            if (position.V + moveV < 1 || position.H + moveH < 1 || position.V + moveV > 8 || position.H + moveH > 8)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds one layer of children to current position. Each child will be a possible and allowed knight move on a board.
        /// </summary>
        /// <param name="treeNode">Current position in tree</param>
        private static void PopulatePossibleMoves(PointTree treeNode)
        {
            foreach (var possibleMove in PossibleKnightMoves)
            {
                if (IsOutsideTheBoard(treeNode.Point, possibleMove.V, possibleMove.H))
                {
                    continue;
                }

                treeNode.AddChild(new Point(treeNode.Point.V + possibleMove.V, treeNode.Point.H + possibleMove.H));
            }
        }

        /// <summary>
        /// Simple check if any of childs of current position in tree will beat te queen, returns win combination in case there is such
        /// </summary>
        /// <param name="currentPosition">The current position in tree</param>
        /// <param name="queenPosition">Queen coordinates</param>
        private static PointTree? PossibleChildMovesWin(PointTree currentPosition, Point queenPosition)
        {
            foreach (var move in currentPosition.Children)
            {
                if (move.Point.V == queenPosition.V && move.Point.H == queenPosition.H)
                {
                    return move;
                }
            }

            return null;
        }

        /// <summary>
        /// Function calculates the minimum number of moves needed to beat the queen
        /// </summary>
        /// <param name="knightPosition">Initial knight position</param>
        /// <param name="queenPosition">Initial queen position</param>
        public static int CalculateMinimumMoves(Point knightPosition, Point queenPosition)
        {
            if (IsOutsideTheBoard(knightPosition, 0, 0) || IsOutsideTheBoard(queenPosition, 0, 0))
            {
                throw new ArgumentException("Coordinates should be within a board!");
            }
            
            var currentMove = 1;
            var chackmate = false;

            var pointTree = new PointTree(knightPosition);
            PopulatePossibleMoves(pointTree);

            var firstTreeLayer = new List<PointTree>();
            firstTreeLayer.AddRange(pointTree.Children);

            var finalMove = PossibleChildMovesWin(pointTree, queenPosition);
            if (finalMove != null)
            {
                PrintPathToRoot(finalMove);
                return currentMove;
            }

            CalculateMinimumMoves(firstTreeLayer, queenPosition, ref currentMove, ref chackmate);

            return currentMove;
        }

        /// <summary>
        /// Internal recursive calculations for minimum number of moves needed to beat the queen, implements BFS, iterates child of next position's tree layer by layer
        /// </summary>
        /// <param name="listOfLatestSteps">Current layer of tree</param>
        /// <param name="queenPosition">Queen coordinates</param>
        /// <param name="currentMove">Move counter</param>
        /// <param name="chackmate">Trigger to exit the recursion, true means we have found the victory step</param>
        private static void CalculateMinimumMoves(List<PointTree> listOfLatestSteps, Point queenPosition, ref int currentMove, ref bool chackmate)
        {
            currentMove++;
            var nextTreeLayer = new List<PointTree>();
            PointTree? finalMove = null;

            foreach (var step in listOfLatestSteps)
            {
                PopulatePossibleMoves(step);
                nextTreeLayer.AddRange(step.Children);

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
                CalculateMinimumMoves(nextTreeLayer, queenPosition, ref currentMove, ref chackmate);
            }
        }

        /// <summary>
        /// Writing to the console path representation for current position to the very start of the tree
        /// </summary>
        /// <param name="position">Current position we should start from</param>
        private static void PrintPathToRoot(PointTree? position)
        {
            Console.WriteLine("Move combination: ");
            var sb = new StringBuilder();
            sb.AppendLine("V:" + position?.Point.V + " H:" + position?.Point.H);
            while (position?.Parent != null)
            {
                position = position.Parent;
                sb.AppendLine("V:" + position?.Point.V + " H:" + position?.Point.H);
            }
            Console.Write(sb);
        }
    }
}
