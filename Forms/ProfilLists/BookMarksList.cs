using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WebForum.Forms.WebLists;
using Button = System.Windows.Forms.Button;
using MySql.Data.MySqlClient;
using System.Diagnostics.Metrics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WebForum.Forms.ProfilLists
{
    internal class BookMarksList
    {
        static Form form = new Form();
        static int page = 1;
        static Panel panel = new Panel();
        static MySqlConnection connection;
        static int Id;
        static int UserId;

        public void BookmarksListIni(Form formF, MySqlConnection connectionF, int id, int userId)
        {
            UserId = userId;
            Id = id;
            connection = connectionF;
            form = formF;
            form.Size = new System.Drawing.Size(440, 285);
            //
            Panel panelHeader = new Panel();
            panelHeader.BorderStyle = BorderStyle.Fixed3D;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Size = new System.Drawing.Size(formF.Size.Width - 15, 25);

            Button buttonProfile = new Button();
            Button buttonForum = new Button();
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
            string query = $"Select count(*) from m2m_Bookmarks where Bo_Profile = {UserId}";

            MySqlCommand command = new MySqlCommand(query, connection);
            object result = command.ExecuteScalar();
            int Num = Convert.ToInt32(result);
            double i = (Num - 1) / 7;
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

        private static void drawPanel()
        {
            panel.Controls.Clear();

            List<int> boTopics = new List<int>();
            string query = $"SELECT Bo_Topic FROM m2m_Bookmarks WHERE Bo_Profile = {UserId}";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int boTopic = reader.GetInt32(0);
                        boTopics.Add(boTopic);
                    }
                }
            }

            for (int i = 0; i < 7 && i < boTopics.Count(); i++)
            {
                string queryTopic = $"SELECT Top_Name FROM Topic where Top_id = {boTopics[i + (7 * (page - 1))]};";

                MySqlCommand command = new MySqlCommand(queryTopic, connection);
                MySqlDataReader reader = command.ExecuteReader();
                string name = "";
                if (reader.Read())
                    name = reader.GetString("Top_Name");

                if (name != "")
                {
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
                    button.Text = $"Watch";
                    button.Name = $"{name}";
                    button.Click += buttonTopic_Click;
                    innerPanel.Controls.Add(button);
                }
                command.Dispose();
                reader.Close();
            }

        }

        private static void buttonTopic_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string queryTopicId = $"SELECT Top_id FROM Topic WHERE Top_Name = \'{button.Name}\'";
            int TopicId = -1;
            using (MySqlCommand TopicCommand = new MySqlCommand(queryTopicId, connection))
            {
                object result = TopicCommand.ExecuteScalar();
                TopicId = Convert.ToInt32(result);
            }

            PostsList UserList = new PostsList();
            form.Controls.Clear();
            UserList.PostsListIni(form, connection, Id, TopicId);
        }

        private static void buttonUserList_Click(object sender, EventArgs e)
        {
            UsersList UserList = new UsersList();
            form.Controls.Clear();
            UserList.UsersListIni(form, connection, Id);
        }
    }
}
