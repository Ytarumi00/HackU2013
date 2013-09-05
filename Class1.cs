using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using OpenCvSharp;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Xml;


public class test_list
{
    struct point
    {
        public int x;
        public int y;
        public string Label;
    }
    private List<point> lst = new List<point>();
    public void set_point(int X, int Y,string label)
    {
        point temp;
        temp.x = X;
        temp.y = Y;
        temp.Label = label;
        lst.Add(temp);
    }
    public int get_pointX(int i)
    {
        return this.lst[i].x;
    }

    public int get_pointY(int i)
    {
        return this.lst[i].y;
    }

    public string get_PointLabel(int i)
    {

        return this.lst[i].Label;
    }

    public int get_count()
    {
        return this.lst.Count;
    }

    public int find_label_point_X(string label)
    {
        point tmp;
        tmp = lst.Find(delegate(point point) { return point.Label == label; });

        return tmp.x;
    }
    public int find_label_point_Y(string label)
    {
        point tmp;
        tmp = lst.Find(delegate(point point) { return point.Label == label; });

        return tmp.y;
    }


}

public class opencvsharp_test
{
    public IplImage testImage;
    private XmlDocument FaceXml;
    public test_list facePoints;

    public opencvsharp_test()
    {
        loadimage();
        Init();
    }

    private void Init()
    {
        this.FaceXml = new XmlDocument();
        this.facePoints = new test_list();
        readXML();

    }


    public void loadimage()
    {
        testImage = Cv.LoadImage("sample_640.jpg");
    }

    public void myshowImage()
    {
        Cv.NamedWindow("window");
        Cv.ShowImage("window", testImage);

    }

    void readXML()
    {
        FaceXml.Load("sample-2.xml");
        XmlNode root = FaceXml.DocumentElement;
        XmlNode faces = root.FirstChild;

        Console.WriteLine(((XmlElement)faces).GetAttribute("id"));
        XmlNode bounds = faces.SelectSingleNode("bounds");
        Console.WriteLine(((XmlElement)bounds).GetAttribute("width"));
        XmlNode righteye = faces.SelectSingleNode("right-eye");
        XmlNode lefteye = faces.SelectSingleNode("left-eye");
        XmlNode features = faces.SelectSingleNode("features");


        //featuresの子供Points一斉走査
        XmlNodeList featuresChildren = features.ChildNodes;
        string Point_X;
        string Point_Y;
        foreach (XmlNode featurechild in featuresChildren)
        {
            Console.WriteLine(" id={1}, X=  {0},Y={2}", ((XmlElement)featurechild).GetAttribute("x"), ((XmlElement)featurechild).GetAttribute("id"), ((XmlElement)featurechild).GetAttribute("y"));
            Point_X = ((XmlElement)featurechild).GetAttribute("x");
            Point_Y = ((XmlElement)featurechild).GetAttribute("y");
            facePoints.set_point(int.Parse(Point_X), int.Parse(Point_Y), ((XmlElement)featurechild).GetAttribute("id"));
        }
        Console.WriteLine("debug:facesPoints_count:{0}", facePoints.get_count());
        Console.WriteLine("debug: [20]'s number id is {0}",facePoints.get_PointLabel(20));

        Console.WriteLine(facePoints.find_label_point_X("N2"));

        //XmlNodeList bounds = facesElement.GetElementsByTagName("bounds");
        //XmlElement boundsElement = (XmlElement)bounds.Item(0);
        //Console.WriteLine("X={0},Y={1},width = {2},height = {3}", boundsElement.GetAttribute("x"), boundsElement.GetAttribute("y"), boundsElement.GetAttribute("width"), boundsElement.GetAttribute("height"));
    }

    void displayHTML()
    {

    }


    ~opencvsharp_test()
    {
        Cv.DestroyWindow("window");
        Cv.ReleaseImage(testImage);
    }

}

public class MyButton : Button
{
    private int tab_num;
    private int val;
    public MyButton(int t, int v)
    {
        set_tab_num(t);
        set_val(v);
    }
    void set_tab_num(int t)
    {
        this.tab_num = t;
    }
    void set_val(int v)
    {
        this.val = v;
    }
    public int get_tab_num()
    {
        return this.tab_num;
    }
    public int get_val()
    {
        return this.val;
    }
}

public class get_TabImage{
	private ImageFormat fmt = ImageFormat.Png;
	private string fullpass_filename;
	public Bitmap[] img;
	public get_TabImage(string pass, string file_name,int file_num){
		string passfile = pass+file_name;
		Console.Write(passfile);
		img = new Bitmap[file_num];
		for(int i = 0;i < file_num;i++){
			fullpass_filename = passfile + "_" + i.ToString("00") + "." + (fmt.ToString()).ToLower();
			FileStream sr = new FileStream(fullpass_filename,FileMode.Open,FileAccess.Read);
			img[i] = (Bitmap)Bitmap.FromStream(sr);
			sr.Close();
		}
	}
}

public class Form1 : System.Windows.Forms.Form
{
    // Required designer variable.
    private System.ComponentModel.Container components;


    private MyButton[] tab1Button;
    private MyButton[] tab2Button;
    private MyButton[] tab3Button;
    private MyButton[] tab4Button;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TabControl tabControl1;

    private System.Windows.Forms.PictureBox Face;
    private System.Windows.Forms.PictureBox static_Face;
    private opencvsharp_test picture;

