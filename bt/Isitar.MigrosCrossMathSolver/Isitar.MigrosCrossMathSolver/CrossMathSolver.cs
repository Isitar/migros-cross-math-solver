using System;
using System.Collections.Generic;
using System.Linq;

namespace Isitar.MigrosCrossMathSolver
{
    public class CrossMathSolver
    {
        private readonly int[] field = new int[9];
        private readonly Func<int, int, int, bool>[] predicates;

        /// <summary>
        /// A solver used to solve the cross-math puzzle using backtracking
        /// </summary>
        /// <param name="predicates">the functions for each line (0 => top line horizontal, 1 => middle line horizontal ... 3 => left line vertical ...</param>
        public CrossMathSolver(Func<int, int, int, bool>[] predicates)
        {
            this.predicates = predicates;
        }

        public int[] Solve()
        {
            var availableNumbers = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
            var rnd = new Random();

            return Solve(0, availableNumbers.OrderBy(x => rnd.Next()).ToArray()) ? field : new int[] { };
        }

        private bool Solve(int currentField, int[] availableNumbers)
        {
            // check precondition
            if (!(checkPredicate(field[0], field[1], field[2], predicates[0])
                  && checkPredicate(field[3], field[4], field[5], predicates[1])
                  && checkPredicate(field[6], field[7], field[8], predicates[2])
                  && checkPredicate(field[0], field[3], field[6], predicates[3])
                  && checkPredicate(field[1], field[4], field[7], predicates[4])
                  && checkPredicate(field[2], field[5], field[8], predicates[5])
                ))
            {
                return false;
            }

            if (currentField >= field.Length)
            {
                return true;
            }

            foreach (var availableNumber in availableNumbers)
            {
                field[currentField] = availableNumber;
                if (Solve(currentField + 1, availableNumbers.Where(n => n != availableNumber).ToArray()))
                {
                    return true;
                }
            }

            field[currentField] = 0;
            return false;
        }

        private bool checkPredicate(int f1, int f2, int f3, Func<int, int, int, bool> predicate)
        {
            if (f1 == 0 || f2 == 0 || f3 == 0)
            {
                return true;
            }

            return predicate(f1, f2, f3);
        }
    }
}