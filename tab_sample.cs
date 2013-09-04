using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;
public class MyButton : Button{
	public int tab_num;
	public int val;
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
public class Form1 : System.Windows.Forms.Form
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
		Size W_size = new Size(680,900);
		this.ClientSize = new System.Drawing.Size(W_size.Width,W_size.Height);
		InitializeComponent(W_size);
	}
	// This method is required for Designer support.
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
//    this.tab1Button1 = new System.Windows.Forms.Button();
		this.tab1Button = new MyButton[3];
//    this.tab2Button = new MyButton[3];
//    this.tab3Button = new MyButton[3];

		int allowance = 20;
		Size T_size = new System.Drawing.Size(640,360);
		Size T_size2 = new System.Drawing.Size(640,20);
		tabControl1.Location = new System.Drawing.Point(allowance,W_size.Height/2 - 50);
		tabControl1.Size = T_size;
		tabControl1.SelectedIndex = 0;
		tabControl1.TabIndex = 0;
		tabPage1.Text = "cheek";
		tabPage1.Size = T_size2;
		tabPage1.TabIndex = 0;
		tabPage2.Text = "nose";
		tabPage2.Size = T_size2;
		tabPage2.TabIndex = 1;
		tabPage3.Text = "mouse";
		tabPage3.Size = T_size2;
		tabPage3.TabIndex = 2;
		for(int i = 0;i < 3;i++){
			string s = i.ToString();
			tab1Button[i] = new MyButton(0,i){
				Text = "cheek"+s,
					Location = new System.Drawing.Point(10,50+i*50),
					Size = new System.Drawing.Size(80,40),
					TabIndex = i,
			};
			tab1Button[i].Click += new System.EventHandler(this.tab1Button_Click);
		}
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
//    tab1Button1.Location = new System.Drawing.Point(88, 144);
//    tab1Button1.Size = new System.Drawing.Size(80, 40);
//    tab1Button1.TabIndex = 0;
//    tab1Button1.Text = "button1";
//    tab1Button.Click += new 
//      System.EventHandler(this.tab1Button_Click);
		this.Text = "Form1";

		// Adds controls to the second tab page.
		tabPage2.Controls.Add(this.tab2CheckBox3);
		tabPage2.Controls.Add(this.tab2CheckBox2);
		tabPage2.Controls.Add(this.tab2CheckBox1);
		// Adds controls to the third tab page.
		tabPage3.Controls.Add(this.tab3RadioButton2);
		tabPage3.Controls.Add(this.tab3RadioButton1);
		// Adds controls to the first tab page.
		tabPage1.Controls.Add(this.tab1Label1);
//    tabPage1.Controls.Add(this.tab1Button1);
		for(int i = 0;i < 3;i++)
			tabPage1.Controls.Add(this.tab1Button[i]);
		// Adds the TabControl to the form.
		this.Controls.Add(this.tabControl1);
		// Adds the tab pages to the TabControl.
		tabControl1.Controls.Add(this.tabPage1);
		tabControl1.Controls.Add(this.tabPage2);
		tabControl1.Controls.Add(this.tabPage3);
	}

	private void tab1Button_Click (object sender, System.EventArgs e)
	{
		// Inserts the code that should run when the button is clicked.
	}
}
class Program{
	public static void Main(string[] args)
	{
		Application.EnableVisualStyles();
		Application.Run(new Form1());
	}
}

