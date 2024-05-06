using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WebForum.Forms.WebLists
{
    internal class CommentsOfPost
    {
        static Form form = new Form();
        static int page = 1;
        static MySqlConnection connection;
        static int Id;
        static int PostId;
        static List<int> CommentsId;
        static Panel panel = new Panel();
        public void CommentsOfPostIni(Form formF, MySqlConnection connectionF, int id, int postId)
        {
            Id = id;
            connection = connectionF;
            form = formF;
            PostId = postId;
            CommentsId = new List<int>();
            //

            string querySkip = $"SELECT Com_id FROM Comment where Com_Post = {postId};";
            MySqlCommand commandSkip = new MySqlCommand(querySkip, connection);
            MySqlDataReader readerSkip = commandSkip.ExecuteReader();

            CommentsId = new List<int>();
            while (readerSkip.Read())
            {
                CommentsId.Add(readerSkip.GetInt32("Com_id"));
            }
            readerSkip.Close();
            commandSkip.Dispose();

            //
            Panel panelHeader = new Panel();
            panelHeader.BorderStyle = BorderStyle.Fixed3D;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Size = new System.Drawing.Size(formF.Size.Width - 15, 25);

            Button buttonProfile = new Button();
            Button buttonForum = new Button();
            Button buttonUsersList = new Button();
            Button buttonBack = new Button();

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

            drawPanel();

            form.Controls.Add(panelHeader);
            form.Controls.Add(NextPage);
            form.Controls.Add(PrevPage);
        }

        private static void drawPanel()
        {
            panel.Controls.Clear();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Location = new System.Drawing.Point(0, 25);
            panel.Size = new System.Drawing.Size(form.Size.Width - 17, 186);

            //
            Panel panelComment1 = new Panel();
            panelComment1.Location = new System.Drawing.Point(0, 0);
            panelComment1.Size = new System.Drawing.Size(panel.Size.Width, 93);
            panelComment1.BorderStyle = BorderStyle.FixedSingle;
            panel.Controls.Add(panelComment1);

            if (CommentsId.Count > 0)
            {
                string queryGetDate = $"SELECT Com_Date FROM Comment WHERE Com_id = {CommentsId[0 + ((page - 1) * 2)]}";
                string Date = "";
                using (MySqlCommand DateCommand = new MySqlCommand(queryGetDate, connection))
                {
                    Date = (string)DateCommand.ExecuteScalar();
                    DateCommand.Dispose();
                }

                string queryGetText = $"SELECT Com_Text FROM Comment WHERE Com_id = {CommentsId[0 + ((page - 1) * 2)]}";
                string Text = "";
                using (MySqlCommand TextCommand = new MySqlCommand(queryGetText, connection))
                {
                    Text = (string)TextCommand.ExecuteScalar();
                    TextCommand.Dispose();
                }


                string queryProfileCommId = $"SELECT Com_Profile FROM Comment WHERE Com_id = {CommentsId[0 + ((page - 1) * 2)]}";
                int profileId = -1;
                using (MySqlCommand profileCommand = new MySqlCommand(queryProfileCommId, connection))
                {
                    object result = profileCommand.ExecuteScalar();
                    profileId = Convert.ToInt32(result);
                }

                string queryGetName = $"SELECT P_Login FROM Profile WHERE P_id = {profileId}";
                string Name1 = "";
                using (MySqlCommand NameCommand1 = new MySqlCommand(queryGetName, connection))
                {
                    Name1 = (string)NameCommand1.ExecuteScalar();
                    NameCommand1.Dispose();
                }

                Panel profileInf = new Panel();
                profileInf.BorderStyle = BorderStyle.FixedSingle;
                profileInf.Location = new System.Drawing.Point(10, 10);
                profileInf.Size = new System.Drawing.Size(100, 40);
                panelComment1.Controls.Add(profileInf);

                Label name = new Label();
                name.Text = "Author: " + Name1;
                name.Location = new System.Drawing.Point(2, 5);
                name.Size = new System.Drawing.Size(profileInf.Size.Width, 15);

                Label date = new Label();
                date.AutoSize = true;
                date.Text = "Date: " + Date;
                date.Location = new System.Drawing.Point(2, 20);
                date.Size = new System.Drawing.Size(profileInf.Size.Width, 15);

                TextBox TextCom = new TextBox();
                TextCom.Location = new System.Drawing.Point(115, 0);
                TextCom.Size = new System.Drawing.Size(panelComment1.Size.Width - 115, 93);
                TextCom.Text = Text;
                TextCom.Enabled = false;
                TextCom.Multiline = true;

                panelComment1.Controls.Add(TextCom);
                profileInf.Controls.Add(name);
                profileInf.Controls.Add(date);
            }
            //
            Panel panelComment2 = new Panel();
            panelComment2.Location = new System.Drawing.Point(0, 93);
            panelComment2.Size = new System.Drawing.Size(panel.Size.Width, 93);
            panelComment2.BorderStyle = BorderStyle.FixedSingle;
            panel.Controls.Add(panelComment2);

            if (CommentsId.Count > ((page - 1) * 2) + 1)
            {
                string queryGetDate = $"SELECT Com_Date FROM Comment WHERE Com_id = {CommentsId[1 + ((page - 1) * 2)]}";
                string Date = "";
                using (MySqlCommand DateCommand1 = new MySqlCommand(queryGetDate, connection))
                {
                    Date = (string)DateCommand1.ExecuteScalar();
                    DateCommand1.Dispose();
                }

                string queryGetText = $"SELECT Com_Text FROM Comment WHERE Com_id = {CommentsId[1 + ((page - 1) * 2)]}";
                string Text = "";
                using (MySqlCommand TextCommand = new MySqlCommand(queryGetText, connection))
                {
                    Text = (string)TextCommand.ExecuteScalar();
                    TextCommand.Dispose();
                }

                string queryProfileCommId = $"SELECT Com_Profile FROM Comment WHERE Com_id = {CommentsId[1 + ((page - 1) * 2)]}";
                int profileId = -1;
                using (MySqlCommand profileCommand = new MySqlCommand(queryProfileCommId, connection))
                {
                    object result = profileCommand.ExecuteScalar();
                    profileId = Convert.ToInt32(result);
                }

                string queryGetName = $"SELECT P_Login FROM Profile WHERE P_id = {profileId}";
                string Name = "";
                using (MySqlCommand NameCommand1 = new MySqlCommand(queryGetName, connection))
                {
                    Name = (string)NameCommand1.ExecuteScalar();
                    NameCommand1.Dispose();
                }

                Panel profileInf = new Panel();
                profileInf.BorderStyle = BorderStyle.FixedSingle;
                profileInf.Location = new System.Drawing.Point(10, 10);
                profileInf.Size = new System.Drawing.Size(100, 40);
                panelComment2.Controls.Add(profileInf);

                Label name = new Label();
                name.Text = "Author: " + Name;
                name.Location = new System.Drawing.Point(2, 5);
                name.Size = new System.Drawing.Size(profileInf.Size.Width, 15);

                TextBox TextCom = new TextBox();
                TextCom.Location = new System.Drawing.Point(115, 0);
                TextCom.Size = new System.Drawing.Size(panelComment1.Size.Width - 115, 93);
                TextCom.Text = Text;
                TextCom.Enabled = false;
                TextCom.Multiline = true;


                Label date = new Label();
                date.AutoSize = true;
                date.Text = "Date: " + Date;
                date.Location = new System.Drawing.Point(2, 20);
                date.Size = new System.Drawing.Size(profileInf.Size.Width, 15);

                panelComment2.Controls.Add(TextCom);
                profileInf.Controls.Add(name);
                profileInf.Controls.Add(date);
            }
            form.Controls.Add(panel);



        }

        private static void buttonBack_Click(object sender, EventArgs e)
        {
            PostInf PostInf = new PostInf();
            form.Controls.Clear();
            PostInf.PostInfIni(form, connection, Id, PostId);
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

        private static void buttonNextPage_Click(object sender, EventArgs e)
        {
            string query = $"Select count(*) from Comment where Com_Post = {PostId}";

            MySqlCommand command = new MySqlCommand(query, connection);
            object result = command.ExecuteScalar();
            int Num = Convert.ToInt32(result);
            double i = (Num - 1) / 2;
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
    }
}
