namespace SimpleGameSnake.GameLibrary
{
    public class Snake
    {
        private Point _head;
        public Point Head { get => _head; }
        public List<Point> Body { get; private set; }
        public Point PrevHead { get; private set; }
        public Point LastPart { get; private set; }
        public int TailLength { get; private set; }
        public Direction CurrentDirection { get; private set; }

        public Snake(int headX, int headY)
        {
            _head = new Point(headX, headY);
            TailLength = 2;

            Body = new List<Point>();
            Body.Add(new Point(headX - (TailLength - 1), Head.Y));
            Body.Add(new Point(headX - TailLength, Head.Y));

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
                Direction.Up => _head.Y--,
                Direction.Down => _head.Y++,
                Direction.Left => _head.X--,
                Direction.Right => _head.X++,
                _ => throw new NotImplementedException()
            };

            LastPart = Body.Last();
            Body.RemoveAt(Body.Count - 1);
            Body.Insert(0, PrevHead);
        }
        public bool IsReachTheEndOfField(int width, int height)
        {
            return _head.X == 0 || _head.X == width - 1 || _head.Y == 0 || _head.Y == height - 1;
        }

        public bool IsCrashIntoBody()
        {
            return Body.Contains(_head);
        }

        public bool IsFoodAhead(int x, int y)
        {
            if (_head.X == x && _head.Y == y)
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
