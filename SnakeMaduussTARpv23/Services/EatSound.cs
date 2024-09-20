using System;

namespace SnakeMaduussTARpv23.Services
{
    public static class EatSound
    {
        public const string EatSoundFilePath = @"C:\Users\ripmy\source\repos\SnakeMaduussTARpv231\SnakeMaduussTARpv23\Music\Старый бог.mp3";

        public static void PlayEatSound()
        {
            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.PlayAsync(EatSoundFilePath);
            }
        }
    }
}
