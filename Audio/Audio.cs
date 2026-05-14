using System;
using System.IO;
using System.Media;

namespace CybersecurityChatbotWPF.Audio
{
    public class AudioPlayer : IDisposable
    {
        private SoundPlayer? _player;
        private bool _disposed = false;

        public void PlayGreeting(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    _player = new SoundPlayer(filePath);
                    _player.Load();
                    _player.Play();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio error: {ex.Message}");
            }
        }

        public void StopGreeting()
        {
            try
            {
                _player?.Stop();
            }
            catch { }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _player?.Dispose();
                _disposed = true;
            }
        }
    }
}