    public Form1()
    {
        // This call is required for Windows Form Designer support.
        Size W_size = new Size(1340, 700);
        this.ClientSize = new System.Drawing.Size(W_size.Width, W_size.Height);
        InitializeComponent(W_size);
    }
    // This method is required for Designer support.
    private void InitializeComponent(Size W_size)
    {
        this.components = new System.ComponentModel.Container();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.tabPage4 = new System.Windows.Forms.TabPage();
        this.tab1Button = new MyButton[4];
        this.tab2Button = new MyButton[4];
        this.tab3Button = new MyButton[4];
        this.tab4Button = new MyButton[4];

        //江夏側の設定部分
        //Face:UIに出力する顔画像部分のPicture Box
        //picture:OpenCVSharpで顔画像の読み込みクラス
        this.Face = new System.Windows.Forms.PictureBox();
        this.static_Face = new System.Windows.Forms.PictureBox();
        this.picture = new opencvsharp_test();

        Face.Location = new Point(W_size.Width - 660, 20);
        static_Face.Location = new Point(20, 20);
        Face.Image = picture.testImage.ToBitmap();
        static_Face.Image = picture.testImage.ToBitmap();
        Face.Size = new System.Drawing.Size(640, 480);
        static_Face.Size = new System.Drawing.Size(640, 480);


        int allowance = 50;
        int first_x = 50;
        Size T_size = new System.Drawing.Size(W_size.Width - 40, 180 - 20);
        Size T_size2 = new System.Drawing.Size(40, 20);
        Size B_size = new System.Drawing.Size(60, 80);
        tabControl1.Location = new System.Drawing.Point(20, 520);
        tabControl1.Size = T_size;
        tabControl1.SelectedIndex = 0;
        tabControl1.TabIndex = 0;
        tabPage1.Text = "cheek";
        tabPage1.Size = T_size2;
        tabPage1.TabIndex = 0;
        tabPage2.Text = "eye";
        tabPage2.Size = T_size2;
        tabPage2.TabIndex = 1;
        tabPage3.Text = "mouse";
        tabPage3.Size = T_size2;
        tabPage3.TabIndex = 2;
        tabPage4.Text = "eyeshadow";
        tabPage4.Size = T_size2;
        tabPage4.TabIndex = 3;

        get_TabImage Tab1Image = new get_TabImage("TabImages/cheek/", "cheek", 3);
        for (int i = 0; i < 3; i++)
        {
            tab1Button[i] = new MyButton(0, i)
            {
                Image = Tab1Image.img[i],
                Location = new System.Drawing.Point(first_x + i * (B_size.Width + allowance), T_size.Height / 4),
                Size = B_size,
                TabIndex = i,
                Cursor = Cursors.Hand,
            };
            tab1Button[i].Click += new System.EventHandler(this.TabButton_Click);
        }
        get_TabImage Tab2Image = new get_TabImage("TabImages/eye/", "eye", 3);
        for (int i = 0; i < 3; i++)
        {
            tab2Button[i] = new MyButton(1, i)
            {
                Image = Tab2Image.img[i],
                Location = new System.Drawing.Point(first_x + i * (B_size.Width + allowance), T_size.Height / 4),
                Size = B_size,
                TabIndex = i,
                Cursor = Cursors.Hand,
            };
            tab2Button[i].Click += new System.EventHandler(this.TabButton_Click);
        }
        get_TabImage Tab3Image = new get_TabImage("TabImages/mouse/", "mouse", 3);
        for (int i = 0; i < 3; i++)
        {
            tab3Button[i] = new MyButton(2, i)
            {
                Image = Tab3Image.img[i],
                Location = new System.Drawing.Point(first_x + i * (B_size.Width + allowance), T_size.Height / 4),
                Size = B_size,
                TabIndex = i,
                Cursor = Cursors.Hand,
            };
            tab3Button[i].Click += new System.EventHandler(this.TabButton_Click);
        }
        get_TabImage Tab4Image = new get_TabImage("TabImages/eyeshadow/", "eyeshadow", 3);
        for (int i = 0; i < 3; i++)
        {
            tab4Button[i] = new MyButton(3, i)
            {
                Image = Tab4Image.img[i],
                Location = new System.Drawing.Point(first_x + i * (B_size.Width + allowance), T_size.Height / 4),
                Size = B_size,
                TabIndex = i,
                Cursor = Cursors.Hand,
            };
            tab4Button[i].Click += new System.EventHandler(this.TabButton_Click);
        }
        tab1Button[3] = new MyButton(0, 3)
        {
            Text = "Reset",
            Location = new System.Drawing.Point(T_size.Width - B_size.Width - 20, T_size.Height / 4),
            Size = B_size,
            TabIndex = 3,
            Cursor = Cursors.Hand,
        };
        tab1Button[3].Click += new System.EventHandler(this.TabButton_Click);
        tab2Button[3] = new MyButton(1, 3)
        {
            Text = "Reset",
            Location = new System.Drawing.Point(T_size.Width - B_size.Width - 20, T_size.Height / 4),
            Size = B_size,
            TabIndex = 3,
            Cursor = Cursors.Hand,
        };
        tab2Button[3].Click += new System.EventHandler(this.TabButton_Click);
        tab3Button[3] = new MyButton(2, 3)
        {
            Text = "Reset",
            Location = new System.Drawing.Point(T_size.Width - B_size.Width - 20, T_size.Height / 4),
            Size = B_size,
            TabIndex = 3,
            Cursor = Cursors.Hand,
        };
        tab3Button[3].Click += new System.EventHandler(this.TabButton_Click);
        tab4Button[3] = new MyButton(2, 3)
        {
            Text = "Reset",
            Location = new System.Drawing.Point(T_size.Width - B_size.Width - 20, T_size.Height / 4),
            Size = B_size,
            TabIndex = 3,
            Cursor = Cursors.Hand,
        };
        tab4Button[3].Click += new System.EventHandler(this.TabButton_Click);

        this.Text = "Form1";

        for (int i = 0; i < 4; i++)
            tabPage1.Controls.Add(this.tab1Button[i]);
        for (int i = 0; i < 4; i++)
            tabPage2.Controls.Add(this.tab2Button[i]);
        for (int i = 0; i < 4; i++)
            tabPage3.Controls.Add(this.tab3Button[i]);
        for (int i = 0; i < 4; i++)
            tabPage4.Controls.Add(this.tab4Button[i]);

        this.Controls.Add(this.tabControl1);

        this.Controls.Add(this.Face);
        this.Controls.Add(this.static_Face);

        tabControl1.Controls.Add(this.tabPage1);
        tabControl1.Controls.Add(this.tabPage2);
        tabControl1.Controls.Add(this.tabPage3);
        tabControl1.Controls.Add(this.tabPage4);
    }



