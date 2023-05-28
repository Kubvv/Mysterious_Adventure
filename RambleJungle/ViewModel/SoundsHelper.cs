using System;
using System.IO;
using System.Windows.Media;

namespace RambleJungle.ViewModel
{
    public class SoundsHelper
    {
        private readonly MediaPlayer mediaPlayer = new();

        public void PlaySound(string soundName)
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Sounds\{soundName}.wav");
            if (!File.Exists(fileName))
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Sounds\{soundName}.mp3");
            }
            if (File.Exists(fileName))
            {
                mediaPlayer.Stop();
                mediaPlayer.Open(new Uri(fileName));
                mediaPlayer.Play();
            }
        }
    }
}
