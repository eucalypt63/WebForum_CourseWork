using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.Forms.SettingTopic;
using static System.Net.Mime.MediaTypeNames;

namespace WebForum.Forms.WebLists
{
    internal class PostInf
    {
        static Form form = new Form();
        static MySqlConnection connection;
        static int Id;
        static int PostId;

        public void PostInfIni(Form formF, MySqlConnection connectionF, int id, int postId)
        {
            Id = id;
            connection = connectionF;
            form = formF;
            PostId = postId;

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
            form.Controls.Add(panelHeader);
            //
            Panel profileInf = new Panel();
            profileInf.BorderStyle = BorderStyle.FixedSingle;
            profileInf.Location = new System.Drawing.Point(10, 10);
            profileInf.Size = new System.Drawing.Size(100, 40);

            string queryPostProfileId = $"SELECT Post_Profile FROM Post WHERE Post_id = {PostId}";
            int profileId = -1;
            using (MySqlCommand profileCommand = new MySqlCommand(queryPostProfileId, connection))
            {
                object result = profileCommand.ExecuteScalar();
                profileId = Convert.ToInt32(result);
            }

            string ProfileNamequery = $"SELECT P_Login FROM Profile WHERE P_id = {profileId}";
            string Name = "";
            using (MySqlCommand cityCommand = new MySqlCommand(ProfileNamequery, connection))
            {
                Name = (string)cityCommand.ExecuteScalar();
                cityCommand.Dispose();
            }

            string queryPostDate = $"SELECT Post_Date FROM Post WHERE Post_id = {PostId}";
            string PostDaate = "";
            using (MySqlCommand PostCommand = new MySqlCommand(queryPostDate, connection))
            {
                PostDaate = (string)PostCommand.ExecuteScalar();
                PostCommand.Dispose();
            }

            Label name = new Label();
            name.Text = "Author: " + Name;
            name.Location = new System.Drawing.Point(2, 5);
            name.Size = new System.Drawing.Size(profileInf.Size.Width, 15);

            Label date = new Label();
            date.AutoSize = true;
            date.Text = "Date: " + PostDaate;
            date.Location = new System.Drawing.Point(2, 20);
            date.Size = new System.Drawing.Size(profileInf.Size.Width, 15);

            profileInf.Controls.Add(name);
            profileInf.Controls.Add(date);

            Panel postInf = new Panel();
            postInf.Location = new System.Drawing.Point(0, 25);
            postInf.BorderStyle = BorderStyle.FixedSingle;
            postInf.Size = new System.Drawing.Size(423, 60);

            string queryTopicName = $"SELECT Post_Title FROM Post WHERE Post_id = {PostId}";
            string TopicName = "";
            using (MySqlCommand TopicCommand = new MySqlCommand(queryTopicName, connection))
            {
                TopicName = (string)TopicCommand.ExecuteScalar();
                TopicCommand.Dispose();
            }

            Label TopicTitle = new Label();
            TopicTitle.Font = new System.Drawing.Font("Arial", 14);
            TopicTitle.Location = new System.Drawing.Point(120, 8);
            TopicTitle.Size = new System.Drawing.Size(100, 20);
            TopicTitle.Text = TopicName;



            Button CommList = new Button();
            Button AddComm = new Button();
            
            CommList.Location = new System.Drawing.Point(120, 30);
            CommList.Size = new System.Drawing.Size(90, 20);
            CommList.Text = "Comments";

            AddComm.Location = new System.Drawing.Point(CommList.Location.X + 90, CommList.Location.Y);
            AddComm.Size = CommList.Size;
            AddComm.Text = "Add Comment";
            AddComm.Click += AddComm_Click;

            postInf.Controls.Add(profileInf);
            postInf.Controls.Add(TopicTitle);
            postInf.Controls.Add(CommList);
            postInf.Controls.Add(AddComm);
            form.Controls.Add(postInf);

            string queryTopicText = $"SELECT Post_Text FROM Post WHERE Post_id = {postId}";
            string TextPost = "";
            using (MySqlCommand PostCommand = new MySqlCommand(queryTopicText, connection))
            {
                TextPost = (string)PostCommand.ExecuteScalar();
                PostCommand.Dispose();
            }
            Label TopicText = new Label();
            TopicText.Text = TextPost;
            TopicText.AutoSize = true;
            TopicText.Location = new System.Drawing.Point(0, 85);
            TopicText.Size = new System.Drawing.Size(424, 200);
            form.Controls.Add(TopicText);
        }

        private static void buttonUserList_Click(object sender, EventArgs e)
        {
            UsersList UserList = new UsersList();
            form.Controls.Clear();
            UserList.UsersListIni(form, connection, Id);
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
            string queryTopicId = $"SELECT Post_Topic FROM Post WHERE Post_id = {PostId}";
            int TopicId = -1;
            using (MySqlCommand TopicCommand = new MySqlCommand(queryTopicId, connection))
            {
                object result = TopicCommand.ExecuteScalar();
                TopicId = Convert.ToInt32(result);
            }

            PostsList PostList = new PostsList();
            form.Controls.Clear();
            PostList.PostsListIni(form, connection, Id, TopicId);
        }

        private static void AddComm_Click(object sender, EventArgs e)
        {
            AddComment AddComm = new AddComment();
            form.Controls.Clear();
            AddComm.AddCommentIni(form, connection, Id, PostId);
        }
            
    }
}