    private void TabButton_Click(object sender, System.EventArgs e)
    {
        IplImage tmpImage;
        tmpImage = Cv.CloneImage(picture.testImage);

        Console.Write((sender as MyButton).get_tab_num());
        Console.Write((sender as MyButton).get_val());
        tmpImage = face_change((sender as MyButton).get_tab_num(), (sender as MyButton).get_val(), picture.testImage);
        Face.Image = tmpImage.ToBitmap();    
    }
    IplImage face_change(int tab_number, int button_number,IplImage inputImg)
    {
        //口と鼻とほほ
        //ほほ:tab_number=0;
        //目：tab_number = 1;
        //口：tab_number = 2;
        //アイシャドウ　tab_number = 3;
        //tmpImg:編集用画像

        IplImage tmpImg = Cv.CloneImage(inputImg);

        System.Console.Write("入力されたtab:{0},button:{1}\n", tab_number, button_number);
        switch (tab_number)
        {
            case 0:
                //ほほについて
                System.Console.Write("test 0\n");
                switch (button_number)
                {
                    case 0:
                        //ほほのエフェクト0
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectCheek1(tmpImg, tmpImg);
                        break;
                    case 1:
                        //ほほのエフェクト1
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectCheek2(tmpImg, tmpImg);
                        break;
                    case 2:
                        //ほほのエフェクト2
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectCheek3(tmpImg, tmpImg);
                        break;
                    default:
                        //エラー処理（エフェクト番号間違い)
                        System.Console.Write("Error!予期せぬbutton番号です\n");
                        break;
                }
                break;
            case 1:
                //鼻について
                System.Console.Write("test 1\n");
                switch (button_number)
                {
                    case 0:
                        //目のエフェクト0
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectEyeBlack(tmpImg, tmpImg);
                        break;
                    case 1:
                        //目のエフェクト1
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectEyeBlown(tmpImg, tmpImg);
                        break;
                    case 2:
                        //目のエフェクト2
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectEyeBlue(tmpImg, tmpImg);
                        break;
                    default:
                        //エラー処理(エフェクト番号違い)
                        System.Console.Write("Error!予期せぬbutton番号です\n");
                        break;
                }
                break;
            case 2:
                //口について
                System.Console.Write("test 2\n");
                switch (button_number)
                {
                    case 0:
                        //口のエフェクト0
                        System.Console.Write("button number is {0}\n", button_number);
                        System.Console.Write("test button");
                        tmpImg = effectMouthRed(tmpImg, tmpImg);
                        break;
                    case 1:
                        //口のエフェクト1
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectMouthPink(tmpImg, tmpImg);
                        break;
                    case 2:
                        //口のエフェクト2
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectMouthOrange(tmpImg, tmpImg);

                        break;
                    default:
                        //エラー処理（エフェクト番号違い)
                        System.Console.Write("Error!予期せぬbutton番号です\n");
                        break;
                }
                break;
            case 3:
                //口について
                System.Console.Write("test 2\n");
                switch (button_number)
                {
                    case 0:
                        //アイシャドウのエフェクト0
                        System.Console.Write("button number is {0}\n", button_number);
                        System.Console.Write("test button");
                        tmpImg = effectShadow1(tmpImg, tmpImg);
                        break;
                    case 1:
                        //アイシャドウのエフェクト1
                        System.Console.Write("button number is {0}\n", button_number);
                        tmpImg = effectShadow2(tmpImg, tmpImg);
                        break;
                    case 2:
                        //アイシャドウのエフェクト2
                        System.Console.Write("button number is {0}\n", button_number);
                        //tmpImg = effectMouthOrange(tmpImg, tmpImg);
                        tmpImg = effectShadow3(tmpImg, tmpImg);
                        break;
                    default:
                        //エラー処理（エフェクト番号違い)
                        System.Console.Write("Error!予期せぬbutton番号です\n");
                        break;
                }
                break;
            default:
                //エラー処理（タブ番号違い)
                System.Console.Write("Error!予期せぬ番号です.\n");
                //tmpImg = test_effect(tmpImg);

                
                break;

        }
        return tmpImg;
    }

    public IplImage effectMouthRed(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(srcImage);

        CvPoint M1;
        M1.X = picture.facePoints.find_label_point_X("M1");
        M1.Y = picture.facePoints.find_label_point_Y("M1");
        CvPoint M2;
        M2.X = picture.facePoints.find_label_point_X("M2");
        M2.Y = picture.facePoints.find_label_point_Y("M2");
        CvPoint M3;
        M3.X = picture.facePoints.find_label_point_X("M3");
        M3.Y = picture.facePoints.find_label_point_Y("M3");
        CvPoint M4;
        M4.X = picture.facePoints.find_label_point_X("M4");
        M4.Y = picture.facePoints.find_label_point_Y("M4");
        CvPoint M5;
        M5.X = picture.facePoints.find_label_point_X("M5");
        M5.Y = picture.facePoints.find_label_point_Y("M5");
        CvPoint M6;
        M6.X = picture.facePoints.find_label_point_X("M6");
        M6.Y = picture.facePoints.find_label_point_Y("M6");
        CvPoint M7;
        M7.X = picture.facePoints.find_label_point_X("M7");
        M7.Y = picture.facePoints.find_label_point_Y("M7");
        CvPoint M8;
        M8.X = picture.facePoints.find_label_point_X("M8");
        M8.Y = picture.facePoints.find_label_point_Y("M8");

        CvPoint[][] points = new CvPoint[][]{
            new CvPoint[] {M1,M2,M3,M4,M5,M6,M7,M8},
        };
        CvScalar orange;
        orange = new CvScalar();
        orange.Val0 = 20;
        orange.Val1 = 10;
        orange.Val2 = 200;
        Cv.FillPoly(copyImage, points, orange);
        Cv.AddWeighted(copyImage, 0.2, tmpImage, 0.8, 0, tmpImage);
        return tmpImage;
    }

