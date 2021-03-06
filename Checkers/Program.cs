﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args) //OK
        {
            string wanttoplay = "yes";
            int playerpoints = 0;
            int computerpoints = 0;
            Console.WriteLine("Welcome to Checkers \n In your turn, first enter the piece's row and column number you wish to play, \n and then enter thr direction number(1/3/7/9)");
            while (wanttoplay.ToLower() != "no")
            {
                int gameresult = Game();
                if (gameresult == 1)
                {
                    Console.WriteLine("The computer won!!");
                    computerpoints++;
                }
                else if (gameresult == -1)
                {
                    Console.WriteLine("You won!!");
                    playerpoints++;
                }
                WhoLeads(playerpoints, computerpoints);
                Console.WriteLine("Do you want to continue?");
                wanttoplay = Console.ReadLine();
            }
            //char[,] state = new char[8, 8] {{'-','X','-','X','-','X','-','X'},
            //                                {'X','-','X','-','X','-','X','-'},
            //                                {'-','B','-','B','-','B','-','-'},                                           
            //                                {'-','-','-','-','-','-','B','-'},
            //                                {'-','-','-','-','-','-','-','O'},
            //                                {'O','-','O','-','O','-','-','-'},
            //                                {'-','O','-','O','-','O','-','O'},
            //                                {'O','-','O','-','O','-','O','-'}};
            //char[,] state = new char[8, 8] {{'-','X','-','X','-','X','-','X'},
            //                                {'X','-','X','-','X','-','X','-'},
            //                                {'-','B','-','B','-','O','-','B'},                                           
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'O','-','O','-','O','-','-','-'},
            //                                {'-','O','-','O','-','O','-','O'},
            //                                {'O','-','O','-','O','-','O','-'}};
            //char[,] state = new char[8, 8] {{'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},                                           
            //                                {'-','-','X','-','-','-','-','-'},
            //                                {'-','O','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'}};
            //char[,] state = new char[8, 8] {{'-','-','-','-','-','-','-','-'},
            //                                {'-','-','X','-','-','-','-','-'},
            //                                {'-','-','-','X','-','-','-','X'},                                           
            //                                {'-','-','-','-','-','-','X','-'},
            //                                {'-','-','-','-','-','O','-','-'},
            //                                {'-','-','-','-','O','-','O','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'}};

            //Console.WriteLine("original state:");
            //PrintState(state);
            //char[,] newstate = new char[8, 8] {{'-','-','-','-','-','-','-','-'},
            //                                {'-','-','X','-','-','-','-','-'},
            //                                {'-','-','-','X','-','-','-','X'},                                           
            //                                {'-','-','-','-','-','-','X','-'},
            //                                {'-','-','-','O','-','O','-','-'},
            //                                {'-','-','-','-','-','-','O','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'}};
            //char[,] newstate = new char[8, 8] {{'-','X','-','X','-','X','-','X'},
            //                                   {'X','-','X','-','X','-','-','-'},
            //                                   {'-','B','-','B','-','-','-','B'},                                           
            //                                   {'-','-','-','-','X','-','-','-'},
            //                                   {'-','-','-','-','-','-','-','-'},
            //                                   {'O','-','O','-','O','-','-','-'},
            //                                   {'-','O','-','O','-','O','-','O'},
            //                                   {'O','-','O','-','O','-','O','-'}};

            //char[,] newstate = new char[8, 8] {{'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},                                           
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','B','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'},
            //                                {'-','-','-','-','-','-','-','-'}};
            //char[,] newstate = MovePiece(state, 6 - 1, 1 - 1, -1, 9);
            //char[,] newstate = ComputerTurn(state, 1);
            //Console.WriteLine("\n after computerturn:");
            //PrintState(newstate);
            //newstate = Burned(state, newstate, -1);
            //Console.WriteLine("\n after burned:");
            //PrintState(newstate);
            //newstate = BecomeKing(newstate, -1);
            //Console.WriteLine("\n after Becomeking:");
            //PrintState(newstate);
            Console.ReadKey();

        }


        static double Minimax(char[,] state, int player, int depth) //OK (needs checking)
        {//gameover, statevalue, nextstates,
            double statescore = StateValue(state, player);
            if (depth == 0 || GameOver(state) != 0)
                return statescore;
            double maxValue = double.NegativeInfinity;
            foreach (char[,] nextState in NextStates(state, player))
            {
                double nextValue = -Minimax(nextState, -player, depth - 1);
                if (nextValue > maxValue)
                    maxValue = nextValue;
            }
            if (double.IsNegativeInfinity(maxValue))
                return statescore;
            return maxValue;
        }


        private static List<char[,]> NextStates(char[,] state, int player) //add kings. + option to add multiple eating in here.
        {
            List<char[,]> result = new List<char[,]>();

            for (int i = 0; i < state.GetLength(0); i++) //change to (int i = 0; i < state.GetLength(0); i++) if added kings to function
            {
                for (int j = (i + 1) % 2; j < state.GetLength(1); j += 2)
                {
                    if (IsYours(state, i, j, player))//הבדיקות נוגעות גם למלכים
                    {   //הבדיקות נוגעות גם למלכים
                        if (IsAbleLeft(state, player, i, j))
                        {
                            TurnMove(state, player, result, i, j, 1, -1);
                        }
                        if (IsAbleRight(state, player, i, j))
                        {
                            TurnMove(state, player, result, i, j, 1, 1);
                        }
                        if (IsAbleEatingLeft(state, player, i, j))
                        {
                            TurnMove(state, player, result, i, j, 2, -2);
                        }
                        if (IsAbleEatingRight(state, player, i, j))
                        {
                            TurnMove(state, player, result, i, j, 2, 2);
                        }
                        if (IsKing(state, i, j)) //צעדים למלכים בלבד
                        {
                            if (IsAbleLeft(state, -player, i, j))
                            {
                                TurnMove(state, player, result, i, j, -1, -1);
                            }
                            if (IsAbleRight(state, -player, i, j))
                            {
                                TurnMove(state, player, result, i, j, -1, 1);
                            }
                            if (IsAbleEatingLeft(state, -player, i, j))
                            {
                                TurnMove(state, player, result, i, j, -2, -2);
                            }
                            if (IsAbleEatingRight(state, -player, i, j))
                            {
                                TurnMove(state, player, result, i, j, -2, 2);
                            }
                        }
                    }
                }
            }
            return result;
        }


        private static void TurnMove(char[,] state, int player, List<char[,]> result, int i, int j, int diffi, int diffj)
        {
            //move one step or eat.
            //for regular pieces: diffi>0, diffj!=0.
            //for kings:       *diffi!=0*, diffj!=0
            char[,] tempstate = (char[,])state.Clone();
            tempstate[i + diffi*player, j + diffj] = tempstate[i, j];
            tempstate[i, j] = '-';
            if (diffi == 2)
                tempstate[i + player, j + diffj / 2] = '-';
            //Console.WriteLine("tempstate (" + diffi + ", " + diffj + ")" + "after moving:");
            //PrintState(tempstate);
            //Console.WriteLine("\n\n");
            tempstate = Burned(state, tempstate, player);
            if (diffi == 2)
            {
                state = (char[,])tempstate.Clone();
                if (IsAbleEatingLeft(tempstate, player, i + diffi * player, j + diffj))
                {
                    TurnMove(state, player, result, i + diffi * player, j + diffj, 2, -2);
                }
                else if (IsAbleEatingRight(tempstate, player, i + diffi * player, j + diffj))
                {
                    TurnMove(state, player, result, i + diffi * player, j + diffj, 2, +2);
                }
                else if (IsAbleEatingLeft(tempstate, -player, i + diffi * player, j + diffj))
                {
                    TurnMove(state, player, result, i + diffi * player, j + diffj, -2, -2);
                }
                else if (IsAbleEatingRight(tempstate, -player, i + diffi * player, j + diffj))
                {
                    TurnMove(state, player, result, i + diffi * player, j + diffj, -2, +2);
                }
                else
                {
                    tempstate = BecomeKing(tempstate, player);
                    //Console.WriteLine("tempstate:");
                    //PrintState(tempstate);
                    //Console.WriteLine("\n\n");
                    result.Add(tempstate);
                }
            }
            //Console.WriteLine("after burn:");
            //PrintState(tempstate);
            //Console.WriteLine("\n\n");
            if (diffi!=2)
            {
                tempstate = BecomeKing(tempstate, player);
                //Console.WriteLine("tempstate:");
                //PrintState(tempstate);
                //Console.WriteLine("\n\n");
                result.Add(tempstate);
            }
        }
        //private static void TurnMoveBackwards(char[,] state, int player, List<char[,]> result, int i, int j, int diffi, int diffj)
        //{
        //    //move one step or eat.
        //    //for regular pieces: diffi>0, diffj!=0.
        //    //for kings:       *diffi!=0*, diffj!=0
        //    char[,] tempstate = (char[,])state.Clone();
        //    tempstate[i + diffi * player, j + diffj] = tempstate[i, j];
        //    tempstate[i, j] = '-';
        //    if (diffi == 2)
        //        tempstate[i + player, j + diffj / 2] = '-';
        //    //Console.WriteLine("tempstate (" + diffi + ", " + diffj + ")" + "after moving:");
        //    //PrintState(tempstate);
        //    //Console.WriteLine("\n\n");
        //    tempstate = Burned(state, tempstate, player);
        //    //Console.WriteLine("after burn:");
        //    //PrintState(tempstate);
        //    //Console.WriteLine("\n\n");
        //    tempstate = BecomeKing(tempstate, player);
        //    //Console.WriteLine("tempstate:");
        //    //PrintState(tempstate);
        //    //Console.WriteLine("\n\n");
        //    result.Add(tempstate);
        //}

        private static double StateValue(char[,] state, int player) //לשנות כך שיהיה שימוש ב GameOver
        {
            /*
            1) more pieces than the other (regular and kings calculation)
            2) + eat and be eaten (not many points because it could lead to being eaten next turn) 
               - be eaten and eat (")
               * could be affected by if you have more pieces or not! (like atack and defence mode)
            3) + eat with no harm (win many points)
               - be eaten with no eating (lose many point)
            4) traps (????????)
            5) do not abandone 1st line
            6) clear way towards king
            7) building triangles and squares
            8) double and triple eating
            9)
            10)
            */
            //throw new NotImplementedException();
            double value = 0;
            value += PiecesSub(state) * 50 * player; //1
            int whowon = GameOver(state);
            if (whowon != 0)
                return 1000 * player * whowon;
            return value;
        }


        static char[,] MovePiece(char[,] state, int i, int j, int player, int dir)
        {
            char[,] newstate = (char[,])state.Clone();
            if (i >= 0 && i < newstate.GetLength(0) && j >= 0 && j < newstate.GetLength(1))
            {
                if (i != 0 && IsYours(newstate, i, j, player) && (dir == 7 || dir == 9))
                {
                    if (newstate[i - 1, j + dir - 8] == '-')
                    {
                        newstate[i, j] = '-';
                        newstate[i - 1, j + dir - 8] = 'O';
                        return newstate;
                    }
                    else if (i != 1 && IsEnemy(newstate, i - 1, j + dir - 8, player) && newstate[i - 2, j + 2 * (dir - 8)] == '-')
                    {
                        newstate[i, j] = '-';
                        newstate[i - 1, j + dir - 8] = '-';
                        newstate[i - 2, j + 2 * (dir - 8)] = 'O';
                        return newstate;
                    }
                    else
                    {
                        Console.WriteLine("you can't move your piece there");
                        return null;
                    }
                }
                else if (newstate[i, j] == 'W' && (dir == 1 || dir == 3 || dir == 7 || dir == 9)) //complete kings
                {
                    newstate[i, j] = '-';
                    if (dir == 1)
                    {
                        if (i < newstate.GetLength(0) - 1 && j > 0 && newstate[i + 1, j - 1] == '-')
                            newstate[i + 1, j - 1] = 'W';
                        else if (i < newstate.GetLength(0) - 2 && j > 1 && IsEnemy(newstate, i + 1, j - 1, player) && newstate[i + 2, j - 2] == '-')
                        {
                            newstate[i + 1, j - 1] = '-';
                            newstate[i + 2, j - 2] = 'W';
                        }
                    }
                    else if (dir == 3)
                    {
                        if (i < newstate.GetLength(0) - 1 && j < newstate.GetLength(1) - 1 && newstate[i + 1, j + 1] == '-')
                            newstate[i + 1, j + 1] = 'W';
                        else if (i < newstate.GetLength(0) - 2 && j < newstate.GetLength(1) - 2 && IsEnemy(newstate, i + 1, j + 1, player) && newstate[i + 2, j + 2] == '-')
                        {
                            newstate[i + 1, j + 1] = '-';
                            newstate[i + 2, j + 2] = 'W';
                        }
                    }
                    else if (dir == 7)
                    {
                        if (i > 0 && j > 0 && newstate[i - 1, j - 1] == '-')
                            newstate[i - 1, j - 1] = 'W';
                        else if (i > 1 && j > 1 && IsEnemy(newstate, i - 1, j - 1, player) && newstate[i - 2, j - 2] == '-')
                        {
                            newstate[i - 1, j - 1] = '-';
                            newstate[i - 2, j - 2] = 'W';
                        }
                    }
                    else if (dir == 9)
                    {
                        if (i > 0 && j < newstate.GetLength(1) - 1 && newstate[i - 1, j + 1] == '-')
                            newstate[i - 1, j + 1] = 'W';
                        else if (i > 1 && j < newstate.GetLength(1) - 2 && IsEnemy(newstate, i - 1, j + 1, player) && newstate[i - 2, j + 2] == '-')
                        {
                            newstate[i - 1, j + 1] = '-';
                            newstate[i - 2, j + 2] = 'W';
                        }
                    }
                    else
                        newstate[i, j] = 'W';
                    return newstate;
                }
                else
                {
                    Console.WriteLine("Something wrong with the player's location or the direction you chose");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Something wrong with row or col number");
                return null;
            }

        }


        static void PrintState(char[,] state)
        {
            Console.WriteLine("   1  2  3  4  5  6  7  8\n");
            for (int i = 0; i < state.GetLength(0); i++)
            {
                Console.Write(i+1 + "  ");
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    Console.Write(state[i, j] + "  ");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine();
        }


        static void WhoLeads(int playerpoints, int computerpoints)
        {
            if (playerpoints > computerpoints)
                Console.WriteLine(playerpoints + " - " + computerpoints + " You lead!");
            else if (playerpoints == computerpoints)
                Console.WriteLine(playerpoints + " - " + computerpoints + " It's a tie!");
            else
                Console.WriteLine(playerpoints + " - " + computerpoints + " The computer leads!");
        }
        static char InputFromPlayer(int player)
        {
            if (player == 1)
                return 'X';
            return 'O';
        }
        static int PlayerFromInput(char input)
        {
            if (input == 'X')
                return 1;
            return -1;
        }
        static int GameOver(char[,] state) //updated
        {
            bool flagwhite = false;
            bool flagblack = false;
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = (i + 1) % 2; j < state.GetLength(1); j += 2)
                {
                    if (state[i, j] == KingInputFromPlayer(1) || state[i, j] == InputFromPlayer(1))
                        flagblack = true;
                    else if (state[i, j] == KingInputFromPlayer(-1) || state[i, j] == InputFromPlayer(-1))
                        flagwhite = true;
                }
            }
            if (!flagwhite) //no more white pieces (O)
                return 1; //computer won
            else if (!flagblack) //no more black pieces (X)
                return -1; //player won
            else
                return 0;
        }
        static int Game()
        {
            int player;
            int turncounter = 0;
            Console.WriteLine("Do you want to start?");
            string wanttostart = Console.ReadLine();
            if (wanttostart.ToLower() != "no")
                player = -1;
            else
                player = 1;

            char[,] state = new char[8, 8] 
                                           {{'-','X','-','X','-','X','-','X'},
                                            {'X','-','X','-','X','-','X','-'},
                                            {'-','X','-','X','-','X','-','X'},                                           
                                            {'-','-','-','-','-','-','-','-'},
                                            {'-','-','-','-','-','-','-','-'},
                                            {'O','-','O','-','O','-','O','-'},
                                            {'-','O','-','O','-','O','-','O'},
                                            {'O','-','O','-','O','-','O','-'}};
            //char[,] state = new char[8, 8]
                                           //{{'-','-','-','-','-','-','-','-'},
                                           // {'-','-','-','-','-','-','-','-'},
                                           // {'-','-','-','-','-','-','-','-'},                                           
                                           // {'-','-','-','-','-','-','-','-'},
                                           // {'-','-','-','O','-','-','-','-'},
                                           // {'-','-','-','-','-','-','-','-'},
                                           // {'-','-','-','-','-','-','-','-'},
                                           // {'-','-','-','-','-','-','B','-'}};
            PrintState(state);
            do
            {
                if (player == -1)
                    state = PlayerTurn(state);
                else
                    state = ComputerTurn(state, player);
                PrintState(state);
                turncounter++;
                player = -player;
            } while (GameOver(state) == 0);

            return GameOver(state);
        }
        static char[,] PlayerTurn(char[,] state)
        {
            while (true)
            {
                Console.WriteLine("enter row");
                int row = int.Parse(Console.ReadLine());
                Console.WriteLine("enter column");
                int col = int.Parse(Console.ReadLine());
                Console.WriteLine("enter direction (for regular player 7/9, for king 1/3/7/9)");
                int dir = int.Parse(Console.ReadLine());
                char[,] newstate = MovePiece(state, row - 1, col - 1, -1, dir);
                if (row <= state.GetLength(0) && col <= state.GetLength(1) && row >= 1 && col >= 1)
                {
                    if (newstate != null)
                    {
                        newstate = Burned(state, newstate, -1);
                        newstate = BecomeKing(newstate, -1);
                        return newstate;
                    }
                }
                PrintState(state);
                Console.WriteLine("Please enter row, column and direction again");
            }
        }
        static char[,] ComputerTurn(char[,] state, int player)
        {
            Console.WriteLine("Computer's turn:");
            double idealscore = double.PositiveInfinity;
            char[,] idealstate = null;
            foreach (char[,] nextstate in NextStates(state, player))
            {
                double score = Minimax(nextstate, -1, 3);
                if (score < idealscore)
                {
                    idealscore = score;
                    idealstate = nextstate;
                }
                //!!Randomising option!! if (Minimax(nextstate, -1, 4) = idealscore)
                //{
                //    
                //    if(
                //}
            }
            return idealstate;
        }

        static char[,] Burned(char[,] state, char[,] newstate, int player) //מטרת הפעולה לבדוק אם נאכל שחקן של היריב, ובמקרה ולא נאכל - לבדוק האם יש שחקנים שרופים (שיכלו לאכול ולא אכלו). במידה וכן הפעולה מוחקת את הראשון שבהם
        {
            char[,] burnstate = new char[8, 8];
            char temp = InputFromPlayer(player);
            int imovedfrom = 0, jmovedfrom = 0, imovedto = 0, jmovedto = 0;
            for (int i = 0; i < state.GetLength(0); i++)//בודק האם נאכל אויב ומגדיר את מערך ברנסטייט (מאנגלית) ואת מיקום השחקן שזז
            {
                for (int j = 0; j < state.GetLength(1); j ++)
                {
                    if (state[i, j] == newstate[i, j]) //אם לא חל שינוי במשבצת הספציפית בלוח
                    {
                        burnstate[i, j] = state[i, j];
                    }
                    else if (IsEnemy(state, i, j, player))//אויב נאכל - אין טעם לבדוק שחקנים שרופים
                        return newstate;
                    else if (IsYours(state, i, j, player))//המקום ממנו זז השחקן שלך
                    {
                        burnstate[i, j] = state[i, j];
                        imovedfrom = i;
                        jmovedfrom = j;
                    }
                    else//המקום אליו זז השחקן שלך
                    {
                        burnstate[i, j] = '-';
                        imovedto = i;
                        jmovedto = j;
                    }
                }
            }
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = (i + 1) % 2; j < state.GetLength(1); j += 2)
                {
                    //if (i == addi && j == addj)
                    //    continue;
                    if(IsYours(burnstate,i,j,player))
                    {
                        if (IsAbleEatingLeft(burnstate, player, i, j) || IsAbleEatingRight(burnstate, player, i, j) || (burnstate[i, j] == KingInputFromPlayer(player) && (IsAbleEatingRight(burnstate, -player, i, j) || IsAbleEatingLeft(burnstate, -player, i, j))))
                        {
                            burnstate[i, j] = '-';
                            burnstate[imovedto, jmovedto] = burnstate[imovedfrom, jmovedfrom];
                            burnstate[imovedfrom, jmovedfrom] = '-';
                            return burnstate;
                        }
                    }
                }
            }
            return newstate;
        }
        static bool IsAbleLeft(char[,] state, int player, int i, int j)
        {
            if (j > 0 && (player == 1 && i < 7 || player == -1 && i > 0) && state[i + player, j - 1] == '-')
                return true;
            return false;
        }
        static bool IsAbleRight(char[,] state, int player, int i, int j)
        {
            if (j < state.GetLength(1) - 1 && (player == 1 && i < 7 || player == -1 && i > 0) && state[i + player, j + 1] == '-')
                return true;
            return false;
        }
        static bool IsAbleEatingLeft(char[,] state, int player, int i, int j)
        {
            if (j > 1 && (player == 1 && i < 6 || player == -1 && i > 1) && IsEnemy(state, i + player, j - 1, player) && state[i + 2 * player, j - 2] == '-')
                return true;
            return false;
        }
        static bool IsAbleEatingRight(char[,] state, int player, int i, int j)
        {
            if (j < state.GetLength(1) - 2 && (player == 1 && i < 6 || player == -1 && i > 1) && IsEnemy(state, i + player, j + 1, player) && state[i + 2 * player, j + 2] == '-')
                return true;
            return false;
        }
        static int PiecesSub(char[,] state)
        {
            int CountBlack = 0, CountWhite = 0;
            foreach (char i in state)
            {
                if (i == 'X')//black
                    CountBlack++;
                else if (i == 'O')//white
                    CountWhite++;
                else if (i == 'B')//white
                    CountBlack += 3;
                else if (i == 'W')//white
                    CountWhite += 3;
            }
            return CountBlack - CountWhite;
        }
        static char KingInputFromPlayer(int player)
        {
            if (player == 1)
                return 'B';
            return 'W';
        }
        static bool IsEnemy(char[,] state, int i, int j, int player)
        {
            if (state[i, j] == KingInputFromPlayer(-player) || state[i, j] == InputFromPlayer(-player))
                return true;
            return false;
        }
        static bool IsYours(char[,] state, int i, int j, int player)
        {
            if (state[i, j] == KingInputFromPlayer(player) || state[i, j] == InputFromPlayer(player))
                return true;
            return false;
        }
        static bool IsKing(char[,] state, int i, int j)
        {
            if (state[i, j] == 'B' || state[i, j] == 'W')
                return true;
            return false;
        }
        static char[,] BecomeKing(char[,] state, int player)
        {
            char[,] newstate = (char[,])state.Clone();
            if (player == -1)
            {
                for (int j = 1; j < newstate.GetLength(1); j += 2)
                {
                    if (newstate[0, j] == InputFromPlayer(player))
                        newstate[0, j] = 'W';
                }
            }
            else
            {
                for (int j = 0; j < newstate.GetLength(1); j += 2)
                {
                    if (newstate[7, j] == InputFromPlayer(player))
                        newstate[7, j] = 'B';
                }
            }
            return newstate;
        }
        static int FirstLine(int player, int length)
        {
            if (player == 1)
                return 0;
            else
                return length - 1;
        }
        /*           for (int i=0; i<state.GetLength(0); i++)
       {
           for (int j = (i + 1) % 2; j < state.GetLength(1); j += 2)
           {
           
           }
       }*/
    }
}
