using System;
using System.Diagnostics;

namespace Isitar.MigrosCrossMathSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var solver = new CrossMathSolver(new Func<int, int, int, bool>[]
            {
                (a, b, c) => (a - b) * c == 3,
                (a, b, c) => (a + b) * c == 20,
                (a, b, c) => a + b + c == 20,
                (a, b, c) => a - b + c == 13,
                (a, b, c) => (a + b) / c == 1,
                (a, b, c) => a * b * c == 20,
            });
            
            var result = solver.Solve();

            for (int i = 0; i < result.Length; i++)
            {
                Console.Write($"{result[i]} ");
                if ((i + 1) % 3 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}