    public IplImage effectMouthPink(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(srcImage);

        CvPoint M1;
        M1.X = picture.facePoints.find_label_point_X("M1");
        M1.Y = picture.facePoints.find_label_point_Y("M1");
        CvPoint M2;
        M2.X = picture.facePoints.find_label_point_X("M2");
        M2.Y = picture.facePoints.find_label_point_Y("M2");
        CvPoint M3;
        M3.X = picture.facePoints.find_label_point_X("M3");
        M3.Y = picture.facePoints.find_label_point_Y("M3");
        CvPoint M4;
        M4.X = picture.facePoints.find_label_point_X("M4");
        M4.Y = picture.facePoints.find_label_point_Y("M4");
        CvPoint M5;
        M5.X = picture.facePoints.find_label_point_X("M5");
        M5.Y = picture.facePoints.find_label_point_Y("M5");
        CvPoint M6;
        M6.X = picture.facePoints.find_label_point_X("M6");
        M6.Y = picture.facePoints.find_label_point_Y("M6");
        CvPoint M7;
        M7.X = picture.facePoints.find_label_point_X("M7");
        M7.Y = picture.facePoints.find_label_point_Y("M7");
        CvPoint M8;
        M8.X = picture.facePoints.find_label_point_X("M8");
        M8.Y = picture.facePoints.find_label_point_Y("M8");

        CvPoint[][] points = new CvPoint[][]{
            new CvPoint[] {M1,M2,M3,M4,M5,M6,M7,M8},
        };
        CvScalar orange;
        orange = new CvScalar();
        orange.Val0 = 159;
        orange.Val1 = 168;
        orange.Val2 = 251;
        Cv.FillPoly(copyImage, points, orange);
        Cv.AddWeighted(copyImage, 0.2, tmpImage, 0.8, 0, tmpImage);
        return tmpImage;
    }

    public IplImage effectMouthOrange(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(srcImage);

        CvPoint M1;
        M1.X = picture.facePoints.find_label_point_X("M1");
        M1.Y = picture.facePoints.find_label_point_Y("M1");
        CvPoint M2;
        M2.X = picture.facePoints.find_label_point_X("M2");
        M2.Y = picture.facePoints.find_label_point_Y("M2");
        CvPoint M3;
        M3.X = picture.facePoints.find_label_point_X("M3");
        M3.Y = picture.facePoints.find_label_point_Y("M3");
        CvPoint M4;
        M4.X = picture.facePoints.find_label_point_X("M4");
        M4.Y = picture.facePoints.find_label_point_Y("M4");
        CvPoint M5;
        M5.X = picture.facePoints.find_label_point_X("M5");
        M5.Y = picture.facePoints.find_label_point_Y("M5");
        CvPoint M6;
        M6.X = picture.facePoints.find_label_point_X("M6");
        M6.Y = picture.facePoints.find_label_point_Y("M6");
        CvPoint M7;
        M7.X = picture.facePoints.find_label_point_X("M7");
        M7.Y = picture.facePoints.find_label_point_Y("M7");
        CvPoint M8;
        M8.X = picture.facePoints.find_label_point_X("M8");
        M8.Y = picture.facePoints.find_label_point_Y("M8");

        CvPoint[][] points = new CvPoint[][]{
            new CvPoint[] {M1,M2,M3,M4,M5,M6,M7,M8},
        };
        CvScalar orange;
        orange = new CvScalar();
        orange.Val0 = 0;
        orange.Val1 = 140;
        orange.Val2 = 255;
        Cv.FillPoly(copyImage, points, orange);
        Cv.AddWeighted(copyImage, 0.2, tmpImage, 0.8, 0, tmpImage);
        return tmpImage;
    }

    public IplImage effectCheek1(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(srcImage);

        CvPoint leftCenter, rightCenter = new CvPoint();
        leftCenter.X = picture.facePoints.find_label_point_X("N4") + picture.facePoints.find_label_point_X("F8");
        leftCenter.Y = picture.facePoints.find_label_point_Y("EL5") + picture.facePoints.find_label_point_Y("N4") + picture.facePoints.find_label_point_Y("F8");
        rightCenter.X = picture.facePoints.find_label_point_X("N2") + picture.facePoints.find_label_point_X("F4");
        rightCenter.Y = picture.facePoints.find_label_point_Y("ER5") + picture.facePoints.find_label_point_Y("N2") + picture.facePoints.find_label_point_Y("F4");

        double leftLength = Math.Abs(picture.facePoints.find_label_point_X("N4") - picture.facePoints.find_label_point_X("F8"))*0.4;
        double rightLength = Math.Abs(picture.facePoints.find_label_point_X("N2") - picture.facePoints.find_label_point_X("F4")) * 0.4;

        int length;
        if (leftLength > rightLength)
        {
            length = (int)rightLength;
        }
        else length = (int)leftLength;
        CvScalar red = new CvScalar();
        red.Val0 = 0;
        red.Val1 = 0;
        red.Val2 = 255;
        CvPoint tmpleft = new CvPoint();
        tmpleft.X = leftCenter.X / 2;
        tmpleft.Y = leftCenter.Y / 3;

        CvPoint tmpright = new CvPoint();
        tmpright.X = rightCenter.X / 2;
        tmpright.Y = rightCenter.Y / 3;

        Cv.Circle(copyImage, tmpleft, length, red, -1);
        Cv.Circle(copyImage, tmpright, length, red, -1);
        Cv.AddWeighted(copyImage, 0.05, tmpImage, 0.95, 0, tmpImage);
        return tmpImage;
    }


