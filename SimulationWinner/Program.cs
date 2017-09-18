using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulationWinner
{
    class Program
    {
        // data structures
        class Rope
        {
            private Point _start;
            private Point _end;

            public Rope(Point start, Point end)
            {
                Start = start;
                End = end;
            }

            public Point Start { get { return _start; } set  {_start = value; } } 
            public Point End { get { return _end; } set { _end = value; } }
            public override bool Equals(object obj)
            {
                Rope r = obj as Rope;
                if (r == null) return false;
                return r.GetType() == GetType() &&
                       r.Start == this.Start &&
                       r.End == this.End;
            }
        }

        class Point
        {
            private float _y;
            private float _x;

            public Point(float x, float y)
            {
                X = x;
                Y = y;

            }

            public float X { get { return _x; } set { _x = value; } }
            public float Y { get { return _y; } set { _y = value; } }
            public override bool Equals(object obj)
            {
                Point pt = obj as Point;
                if (pt == null) return false;
                return pt.GetType() == GetType() &&
                       pt.X == this.X &&
                       pt.Y == this.Y;
            }
        }

        //Bat Man Joker Problem. Find number of intersections of ropes between two buildings
        static void Main(string[] args)
        {
            
            
            List<Rope> ropes = GetRopes().ToList();
            //Build Points in 2 D space
            
            int outputIntersections = 0;
            //Calculate number of intersections
            for (int i = 0; i < ropes.Count; i++)
            {
                for (int j = i; j < ropes.Count; j++)
                {
                    if (i == j) continue;
                    if (FindIntersection(ropes[i], ropes[j]))
                        outputIntersections += 1;
                }
            }
            Console.WriteLine(outputIntersections);
            Console.Read();
        }

        private static IEnumerable<Rope> GetRopes()
        {
            int numOfRopes = 0;
            int.TryParse(Console.ReadLine(), out numOfRopes);
            List<Rope> ropes = new List<Rope>();

            //Let two buildings are separated by length sep=10 /seed value randomly taken
            //This helps to construct points in 2d space
            int sep = 10;

            while (numOfRopes != 0)
            {
                var ropeStartEnds = Console.ReadLine();

                var res = ropeStartEnds.Split(' ').Select(Int32.Parse).ToList();
                //Validate rope start and end points are only given and nothing else
                if (res.Count != 2)
                {
                    return new List<Rope>();
                }
                // Assuming start point is on the y plane which means x=0 and
                // end point is at a separated distance sep=10 which means x=>x+sep=10
                // start point on y axis, x=0;
                Point start = new Point(0, res[0]);
                Point end = new Point(sep, res[1]);
                Rope r = new Rope(start, end);
                if (!ropes.Any(x => r.Start.Equals(x.Start) && r.End.Equals(x.End)))
                    ropes.Add(r);
                numOfRopes--;
            }
            return ropes;
        }
        
        private static bool FindIntersection(Rope r1, Rope r2)
        {
            float distXr1 = r1.End.X - r1.Start.X;
            float distYr1 = r1.End.Y - r1.Start.Y;
            float distXr2 = r2.End.X - r2.Start.X;
            float distYr2 = r2.End.Y - r2.Start.Y;

            // Solve for t1 and t2
            float dist = (distYr1 * distXr2 - distXr1 * distYr2);

            float res = ((r1.Start.X - r2.Start.X) * distYr2 + (r2.Start.Y - r1.Start.Y) * distXr2) / dist;
            return !float.IsInfinity(res);
        }

    }
}
