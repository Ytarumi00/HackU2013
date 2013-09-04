using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
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
        testImage = Cv.LoadImage("DSC_0154.png");
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


    // Required designer variable.
    private System.ComponentModel.Container components;

    // Declare variables.
    private System.Windows.Forms.RadioButton tab3RadioButton2;
    private System.Windows.Forms.RadioButton tab3RadioButton1;
    private System.Windows.Forms.CheckBox tab2CheckBox3;
    private System.Windows.Forms.CheckBox tab2CheckBox2;
    private System.Windows.Forms.CheckBox tab2CheckBox1;
    private System.Windows.Forms.Label tab1Label1;
    private System.Windows.Forms.Button tab1Button1;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabControl tabControl1;
   
    private System.Windows.Forms.PictureBox Face;
    private opencvsharp_test picture;

 
    public Form1()
    {
        // This call is required for Windows Form Designer support.
        Size W_size = new Size(680, 640);
        this.ClientSize = new System.Drawing.Size(W_size.Width, W_size.Height);
        InitializeComponent(W_size);


        /*
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
         * */

    }
    private void InitializeComponent(Size W_size)
    {
        this.components = new System.ComponentModel.Container();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.tab2CheckBox3 = new System.Windows.Forms.CheckBox();
        this.tab3RadioButton2 = new System.Windows.Forms.RadioButton();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tab2CheckBox2 = new System.Windows.Forms.CheckBox();
        this.tab2CheckBox1 = new System.Windows.Forms.CheckBox();
        this.tab3RadioButton1 = new System.Windows.Forms.RadioButton();
        this.tab1Label1 = new System.Windows.Forms.Label();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.tab1Button1 = new System.Windows.Forms.Button();

        this.Face = new System.Windows.Forms.PictureBox();
        this.picture = new opencvsharp_test();

        Face.Location = new Point(20, 20);
        Face.Image = picture.testImage.ToBitmap();
        Face.Size = new System.Drawing.Size(640, 480);


        int allowance = 20;
        Size T_size = new System.Drawing.Size(640, 360);
        Size T_size2 = new System.Drawing.Size(640, 20);
        tabControl1.Location = new System.Drawing.Point(allowance, W_size.Height / 2 );
        tabControl1.Size = T_size;
        tabControl1.SelectedIndex = 0;
        tabControl1.TabIndex = 0;
        tabPage1.Text = "eye";
        tabPage1.Size = T_size2;
        tabPage1.TabIndex = 0;
        tabPage2.Text = "tabPage2";
        tabPage2.Size = T_size2;
        tabPage2.TabIndex = 1;
        tabPage3.Text = "tabPage3";
        tabPage3.Size = T_size2;
        tabPage3.TabIndex = 2;
        tab2CheckBox3.Location = new System.Drawing.Point(32, 136);
        tab2CheckBox3.Text = "checkBox3";
        tab2CheckBox3.Size = new System.Drawing.Size(176, 32);
        tab2CheckBox3.TabIndex = 2;
        tab2CheckBox3.Visible = true;
        tab3RadioButton2.Location = new System.Drawing.Point(40, 72);
        tab3RadioButton2.Text = "radioButton2";
        tab3RadioButton2.Size = new System.Drawing.Size(152, 24);
        tab3RadioButton2.TabIndex = 1;
        tab3RadioButton2.Visible = true;
        tab2CheckBox2.Location = new System.Drawing.Point(32, 80);
        tab2CheckBox2.Text = "checkBox2";
        tab2CheckBox2.Size = new System.Drawing.Size(176, 32);
        tab2CheckBox2.TabIndex = 1;
        tab2CheckBox2.Visible = true;
        tab2CheckBox1.Location = new System.Drawing.Point(32, 24);
        tab2CheckBox1.Text = "checkBox1";
        tab2CheckBox1.Size = new System.Drawing.Size(176, 32);
        tab2CheckBox1.TabIndex = 0;
        tab3RadioButton1.Location = new System.Drawing.Point(40, 32);
        tab3RadioButton1.Text = "radioButton1";
        tab3RadioButton1.Size = new System.Drawing.Size(152, 24);
        tab3RadioButton1.TabIndex = 0;
        tab1Label1.Location = new System.Drawing.Point(16, 24);
        tab1Label1.Text = "label1";
        tab1Label1.Size = new System.Drawing.Size(224, 96);
        tab1Label1.TabIndex = 1;
        tab1Button1.Location = new System.Drawing.Point(88, 144);
        tab1Button1.Size = new System.Drawing.Size(80, 40);
        tab1Button1.TabIndex = 0;
        tab1Button1.Text = "button1";
        tab1Button1.Click += new
            System.EventHandler(this.tab1Button1_Click);
        this.Text = "Form1";


        this.Controls.Add(Face);

        // Adds controls to the second tab page.
        tabPage2.Controls.Add(this.tab2CheckBox3);
        tabPage2.Controls.Add(this.tab2CheckBox2);
        tabPage2.Controls.Add(this.tab2CheckBox1);
        // Adds controls to the third tab page.
        tabPage3.Controls.Add(this.tab3RadioButton2);
        tabPage3.Controls.Add(this.tab3RadioButton1);
        // Adds controls to the first tab page.
        tabPage1.Controls.Add(this.tab1Label1);
        tabPage1.Controls.Add(this.tab1Button1);
        // Adds the TabControl to the form.
        this.Controls.Add(this.tabControl1);
        // Adds the tab pages to the TabControl.
        tabControl1.Controls.Add(this.tabPage1);
        tabControl1.Controls.Add(this.tabPage2);
        tabControl1.Controls.Add(this.tabPage3);

        

    }

    private void tab1Button1_Click(object sender, System.EventArgs e)
    {
        // Inserts the code that should run when the button is clicked.
    }


    void button_Click(object sender, EventArgs e)
    {
        // 押されたボタンのテキストをタイトルバーに表示
        this.Text = (sender as Button).Text;
    }

    void my_button_Click(object sender, EventArgs e)
    {
        IplImage effected_Img = Cv.CloneImage(picture.testImage);
        effected_Img = test_effect(picture.testImage);

        Face.Image = effected_Img.ToBitmap();
        Face.Size = new System.Drawing.Size(360, 480);
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

    void face_change(int tab_number, int button_number)
    {
        //口と鼻とほほ
        //ほほ:tab_number=0;
        //鼻：tab_number = 1;
        //口：tab_number = 2;
        System.Console.Write("入力されたtab:%d,button:%d", tab_number, button_number);
        switch (tab_number)
        {
            case 0:
                //ほほについて
                System.Console.Write("test 0");
                switch (button_number)
                {
                    case 0:
                        //ほほのエフェクト0
                        System.Console.Write("button number is %d", button_number);
                        break;
                    case 1:
                        //ほほのエフェクト1
                        System.Console.Write("button number is %d", button_number);
                        break;
                    case 2:
                        //ほほのエフェクト2
                        System.Console.Write("button number is %d", button_number);
                        break;
                    default:
                        //エラー処理（エフェクト番号間違い)
                        System.Console.Write("Error!予期せぬbutton番号です");
                        break;
                }
                break;
            case 1:
                //鼻について
                System.Console.Write("test 1");
                switch (button_number)
                {
                    case 0:
                        //鼻のエフェクト0
                        System.Console.Write("button number is %d", button_number);
                        break;
                    case 1:
                        //鼻のエフェクト1
                        System.Console.Write("button number is %d", button_number);
                        break;
                    case 2:
                        //鼻のエフェクト2
                        System.Console.Write("button number is %d", button_number);
                        break;
                    default:
                        //エラー処理(エフェクト番号違い)
                        System.Console.Write("Error!予期せぬbutton番号です");
                        break;
                }
                break;
            case 2:
                //口について
                System.Console.Write("test 2");
                switch (button_number)
                {
                    case 0:
                        //口のエフェクト0
                        System.Console.Write("button number is %d", button_number);
                        break;
                    case 1:
                        //口のエフェクト1
                        System.Console.Write("button number is %d", button_number);
                        break;
                    case 2:
                        //口のエフェクト2
                        System.Console.Write("button number is %d", button_number);
                        break;
                    default:
                        //エラー処理（エフェクト番号違い)
                        System.Console.Write("Error!予期せぬbutton番号です");
                        break;
                }
                break;
            default:
                //エラー処理（タブ番号違い)
                System.Console.Write("Error!予期せぬ番号です");
                break;

        }

    }
    IplImage test_effect(IplImage inputImg)
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
