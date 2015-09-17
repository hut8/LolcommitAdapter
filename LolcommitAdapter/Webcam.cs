using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;

namespace LolcommitAdapter
{
    public class Webcam
    {
        private FilterInfoCollection _videoDevices;
        private FilterInfo _currentDevice;
        private VideoCaptureDevice _videoSource;
        private Bitmap _frame;


        #region Singleton Stuff
        private Webcam()
        {
            _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            _currentDevice = _videoDevices.Cast<FilterInfo>().First();
            _videoSource = new VideoCaptureDevice(_currentDevice.MonikerString);
            _videoSource.NewFrame += _videoSource_NewFrame;
        }

        void _videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (eventArgs.Frame.Width == 0)
            {
                return;
            }
            _frame = eventArgs.Frame.Clone() as Bitmap;

            _frame.Save(@"C:\Users\14581\Desktop\lolcommit.bmp");
            _videoSource.SignalToStop();
        }
        private static Webcam _instance;
        public static Webcam Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Webcam();
                }
                return _instance;
            }
        }
        #endregion

        public Bitmap TakePicture()
        {
            _videoSource.Start();
            _videoSource.WaitForStop();
            return _frame;
        }


    }
}
