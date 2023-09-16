using System;
using System.Threading;

namespace Snake_Game
{
    class Snake
    {
        //If you can somehow get 100 fruits this will break
        static int[,] PartPositions = new int[2,100];
        
        
        static bool isAlive = true;
        static bool Wisallowed = true;
        static bool Sisallowed = true; 
        static bool Aisallowed = true;
        static bool Disallowed = true;
        static char PreviousKey = 'w';

        static int counter = 0; 
        //Data for border
        int Height = 20;
        int Width = 30;

        int[] X = new int[50];
        int[] Y = new int[50];


        static int fruitX;
        static int fruitY;

        int parts = 3;

        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        char key = 'W';
        //it will save previous position of snake for the number of parts it has

        Random rnd = new Random();

        Snake()
        {
            //Where the snake starts
            X[0] = 5;
            Y[0] = 5;

            


            Console.CursorVisible = false;
            //Sets the fruit to a random location within the border
            fruitX = rnd.Next(2, (Width - 2));
            fruitY = rnd.Next(2, (Height - 2));
        }
        public void WriteBoard() // creates background
        {
            Console.Clear();
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.SetCursorPosition(i, (Height + 2));
                Console.Write("-");
            }
            for (int i = 1; i <= (Height + 1); i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("|");
            }
            for (int i = 1; i <= (Height + 1); i++)
            {
                Console.SetCursorPosition((Width + 2), i);
                Console.Write("|");
            }
        }

        public void Input()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.KeyChar;
                key = char.ToLower(keyInfo.KeyChar);





                if (key != 'w' && key != 's' && key != 'a' && key != 'd')
                {
                    key = PreviousKey;
                }
                
                if (key == 'w' && Wisallowed == false)
                {
                    key = 's';
                    
                    
                }
                else if (key == 's' && Sisallowed == false)
                {
                    key = 'w';
                    
                    
                }
                else if (key == 'a' && Aisallowed == false)
                {
                    key = 'd';
                    
                }
                
                else if (key == 'd' && Disallowed == false)
                {
                    key = 'a';
                    
                }
                
            }
        }

        public void WritePoint(int x, int y)
        {
            Console.SetCursorPosition(x, y);

            //Update Part positions
            
            
            Console.Write("#");
            if (counter >= 5)
            {
                if (x >= 32|| x <= 1 || y >= 22 || y <= 1)
                 {
                   isAlive = false;
                 }
            }
            if (counter >= 5)
            {
                
                for (int i = 0; i < parts -1 ; i++)
                {
                    PartPositions[0, i] = PartPositions[0, i + 1];
                    PartPositions[1, i] = PartPositions[1, i + 1];
                }
            

                
           //Checks if current position is one of the part's position
                PartPositions[0, parts -1 ] = x;
                PartPositions[1, parts -1 ] = y;

                for (int i = 0; i < parts - 1; i++)
                {
                    if (PartPositions[0, parts - 1] == PartPositions[0, i] && PartPositions[1, parts - 1] == PartPositions[1, i])
                    {
                        isAlive = false;
                    }
                }
                
               
            }

            
            
            
        }

        public void Logic() // Randomizes fruit position
        {
            if (X[0] == fruitX)
            {
                if (Y[0] == fruitY)
                {
                    parts++;
                    fruitX = rnd.Next(2, (Width - 2));
                    fruitY = rnd.Next(2, (Height - 2));


                }
            }
            
            for (int i = parts; i > 1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
            switch (key)
            {
                case'w' :
                    Y[0]--;
                    counter++;
                    PreviousKey = 'w';
                    Sisallowed = false;
                    Aisallowed = true;
                    Disallowed = true;

                    break;
                case 's':
                    Y[0]++;
                    counter++;
                    PreviousKey = 's';
                    Wisallowed = false;
                    Aisallowed = true;
                    Disallowed = true;

                    break;
                case 'd':
                    X[0]++;
                    counter++;
                    PreviousKey = 'd';
                    Aisallowed = false;
                    Wisallowed = true;
                    Sisallowed = true;

                    break;
                case 'a':
                    X[0]--;
                    counter++;
                    PreviousKey = 'a';
                    Disallowed = false;
                    Wisallowed = true;
                    Sisallowed = true;

                    break;
            }
            
           
            for (int i = 0; i <= (parts - 1); i++)
            {
                WritePoint(X[i], Y[i]);
                WriteFruit(fruitX, fruitY);
            }
            //Decides how fast the program goes
            Thread.Sleep(150);
        }

        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine("YOU HAVE DIED\nYou have collected {0} Fruits",parts-3);
        }

        public void WriteFruit(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("#");
        }

        static void Main(string[] args)
        {
            Snake snake = new Snake();
            while (isAlive)
            {
                //all functions called
                snake.WriteBoard();
                snake.Input();
                snake.Logic();
                
            }
            snake.GameOver();
            Console.ReadKey();

        }
    }
}