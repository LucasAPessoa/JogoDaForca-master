using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Player
{
    class LoopingPlayer
    {
        private static WaveOutEvent outputDevice;
        private static AudioFileReader audioFile;
        private static bool isLooping;

        public static void StartLoop(string caminho)
        {
            if (isLooping) return;

            audioFile = new AudioFileReader(caminho);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.PlaybackStopped += Recomecar;

            isLooping = true;
            outputDevice.Play();
        }

        public static void StopLoop()
        {
            if (!isLooping) return;

            isLooping = false;
            outputDevice?.Stop();

            // Limpeza de recursos
            outputDevice?.Dispose();
            audioFile?.Dispose();
            outputDevice = null;
            audioFile = null;
        }

        private static void Recomecar(object sender, StoppedEventArgs e)
        {
            if (isLooping && audioFile != null && outputDevice != null)
            {
                audioFile.Position = 0;
                outputDevice.Play();
            }
        }
    }
}