    public IplImage effectCheek2(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(srcImage);

        CvPoint leftCenter, rightCenter = new CvPoint();
        leftCenter.X = picture.facePoints.find_label_point_X("N4") + picture.facePoints.find_label_point_X("F8");
        leftCenter.Y = picture.facePoints.find_label_point_Y("EL5") + picture.facePoints.find_label_point_Y("N4") + picture.facePoints.find_label_point_Y("F8");
        rightCenter.X = picture.facePoints.find_label_point_X("N2") + picture.facePoints.find_label_point_X("F4");
        rightCenter.Y = picture.facePoints.find_label_point_Y("ER5") + picture.facePoints.find_label_point_Y("N2") + picture.facePoints.find_label_point_Y("F4");

        double leftLength = Math.Abs(picture.facePoints.find_label_point_X("N4") - picture.facePoints.find_label_point_X("F8")) * 0.6;
        double rightLength = Math.Abs(picture.facePoints.find_label_point_X("N2") - picture.facePoints.find_label_point_X("F4")) * 0.6;

        CvScalar red = new CvScalar();
        red.Val0 = 0;
        red.Val1 = 0;
        red.Val2 = 255;

        CvPoint tmpleft = new CvPoint();
        tmpleft.X = leftCenter.X - picture.facePoints.find_label_point_X("N4");
        tmpleft.Y = leftCenter.Y / 3;
        CvSize tmpleftSize = new CvSize();
        tmpleftSize.Width = (int)leftLength;
        tmpleftSize.Height = 20;

        CvPoint tmpright = new CvPoint();
        tmpright.X = rightCenter.X - picture.facePoints.find_label_point_X("N2");
        tmpright.Y = rightCenter.Y / 3;
        CvSize tmprightSize = new CvSize();
        tmprightSize.Width = (int)rightLength;
        tmprightSize.Height = 20;

        Cv.Ellipse(copyImage, tmpleft, tmpleftSize ,0.0, 90.0, 270.0, red, -1);
        Cv.Ellipse(copyImage, tmpright, tmprightSize, 0.0, -90.0, 90.0, red, -1);

        Cv.AddWeighted(copyImage, 0.05, tmpImage, 0.95, 0, tmpImage);
        return tmpImage;
    }

    public IplImage effectCheek3(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage;
        tmpImage = Cv.CloneImage(srcImage);
        CvPoint leftCenter, rightCenter = new CvPoint() ;
        double leftWidth, leftHeight, rightWidth, rightHeight;
        leftCenter.X = picture.facePoints.find_label_point_X("EL5") + picture.facePoints.find_label_point_X("EL6") + picture.facePoints.find_label_point_X("N4") + picture.facePoints.find_label_point_X("F8");
        leftCenter.Y = picture.facePoints.find_label_point_Y("EL5") + picture.facePoints.find_label_point_Y("EL6") + picture.facePoints.find_label_point_Y("N4") + picture.facePoints.find_label_point_Y("F8");
        leftWidth = Math.Abs(picture.facePoints.find_label_point_X("N4") - picture.facePoints.find_label_point_X("F8"))*0.6;
        leftHeight = Math.Abs(picture.facePoints.find_label_point_Y("EL6") - picture.facePoints.find_label_point_Y("F8"))*0.3;

        rightCenter.X = picture.facePoints.find_label_point_X("ER5") + picture.facePoints.find_label_point_X("ER6") + picture.facePoints.find_label_point_X("N2") + picture.facePoints.find_label_point_X("F4");
        rightCenter.Y = picture.facePoints.find_label_point_Y("ER5") + picture.facePoints.find_label_point_Y("ER6") + picture.facePoints.find_label_point_Y("N2") + picture.facePoints.find_label_point_Y("F4");
        rightWidth = Math.Abs(picture.facePoints.find_label_point_X("N2") - picture.facePoints.find_label_point_X("N4"))*0.6;
        rightHeight = Math.Abs(picture.facePoints.find_label_point_Y("ER5") - picture.facePoints.find_label_point_Y("F4"))*0.3;

        CvPoint tmpleft,tmpright = new CvPoint();
        CvSize tmpleftSize,tmprightSize = new CvSize();

        tmpleft.X = leftCenter.X/4;
        tmpleft.Y = leftCenter.Y/4;
        tmpright.X = rightCenter.X/4;
        tmpright.Y = rightCenter.Y/4;
        tmpleftSize.Width = (int)leftWidth;
        tmpleftSize.Height = (int)leftHeight;
        tmprightSize.Width = (int)rightWidth;
        tmprightSize.Height = (int)rightHeight;


        CvScalar red = new CvScalar();
        red.Val0 = 0;
        red.Val1 = 0;
        red.Val2 = 255;

        Cv.Ellipse(copyImage,tmpleft,tmpleftSize,0.0,0.0,360,red,-1);
        Cv.Ellipse(copyImage,tmpright,tmprightSize,0.0,0.0,360,red,-1);
        Cv.AddWeighted(copyImage,0.05,tmpImage,0.95,0,tmpImage);

        return tmpImage;
    }

