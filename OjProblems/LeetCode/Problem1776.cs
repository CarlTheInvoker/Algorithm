namespace OjProblems.LeetCode
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem1776
    {
        public class Solution
        {
            public double[] GetCollisionTimes(int[][] cars)
            {
                var carStatuses = cars.Select(car => new CarStatus(car[0], car[1])).ToList();
                Stack<CarStatus> stack = new Stack<CarStatus>();

                for (int i = carStatuses.Count - 1; i >= 0; --i)
                {
                    var current = carStatuses[i];
                    while (stack.Count > 0 && this.CurrentCarWillCollideLater(current, stack))
                    {
                        // The next car will collide earlier, so the status of next car could be determined
                        stack.Pop();
                    }

                    stack.Push(current);
                }

                return carStatuses.Select(car => car.WhenCollide).ToArray();
            }

            private bool CurrentCarWillCollideLater(CarStatus current, Stack<CarStatus> stack)
            {
                CarStatus next = stack.Peek();
                current.WhenCollide = current.Speed <= next.Speed ? -1 : (next.Position - current.Position) * 1.0 / (current.Speed - next.Speed);
                return current.WhenCollide < 0 || (current.WhenCollide > next.WhenCollide && next.WhenCollide > 0);
            }

            internal class CarStatus
            {
                public CarStatus(int position, int speed)
                {
                    this.Position = position;
                    this.Speed = speed;
                    this.WhenCollide = -1; // Means won't collide
                }

                public int Position { get; set; }
                public int Speed { get; set; }
                public double WhenCollide { get; set; }
            }
        }
    }
}
