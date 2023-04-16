using E_diary.Properties;
using E_diary.Администратор;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_diary
{
    public partial class Administrator_Forms : Form
    {
        bool sidebarExpand;//Боковая панель
     //   bool группыCollapse; //Свернуть группы
        bool homeCollapse;//Свернуть группы


        private Form active;
        public Administrator_Forms()
        {
            InitializeComponent();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                //если боковая панель развернута, свернуть
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }
        private void HomeTimer_Tick(object sender, EventArgs e)
        {
            if (homeCollapse == true)
            {
                ГруппаContainer.Height += 50;
                if (ГруппаContainer.Height == ГруппаContainer.MaximumSize.Height)
                {
                    homeCollapse = false;
                    HomeTimer.Stop();
                }
            }
            else
            {
                ГруппаContainer.Height -= 50;
                if (ГруппаContainer.Height == ГруппаContainer.MinimumSize.Height)
                {
                    homeCollapse = true;
                    HomeTimer.Stop();
                }
            }
        }

        private void ButtonГруппы_Click(object sender, EventArgs e)
        {
            HomeTimer.Start();
        }

     

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //Кнопки новигации -
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                pictureBox3.Image = Resources.свернкть;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                pictureBox3.Image = Resources.развернуть;
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
           this.WindowState = FormWindowState.Minimized;
        }
        //;
        //

        //Переходы
        public void PanelForm(Form fm)
        {
            if (active != null)
            {
                active.Close();
            }

            active = fm;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            this.panel11.Controls.Add(fm);
            this.panel11.Tag = fm;
            fm.BringToFront();
            fm.Show();
        }

        //переход на кнопку 3П11 и 4П9
        private void bt_4П9и3П11_Click(object sender, EventArgs e)
        {
            PanelForm(new _4П9и3П11Расписание());
        }

        private void ButtonУчителя_Click(object sender, EventArgs e)
        {
            PanelForm(new Addteachers());
        }
    }
}
