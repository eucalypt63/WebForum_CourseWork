using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Button = System.Windows.Forms.Button;

namespace WebForum.Forms.WebLists
{
    internal class TopicsLists
    {
        static Form form = new Form();
        static int page = 1;
        static Panel panel = new Panel();
        static MySqlConnection connection;
        static int Id;
        static int forumId;
        public void TopicsListIni(Form formF, MySqlConnection connectionF, int id, int ForumId)
        {
            forumId = ForumId;
            connection = connectionF;
            form = formF;
            Id = id;
            form.Size = new System.Drawing.Size(440, 285);
            //
            Panel panelHeader = new Panel();
            panelHeader.BorderStyle = BorderStyle.Fixed3D;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Size = new System.Drawing.Size(formF.Size.Width - 15, 25);

            Button buttonProfile = new Button();
            Button buttonForum = new Button();
            Button buttonBack = new Button();
            Button buttonUsersList = new Button();

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

            buttonUsersList.Text = "Users List";
            buttonUsersList.Location = new System.Drawing.Point(buttonForum.Location.X + buttonForum.Size.Width + 5, buttonForum.Location.Y);
            buttonUsersList.Size = buttonProfile.Size;
            buttonUsersList.Click += buttonUserList_Click;
            panelHeader.Controls.Add(buttonUsersList);

            buttonBack.Text = "Back";
            buttonBack.Location = new System.Drawing.Point(panelHeader.Size.Width - 77, 0);
            buttonBack.Size = buttonProfile.Size;
            buttonBack.Click += buttonBack_Click;
            panelHeader.Controls.Add(buttonBack);
            //Действие возврата
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
        }

        private static void buttonNextPage_Click(object sender, EventArgs e)
        {
            string query = "Select count(*) from Topic";

            MySqlCommand command = new MySqlCommand(query, connection);
            object result = command.ExecuteScalar();
            int Num = Convert.ToInt32(result);
            double i = Num / 7;
            if (i + 1 > page)
            {
                page++;
                drawPanel();
            }
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
            forums.ForumsListIni(form, connection, Id);
        }

        private static void buttonProfile_Click(object sender, EventArgs e)
        {
            Profile Profile = new Profile();
            form.Controls.Clear();
            Profile.ProfileFormIni(form, connection, Id);
        }

        private static void buttonBack_Click(object sender, EventArgs e)
        {
            ForumsList ForumList = new ForumsList();
            form.Controls.Clear();
            ForumList.ForumsListIni(form, connection, Id);
        }

        private static void drawPanel()
        {
            panel.Controls.Clear();

            string query = $"SELECT Top_Name FROM Topic where Top_Forum = {forumId};";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            command.Dispose();

            int i = 0;//Добавить ограничение на 7 топиков, тоже самое с поставми и форумами
            while (reader.Read())
            {
                string name = reader.GetString("Top_Name");

                Panel innerPanel = new Panel();
                innerPanel.BorderStyle = BorderStyle.Fixed3D;
                innerPanel.Location = new System.Drawing.Point(0, 26 * i);
                innerPanel.Size = new System.Drawing.Size(panel.Size.Width, 26);
                panel.Controls.Add(innerPanel);

                Label label = new Label();
                label.Text = $"Topic: {name}";
                label.Size = new System.Drawing.Size(120, label.Size.Height);
                innerPanel.Controls.Add(label);

                Button button = new Button();
                button.Location = new System.Drawing.Point(panel.Size.Width - 80, 0);
                button.Text = "Watch";
                button.Name = $"{name}";
                innerPanel.Controls.Add(button);
                //button.Click += buttonTopic_Click;

                i++;
            }
          
            reader.Close();
     
        }

        private static void buttonUserList_Click(object sender, EventArgs e)
        {
            UsersList UserList = new UsersList();
            form.Controls.Clear();
            UserList.UsersListIni(form, connection, Id);
        }
    }
}