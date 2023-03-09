namespace KnightMoveCalculator
{
    public struct Point
    {
        public Point(int v, int h)
        {
            this.V = v;
            this.H = h;
        }

        public int V { get; set; }
        public int H { get; set; }
    }

    class PointTree
    {
        public Point Point;
        public List<PointTree> Children = new();
        public PointTree? Parent { get; private set; }

        public PointTree(Point point)
        {
            this.Point = point;
            Children = new List<PointTree>();
            Parent = null;
        }

        public PointTree AddChild(Point point)
        {
            var node = new PointTree(point) { Parent = this };
            Children.Add(node);
            return node;
        }
    }
}
