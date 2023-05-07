using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace wLib
{
    public class SOUND_PLAYER
    {
        LOG_T log = LOG_T.Instance;

        MediaPlayer mediaPlayer = new MediaPlayer();
        System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();

        public string source { get; set; }
        public bool playing { get; set; } = false;

        public SOUND_PLAYER()
        {
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        public bool OpenSource(string path)
        {
            bool rtv;

            rtv = System.IO.File.Exists(path);
            if (rtv)
            {
                source = path;
            }

            return rtv;
        }

        public void AlertON()
        {
            if (playing == true)
                return;

            mediaPlayer.Open(new Uri(System.Windows.Forms.Application.StartupPath + @"/" + source));

            try
            {
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Play();

                //if (mediaPlayer.HasAudio == false)
                    //soundPlayer.PlayLooping();

                playing = true;
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.Message);        
            }
        }

        public void AlertOFF()
        {
            mediaPlayer.Stop();
            //soundPlayer.Stop();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            AlertOFF();
        }
    }
}
