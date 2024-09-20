using NAudio.Wave;
using SnakeMaduussTARpv23.Models;
using SnakeMaduussTARpv23.Services;
using System;
using System.Diagnostics;
using System.Threading;

namespace SnakeMaduussTARpv23.Game
{
    public static class GameController
    {
        public static void StartGame(string playerName) 
        {
            Console.Clear();

            IWavePlayer waveOutDevice = new WaveOutEvent();
            AudioFileReader audioFileReader = new AudioFileReader(@"C:\Users\ripmy\source\repos\SnakeMaduussTARpv231\SnakeMaduussTARpv23\Music\song.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();

            FileSaveRead fileSaveRead = new FileSaveRead();

            Console.SetWindowSize(80, 25);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            Console.ForegroundColor = ConsoleColor.Red;
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Thread timerThread = new Thread(() =>
            {
                while (true)
                {
                    TimeSpan ts = stopwatch.Elapsed;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine($"Time Played: {ts.Minutes:D2}:{ts.Seconds:D2}  ");
                    Thread.Sleep(1000);
                }
            });
            timerThread.Start();

            while (true)
            {

                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }


                if (snake.Eat(food))
                {
                    EatSound.PlayEatSound();
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true); 
                    snake.HandleKey(key.Key);
                }

                Console.SetCursorPosition(0, 1);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            stopwatch.Stop();
            fileSaveRead.SaveGameData(playerName, stopwatch.Elapsed); 

            GameOver.WriteGameOver();
            Console.ReadLine();
        }
    }
}
