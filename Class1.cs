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
		testImage = Cv.LoadImage("image.png");
	}

	public void myshowImage()
	{
		Cv.NamedWindow("window");
		Cv.ShowImage("window", testImage);

	}

	void readXML()
	{
		FaceXml.Load("facedetect.xml");
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


        //江夏側の設定部分
        //Face:UIに出力する顔画像部分のPicture Box
        //picture:OpenCVSharpで顔画像の読み込みクラス
        this.Face = new System.Windows.Forms.PictureBox();
        this.picture = new opencvsharp_test();

        Face.Location = new Point(20, 20);
        Face.Image = picture.testImage.ToBitmap();
        Face.Size = new System.Drawing.Size(640, 480);




        int allowance = 20;
        Size T_size = new System.Drawing.Size(640, 360);
        Size T_size2 = new System.Drawing.Size(640, 20);
        tabControl1.Location = new System.Drawing.Point(allowance, W_size.Height / 2);
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

        IplImage tmpImg;
        tmpImg = Cv.CloneImage(picture.testImage);
        //テスト段階なのではずれボタンとタブの情報を入れています
        tmpImg = face_change(9, 9, tmpImg);
        
        Face.Image = tmpImg.ToBitmap();
        Face.Size = new System.Drawing.Size(640, 480);

    }


    void button_Click(object sender, EventArgs e)
    {
        // 押されたボタンのテキストをタイトルバーに表示
        this.Text = (sender as Button).Text;
    }

    IplImage face_change(int tab_number, int button_number,IplImage inputImg)
    {
        //口と鼻とほほ
        //ほほ:tab_number=0;
        //鼻：tab_number = 1;
        //口：tab_number = 2;
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
                        break;
                    case 1:
                        //ほほのエフェクト1
                        System.Console.Write("button number is {0}\n", button_number);
                        break;
                    case 2:
                        //ほほのエフェクト2
                        System.Console.Write("button number is {0]\n", button_number);
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
                        //鼻のエフェクト0
                        System.Console.Write("button number is {0}\n", button_number);
                        break;
                    case 1:
                        //鼻のエフェクト1
                        System.Console.Write("button number is {0}\n", button_number);
                        break;
                    case 2:
                        //鼻のエフェクト2
                        System.Console.Write("button number is {0}\n", button_number);
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
                        break;
                    case 1:
                        //口のエフェクト1
                        System.Console.Write("button number is {0}\n", button_number);
                        break;
                    case 2:
                        //口のエフェクト2
                        System.Console.Write("button number is {0}\n", button_number);
                        break;
                    default:
                        //エラー処理（エフェクト番号違い)
                        System.Console.Write("Error!予期せぬbutton番号です\n");
                        break;
                }
                break;
            default:
                //エラー処理（タブ番号違い)
                System.Console.Write("Error!予期せぬ番号です.現在はテスト用のエフェクトが走ります");
                //tmpImg = test_effect(tmpImg);
                tmpImg = effectMouthOrange(tmpImg, tmpImg);
                
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
        Cv.FillPoly(tmpImage, points, orange);
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
        Cv.FillPoly(tmpImage, points, orange);
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
        Cv.FillPoly(tmpImage, points, orange);
        Cv.AddWeighted(copyImage, 0.2, tmpImage, 0.8, 0, tmpImage);
        return tmpImage;
    }


    /*
     * C++版エフェクト
     *IplImage* effectMouthRed(IplImage* srcImage, IplImage* copyImage)
{
>>>>>>> 843ef95bcd5689668125e96d6282ed219343ae90
	//口の座標取得部分ここから
	CvPoint** points;
	string parts_name = "MOUTH";

	points = (CvPoint**)cvAlloc(sizeof(CvPoint*));
	points[0] = (CvPoint*)cvAlloc(sizeof(CvPoint)*parts[parts_name].size());
	int number[1];
	number[0] = parts[parts_name].size();
	for(unsigned int i = 0; i < parts[parts_name].size(); ++i){
	points[0][i].x = parts[parts_name][i].x;
	points[0][i].y = parts[parts_name][i].y;
	}
	//口の座標ここまで
	//M9以外を使っている
	cvFillPoly(copyImage, points, number, 1, cvScalar(20,10,200));
	cvAddWeighted(copyImage, 0.2, srcImage, 0.8, 0, srcImage);

	return srcImage;
	}
	 * */





	//テストエフェクト
	//全体の色合いを適当に変化させます。
	/*
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
	 * */

}
