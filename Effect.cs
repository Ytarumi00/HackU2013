using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;


    class Effect
    {
        public IplImage inputImage;

        public Effect(IplImage input)
        {
            inputImage = Cv.CloneImage(input);

        }
         ~Effect()
        {
            Cv.ReleaseImage(inputImage);
        }


        public IplImage test_effect(IplImage inputImg)
        {
            IplImage tmp_Img = Cv.CloneImage(inputImg);
            int x, y;
            for (y = 0; y < tmp_Img.Height; y++)
            {
                for (x = 0; x < tmp_Img.Width; x++)
                {
                    CvColor c = tmp_Img[y, x];
                    tmp_Img[y, x] = new CvColor()
                    {
                        B = (byte)Math.Round(c.B * 0.7 + 10),
                        G = (byte)Math.Round(c.G * 1.0),
                        R = (byte)Math.Round(c.R * 0.0),
                    };
                }
            }
            return tmp_Img;
        }



    }

