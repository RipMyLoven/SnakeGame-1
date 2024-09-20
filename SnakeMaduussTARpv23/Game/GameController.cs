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
        private static object _consoleLock = new object();

        public static void StartGame(string playerName) 
        {
            Console.Clear();

            IWavePlayer waveOutDevice = new WaveOutEvent();
            AudioFileReader audioFileReader = new AudioFileReader(@"..\..\..\song.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();

            FileSaveRead fileSaveRead = new FileSaveRead();

            Console.SetWindowSize(120, 25);

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
                    lock (_consoleLock)
                    {
                        Console.SetCursorPosition(83, 0);
                        Console.Write(new string(' ', Console.WindowWidth - 83));
                        Console.SetCursorPosition(83, 0);
                        Console.WriteLine($"Time Played: {ts.Minutes:D2}:{ts.Seconds:D2}");
                    }
                    Thread.Sleep(500);
                }
            });
            timerThread.Start();

            while (true)
            {
                lock (_consoleLock)
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
                }
            }

            stopwatch.Stop();
            fileSaveRead.SaveGameData(playerName, stopwatch.Elapsed);

            GameOver.WriteGameOver();
            Console.ReadLine();
        }
    }
}
