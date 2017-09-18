using System;
using System.Linq;

namespace SimulationWinner
{
    public class SimulationWinner
    {
        /*
         * A war tactician in making, Desmond, is studying battles between the two generals Altair and Ezio. 
         * He is running simulations of various battles across different battlefields between the generals. 
         * The battlefields are always of n x n dimension divided into equal sized 1 x 1 sectors. 
         * The sectors are numbered row-wise from 1 to n*n starting from top left to right bottom 
         * e.g. a 3x3 battlefield would have sectors numbered as follows:
         * 
         * -------------------
         * |  1  |  2  |  3  |
         * -------------------
         * |  4  |  5  |  6  |
         * -------------------
         * |  7  |  8  |  9  |
         * -------------------
         * 
         * Each general can tactically capture only one sector before conceding one to the rival general, and so on. 
         * Once a sector is captured by a general, the rival general cannot capture that sector. 
         * As Desmond has a keen eye for tactics, he observes that whichever general captures n sectors in the same row, column or diagonal first invariably wins the battle.
         * He also observes that whenever any of the generals failed to capture n sectors in the aforementioned fashion, the battle would end up in a stalemate.
         *
         * Each simulation has the information about the dimension n of the battlefield. 
         * The simulation also has a series of sectors which were captured by Altair and Ezio (in that order) alternatively. 
         * As mentioned before, when one general captures a sector, the rival general will capture the next sector invariably 
         * e.g. if the series of sectors was 3, 1, 2, 4 
         * then it means that Altair captured sector 3, then Ezio captured sector 1, then Altair captured sector 2 and finally Ezio captured sector 4.
         *
         * Write to program to help Desmond determine if a battle simulation was won by Altair, Ezio or was a stalemate.
         *
         * Input:
         * The dimension of the battlefield n ( 3 < n < = 20), followed by n*n integers a such that 1 <= a <= n*n
         *
         * Output:
         * “Altair” if general Altair wins the battle, “Ezio” if general Ezio wins the battle and “Stale” in case of a stalemate.
         * Note: Although the simulation provides all the sectors captured, the winner could have been determined before all the sectors have been captured.
         *
         * -----------------------------------------
         * Input
         * 3
         * 5 9 3 7 8 2 4 6 1
         *
         * Output
         * Stale
         * -----------------------------------------
         * Input
         * 3
         * 3 1 5 7 9 4 6 8 2
         *
         * Output
         * Ezio
         * -----------------------------------------
         * Input
         * 3
         * 3 1 6 9 5 7 4 8 2
         *
         * Output
         * Altair
         * -----------------------------------------
         */
        enum General
        {
            Altair,
            Ezio
        }
        enum WinningCriterion
        {
            Success,
            Fail,
            Stale
        }

        static void Main(string[] args)
        {
            string str_n = Console.ReadLine();
            int n = Int32.Parse(str_n);

            string str_line = Console.ReadLine();
            char[] sep = { };
            string[] inputs = str_line.Split(sep);

            int[] sectorArray = new int[n * n];

            for (int i = 0; i < (Math.Pow(n, 2)); i++)
                sectorArray[i] = Int32.Parse(inputs[i]);

            Console.WriteLine(FindWinner(sectorArray, n));
            Console.ReadLine();
        }

        private static string FindWinner(int[] sectorArray, int n)
        {
            int generalTurn = 0;
            int[] fieldSector = Enumerable.Range(1, n * n).ToArray();
            WinningCriterion criterion = WinningCriterion.Fail;
            General general = General.Altair;
            while (criterion != WinningCriterion.Success && criterion != WinningCriterion.Stale)
            {
                int sector = sectorArray[generalTurn];
                int sectorIndex = sector - 1;
                // checking that position where user want to run is marked (with -1 or -2) or not  
                if (fieldSector[sectorIndex] != -1 && fieldSector[sectorIndex] != -2)
                {
                    if (generalTurn % 2 == Convert.ToInt16(General.Altair))
                    {
                        fieldSector[sectorIndex] = -1;
                        generalTurn++;
                        general = General.Altair;
                    }
                    else
                    {
                        fieldSector[sectorIndex] = -2;
                        generalTurn++;
                        general = General.Ezio;
                    }
                }
                criterion = VerifyWinningCriterion(fieldSector, n);// calling of check win  
            } 

            return criterion == WinningCriterion.Success ? general.ToString() : "Stale";
        }

