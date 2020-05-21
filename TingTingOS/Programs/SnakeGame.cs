using System;
using System.Collections.Generic;

namespace TingTingOS.Programs
{
    public static class SnakeGame
    {
        static int[] ConsoleMatrix;
        static List<int[]> Commands;
        static List<int[]> SnakeBody;
        static List<int> Food;
        static Random random = new Random();

        public static void Init()
        {
            ConfigSnake();
            PrintGame();

            Console.ReadKey(true);

            Run();
        }

        public static void Run()
        {
            ConsoleKey inputKey;

            while (true)
            {
                while (IsGameover())
                {
                    PrintGame();

                    Boolean endGame = false;
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.R:
                            ConfigSnake();
                            break;
                        case ConsoleKey.Escape:
                            endGame = true;
                            break;
                    }

                    if (endGame)
                    {
                        break;
                    }
                }

                while (!Console.KeyAvailable && !IsGameover())
                {
                    UpdateDirections();
                    UpdateSnakePosotion();

                    CheckIfTouchFood();

                    Console.Clear();
                    UpdateGameMatrix();
                    PrintGame();

                    Delay(20000000);
                }

                inputKey = Console.ReadKey(true).Key;

                if (inputKey == ConsoleKey.LeftArrow)
                {
                    if (SnakeBody[0][1] != 3)
                    {
                        Commands.Add(new int[2] { 1, 0 });
                    }
                }
                else if (inputKey == ConsoleKey.UpArrow)
                {
                    if (SnakeBody[0][1] != 2)
                    {
                        Commands.Add(new int[2] { 4, 0 });
                    }
                }
                else if (inputKey == ConsoleKey.RightArrow)
                {
                    if (SnakeBody[0][1] != 1)
                    {
                        Commands.Add(new int[2] { 3, 0 });
                    }
                }
                else if (inputKey == ConsoleKey.DownArrow)
                {
                    if (SnakeBody[0][1] != 4)
                    {
                        Commands.Add(new int[2] { 2, 0 });
                    }
                }
                else if (inputKey == ConsoleKey.R)
                {
                    ConfigSnake();
                }
                else if (inputKey == ConsoleKey.Escape)
                {
                    break;
                }
            }

            Console.Clear();
        }

        private static void ConfigSnake()
        {
            ConsoleMatrix = new int[1760];

            Commands = new List<int[]>();

            SnakeBody = new List<int[]>();
            SnakeBody.Add(new int[2] { XY2P(10, 10), 3 });

            Food = new List<int>();
            Food.Add(RandomFood());
            
            UpdateGameMatrix();
        }

        private static void Delay(int time)
        {
            for (int i = 0; i < time; i++);
        }

        private static int XY2P(int x, int y)
        {
            return y * 80 + x;
        }

        private static int RandomFood()
        {
            int randomNumber = random.Next(81, 1700);
            return randomNumber;
        }

