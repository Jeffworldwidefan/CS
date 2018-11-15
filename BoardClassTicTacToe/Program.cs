//Chris Reed
//11-13-18
//Tic Tac Toe
//Contributors:
//none

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BoardClassTicTacToe
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Player vs. Player \n2. Player vs. Computer\n3. Computer Vs. Player\n4. Computer vs. Computer");
                Console.Write("Input: ");
                int input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
                    PlayerVsPlayer();
                }//end inp 1
                if (input == 2)
                {
                    Console.WriteLine("");
                    PlayerVsComp(80, true);
                }//end inp 2
                if (input == 3)
                {
                    PlayerVsComp(80, false);
                }//end inp 3
                if (input == 4)
                {
                    CompVsComp(2, 2);
                }//end inp 4
                Console.ReadKey();
            }//end while
        }//end main
        public static void CompVsComp(int rate1, int rate2)
        {
            GameBoard board = new GameBoard();
            AI ai1 = new AI(rate1, 255, true);
            AI ai2 = new AI(rate2, 255, false);
            ConsoleKeyInfo press;
            while (true)
            {
                int turn = 0;
                while (true)
                {
                    Console.Clear();
                    //turn 1
                    ai1.OutputDes();
                    board.OutputBoard();
                    Console.WriteLine("AI1's turn:");
                    press = Console.ReadKey();
                    if ((press.Modifiers & ConsoleModifiers.Control) != 0) PlayerVsPreComp(rate1, false, ai1);
                    if ((press.Modifiers & ConsoleModifiers.Alt) != 0) PlayerVsPreComp(rate2, true, ai2);
                    bool[] checkArray = { board.IsSpaceEmpty(0), board.IsSpaceEmpty(1), board.IsSpaceEmpty(2), board.IsSpaceEmpty(3), board.IsSpaceEmpty(4), board.IsSpaceEmpty(5), board.IsSpaceEmpty(6), board.IsSpaceEmpty(7), board.IsSpaceEmpty(8) };
                    board.SetSpace(ai1.Move(checkArray, turn), "X");
                    if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                    Console.Clear();
                    //turn 2
                    ai2.OutputDes();
                    board.OutputBoard();
                    Console.WriteLine("AI2's turn:");
                    press = Console.ReadKey();
                    if ((press.Modifiers & ConsoleModifiers.Control) != 0) PlayerVsPreComp(rate1, false, ai1);
                    if ((press.Modifiers & ConsoleModifiers.Alt) != 0) PlayerVsPreComp(rate2, true, ai2);
                    bool[] checkArray2 = { board.IsSpaceEmpty(0), board.IsSpaceEmpty(1), board.IsSpaceEmpty(2), board.IsSpaceEmpty(3), board.IsSpaceEmpty(4), board.IsSpaceEmpty(5), board.IsSpaceEmpty(6), board.IsSpaceEmpty(7), board.IsSpaceEmpty(8) };
                    board.SetSpace(ai2.Move(checkArray2, turn), "O");
                    turn++;
                    if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                }//end while
                Console.Clear();
                if (board.HasWinner("X"))
                {
                    Console.WriteLine("AI1 has won.");
                    ai1.Eval(true);
                    ai2.Eval(false);
                }//end if
                if (board.HasWinner("O"))
                {
                    Console.WriteLine("AI2 has won.");
                    ai1.Eval(false);
                    ai2.Eval(true);
                }//end if
                if (board.IsCat())
                {
                    Console.WriteLine("It was a tie.");
                    ai1.Eval(false);
                    ai2.Eval(false);
                }//end if
                board.OutputBoard();
                press = Console.ReadKey();
                if ((press.Modifiers & ConsoleModifiers.Control) != 0) PlayerVsPreComp(rate1, false, ai1);
                if ((press.Modifiers & ConsoleModifiers.Alt) != 0) PlayerVsPreComp(rate2, true, ai2);
                board.Clear();
                Console.Clear();
            }//end while
        }//end compvscomp
        public static void PlayerVsComp(int rate, bool pFirst)
        {
            GameBoard board = new GameBoard();
            AI ai;
            if (pFirst)
            {
                ai = new AI(80, 450, false);
            }//end if
            else
            {
                ai = new AI(80, 450, true);
            }//end if
            while (true)
            {
                bool cont = false;
                int turn = 0;
                while (true)
                {
                    if (pFirst)
                    {
                        Console.Clear();
                        ai.OutputDes();
                        Console.WriteLine("Your turn:");
                        board.OutputBoard();
                        board.OutputBoardLocations();
                        while (!cont)
                        {
                            bool isNumeric;
                            string inp;
                            int num;
                            do
                            {
                                Console.Write("Enter move: ");
                                inp = Console.ReadLine();
                                isNumeric = int.TryParse(inp, out num);
                                if (!isNumeric)
                                {
                                    Console.WriteLine("Could not parse the input. Please try again.");
                                }//end if
                                if (num < 0 || num > 8)
                                {
                                    Console.WriteLine("Please enter a valid number.");
                                }//end if
                            } while (!isNumeric || num > 8 || num < 0);
                            if (board.GetSpace(num) == " ") { board.SetSpace(num, "X"); cont = true; }
                            else Console.WriteLine("That space is not available.");
                        }//end while
                        if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                        bool[] checkArray = { board.IsSpaceEmpty(0), board.IsSpaceEmpty(1), board.IsSpaceEmpty(2), board.IsSpaceEmpty(3), board.IsSpaceEmpty(4), board.IsSpaceEmpty(5), board.IsSpaceEmpty(6), board.IsSpaceEmpty(7), board.IsSpaceEmpty(8) };
                        board.SetSpace(ai.Move(checkArray, turn), "O");
                    } else
                    {
                        bool[] checkArray = { board.IsSpaceEmpty(0), board.IsSpaceEmpty(1), board.IsSpaceEmpty(2), board.IsSpaceEmpty(3), board.IsSpaceEmpty(4), board.IsSpaceEmpty(5), board.IsSpaceEmpty(6), board.IsSpaceEmpty(7), board.IsSpaceEmpty(8) };
                        board.SetSpace(ai.Move(checkArray, turn), "X");
                        if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                        Console.Clear();
                        ai.OutputDes();
                        Console.WriteLine("Your turn:");
                        board.OutputBoard();
                        board.OutputBoardLocations();
                        while (!cont)
                        {
                            bool isNumeric;
                            string inp;
                            int num;
                            do
                            {
                                Console.Write("Enter move: ");
                                inp = Console.ReadLine();
                                isNumeric = int.TryParse(inp, out num);
                                if (!isNumeric)
                                {
                                    Console.WriteLine("Could not parse the input. Please try again.");
                                }//end if
                                if (num < 0 || num > 8)
                                {
                                    Console.WriteLine("Please enter a valid number.");
                                }//end if
                            } while (!isNumeric || num > 8 || num < 0);
                            if (board.GetSpace(num) == " ") { board.SetSpace(num, "O"); cont = true; }
                            else Console.WriteLine("That space is not available.");
                        }//end while
                    }//end if
                    turn += 1;
                    cont = false;
                    if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                }//end while
                Console.Clear();
                if (board.HasWinner("O"))
                {
                    Console.WriteLine("Player has won.");
                    ai.Eval(false);
                }//end if
                if (board.HasWinner("X"))
                {
                    Console.WriteLine("Computer has won.");
                    ai.Eval(true);
                }//end if
                if (board.IsCat())
                {
                    Console.WriteLine("It was a tie.");
                    ai.Eval(false);
                }//end if
                board.OutputBoard();
                Console.ReadKey();
                board.Clear();
                Console.Clear();
            }//end while
        }//end PlayerVsComp
        public static void PlayerVsPreComp(int rate, bool pFirst, AI ai)
        {
            GameBoard board = new GameBoard();        
            while (true)
            {
                bool cont = false;
                int turn = 0;
                while (true)
                {
                    if (pFirst)
                    {
                        Console.Clear();
                        ai.OutputDes();
                        Console.WriteLine("Your turn:");
                        board.OutputBoard();
                        board.OutputBoardLocations();
                        while (!cont)
                        {
                            bool isNumeric;
                            string inp;
                            int num;
                            do
                            {
                                Console.Write("Enter move: ");
                                inp = Console.ReadLine();
                                isNumeric = int.TryParse(inp, out num);
                                if (!isNumeric)
                                {
                                    Console.WriteLine("Could not parse the input. Please try again.");
                                }//end if
                                if (num < 0 || num > 8)
                                {
                                    Console.WriteLine("Please enter a valid number.");
                                }//end if
                            } while (!isNumeric || num > 8 || num < 0);
                            if (board.GetSpace(num) == " ") { board.SetSpace(num, "X"); cont = true; }
                            else Console.WriteLine("That space is not available.");
                        }//end while
                        if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                        bool[] checkArray = { board.IsSpaceEmpty(0), board.IsSpaceEmpty(1), board.IsSpaceEmpty(2), board.IsSpaceEmpty(3), board.IsSpaceEmpty(4), board.IsSpaceEmpty(5), board.IsSpaceEmpty(6), board.IsSpaceEmpty(7), board.IsSpaceEmpty(8) };
                        board.SetSpace(ai.Move(checkArray, turn), "O");
                    }
                    else
                    {
                        bool[] checkArray = { board.IsSpaceEmpty(0), board.IsSpaceEmpty(1), board.IsSpaceEmpty(2), board.IsSpaceEmpty(3), board.IsSpaceEmpty(4), board.IsSpaceEmpty(5), board.IsSpaceEmpty(6), board.IsSpaceEmpty(7), board.IsSpaceEmpty(8) };
                        board.SetSpace(ai.Move(checkArray, turn), "X");
                        if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                        Console.Clear();
                        ai.OutputDes();
                        Console.WriteLine("Your turn:");
                        board.OutputBoard();
                        board.OutputBoardLocations();
                        while (!cont)
                        {
                            bool isNumeric;
                            string inp;
                            int num;
                            do
                            {
                                Console.Write("Enter move: ");
                                inp = Console.ReadLine();
                                isNumeric = int.TryParse(inp, out num);
                                if (!isNumeric)
                                {
                                    Console.WriteLine("Could not parse the input. Please try again.");
                                }//end if
                                if (num < 0 || num > 8)
                                {
                                    Console.WriteLine("Please enter a valid number.");
                                }//end if
                            } while (!isNumeric || num > 8 || num < 0);
                            if (board.GetSpace(num) == " ") { board.SetSpace(num, "O"); cont = true; }
                            else Console.WriteLine("That space is not available.");
                        }//end while
                    }//end if
                    turn += 1;
                    cont = false;
                    if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                }//end while
                Console.Clear();
                if (board.HasWinner("O"))
                {
                    Console.WriteLine("Player has won.");
                    ai.Eval(false);
                }//end if
                if (board.HasWinner("X"))
                {
                    Console.WriteLine("Computer has won.");
                    ai.Eval(true);
                }//end if
                if (board.IsCat())
                {
                    Console.WriteLine("It was a tie.");
                    ai.Eval(false);
                }//end if
                board.OutputBoard();
                Console.ReadKey();
                board.Clear();
                Console.Clear();
            }//end while
        }//end PlayerVsComp
        public static void PlayerVsPlayer()
        {
            GameBoard board = new GameBoard();
            Console.Write("Enter Name1: ");
            string name1 = Console.ReadLine();
            Console.Write("Enter Name2: ");
            string name2 = Console.ReadLine();
            bool cont = false;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("{0}\'s turn:", name1);
                board.OutputBoard();
                board.OutputBoardLocations();
                while (!cont)
                {
                    bool isNumeric;
                    string inp;
                    int num;
                    do
                    {
                        Console.Write("Enter move: ");
                        inp = Console.ReadLine();
                        isNumeric = int.TryParse(inp, out num);
                        if (!isNumeric)
                        {
                            Console.WriteLine("Could not parse the input. Please try again.");
                        }//end if
                        if (num < 0 || num > 8)
                        {
                            Console.WriteLine("Please enter a valid number.");
                        }//end if
                    } while (!isNumeric || num > 8 || num < 0);
                    if (board.GetSpace(num) == " ") { board.SetSpace(num, "X"); cont = true; }
                    else Console.WriteLine("That space is not available.");
                }//end while
                if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
                cont = false;
                Console.Clear();
                Console.WriteLine("{0}\'s turn:", name2);
                board.OutputBoard();
                board.OutputBoardLocations();
                while (!cont)
                {
                    bool isNumeric;
                    string inp;
                    int num;
                    do
                    {
                        Console.Write("Enter move: ");
                        inp = Console.ReadLine();
                        isNumeric = int.TryParse(inp, out num);
                        if (!isNumeric)
                        {
                            Console.WriteLine("Could not parse the input. Please try again.");
                        }//end if
                        if (num < 0 || num > 8)
                        {
                            Console.WriteLine("Please enter a valid number.");
                        }//end if
                    } while (!isNumeric || num > 8 || num < 0);
                    if (board.GetSpace(num) == " ") { board.SetSpace(num, "O"); cont = true; }
                    else Console.WriteLine("That space is not available.");
                }//end while
                cont = false;
                if (board.HasWinner("X") || board.HasWinner("O") || board.IsCat()) break;
            }//end while
            if (board.HasWinner("X")) Console.WriteLine("{0} has won.", name1);
            if (board.HasWinner("O")) Console.WriteLine("{0} has won.", name2);
            if (board.IsCat()) Console.WriteLine("It was a tie.");
            board.OutputBoard();
        }//end PlayerVsPlayer
    }//end class
    class AI
    {
        private int rate;
        private int[] des0;
        private int[] des1;
        private int[] des2;
        private int[] des3;
        private int[] des4;
        private int[] des5;
        private int[] des6;
        private int[] des7;
        private int[] des8;
        private int[] moves = new int[5] { -1, -1, -1, -1, -1 };
        public AI(int r, int def, bool first)
        {
            if (first)
            {
                des0 = new int[] { def, def, def, def, def };
                des1 = new int[] { def, def, def, def, def };
                des2 = new int[] { def, def, def, def, def };
                des3 = new int[] { def, def, def, def, def };
                des4 = new int[] { def, def, def, def, def };
                des5 = new int[] { def, def, def, def, def };
                des6 = new int[] { def, def, def, def, def };
                des7 = new int[] { def, def, def, def, def };
                des8 = new int[] { def, def, def, def, def };
            } else
            {
                des0 = new int[] { def, def, def, def };
                des1 = new int[] { def, def, def, def };
                des2 = new int[] { def, def, def, def };
                des3 = new int[] { def, def, def, def };
                des4 = new int[] { def, def, def, def };
                des5 = new int[] { def, def, def, def };
                des6 = new int[] { def, def, def, def };
                des7 = new int[] { def, def, def, def };
                des8 = new int[] { def, def, def, def };
            }//end if
            rate = r;
        }//
        public void OutputDes()
        {
            Console.WriteLine("{0} | {1} | {2} \n{3} | {4} | {5} \n{6} | {7} | {8}", string.Join(",", des0), string.Join(",", des1), string.Join(",", des2), string.Join(",", des3), string.Join(",", des4), string.Join(",", des5), string.Join(",", des6), string.Join(",", des7), string.Join(",", des8));
        }//end OutputDes
        public int Move(bool[] c, int t)
        {
            int total = 0;
            //get total
            for (int i = 0; i <= 8; i++)
            {
                if (c[i])
                {
                    if (i == 0) total += des0[t];
                    if (i == 1) total += des1[t];
                    if (i == 2) total += des2[t];
                    if (i == 3) total += des3[t];
                    if (i == 4) total += des4[t];
                    if (i == 5) total += des5[t];
                    if (i == 6) total += des6[t];
                    if (i == 7) total += des7[t];
                    if (i == 8) total += des8[t];
                }//end if
            }//end for
            int totalTemp = total;
            int[] minMax = new int[18];
            if (c[8])
            {
                minMax[17] = total - 1;
                minMax[16] = total - des8[t];
                total -= des8[t];
            }//end if
            else { minMax[17] = -1; minMax[16] = -1; }
            if (c[7])
            {
                minMax[15] = total - 1;
                minMax[14] = total - des7[t];
                total -= des7[t];
            }//end if
            else { minMax[15] = -1; minMax[14] = -1; }
            if (c[6])
            {
                minMax[13] = total - 1;
                minMax[12] = total - des6[t];
                total -= des6[t];
            }//end if
            else { minMax[13] = -1; minMax[12] = -1; }
            if (c[5])
            {
                minMax[11] = total - 1;
                minMax[10] = total - des5[t];
                total -= des5[t];
            }//end if
            else { minMax[11] = -1; minMax[10] = -1; }
            if (c[4])
            {
                minMax[9] = total - 1;
                minMax[8] = total - des4[t];
                total -= des4[t];
            }//end if
            else { minMax[9] = -1; minMax[8] = -1; }
            if (c[3])
            {
                minMax[7] = total - 1;
                minMax[6] = total - des3[t];
                total -= des3[t];
            }//end if
            else { minMax[7] = -1; minMax[6] = -1; }
            if (c[2])
            {
                minMax[5] = total - 1;
                minMax[4] = total - des2[t];
                total -= des2[t];
            }//end if
            else { minMax[5] = -1; minMax[4] = -1; }
            if (c[1])
            {
                minMax[3] = total - 1;
                minMax[2] = total - des1[t];
                total -= des1[t];
            }//end if
            else { minMax[3] = -1; minMax[2] = -1; }
            if (c[0])
            {
                minMax[1] = total - 1;
                minMax[0] = total - des0[t];
                total -= des0[t];
            }//end if
            else { minMax[1] = -1; minMax[0] = -1; }
            Random rand = new Random();
            int randomNum = rand.Next(totalTemp);
            if (randomNum >= minMax[0] && randomNum <= minMax[1])
            {
                moves[t] = 0;
                return 0;
            }//end if
            if (randomNum >= minMax[2] && randomNum <= minMax[3])
            {
                moves[t] = 1;
                return 1;
            }//end if
            if (randomNum >= minMax[4] && randomNum <= minMax[5])
            {
                moves[t] = 2;
                return 2;
            }//end if
            if (randomNum >= minMax[6] && randomNum <= minMax[7])
            {
                moves[t] = 3;
                return 3;
            }//end if
            if (randomNum >= minMax[8] && randomNum <= minMax[9])
            {
                moves[t] = 4;
                return 4;
            }//end if
            if (randomNum >= minMax[10] && randomNum <= minMax[11])
            {
                moves[t] = 5;
                return 5;
            }//end if
            if (randomNum >= minMax[12] && randomNum <= minMax[13])
            {
                moves[t] = 6;
                return 6;
            }//end if
            if (randomNum >= minMax[14] && randomNum <= minMax[15])
            {
                moves[t] = 7;
                return 7;
            }//end if
            if (randomNum >= minMax[16] && randomNum <= minMax[17])
            {
                moves[t] = 8;
                return 8;
            }//end if
            Console.WriteLine(string.Join(",", minMax) + " | " + randomNum);
            return 9;
        }//end Move
        public void Eval(bool win)
        {
            if (win)
            {
                for (int i = 0; i <= moves.Length - 1; i++)
                {
                    if (moves[i] == 0)
                    {
                        des0[i] += rate;
                    }//end if
                    if (moves[i] == 1)
                    {
                        des1[i] += rate;
                    }//end if
                    if (moves[i] == 2)
                    {
                        des2[i] += rate;
                    }//end if
                    if (moves[i] == 3)
                    {
                        des3[i] += rate;
                    }//end if
                    if (moves[i] == 4)
                    {
                        des4[i] += rate;
                    }//end if
                    if (moves[i] == 5)
                    {
                        des5[i] += rate;
                    }//end if
                    if (moves[i] == 6)
                    {
                        des6[i] += rate;
                    }//end if
                    if (moves[i] == 7)
                    {
                        des7[i] += rate;
                    }//end if
                    if (moves[i] == 8)
                    {
                        des8[i] += rate;
                    }//end if
                }//end for
            }
            else
            {
                for (int i = 0; i <= moves.Length - 1; i++)
                {
                    if (moves[i] == 0)
                    {
                        des0[i] -= rate;
                    }//end if
                    if (moves[i] == 1)
                    {
                        des1[i] -= rate;
                    }//end if
                    if (moves[i] == 2)
                    {
                        des2[i] -= rate;
                    }//end if
                    if (moves[i] == 3)
                    {
                        des3[i] -= rate;
                    }//end if
                    if (moves[i] == 4)
                    {
                        des4[i] -= rate;
                    }//end if
                    if (moves[i] == 5)
                    {
                        des5[i] -= rate;
                    }//end if
                    if (moves[i] == 6)
                    {
                        des6[i] -= rate;
                    }//end if
                    if (moves[i] == 7)
                    {
                        des7[i] -= rate;
                    }//end if
                    if (moves[i] == 8)
                    {
                        des8[i] -= rate;
                    }//end if
                }//end for
            }//end if
        }//end Eval
    }//end class
    class GameBoard
    {
        //private data
        private string[] Board = new string[9] { " ", " ", " ", " ", " ", " ", " ", " ", " ", };
        //constructors
        public GameBoard()
        {
            for (int i = 0; i <= 8; i++)
            {
                Board[i] = " ";
            }//end for
        }//end board def
        //mutator
        public void SetSpace(int i, string c)
        {
            Board[i] = c;
        }//end SetSpace
        //accessor
        public string GetSpace(int i)
        {
            return Board[i];
        }//end GetSpace
        //helpers
        public void Clear()
        {
            for (int i = 0; i <= 8; i++)
            {
                Board[i] = " ";
            }//end for
        }//end Clear
        public void OutputBoardLocations()
        {
            Console.WriteLine("0|1|2");
            Console.WriteLine("-----");
            Console.WriteLine("3|4|5");
            Console.WriteLine("-----");
            Console.WriteLine("6|7|8");
        }//end OutputBoardLocations
        public void OutputBoard()
        {
            for (int i = 0; i <= 8; i++) Board[i] = Board[i].ToUpper();
            for (int i = 0; i <= 8; i++)
            {
                if (i != 2 && i != 5 && i != 8)
                {
                    Console.Write(Board[i] + "|");
                }//end if
                else
                {
                    Console.Write(Board[i] + "\n-----\n");
                }//end else
            }//end for
        }//end OutputBoard
        public bool HasThreeRow(string s)
        {
            for (int i = 0; i <= 6; i = i + 3)
            {
                if (Board[i] == s && Board[i + 1] == s && Board[i + 2] == s)
                {
                    return true;

                }//end if
            }//end for
            for (int i = 0; i <= 2; i++)
            {
                if (Board[i] == s && Board[i + 3] == s && Board[i + 6] == s) return true;
            }//end for
            return false;
        }//end HasThreeRow
        public bool HasThreeDiag(string s)
        {
            if ((Board[0] == s && Board[4] == s && Board[8] == s) || (Board[2] == s && Board[4] == s && Board[6] == s)) return true;
            return false;
        }//end HasThreeDiag
        public bool HasWinner(string s)
        {
            if (HasThreeDiag(s) || HasThreeRow(s)) return true;
            else return false;
        }//end HasWinner
        public bool IsSpaceEmpty(int i)
        {
            return Board[i] == " ";
        }//end CheckSpace
        public bool IsFull()
        {
            for (int i = 0; i <= 8; i++)
            {
                if (Board[i] == " ") return false;
            }//end for
            return true;
        }//end IsFull
        public bool IsCat()
        {
            if (!HasWinner("X") && !HasWinner("O") && IsFull()) return true;
            else return false;
        }//end IsCat
    }//end class
}//end name
