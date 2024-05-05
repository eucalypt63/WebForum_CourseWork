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
using Org.BouncyCastle.Bcpg;

namespace WebForum.Forms.ProfilLists
{
    internal class CommentsList
    {
        static Form form = new Form();
        static int page = 1;
        static Panel panel = new Panel();
        static MySqlConnection connection;
        static int Id;
        static int UserId;
        public void ComentsListIni(Form formF, MySqlConnection connectionF, int id, int userId)
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
            string query = $"Select count(*) from Comment where Com_Profile = {UserId}";

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

        private static void buttonUserList_Click(object sender, EventArgs e)
        {
            UsersList UserList = new UsersList();
            form.Controls.Clear();
            UserList.UsersListIni(form, connection, Id);
        }

        private static void drawPanel()
        {
            panel.Controls.Clear();

            string querySkip = $"SELECT Com_id FROM Comment where Com_Profile = {UserId};";
            MySqlCommand commandSkip = new MySqlCommand(querySkip, connection);
            MySqlDataReader readerSkip = commandSkip.ExecuteReader();
            int skip = 0;

            while (skip < (page - 1) * 7)
            {
                if (readerSkip.Read())
                    _ = readerSkip.GetString("Com_id");
                skip++;
            }

            commandSkip.Dispose();
            int pageInt = 0;
            List<int> numbers = new List<int>();
            while (readerSkip.Read() && pageInt < 7)
            {
                numbers.Add(readerSkip.GetInt32("Com_id"));
                pageInt++;
            }
            readerSkip.Close();

            int i = 0;
            foreach (int CommId in numbers)
            {
                string queryProfileId = $"SELECT Com_Profile FROM Comment WHERE Com_id = {CommId}";
                int profileId = -1;
                using (MySqlCommand profileCommand = new MySqlCommand(queryProfileId, connection))
                {
                    object result = profileCommand.ExecuteScalar();
                    profileId = Convert.ToInt32(result);
                }

                string queryProf = $"SELECT P_Login FROM Profile where P_id = {profileId};";
                MySqlCommand command = new MySqlCommand(queryProf, connection);
                MySqlDataReader reader = command.ExecuteReader();
                string name = "";
                if (reader.Read())
                    name = reader.GetString("P_Login");
                command.Dispose();
                reader.Close();

                string queryPostId = $"SELECT Com_Post FROM Comment WHERE Com_id = {CommId}";
                int PostId = -1;
                using (MySqlCommand profileCommand = new MySqlCommand(queryPostId, connection))
                {
                    object result = profileCommand.ExecuteScalar();
                    PostId = Convert.ToInt32(result);
                }

                string queryPost = $"SELECT Post_Title FROM Post where Post_id = {PostId};";
                MySqlCommand commandPost = new MySqlCommand(queryPost, connection);
                MySqlDataReader readerPost = commandPost.ExecuteReader();
                string PostName = "";
                if (readerPost.Read())
                    PostName = readerPost.GetString("Post_Title");
                commandPost.Dispose();
                readerPost.Close();

                Panel innerPanel = new Panel();
                innerPanel.BorderStyle = BorderStyle.Fixed3D;
                innerPanel.Location = new System.Drawing.Point(0, 26 * i);
                innerPanel.Size = new System.Drawing.Size(panel.Size.Width, 26);
                panel.Controls.Add(innerPanel);

                Label labelPost = new Label();
                labelPost.Text = $"Post: {PostName}";
                labelPost.Size = new System.Drawing.Size(80, labelPost.Size.Height);
                labelPost.Location = new System.Drawing.Point(90, 0);
                innerPanel.Controls.Add(labelPost);

                Label labelName = new Label();
                labelName.Text = $"User: {name}";
                labelName.Size = new System.Drawing.Size(80, labelName.Size.Height);
                innerPanel.Controls.Add(labelName);

                Button button = new Button();
                button.Location = new System.Drawing.Point(panel.Size.Width - 80, 0);
                button.Text = $"Watch";
                button.Name = $"{PostName}";
                button.Click += buttonPost_Click;
                innerPanel.Controls.Add(button);

                i++;
            }
            readerSkip.Close();
        }

        private static void buttonPost_Click(object sender, EventArgs e)
        {
            Button buttom = (Button)sender;
            string queryProfileId = $"SELECT Post_id FROM Post WHERE Post_Title = \'{buttom.Name}\'";
            int postId = -1;
            using (MySqlCommand profileCommand = new MySqlCommand(queryProfileId, connection))
            {
                object result = profileCommand.ExecuteScalar();
                postId = Convert.ToInt32(result);
            }

            PostInf profile = new PostInf();
            form.Controls.Clear();
            profile.PostInfIni(form, connection, Id, postId);
        }
    }
}
