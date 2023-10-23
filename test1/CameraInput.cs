using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

using System.Drawing;

namespace testOne
{
    public class CameraInput
    {
        private event EventHandler capture;

        private Mat? currentImage;

        private VideoCapture? camera;

        private bool ready;

        public void captureComplete(Mat _newImage)
        {
            //Console.WriteLine("image taken");
            this.currentImage = _newImage;
            this.ready = true;
        }

        public CameraInput(int _cameraIndex)
        {
            this.camera = new VideoCapture(_cameraIndex);
            this.ready = true;

            capture += async (o, e) =>
            {
                if (this.camera != null)
                {
                    Mat newImage = await Task.Run(() => this.camera.QueryFrame());
                    //Mat modImage = await Task.Run(() => this.convert(newImage));
                    
                    this.captureComplete(newImage);
                } else {
                    //Console.WriteLine("null");  
                }
            };
        }

        private Mat convert(Mat image)
        {
            Mat imgGrayscale = new Mat(image.Size, DepthType.Cv8U, 1);
            Mat imgBlurred = new Mat(image.Size, DepthType.Cv8U, 1);
            Mat imgCanny = new Mat(image.Size, DepthType.Cv8U, 1);

            CvInvoke.CvtColor(image, imgGrayscale, ColorConversion.Bgr2Gray);

            CvInvoke.GaussianBlur(imgGrayscale, imgBlurred, new Size(5, 5), 1.5);

            CvInvoke.Canny(imgBlurred, imgCanny, 50, 100);

            return imgCanny;
        }

        public void captureImage()
        {
            if (this.ready == true)
            {
                this.ready = false;
                capture?.Invoke(this, EventArgs.Empty);
            } 
        }

        public Mat? getImage()
        {
            return this.currentImage;
        }
    }
}