namespace PlatformerMVC
{
    public class Square
    {
        public ControlNode TopLeftNode;
        public ControlNode TopRightNode;
        public ControlNode DownRightNode;
        public ControlNode DownLeftNode;

        public Square(ControlNode topLeftNode, ControlNode topRightNode, ControlNode downRightNode, ControlNode downLeftNode)
        {
            TopLeftNode = topLeftNode;
            TopRightNode = topRightNode;
            DownRightNode = downRightNode;
            DownLeftNode = downLeftNode;
        }
    }
}