    public IplImage effectEyeBlack(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(copyImage);
        int leftWidth, leftHeight, rightWidth, rightHeight;

        CvSize tmpleftSize = new CvSize();
        CvSize tmprightSize = new CvSize();

        leftWidth = Math.Abs(picture.facePoints.find_label_point_X("EL2") - picture.facePoints.find_label_point_X("PL"));
        leftHeight = Math.Abs(picture.facePoints.find_label_point_Y("EL2") - picture.facePoints.find_label_point_Y("PL"));
        rightWidth = Math.Abs(picture.facePoints.find_label_point_X("ER2") - picture.facePoints.find_label_point_X("PR"));
        rightHeight = Math.Abs(picture.facePoints.find_label_point_Y("ER2") - picture.facePoints.find_label_point_Y("PR"));

        tmpleftSize.Width = leftWidth;
        tmpleftSize.Height = leftHeight;
        tmprightSize.Width = rightWidth;
        tmprightSize.Height = rightHeight;

        CvPoint tmpleft, tmpright = new CvPoint();
        tmpleft.X = picture.facePoints.find_label_point_X("PL");
        tmpleft.Y = picture.facePoints.find_label_point_Y("PL");
        tmpright.X = picture.facePoints.find_label_point_X("PR");
        tmpright.Y = picture.facePoints.find_label_point_Y("PR");


        CvScalar eye = new CvScalar();
        eye.Val0 = 2;
        eye.Val1 = 2;
        eye.Val2 = 2;

        Cv.Ellipse(copyImage, tmpleft, tmpleftSize, 0, 0, 360, eye, -1);
        Cv.Ellipse(copyImage, tmpright, tmprightSize, 0, 0, 360, eye, -1);
        Cv.AddWeighted(copyImage, 0.5, tmpImage, 0.5, 0, tmpImage);
        return tmpImage;
    }

    public IplImage effectEyeBlown(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(copyImage);
        int leftWidth, leftHeight, rightWidth, rightHeight;

        CvSize tmpleftSize = new CvSize();
        CvSize tmprightSize = new CvSize();

        leftWidth = Math.Abs(picture.facePoints.find_label_point_X("EL2") - picture.facePoints.find_label_point_X("PL"));
        leftHeight = Math.Abs(picture.facePoints.find_label_point_Y("EL2") - picture.facePoints.find_label_point_Y("PL"));
        rightWidth = Math.Abs(picture.facePoints.find_label_point_X("ER2") - picture.facePoints.find_label_point_X("PR"));
        rightHeight = Math.Abs(picture.facePoints.find_label_point_Y("ER2") - picture.facePoints.find_label_point_Y("PR"));

        tmpleftSize.Width = leftWidth;
        tmpleftSize.Height = leftHeight;
        tmprightSize.Width = rightWidth;
        tmprightSize.Height = rightHeight;

        CvPoint tmpleft, tmpright = new CvPoint();
        tmpleft.X = picture.facePoints.find_label_point_X("PL");
        tmpleft.Y = picture.facePoints.find_label_point_Y("PL");
        tmpright.X = picture.facePoints.find_label_point_X("PR");
        tmpright.Y = picture.facePoints.find_label_point_Y("PR");


        CvScalar eye = new CvScalar();
        eye.Val0 = 0;
        eye.Val1 = 76;
        eye.Val2 = 153;

        Cv.Ellipse(copyImage, tmpleft, tmpleftSize, 0, 0, 360, eye, -1);
        Cv.Ellipse(copyImage, tmpright, tmprightSize, 0, 0, 360, eye, -1);
        Cv.AddWeighted(copyImage, 0.3, tmpImage, 0.7, 0, tmpImage);
        return tmpImage;
    }
    public IplImage effectEyeBlue(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(copyImage);
        int leftWidth, leftHeight, rightWidth, rightHeight;

        CvSize tmpleftSize = new CvSize();
        CvSize tmprightSize = new CvSize();

        leftWidth = Math.Abs(picture.facePoints.find_label_point_X("EL2") - picture.facePoints.find_label_point_X("PL"));
        leftHeight = Math.Abs(picture.facePoints.find_label_point_Y("EL2") - picture.facePoints.find_label_point_Y("PL"));
        rightWidth = Math.Abs(picture.facePoints.find_label_point_X("ER2") - picture.facePoints.find_label_point_X("PR"));
        rightHeight = Math.Abs(picture.facePoints.find_label_point_Y("ER2") - picture.facePoints.find_label_point_Y("PR"));

        tmpleftSize.Width = leftWidth;
        tmpleftSize.Height = leftHeight;
        tmprightSize.Width = rightWidth;
        tmprightSize.Height = rightHeight;

        CvPoint tmpleft, tmpright = new CvPoint();
        tmpleft.X = picture.facePoints.find_label_point_X("PL");
        tmpleft.Y = picture.facePoints.find_label_point_Y("PL");
        tmpright.X = picture.facePoints.find_label_point_X("PR");
        tmpright.Y = picture.facePoints.find_label_point_Y("PR");


        CvScalar eye = new CvScalar();
        eye.Val0 = 229;
        eye.Val1 = 2194;
        eye.Val2 = 91;

        Cv.Ellipse(copyImage, tmpleft, tmpleftSize, 0, 0, 360, eye, -1);
        Cv.Ellipse(copyImage, tmpright, tmprightSize, 0, 0, 360, eye, -1);
        Cv.AddWeighted(copyImage, 0.1, tmpImage, 0.9, 0, tmpImage);
        return tmpImage;
    }

