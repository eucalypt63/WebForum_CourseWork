using MySql.Data.MySqlClient;
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
    internal class PostsList
    {
        static Form form = new Form();
        static int page = 1;
        static Panel panel = new Panel();
        static MySqlConnection connection;
        static int Id;
        static int TopicId;
        static Button buttonBookMark;

        public void PostsListIni(Form formF, MySqlConnection connectionF, int id, int topicId)
        {
            TopicId = topicId;
            Id = id;
            connection = connectionF;
            form = formF;
            form.Size = new System.Drawing.Size(440, 285);
            buttonBookMark = new Button();
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
            //
            string queryBookMark = $"SELECT COUNT(*) FROM m2m_Bookmarks WHERE Bo_Profile = {Id} AND Bo_Topic = {TopicId}";

            using (MySqlCommand command = new MySqlCommand(queryBookMark, connection))
            {

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    buttonBookMark.Text = "Del bookmark";
                }
                else
                {
                    buttonBookMark.Text = "Add bookmark";
                }
            }
            //
            buttonBookMark.Location = new System.Drawing.Point(buttonUsersList.Location.X + buttonUsersList.Size.Width + 5, buttonUsersList.Location.Y);
            buttonBookMark.Size = new System.Drawing.Size(90, 22);
            buttonBookMark.Click += buttonBookMark_Click;
            panelHeader.Controls.Add(buttonBookMark);

            buttonBack.Text = "Back";
            buttonBack.Location = new System.Drawing.Point(panelHeader.Size.Width - 77, 0);
            buttonBack.Size = buttonProfile.Size;
            buttonBack.Click += buttonBack_Click;
            panelHeader.Controls.Add(buttonBack);

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
            string query = $"Select count(*) from Post where Post_Topic = {TopicId}";

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
            forums.ForumsListIni(form,connection, Id);
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

            int i = 0;
            string queryCount = $"Select count(*) from Post where Post_Topic = {TopicId};";
            MySqlCommand commandCount = new MySqlCommand(queryCount, connection);
            object result = commandCount.ExecuteScalar();
            commandCount.Dispose();

            int count = Convert.ToInt32(result);
            int countUs = 0;

            string query = $"SELECT Post_Title FROM Post where Post_Topic = {TopicId};";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            string name = "";
            while (reader.Read() && countUs < 7 && countUs < count - (7 * (page - 1)))
            {
                name = reader.GetString("Post_Title");
                if (name != "")
                {
                    Panel innerPanel = new Panel();
                    innerPanel.BorderStyle = BorderStyle.Fixed3D;
                    innerPanel.Location = new System.Drawing.Point(0, 26 * countUs);
                    innerPanel.Size = new System.Drawing.Size(panel.Size.Width, 26);
                    panel.Controls.Add(innerPanel);

                    Label label = new Label();
                    label.Text = $"Post: {name}";
                    label.Size = new System.Drawing.Size(120, label.Size.Height);
                    innerPanel.Controls.Add(label);

                    Button button = new Button();
                    button.Location = new System.Drawing.Point(panel.Size.Width - 80, 0);
                    button.Text = $"Watch";
                    button.Name = $"{name}";
                    button.Click += buttonPost_Click;
                    innerPanel.Controls.Add(button);
                    countUs++;
                }
                i++;
            }
            command.Dispose();
            reader.Close();
            
        }

        private static void buttonPost_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string query = $"Select Post_id from pOST where Post_Title = \'{button.Name}\';";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            object result = command.ExecuteScalar();
            int IdPosT = Convert.ToInt32(result);

            PostInf Topics = new PostInf();
            form.Controls.Clear();
            Topics.PostInfIni(form, connection, Id, IdPosT);
        }

        private static void buttonUserList_Click(object sender, EventArgs e)
        {
            UsersList UserList = new UsersList();
            form.Controls.Clear();
            UserList.UsersListIni(form, connection, Id);
        }

        private static void buttonBack_Click(object sender, EventArgs e)
        {
            string queryForumId = $"SELECT Top_Forum FROM Topic WHERE Top_id = {TopicId}";
            int ForumId = -1;
            using (MySqlCommand ForumCommand = new MySqlCommand(queryForumId, connection))
            {
                object result = ForumCommand.ExecuteScalar();
                ForumId = Convert.ToInt32(result);
            }

            TopicsLists UserList = new TopicsLists();
            form.Controls.Clear();
            UserList.TopicsListIni(form, connection, Id, ForumId);
        }

        private static void buttonBookMark_Click(object sender, EventArgs e)
        {
            string queryBookMark = $"SELECT COUNT(*) FROM m2m_Bookmarks WHERE Bo_Profile = {Id} AND Bo_Topic = {TopicId}";

            using (MySqlCommand command = new MySqlCommand(queryBookMark, connection))
            {
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    // Удалить элемент из таблицы
                    string deleteQuery = "DELETE FROM m2m_Bookmarks WHERE Bo_Profile = @id AND Bo_Topic = @TopicId";
                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@id", Id);
                        deleteCommand.Parameters.AddWithValue("@TopicId", TopicId);
                        deleteCommand.ExecuteNonQuery();
                    }
                    buttonBookMark.Text = "Add bookmark";
                }
                else
                {
                    // Добавить элемент в таблицу
                    string insertQuery = "INSERT INTO m2m_Bookmarks (Bo_Profile, Bo_Topic) VALUES (@id, @TopicId)";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@id", Id);
                        insertCommand.Parameters.AddWithValue("@TopicId", TopicId);
                        insertCommand.ExecuteNonQuery();
                    }
                    buttonBookMark.Text = "Del bookmark";
                }
            }
        }

    }
}
