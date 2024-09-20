using System;
using System.IO;

namespace SnakeMaduussTARpv23.Services
{
    public class FileSaveRead
    {
        public void SaveGameData(string playerName, TimeSpan timePlayed)
        {
            string filePath = @"C:\Users\ripmy\source\repos\SnakeMaduussTARpv231\SnakeMaduussTARpv23\Services\TxtFiles\Text.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Player: {playerName}, Time Played: {timePlayed.Minutes:D2}:{timePlayed.Seconds:D2}");
                writer.WriteLine("---------------------------------------------------");
            }
        }
    }
}
