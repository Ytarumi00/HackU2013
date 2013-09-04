using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;

public class MyButton : Button{
	public int val = 0 ;
}
class Program
{
	[STAThread]
	static void Main()
	{
		Application.EnableVisualStyles();
		Application.Run(new Form1());
	}
}

class Form1 : Form
{
	MyButton[] button = new MyButton[3];  // ボタン
	int Rout ;

	public Form1()
	{
//    int W_width = 500;
//    int W_height = 500;
		Size W_size = new Size(500,500);
		Size B_size = new Size(50,30);
//    int B_width = 50;
//    int B_height = 30;
		this.ClientSize = new System.Drawing.Size(W_size.Width,W_size.Height);
		button[0] = new MyButton()
		{
			Text = "ボタン A",
					 TabIndex = 0,  // フォーカスの移る順位 0 (最優先)
					 Location = new Point(10, W_size.Height-50),
					 Size = new Size(B_size.Width,B_size.Height),
					 UseVisualStyleBackColor = true,  // ビジュアルスタイル
					 Cursor = Cursors.Hand,  // 手形カーソル
					 val = 0,
		};
		button[0].Click += new EventHandler(button_Click);

		button[1] = new MyButton()
		{
			Text = "ボタン B",
					 TabIndex = 1,
					 Location = new Point(button[0].Location.X+B_size.Width+10, W_size.Height-50),
					 Size = new Size(B_size.Width,B_size.Height),
					 UseVisualStyleBackColor = true,
					 Cursor = Cursors.Hand,  // 手形カーソル
					 val = 1,
		};
		button[1].Click += new EventHandler(button_Click);

		button[2] = new MyButton()
		{
			Text = "ボタン C",
					 TabIndex = 2,
					 Location = new Point(button[1].Location.X+B_size.Width+10, W_size.Height-50),
					 Size = new Size(B_size.Width,B_size.Height),
					 UseVisualStyleBackColor = true,
					 Cursor = Cursors.Hand,  // 手形カーソル
					 val = 2,
		};
		button[2].Click += new EventHandler(button_Click);

		this.Controls.AddRange(button);

		this.BackColor = SystemColors.Window;
	}

	void button_Click(object sender, EventArgs e)
	{
		// 押されたボタンのテキストをタイトルバーに表示
		this.Rout = (sender as MyButton).val;
		Console.Write(this.Rout);
	}
}
