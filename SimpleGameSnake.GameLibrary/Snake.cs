namespace SimpleGameSnake.GameLibrary
{
    public class Snake
    {
        public Point Head { get; private set; }
        public List<Point> Body { get; private set; }
        public Point PrevHead { get; private set; }
        public Point LastPart { get; private set; }
        public int TailLength { get; private set; }
        public Direction CurrentDirection { get; private set; }

        public Snake(int headX, int headY)
        {
            Head = new Point(headX, headY);
            TailLength = 2;

            Body = new List<Point>
            {
                new Point(headX - (TailLength - 1), Head.Y),
                new Point(headX - TailLength, Head.Y)
            };

            CurrentDirection = Direction.Right;
        }

        public void ChangeDirection(Direction newDirection)
        {
            if (!IsPossibleToChangeDirection(newDirection))
                return;

            CurrentDirection = newDirection;
        }

        private bool IsPossibleToChangeDirection(Direction newDirection)
        {
            return newDirection switch
            {
                Direction.Down when CurrentDirection == Direction.Up => false,
                Direction.Up when CurrentDirection == Direction.Down => false,
                Direction.Left when CurrentDirection == Direction.Right => false,
                Direction.Right when CurrentDirection == Direction.Left => false,
                _ => true
            };
        }

        public void MoveSnake()
        {
            PrevHead = Head;

            _ = CurrentDirection switch
            {
                Direction.Up => Head = new Point(Head.X, Head.Y - 1),
                Direction.Down => Head = new Point(Head.X, Head.Y + 1),
                Direction.Left => Head = new Point(Head.X - 1, Head.Y),
                Direction.Right => Head = new Point(Head.X + 1, Head.Y),
                _ => throw new NotImplementedException()
            };

            LastPart = Body.Last();
            Body.RemoveAt(Body.Count - 1);
            Body.Insert(0, PrevHead);
        }

        public bool IsReachTheEndOfField(int width, int height)
        {
            return Head.X == 0 || Head.X == width - 1 || Head.Y == 0 || Head.Y == height - 1;
        }

        public bool IsCrashIntoBody()
        {
            return Body.Contains(Head);
        }

        public bool IsFoodAhead(int x, int y)
        {
            if (Head.X == x && Head.Y == y)
            {
                Eat();
                return true;
            }

            return false;
        }

        private void Eat()
        {
            Body.Add(LastPart);
        }
    }
}