        private static bool IsGameover()
        {
            int head = SnakeBody[0][0];

            // Bite self
            for (int i = 1; i < SnakeBody.Count; i++)
            {
                if (head == SnakeBody[i][0])
                {
                    return true;
                }
            }

            // Out of playing field
            if (head % 80 == 79 || head % 80 == 0 || head <= 1760 && head >= 1679 || head <= 79 && head >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void UpdateDirections()
        {
            List<int[]> temp = new List<int[]>();

            foreach (int[] command in Commands)
            {
                if (command[1] < SnakeBody.Count)
                {
                    SnakeBody[command[1]][1] = command[0];
                    command[1] = command[1] + 1;
                    temp.Add(command);
                }
            }

            Commands = temp;
        }

        private static void UpdateSnakePosotion()
        {
            List<int[]> temp = new List<int[]>();

            foreach (int[] point in SnakeBody)
            {
                switch (point[1])
                {
                    case 1:
                        point[0] = point[0] - 1;
                        break;
                    case 2:
                        point[0] = point[0] + 80;
                        break;
                    case 3:
                        point[0] = point[0] + 1;
                        break;
                    case 4:
                        point[0] = point[0] - 80;
                        break;
                    default:
                        break;
                }
                temp.Add(point);
            }

            SnakeBody = temp;
        }

        private static void CheckIfTouchFood()
        {
            List<int> tempfood = new List<int>();

            foreach (int pos in Food)
            {
                if (SnakeBody[0][0] == pos)
                {
                    tempfood.Add(RandomFood());

                    int tmp1 = SnakeBody[SnakeBody.Count - 1][0];
                    int tmp2 = SnakeBody[SnakeBody.Count - 1][1];

                    if (tmp2 == 1)
                    {
                        tmp1 = tmp1 + 1;
                    }
                    else if (tmp2 == 2)
                    {
                        tmp1 = tmp1 - 80;
                    }
                    else if (tmp2 == 3)
                    {
                        tmp1 = tmp1 - 1;
                    }
                    else if (tmp2 == 4)
                    {
                        tmp1 = tmp1 + 80;
                    }

                    SnakeBody.Add(new int[] { tmp1, tmp2 });
                }
                else
                {
                    tempfood.Add(pos);
                }
            }

            Food = tempfood;
        }

        private static void UpdateGameMatrix()
        {
            for (int i = 0; i < ConsoleMatrix.Length; i++)
            {
                ConsoleMatrix[i] = 0;
            }

            foreach (int[] point in SnakeBody)
            {
                ConsoleMatrix[point[0]] = 3;
            }

            foreach (int point in Food)
            {
                ConsoleMatrix[point] = 2;
            }

            for (int i = 0; i < ConsoleMatrix.Length; i++)
            {
                if (i <= 79 && i >= 0)
                {
                    ConsoleMatrix[i] = 1;
                }
                else if (i <= 1760 && i >= 1679)
                {
                    ConsoleMatrix[i] = 1;
                }
                else if (i % 80 == 0)
                {
                    ConsoleMatrix[i] = 1;
                }

                else if (i % 80 == 79)
                {
                    ConsoleMatrix[i] = 1;
                }
            }
        }

        private static string ScoreWithSpacing()
        {
            // TODO Use PadRight or String.Format("{0,5}")
            if (SnakeBody.Count < 10)
            {
                return SnakeBody.Count + "   ";
            }
            else if (SnakeBody.Count < 100)
            {
                return SnakeBody.Count + "  ";
            }
            else if (SnakeBody.Count < 1000)
            {
                return SnakeBody.Count + " ";
            }
            else
            {
                return SnakeBody.Count + "";
            }
        }

        private static void PrintGame()
        {
            for (int i = 0; i < ConsoleMatrix.Length; i++)
            {
                if (IsGameover() && i == 585)
                {
                    Console.Write("###############################");
                    i += 30;
                }
                else if (IsGameover() && i == 665)
                {
                    Console.Write("###############################");
                    i += 30;
                }
                else if (IsGameover() && i == 745)
                {
                    Console.Write("####                       ####");
                    i += 30;
                }
                else if (IsGameover() && i == 825)
                {
                    Console.Write("####       GAMEOVER        ####");
                    i += 30;
                }
                else if (IsGameover() && i == 905)
                {
                    Console.Write("####      Score: " + ScoreWithSpacing() + "      ####");
                    i += 30;
                }
                else if (IsGameover() && i == 985)
                {
                    Console.Write("####                       ####");
                    i += 30;
                }
                else if (IsGameover() && i == 1065)
                {
                    Console.Write("###############################");
                    i += 30;
                }
                else if (IsGameover() && i == 1145)
                {
                    Console.Write("###############################");
                    i += 30;
                }
                else if (ConsoleMatrix[i] == 1)
                {
                    Console.Write("#");
                }
                else if (ConsoleMatrix[i] == 2)
                {
                    Console.Write("$");
                }
                else if (ConsoleMatrix[i] == 3)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.Write("#           Score: " + ScoreWithSpacing() + "        Exit : ESC             Restart : R              #");
            Console.Write("################################################################################");
        }
    }
}
