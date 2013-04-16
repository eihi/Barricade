using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace BarricadeSpel.Model
{
    class Music
    {
        /* youtube comment testen misschien efficienter
         * 
         * Hello Giawa
         * I also read the official documentation which suggests to use
         * WaveStream stream = new WaveChannel32(new Mp3FileReader(open.FileName));
         * instead of
         * using BlockAlignReductionStream(Wave­FormatConversionStream.CreateP­cmStream(new Mp3FileReader(open.FileName));
         * Can you explain the difference between them? Is that WaveFormatConversionStream converts mp3 to wav,﻿ but Mp3FileReader reads Mp3 directly?
         * Why did you choose BlockAlignReductionStream?
         * 
         */
        private BlockAlignReductionStream stream = null;
        private DirectSoundOut output = null;

        public Music(string path) 
        {
            DisposeMusic();
            WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(path)); 
            stream = new BlockAlignReductionStream(pcm);
            output = new DirectSoundOut();
            output.Init(stream);
        }

        public void Play() // output afspelen
        {
            if (output != null)
                if (output.PlaybackState != PlaybackState.Playing)
                    output.Play();
        }

        public void Pause() // output pauzeren
        {
            if (output != null)
                if (output.PlaybackState == PlaybackState.Playing)
                    output.Pause();
        }
        
        public void Stop() // output stoppen
        {
            if (output != null)
                if (output.PlaybackState == PlaybackState.Playing)
                    output.Stop();
        }

        public void DisposeMusic() // gebruikte resources opruimen
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    output.Stop();
                }
                output.Dispose();
                output = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }
    }
}