        private static WinningCriterion VerifyWinningCriterion(int[] sectorArray, int dim)
        {
            #region Traverse Horizontal
            if (TraverseHorizontal(sectorArray, dim))
                return WinningCriterion.Success;

            #endregion

            #region Traverse Vertical
            if (TraverseVertical(sectorArray, dim))
                return WinningCriterion.Success;
            #endregion

            #region Traverse Diagonal
            if (TraverseDiagonal(sectorArray, dim))
                return WinningCriterion.Success;
            #endregion

            #region Checking For Stale
            if (ValidateStale(sectorArray, dim))
                return WinningCriterion.Stale;
            #endregion

            else
            {
                return WinningCriterion.Fail;
            }
        }

        private static bool ValidateStale(int[] sectorArray, int dim)
        {
            bool isStale = true;
            // If all the cells or values filled with -1 or -2 then stale
            int[] fieldSector = Enumerable.Range(1, dim * dim).ToArray();
            for (int i = 0; i < Math.Pow(dim, 2); i++)
            {
                //this happens when all the sectors are occupied by both generals. If any of the sector still has not occupied then its not a stale
                if (sectorArray[i] == fieldSector[i])
                {
                    isStale = !isStale;
                    break;
                }
            }
            return isStale;
        }

        private static bool TraverseDiagonal(int[] sectorArray, int dim)
        {
            /* Analyse diagonal indices
             * dim=3
             * 0 1 2
             * 3 4 5
             * 6 7 8
             * indices:top-to-bottom {0,4,8} ([dim+1]*colIdx)
             * indices:bottom-to-top {2,4,6} ([dim-1]*[colIdx+1])
             * dim =4
             * 00 01 02 03
             * 04 05 06 07
             * 08 09 10 11
             * 12 13 14 15
             * indices:top-to-bottom {00,05,10,15} ([dim+1]*colIdx)
             * indices:bottom-to-top {03,06,09,12} ([dim-1]*[colIdx+1])
             */
            int col = dim;
            //Fill indices
            int[] top2bottomIndices = new int[dim];
            int[] bottom2topIndices = new int[dim];
            while (col != 0)
            {
                int colIdx = col - 1;
                top2bottomIndices[colIdx] = (dim + 1) * colIdx;
                bottom2topIndices[colIdx] = (dim - 1) * (colIdx + 1);
                col--;
            }
            return (TraverseDiagonallyCrosswards(sectorArray, top2bottomIndices, dim) || TraverseDiagonallyCrosswards(sectorArray, bottom2topIndices, dim));
        }

        private static bool TraverseDiagonallyCrosswards(int[] sectorArray, int[] crossIndices, int dim)
        {
            bool breakWinDiagonal = true;
            for (int i = 0; i < crossIndices.Length - 1; i++)
            {
                if (sectorArray[crossIndices[i]] != sectorArray[crossIndices[i + 1]])
                {
                    breakWinDiagonal = !breakWinDiagonal;
                    break;
                }
            }
            return breakWinDiagonal;
        }

        private static bool TraverseHorizontal(int[] sectorArray, int dim)
        {
            int row = dim;
            bool breakWinHorizontal = true;
            while (row != 0)
            {
                int startIndex = dim * (row - 1);
                int endIndex = (dim * row) - 1;
                breakWinHorizontal = true;
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (sectorArray[i] != sectorArray[i + 1])
                    {
                        breakWinHorizontal = !breakWinHorizontal;
                        break;
                    }
                }
                if (breakWinHorizontal)
                    return breakWinHorizontal;
                row--;
            }
            return breakWinHorizontal;
        }

        private static bool TraverseVertical(int[] sectorArray, int dim)
        {
            int col = dim;
            bool breakWinVertical = true;
            while (col != 0)
            {
                /*
                 * val  idx
                 * 1 2  0 1
                 * 3 4  2 3
                 * dim =4 idx={0,4,8,12} .. {3,7,11,15}
                 */
                //Fill indices
                int[] colIndices = new int[dim];
                for (int ind = dim - 1; ind >= 0; ind--)
                {
                    colIndices[ind] = dim * ind + (col - 1);
                }
                breakWinVertical = true;
                //Compare values based on colIndices
                for (int i = 0; i < colIndices.Length - 1; i++)
                {
                    if (sectorArray[colIndices[i]] != sectorArray[colIndices[i + 1]])
                    {
                        breakWinVertical = !breakWinVertical;
                        break;
                    }
                }
                if (breakWinVertical)
                    return breakWinVertical;
                col--;
            }
            return breakWinVertical;
        }
    }
}
