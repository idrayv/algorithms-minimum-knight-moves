namespace KnightMoveCalculator
{
    /// <summary>
    /// Represents cell on chess board
    /// </summary>
    public struct Cell
    {
        public Cell(int v, int h)
        {
            this.V = v;
            this.H = h;
        }

        /// <summary>
        /// Vertical position, from bottom to top.
        /// </summary>
        public int V { get; set; }

        /// <summary>
        /// Horizontal position, from left to right.
        /// </summary>
        public int H { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class MovementTree
    {
        public Cell CurrentKnightPosition;
        public List<MovementTree> PossibleNextMove = new();
        public MovementTree? Parent { get; private set; }

        public MovementTree(Cell position)
        {
            this.CurrentKnightPosition = position;
            PossibleNextMove = new List<MovementTree>();
            Parent = null;
        }

        public MovementTree AddPossibleMove(Cell position)
        {
            var node = new MovementTree(position) { Parent = this };
            PossibleNextMove.Add(node);
            return node;
        }
    }
}