    public IplImage effectShadow1(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage;
        tmpImage = Cv.CloneImage(copyImage);

        CvPoint L0, L1, L2, L3, L4, L5, L6, L7;
        CvPoint R0, R1, R2, R3, R4, R5, R6, R7;

        L0.X = picture.facePoints.find_label_point_X("EL1");
        L0.Y = picture.facePoints.find_label_point_Y("EL1")-2;

        L1.X = picture.facePoints.find_label_point_X("EL2");
        L1.Y = picture.facePoints.find_label_point_Y("EL2")-2;

        L2.X = picture.facePoints.find_label_point_X("EL3");
        L2.Y = picture.facePoints.find_label_point_Y("EL3")-2;

        L3.X = picture.facePoints.find_label_point_X("EL4");
        L3.Y = picture.facePoints.find_label_point_Y("EL4")-2;

        L4.X = picture.facePoints.find_label_point_X("EL4");
        L4.Y = picture.facePoints.find_label_point_Y("EL4")-5;

        L5.X = picture.facePoints.find_label_point_X("EL3");
        L5.Y = picture.facePoints.find_label_point_Y("EL3")-5;

        L6.X = picture.facePoints.find_label_point_X("EL2");
        L6.Y = picture.facePoints.find_label_point_Y("EL2")-2;

        L7.X = picture.facePoints.find_label_point_X("EL1");
        L7.Y = picture.facePoints.find_label_point_Y("EL1")-2;

        R0.X = picture.facePoints.find_label_point_X("ER1");
        R0.Y = picture.facePoints.find_label_point_Y("ER1") - 5;

        R1.X = picture.facePoints.find_label_point_X("ER2");
        R1.Y = picture.facePoints.find_label_point_Y("ER2") - 5;

        R2.X = picture.facePoints.find_label_point_X("ER3");
        R2.Y = picture.facePoints.find_label_point_Y("ER3") - 5;

        R3.X = picture.facePoints.find_label_point_X("ER4");
        R3.Y = picture.facePoints.find_label_point_Y("ER4") - 5;

        R4.X = picture.facePoints.find_label_point_X("ER4");
        R4.Y = picture.facePoints.find_label_point_Y("ER4") - 8;

        R5.X = picture.facePoints.find_label_point_X("ER3");
        R5.Y = picture.facePoints.find_label_point_Y("ER3") - 8;

        R6.X = picture.facePoints.find_label_point_X("ER2");
        R6.Y = picture.facePoints.find_label_point_Y("ER2") - 7;

        R7.X = picture.facePoints.find_label_point_X("ER1");
        R7.Y = picture.facePoints.find_label_point_Y("ER1") - 7;

        CvPoint[][] points1 = new CvPoint[][]{
            new CvPoint[] {L0,L1,L2,L3,L4,L5,L6,L7},
          
        };

        CvPoint[][] points2 = new CvPoint[][]{
              new CvPoint[] {R0,R1,R2,R3,R4,R5,R6,R7},
        };

        CvScalar color = new CvScalar();
        color.Val0 = 168;
        color.Val1 = 87;
        color.Val2 = 167;

        int[] number = { 8, 8 };
        Cv.FillPoly(copyImage, points1, color);
        Cv.FillPoly(copyImage, points2, color);
        Cv.AddWeighted(copyImage, 0.2, tmpImage, 0.8, 0, tmpImage);

        return tmpImage;
    }

    public IplImage effectShadow2(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage;
        tmpImage = Cv.CloneImage(copyImage);

        CvPoint L0, L1, L2, L3, L4, L5, L6;
        CvPoint R0, R1, R2, R3, R4, R5, R6;

        L0.X = picture.facePoints.find_label_point_X("EL1");
        L0.Y = picture.facePoints.find_label_point_Y("EL1") - 2;

        L1.X = picture.facePoints.find_label_point_X("EL2");
        L1.Y = picture.facePoints.find_label_point_Y("EL2") - 2;

        L2.X = picture.facePoints.find_label_point_X("EL3");
        L2.Y = picture.facePoints.find_label_point_Y("EL3") - 2;

        L3.X = picture.facePoints.find_label_point_X("EL4");
        L3.Y = picture.facePoints.find_label_point_Y("EL4") - 2;

        L4.X = picture.facePoints.find_label_point_X("BL5");
        L4.Y = picture.facePoints.find_label_point_Y("BL5");

        L5.X = picture.facePoints.find_label_point_X("BL6");
        L5.Y = picture.facePoints.find_label_point_Y("BL6");

        L6.X = picture.facePoints.find_label_point_X("BL1");
        L6.Y = picture.facePoints.find_label_point_Y("BL1");

        R0.X = picture.facePoints.find_label_point_X("ER1");
        R0.Y = picture.facePoints.find_label_point_Y("ER1") - 3;

        R1.X = picture.facePoints.find_label_point_X("ER2");
        R1.Y = picture.facePoints.find_label_point_Y("ER2") - 3;

        R2.X = picture.facePoints.find_label_point_X("ER3");
        R2.Y = picture.facePoints.find_label_point_Y("ER3") - 3;

        R3.X = picture.facePoints.find_label_point_X("ER4");
        R3.Y = picture.facePoints.find_label_point_Y("ER4") - 3;

        R4.X = picture.facePoints.find_label_point_X("BR5");
        R4.Y = picture.facePoints.find_label_point_Y("BR5");

        R5.X = picture.facePoints.find_label_point_X("BR6");
        R5.Y = picture.facePoints.find_label_point_Y("BR6");

        R6.X = picture.facePoints.find_label_point_X("BR1");
        R6.Y = picture.facePoints.find_label_point_Y("BR1");

        CvPoint[][] points1 = new CvPoint[][]{
            new CvPoint[] {L0,L1,L2,L3,L4,L5,L6},
          
        };

        CvPoint[][] points2 = new CvPoint[][]{
              new CvPoint[] {R0,R1,R2,R3,R4,R5,R6},
        };

        CvScalar color = new CvScalar();
        color.Val0 = 168;
        color.Val1 = 87;
        color.Val2 = 167;


        Cv.FillPoly(copyImage, points1, color);
        Cv.FillPoly(copyImage, points2, color);
        Cv.AddWeighted(copyImage, 0.2, tmpImage, 0.8, 0, tmpImage);

        return tmpImage;
    }

