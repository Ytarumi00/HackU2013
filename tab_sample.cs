using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text;
using System.IO;
public class MyButton : Button{
	private int tab_num;
	private int val;
	public MyButton(int t,int v){
		set_tab_num(t);
		set_val(v);
	}
	void set_tab_num(int t){
		this.tab_num = t;
	}
	void set_val(int v){
		this.val = v;
	}
	public int get_tab_num(){
		return this.tab_num;
	}
	public int get_val(){
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

	// Declare variables.
	//  private System.Windows.Forms.RadioButton tab3RadioButton2;
	//  private System.Windows.Forms.RadioButton tab3RadioButton1;
	//  private System.Windows.Forms.CheckBox tab2CheckBox3;
	//  private System.Windows.Forms.CheckBox tab2CheckBox2;
	//  private System.Windows.Forms.CheckBox tab2CheckBox1;
	//  private System.Windows.Forms.Label tab1Label1;
	//  private System.Windows.Forms.Button tab1Button1;
	private MyButton[] tab1Button;
	private MyButton[] tab2Button;
	private MyButton[] tab3Button;
	private System.Windows.Forms.TabPage tabPage3;
	private System.Windows.Forms.TabPage tabPage2;
	private System.Windows.Forms.TabPage tabPage1;
	private System.Windows.Forms.TabControl tabControl1;

	public Form1()
	{
		// This call is required for Windows Form Designer support.
		Size W_size = new Size(600,700);
		this.ClientSize = new System.Drawing.Size(W_size.Width,W_size.Height);
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
		this.tab1Button = new MyButton[3];
		this.tab2Button = new MyButton[3];
		this.tab3Button = new MyButton[3];
		//    this.tab2CheckBox3 = new System.Windows.Forms.CheckBox();
		//    this.tab3RadioButton2 = new System.Windows.Forms.RadioButton();
		//    this.tab2CheckBox2 = new System.Windows.Forms.CheckBox();
		//    this.tab2CheckBox1 = new System.Windows.Forms.CheckBox();
		//    this.tab3RadioButton1 = new System.Windows.Forms.RadioButton();
		//    this.tab1Label1 = new System.Windows.Forms.Label();
		//    this.tab1Button1 = new System.Windows.Forms.Button();

		int allowance = 50;
		int first_x = 50;
		Size T_size = new System.Drawing.Size(W_size.Width-40,W_size.Height/2-40);
		Size T_size2 = new System.Drawing.Size(640,20);
		Size B_size = new System.Drawing.Size(60,80);
		tabControl1.Location = new System.Drawing.Point(20,W_size.Height/2 + 30);
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

		get_TabImage Tab1Image = new get_TabImage("/home/yu-suke/Pictures/cheek/","cheek",3);
		for(int i = 0;i < 3;i++){
			tab1Button[i] = new MyButton(0,i){
				Image = Tab1Image.img[i],
							Location = new System.Drawing.Point(first_x+i*(B_size.Width+allowance),T_size.Height/2),
							Size = B_size,
							TabIndex = i,
			};
			tab1Button[i].Click += new System.EventHandler(this.TabButton_Click);
		}
		get_TabImage Tab2Image = new get_TabImage("/home/yu-suke/Pictures/eye/","eye",3);
		for(int i = 0;i < 3;i++){
			tab2Button[i] = new MyButton(1,i){
				Image = Tab2Image.img[i],
							Location = new System.Drawing.Point(first_x+i*(B_size.Width+allowance),T_size.Height/2),
							Size = B_size,
							TabIndex = i,
			};
			tab2Button[i].Click += new System.EventHandler(this.TabButton_Click);
		}
		get_TabImage Tab3Image = new get_TabImage("/home/yu-suke/Pictures/mouse/","mouse",3);
		for(int i = 0;i < 3;i++){
			tab3Button[i] = new MyButton(2,i){
				Image = Tab3Image.img[i],
							Location = new System.Drawing.Point(first_x+i*(B_size.Width+allowance),T_size.Height/2),
							Size = B_size,
							TabIndex = i,
			};
			tab3Button[i].Click += new System.EventHandler(this.TabButton_Click);
		}

		//    tab2CheckBox3.Location = new System.Drawing.Point(32, 136);
		//    tab2CheckBox3.Text = "checkBox3";
		//    tab2CheckBox3.Size = new System.Drawing.Size(176, 32);
		//    tab2CheckBox3.TabIndex = 2;
		//    tab2CheckBox3.Visible = true;
		//    tab3RadioButton2.Location = new System.Drawing.Point(40, 72);
		//    tab3RadioButton2.Text = "radioButton2";
		//    tab3RadioButton2.Size = new System.Drawing.Size(152, 24);
		//    tab3RadioButton2.TabIndex = 1;
		//    tab3RadioButton2.Visible = true;
		//    tab2CheckBox2.Location = new System.Drawing.Point(32, 80);
		//    tab2CheckBox2.Text = "checkBox2";
		//    tab2CheckBox2.Size = new System.Drawing.Size(176, 32);
		//    tab2CheckBox2.TabIndex = 1;
		//    tab2CheckBox2.Visible = true;
		//    tab2CheckBox1.Location = new System.Drawing.Point(32, 24);
		//    tab2CheckBox1.Text = "checkBox1";
		//    tab2CheckBox1.Size = new System.Drawing.Size(176, 32);
		//    tab2CheckBox1.TabIndex = 0;
		//    tab3RadioButton1.Location = new System.Drawing.Point(40, 32);
		//    tab3RadioButton1.Text = "radioButton1";
		//    tab3RadioButton1.Size = new System.Drawing.Size(152, 24);
		//    tab3RadioButton1.TabIndex = 0;
		//    tab1Button1.Location = new System.Drawing.Point(88, 144);
		//    tab1Button1.Size = new System.Drawing.Size(80, 40);
		//    tab1Button1.TabIndex = 0;
		//    tab1Button1.Text = "button1";
		//    tab1Button.Click += new 
		//      System.EventHandler(this.tab1Button_Click);
		this.Text = "Form1";

		// Adds controls to the second tab page.
		//    tabPage2.Controls.Add(this.tab2CheckBox3);
		//    tabPage2.Controls.Add(this.tab2CheckBox2);
		//    tabPage2.Controls.Add(this.tab2CheckBox1);
		// Adds controls to the third tab page.
		//    tabPage3.Controls.Add(this.tab3RadioButton2);
		//    tabPage3.Controls.Add(this.tab3RadioButton1);
		// Adds controls to the first tab page.
		//    tabPage1.Controls.Add(this.tab1Label1);
		//    tabPage1.Controls.Add(this.tab1Button1);
		for(int i = 0;i < 3;i++)
			tabPage1.Controls.Add(this.tab1Button[i]);
		for(int i = 0;i < 3;i++)
			tabPage2.Controls.Add(this.tab2Button[i]);
		for(int i = 0;i < 3;i++)
			tabPage3.Controls.Add(this.tab3Button[i]);
		// Adds the TabControl to the form.
		this.Controls.Add(this.tabControl1);
		// Adds the tab pages to the TabControl.
		tabControl1.Controls.Add(this.tabPage1);
		tabControl1.Controls.Add(this.tabPage2);
		tabControl1.Controls.Add(this.tabPage3);
	}

	private void TabButton_Click (object sender, System.EventArgs e)
	{
		Console.Write((sender as MyButton).get_tab_num());
		Console.Write((sender as MyButton).get_val());

	}
}
class Program{
	public static void Main(string[] args)
	{
		Application.EnableVisualStyles();
		Application.Run(new Form1());
	}
}
