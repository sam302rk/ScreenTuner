using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenTuner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var icon = new NotifyIcon
            {
                Icon = Icon,
                Text = "ScreenTuner"
            };

            icon.Click += (o0, args0) =>
            {
                Visible = !Visible;
            };

            icon.Visible = true;

            Location = new Point(64, 64);

            Width = 5;
            Height = 0;
            short screenNumber = 0;

            foreach (var screen in Screen.AllScreens)
            {
                screenNumber++;

                if (Height < screen.Bounds.Height / 10 + 10) Height = screen.Bounds.Height / 10 + 10;

                var buttonElement = new Button
                {
                    Size = new Size(screen.Bounds.Width / 10, screen.Bounds.Height / 10),
                    Text = "Screen " + screenNumber,
                    Location = new Point(Width, 5),
                    FlatStyle = FlatStyle.Flat
                };

                buttonElement.Click += (o, args) =>
                {
                    buttonElement.Enabled = false;
                    var f = new Form
                    {
                        FormBorderStyle = FormBorderStyle.None,
                        BackColor = Color.Black,
                        Bounds = screen.Bounds
                    };
                    f.Load += (o1, args1) =>
                    {
                        f.Size = screen.Bounds.Size;
                        f.Left = screen.Bounds.Left;
                        f.Top = screen.Bounds.Top;
                    };
                    f.Click += (o2, args2) =>
                    {
                        f.Close();
                        Show();
                        buttonElement.Enabled = true;
                    };
                    f.Show();
                    Hide();
                };
                Controls.Add(buttonElement);

                Width += screen.Bounds.Width / 10 + 5;
            }
        }
    }
}