    public IplImage effectShadow3(IplImage srcImage, IplImage copyImage)
    {
        IplImage tmpImage = Cv.CloneImage(copyImage);

        CvPoint L0, L1, L2, L3, L4, L5, L6, L7;
        CvPoint LL0, LL1, LL2, LL3, LL4, LL5, LL6, LL7;
        CvPoint R0, R1, R2, R3, R4, R5, R6, R7;
        CvPoint RR0, RR1, RR2, RR3, RR4, RR5, RR6, RR7;

        L0.X = picture.facePoints.find_label_point_X("EL1");
        L0.Y = picture.facePoints.find_label_point_Y("EL1");

        L1.X = picture.facePoints.find_label_point_X("EL2");
        L1.Y = picture.facePoints.find_label_point_Y("EL2");

        L2.X = picture.facePoints.find_label_point_X("EL3");
        L2.Y = picture.facePoints.find_label_point_Y("EL3");

        L3.X = picture.facePoints.find_label_point_X("EL4");
        L3.Y = picture.facePoints.find_label_point_Y("EL4");

        L4.X = picture.facePoints.find_label_point_X("EL4");
        L4.Y = picture.facePoints.find_label_point_Y("EL4") - 6;

        L5.X = picture.facePoints.find_label_point_X("EL3");
        L5.Y = picture.facePoints.find_label_point_Y("EL3") - 6;

        L6.X = picture.facePoints.find_label_point_X("EL2");
        L6.Y = picture.facePoints.find_label_point_Y("EL2") - 5;

        L7.X = picture.facePoints.find_label_point_X("EL1");
        L7.Y = picture.facePoints.find_label_point_Y("EL1") - 2;

        R0.X = picture.facePoints.find_label_point_X("ER1");
        R0.Y = picture.facePoints.find_label_point_Y("ER1") - 2;

        R1.X = picture.facePoints.find_label_point_X("ER2");
        R1.Y = picture.facePoints.find_label_point_Y("ER2") - 2;

        R2.X = picture.facePoints.find_label_point_X("ER3");
        R2.Y = picture.facePoints.find_label_point_Y("ER3") - 2;

        R3.X = picture.facePoints.find_label_point_X("ER4");
        R3.Y = picture.facePoints.find_label_point_Y("ER4") - 2;

        R4.X = picture.facePoints.find_label_point_X("ER4");
        R4.Y = picture.facePoints.find_label_point_Y("ER4") - 10;

        R5.X = picture.facePoints.find_label_point_X("ER3");
        R5.Y = picture.facePoints.find_label_point_Y("ER3") - 10;

        R6.X = picture.facePoints.find_label_point_X("ER2");
        R6.Y = picture.facePoints.find_label_point_Y("ER2") - 3;

        R7.X = picture.facePoints.find_label_point_X("ER1");
        R7.Y = picture.facePoints.find_label_point_Y("ER1") - 1;
////////////////////////////////////////////////////////////////////////////////////////////////
        LL0.X = picture.facePoints.find_label_point_X("EL1");
        LL0.Y = picture.facePoints.find_label_point_Y("EL1");

        LL1.X = picture.facePoints.find_label_point_X("EL6");
        LL1.Y = picture.facePoints.find_label_point_Y("EL6");

        LL2.X = picture.facePoints.find_label_point_X("EL5");
        LL2.Y = picture.facePoints.find_label_point_Y("EL5");

        LL3.X = picture.facePoints.find_label_point_X("EL4");
        LL3.Y = picture.facePoints.find_label_point_Y("EL4");

        LL4.X = picture.facePoints.find_label_point_X("EL4");
        LL4.Y = picture.facePoints.find_label_point_Y("EL4") + 6;

        LL5.X = picture.facePoints.find_label_point_X("EL5");
        LL5.Y = picture.facePoints.find_label_point_Y("EL5") + 6;

        LL6.X = picture.facePoints.find_label_point_X("EL6");
        LL6.Y = picture.facePoints.find_label_point_Y("EL6") + 5;

        LL7.X = picture.facePoints.find_label_point_X("EL1");
        LL7.Y = picture.facePoints.find_label_point_Y("EL1") + 2;

        RR0.X = picture.facePoints.find_label_point_X("ER1");
        RR0.Y = picture.facePoints.find_label_point_Y("ER1");

        RR1.X = picture.facePoints.find_label_point_X("ER6");
        RR1.Y = picture.facePoints.find_label_point_Y("ER6");

        RR2.X = picture.facePoints.find_label_point_X("ER5");
        RR2.Y = picture.facePoints.find_label_point_Y("ER5");

        RR3.X = picture.facePoints.find_label_point_X("ER4");
        RR3.Y = picture.facePoints.find_label_point_Y("ER4");

        RR4.X = picture.facePoints.find_label_point_X("ER4");
        RR4.Y = picture.facePoints.find_label_point_Y("ER4") +5;

        RR5.X = picture.facePoints.find_label_point_X("ER5");
        RR5.Y = picture.facePoints.find_label_point_Y("ER5") +5;

        RR6.X = picture.facePoints.find_label_point_X("ER6");
        RR6.Y = picture.facePoints.find_label_point_Y("ER6") +3;

        RR7.X = picture.facePoints.find_label_point_X("ER1");
        RR7.Y = picture.facePoints.find_label_point_Y("ER1") +1;


        CvPoint[][] points1 = new CvPoint[][]{
            new CvPoint[] {L0,L1,L2,L3,L4,L5,L6,L7},
          
        };

        CvPoint[][] points2 = new CvPoint[][]{
              new CvPoint[] {R0,R1,R2,R3,R4,R5,R6,R7},
        };

        CvPoint[][] points3 = new CvPoint[][]{
              new CvPoint[] {LL0,LL1,LL2,LL3,LL4,LL5,LL6,LL7}
        };

        CvPoint[][] points4 = new CvPoint[][]{
              new CvPoint[] {RR0,RR1,RR2,RR3,RR4,RR5,RR6,RR7}
        };

        CvScalar color = new CvScalar();
        color.Val0 = 168;
        color.Val1 = 87;
        color.Val2 = 167;

        Cv.FillPoly(copyImage, points1, color);
        Cv.FillPoly(copyImage, points2, color);
        Cv.FillPoly(copyImage, points3, color);
        Cv.FillPoly(copyImage, points4, color);
        Cv.AddWeighted(copyImage, 0.2, tmpImage, 0.8, 0, tmpImage);

        return tmpImage;
    }
}
