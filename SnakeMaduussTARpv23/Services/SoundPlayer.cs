using NAudio.Wave;
using System;
using System.Threading.Tasks;

namespace SnakeMaduussTARpv23.Services
{
    public class SoundPlayer : IDisposable
    {
        private IWavePlayer _waveOutEvent;
        private AudioFileReader _audioFileReader;

        public void Play(string filePath)
        {
            _waveOutEvent = new WaveOutEvent();
            _audioFileReader = new AudioFileReader(filePath);
            _waveOutEvent.Init(_audioFileReader);
            _waveOutEvent.Play();

            _waveOutEvent.PlaybackStopped += (sender, args) =>
            {
                Dispose();
            };
        }

        public void PlayAsync(string filePath)
        {
            Task.Run(() => Play(filePath));
        }

        public void Dispose()
        {
            _waveOutEvent?.Dispose();
            _audioFileReader?.Dispose();
        }
    }
}
