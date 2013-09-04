using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Windows.Forms;
using System.IO;



namespace ConsoleApplication1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());


        }
    }
}


/*
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
    Button[] button = new Button[3];  // ボタン

    public Form1()
    {
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

        this.Controls.AddRange(button);

        this.BackColor = SystemColors.Window;
    }

    void button_Click(object sender, EventArgs e)
    {
        // 押されたボタンのテキストをタイトルバーに表示
        this.Text = (sender as Button).Text;
    }
}

*/