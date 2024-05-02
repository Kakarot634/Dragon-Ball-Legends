using System;

namespace SnakeGame
{
    class Program
    {
        static int width = 20;
        static int height = 10;
        static int score = 0;
        static int delay = 200;
        static bool gameover = false;
        static Random random = new Random();

        static List<int> snakeX = new List<int>();
        static List<int> snakeY = new List<int>();

        static int foodX;
        static int foodY;

        static int directionX = 1;
        static int directionY = 0;

        static void drawGame()
        {
            Console.Clear();
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O");

            for (int i = 0; i < snakeX.Count; i++)
            {
                Console.SetCursorPosition(snakeX[i], snakeY[i]);
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("■");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("■");
                }
            }
            Console.SetCursorPosition(0, height + 1);
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("Score: " + score);
        }
        static void changeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (directionY != 1)
                    {
                        directionX = 0;
                        directionY = -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (directionX != 1)
                    {
                        directionX = 0;
                        directionY = 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (directionX != -1)
                    {
                        directionX = -1;
                        directionY = 0;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (directionX != -1)
                    {
                        directionX = 1;
                        directionY = 0;
                    }
                    break;
            }
        }
        static void moveSnake()
        {
            for (int i = snakeX.Count - 1; i > 0; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }
            snakeX[0] += directionX;
            snakeY[0] += directionY;

            if (snakeX[0] >= width)
            {
                snakeX[0] = 0;
            }
            else if (snakeX[0] < 0)
            {
                snakeX[0] = width - 1;
            }

            if (snakeY[0] >= height)
            {
                snakeY[0] = 0;
            }
            else if (snakeY[0] < 0)
            {
                snakeY[0] = height - 1;
            }

            for (int i = 1; i < snakeX.Count; i++)
            {
                if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
                {
                    gameover = true;
                }
            }
        }
        static bool isEatingFood()
        {
            if (snakeX[0] == foodX && snakeY[0] == foodY)
            {
                return true;
            }
            return false;
        }
        static void placeFood()
        {
            foodX = random.Next(0, width);
            foodY = random.Next(0, height);
        }
        static bool isCollusion()
        {
            if (snakeX[0] < 0 || snakeX[0] >= width || snakeY[0] < 0 || snakeY[0] >= height)
            {
                return true;
            }
            for (int i = 0; i < snakeX.Count; i++)
            {
                if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            Console.Title = "Snake Game";
            Console.CursorVisible = false;

            snakeX.Add(0);
            snakeY.Add(0);

            placeFood();

            while (!gameover)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    changeDirection(key);
                }

                moveSnake();

                if (isCollusion())
                {
                    gameover = true;
                }

                if (isEatingFood())
                {
                    score++;
                    snakeX.Add(0);
                    snakeY.Add(0);
                    placeFood();
                }

                drawGame();

                Thread.Sleep(delay);
            }
            Console.Clear();
            System.Console.WriteLine("Game Over! Your Score: " + score);
            Console.ReadKey();
        }
    }
}