using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace WebForum.Forms.WebLists
{
    internal class ForumsList
    {
        static Form form = new Form();
        static int page = 1;
        static Panel panel = new Panel();
        public Form ForumsListIni(Form formF)
        {
            form = formF;
            form.Size = new System.Drawing.Size(440, 285);
            //
            Panel panelHeader = new Panel();
            panelHeader.BorderStyle = BorderStyle.Fixed3D;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Size = new System.Drawing.Size(formF.Size.Width - 15, 25);

            Button buttonProfile = new Button();
            Button buttonForum = new Button();

            buttonProfile.Text = "Profile";
            buttonProfile.Location = new System.Drawing.Point(5, 0);
            buttonProfile.Size = new System.Drawing.Size(70, 22);
            buttonProfile.Click += buttonProfile_Click;
            panelHeader.Controls.Add(buttonProfile);

            buttonForum.Text = "Forum List";
            buttonForum.Location = new System.Drawing.Point(buttonProfile.Location.X + buttonProfile.Size.Width + 5, buttonProfile.Location.Y);
            buttonForum.Size = buttonProfile.Size;
            buttonForum.Click += buttonForumList_Click;
            panelHeader.Controls.Add(buttonForum);

            //

            Button PrevPage = new Button();
            PrevPage.Font = new System.Drawing.Font("Arial", 9);
            PrevPage.Text = "Previous";
            PrevPage.Click += buttonPrevPage_Click;
            PrevPage.Location = new System.Drawing.Point(120, 215);
            PrevPage.Size = new System.Drawing.Size(70, 27);
            
            Button NextPage = new Button();
            NextPage.Font = new System.Drawing.Font("Arial", 9);
            NextPage.Text = "Next";
            NextPage.Location = new System.Drawing.Point(220, 215);
            NextPage.Click += buttonNextPage_Click;
            NextPage.Size = new System.Drawing.Size(70, 27);

            panel.BorderStyle = BorderStyle.Fixed3D;
            panel.Location = new System.Drawing.Point(0, 25);
            panel.Size = new System.Drawing.Size(form.Size.Width - 16, 185);

            drawPanel();

            //

            form.Controls.Add(panel);
            form.Controls.Add(panelHeader);
            form.Controls.Add(NextPage);
            form.Controls.Add(PrevPage);
            return form;
        }

        private static void buttonNextPage_Click(object sender, EventArgs e)
        {
            //Добавить ограничения бд
            page++;
            drawPanel();
        }
        private static void buttonPrevPage_Click(object sender, EventArgs e)
        {
            //Добавить ограничения бд
            if (page - 1 > 0)
            {
                page--;
                drawPanel();
            }
        }

        private static void buttonForumList_Click(object sender, EventArgs e)
        {
            ForumsList forums = new ForumsList();
            form.Controls.Clear();
            forums.ForumsListIni(form);
        }

        private static void buttonProfile_Click(object sender, EventArgs e)
        {
            Profile Profile = new Profile();
            form.Controls.Clear();
            Profile.ProfileFormIni(form);
        }
        private static void drawPanel ()
        {
            panel.Controls.Clear();
            for (int i = 0; i < 7 * page; i++)//содержит по 7 полей, прокручивать кнопками
            {
                Panel innerPanel = new Panel();
                innerPanel.BorderStyle = BorderStyle.Fixed3D;
                innerPanel.Location = new System.Drawing.Point(0, 26 * i);
                innerPanel.Size = new System.Drawing.Size(panel.Size.Width, 26);
                panel.Controls.Add(innerPanel);

                Button button = new Button();
                button.Location = new System.Drawing.Point(panel.Size.Width - 80, 0);
                button.Text = "Go to Forum";
                innerPanel.Controls.Add(button);
                //добавить действие

                Label label = new Label();
                label.Text = "Forum Name " + (i + 1 + (7 * (page - 1)));
                innerPanel.Controls.Add(label);
            }
        }
    }
}
