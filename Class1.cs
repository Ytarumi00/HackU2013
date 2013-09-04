using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

public class opencvsharp_test
{
    public  IplImage testImage;

    public opencvsharp_test()
    {
        loadimage();
    }

    public void loadimage(){
        testImage = Cv.LoadImage("C://Users//ImageLab//Documents//Visual Studio 2012//Projects//sharp_sample//features.png");
    }

    public void myshowImage()
    {
        Cv.NamedWindow("window");
        Cv.ShowImage("window",testImage);
        /*
        Cv.WaitKey();
        Cv.DestroyWindow("window");
        Cv.ReleaseImage(testImage);
         * */
    }
    ~opencvsharp_test()
    {
        Cv.DestroyWindow("window");
        Cv.ReleaseImage(testImage);
    }

}

class Form1 : Form
{
    Button[] button = new Button[4];  // ボタン
    PictureBox Face = new PictureBox();
    opencvsharp_test picture = new opencvsharp_test();
 
    public Form1()
    {
        this.ClientSize = new System.Drawing.Size(900, 480);
        button[0] = new Button()
        {
            Text = "ボタン A",
            TabIndex = 0,  // フォーカスの移る順位 0 (最優先)
            Location = new Point(10, 10),
            UseVisualStyleBackColor = true,  // ビジュアルスタイル
        };
        button[0].Click += new EventHandler(button_Click);

        button[1] = new Button()
        {
            Text = "ボタン B",
            TabIndex = 1,
            Location = new Point(10, 40),
            UseVisualStyleBackColor = true,
            Enabled = false,  // 使用不可
        };
        button[1].Click += new EventHandler(button_Click);

        button[2] = new Button()
        {
            Text = "ボタン C",
            TabIndex = 2,
            Location = new Point(10, 70),
            UseVisualStyleBackColor = true,
            Cursor = Cursors.Hand,  // 手形カーソル
        };
        button[2].Click += new EventHandler(button_Click);
        button[3] = new Button()
        {
            Text = "Image test",
            TabIndex = 3,
            Location = new Point(10,100),
            UseVisualStyleBackColor = true,
            Cursor = Cursors.Hand,
        };
        this.Controls.AddRange(button);
        button[3].Click += new EventHandler(my_button_Click);

        Face.Location = new Point(200, 0);
        Face.Image = picture.testImage.ToBitmap();
        Face.Size = new System.Drawing.Size(360, 480);
        
        //picture.myshowImage();

        this.Controls.Add(Face);

    }



    void button_Click(object sender, EventArgs e)
    {
        // 押されたボタンのテキストをタイトルバーに表示
        this.Text = (sender as Button).Text;
    }

    void my_button_Click(object sender, EventArgs e)
    {
        int x, y;
        for (y = 0; y < picture.testImage.Height; y++)
        {
            for (x = 0; x < picture.testImage.Width; x++)
            {
                CvColor c = picture.testImage[y, x];
                picture.testImage[y, x] = new CvColor()
                {
                    B = (byte)Math.Round(c.B * 0.7+10),
                    G = (byte)Math.Round(c.G * 1.0),
                    R = (byte)Math.Round(c.R*0.0),
                };
            }
        }

        Face.Location = new Point(200, 0);
        Face.Image = picture.testImage.ToBitmap();
        Face.Size = new System.Drawing.Size(360, 480);
        
       picture.myshowImage();
    }
    /*
    public void loadimage()
    {
        testImage = Cv.LoadImage("C://Users//ImageLab//Documents//Visual Studio 2012//Projects//sharp_sample//features.png");
    }

    public void myshowImage()
    {
        Cv.NamedWindow("window");
        Cv.ShowImage("window", testImage);
    }

    */
}
