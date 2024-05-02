using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.forms;
using WebForum.Forms.Bans;
using WebForum.Forms.ProfilLists;
using WebForum.Forms.SettingTopic;
using WebForum.Forms.WebLists;

namespace WebForum.Forms
{
    internal class Profile
    {
        static TextBox textBoxPassword;
        static Form form = new Form();
        public Form ProfileFormIni(Form formF)
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

            Panel panelProfileInf = new Panel();
            panelProfileInf.BorderStyle = BorderStyle.Fixed3D;
            panelProfileInf.Location = new System.Drawing.Point(210, 70);
            panelProfileInf.Size = new System.Drawing.Size(200, 170);

            //Инцилизация элементов панели
            Panel panelButt = new Panel();
            Panel panelProfile = new Panel();
            Label labelLoginName = new Label();
            Label lableDataReg = new Label();//для бд
            Button buttonProfileSettings = new Button();
            Button buttonSubscriptions = new Button();
            Button buttonTopicSettings = new Button();
            Button buttonForumSettings = new Button();
            Button buttonBookmarkSettings = new Button();
            Button buttonCommentsSettings = new Button();
            Button buttonAddPost = new Button();
            Button buttonPosts = new Button();
            Button buttonExit = new Button();

            //Инцилизация остальных элементов
            Label lableHeaderInf = new Label();
            Label labelName = new Label();
            Label labelSurname = new Label();
            Label labelPatronymic = new Label();

            Label labelCountry = new Label();
            Label labelCity = new Label();//для бд
            Label labelGender = new Label();//для бд
            Label labelAge = new Label();// для бд

            lableHeaderInf.Text = "Profile Information:";
            lableHeaderInf.Location = new System.Drawing.Point(205, 40);
            lableHeaderInf.Size = new System.Drawing.Size(300, 30);
            lableHeaderInf.Font = new System.Drawing.Font("Arial", 18);

            //
            labelName.Text = "Name:  " + "Ni!";
            labelName.Location = new System.Drawing.Point(0, 8);
            labelName.Size = new System.Drawing.Size(200, 20);
            labelName.Font = new System.Drawing.Font("Arial", 10);

            labelSurname.Text = "Surname:  " + "Ko!";
            labelSurname.Location = new System.Drawing.Point(labelName.Location.X, labelName.Location.Y + 20);
            labelSurname.Size = labelName.Size;
            labelSurname.Font = labelName.Font;

            labelPatronymic.Text = "Patronymic:  " + "Al!";
            labelPatronymic.Location = new System.Drawing.Point(labelSurname.Location.X, labelSurname.Location.Y + 20);
            labelPatronymic.Size = labelName.Size;
            labelPatronymic.Font = labelName.Font;

            labelCountry.Text = "Country:  " + "Be!";
            labelCountry.Location = new System.Drawing.Point(labelPatronymic.Location.X, labelPatronymic.Location.Y + 20);
            labelCountry.Size = labelName.Size;
            labelCountry.Font = labelName.Font;

            labelCity.Text = "City:  " + "Mi!";
            labelCity.Location = new System.Drawing.Point(labelCountry.Location.X, labelCountry.Location.Y + 20);
            labelCity.Size = labelName.Size;
            labelCity.Font = labelName.Font;

            labelGender.Text = "Gender:  " + "Ma!";
            labelGender.Location = new System.Drawing.Point(labelCity.Location.X, labelCity.Location.Y + 20);
            labelGender.Size = labelName.Size;
            labelGender.Font = labelName.Font;

            labelAge.Text = "Age:  " + "20!";
            labelAge.Location = new System.Drawing.Point(labelGender.Location.X, labelGender.Location.Y + 20);
            labelAge.Size = labelName.Size;
            labelAge.Font = labelName.Font;

            form.Controls.Add(lableHeaderInf);
            panelProfileInf.Controls.Add(labelName);
            panelProfileInf.Controls.Add(labelSurname);
            panelProfileInf.Controls.Add(labelPatronymic);
            panelProfileInf.Controls.Add(labelCountry);
            panelProfileInf.Controls.Add(labelCity);
            panelProfileInf.Controls.Add(labelGender);
            panelProfileInf.Controls.Add(labelAge);

            //Панель и её элементы
            panelButt.BorderStyle = BorderStyle.Fixed3D;
            panelButt.Location = new System.Drawing.Point(5, 30);
            panelButt.Size = new System.Drawing.Size(200, 210);
            panelButt.Controls.Add(panelProfile);
            panelButt.Controls.Add(buttonProfileSettings);
            panelButt.Controls.Add(buttonSubscriptions);
            panelButt.Controls.Add(buttonTopicSettings);
            panelButt.Controls.Add(buttonForumSettings);
            panelButt.Controls.Add(buttonBookmarkSettings);
            panelButt.Controls.Add(buttonCommentsSettings);
            panelButt.Controls.Add(buttonAddPost);
            panelButt.Controls.Add(buttonPosts);
            panelButt.Controls.Add(buttonExit);
            //Добавить шапку сайта
            //
            buttonProfileSettings.Text = "Edit profile";
            buttonProfileSettings.Location = new System.Drawing.Point(5, 45);
            buttonProfileSettings.Size = new System.Drawing.Size(90, 25);
            buttonProfileSettings.Click += buttonEditProfile_Click;
            
            buttonSubscriptions.Text = "Subscription";
            buttonSubscriptions.Location = new System.Drawing.Point(100, 45);
            buttonSubscriptions.Size = buttonProfileSettings.Size;
            buttonSubscriptions.Click += buttonSubscriptionsList_Click;
            //
            buttonTopicSettings.Text = "Topic Tag Bun";
            buttonTopicSettings.Location = new System.Drawing.Point(buttonProfileSettings.Location.X, buttonProfileSettings.Location.Y + 30);
            buttonTopicSettings.Size = buttonProfileSettings.Size;
            buttonTopicSettings.Click += buttonTopicTagsBunList_Click;
            
            buttonForumSettings.Text = "Forum Tag Ban";
            buttonForumSettings.Location = new System.Drawing.Point(buttonSubscriptions.Location.X, buttonProfileSettings.Location.Y + 30);
            buttonForumSettings.Size = buttonProfileSettings.Size;
            buttonForumSettings.Click += buttonForumTagsBunList_Click;
            //
            buttonBookmarkSettings.Text = "Bookmarks";
            buttonBookmarkSettings.Location = new System.Drawing.Point(buttonTopicSettings.Location.X, buttonTopicSettings.Location.Y + 30);
            buttonBookmarkSettings.Size = buttonProfileSettings.Size;
            buttonBookmarkSettings.Click += buttonBookmarksList_Click;

            buttonCommentsSettings.Text = "Your Comments";
            buttonCommentsSettings.Location = new System.Drawing.Point(buttonForumSettings.Location.X, buttonForumSettings.Location.Y + 30);
            buttonCommentsSettings.Size = buttonProfileSettings.Size;
            buttonCommentsSettings.Click += buttonCommentsList_Click;
            
            //
            buttonAddPost.Text = "Add Post";
            buttonAddPost.Location = new System.Drawing.Point(buttonBookmarkSettings.Location.X, buttonBookmarkSettings.Location.Y + 30);
            buttonAddPost.Size = buttonProfileSettings.Size;
            buttonAddPost.Click += buttonAddPost_Click;

            buttonPosts.Text = "Your Posts";
            buttonPosts.Location = new System.Drawing.Point(buttonCommentsSettings.Location.X, buttonCommentsSettings.Location.Y + 30);
            buttonPosts.Size = buttonProfileSettings.Size;
            buttonPosts.Click += buttonPosts_Click;

            //

            buttonExit.Text = "Exit Account";
            buttonExit.Location = new System.Drawing.Point(buttonAddPost.Location.X, buttonAddPost.Location.Y + 30);
            buttonExit.Size = buttonProfileSettings.Size;
            buttonExit.Click += buttonExit_Click;

            //
            panelProfile.BorderStyle = BorderStyle.FixedSingle;
            panelProfile.Location = new System.Drawing.Point(0, 0);
            panelProfile.Size = new System.Drawing.Size(panelButt.Size.Width - 4, 40);
            panelProfile.Controls.Add(labelLoginName);
            panelProfile.Controls.Add(lableDataReg);

            labelLoginName.Text = "Your NickName!!!";//--------------------------------!!!
            labelLoginName.Font = new System.Drawing.Font("Arial", 14);
            labelLoginName.Location = new System.Drawing.Point(0, 0);
            labelLoginName.Size = new System.Drawing.Size(200, 20);

            lableDataReg.Text = "Registration date: " + "10.10.1010";
            lableDataReg.Location = new System.Drawing.Point(3, 20);
            lableDataReg.Size = new System.Drawing.Size(panelButt.Size.Width - 4, 30);


            //
            form.Controls.Add(panelButt);
            form.Controls.Add(panelHeader);
            form.Controls.Add(panelProfileInf);

            

            return form;
        }

        private static void buttonExit_Click(object sender, EventArgs e)
        {
            Authorization authorization = new Authorization();
            form.Controls.Clear();
            authorization.AuthorizationFormIni(form);
        }

        private static void buttonEditProfile_Click(object sender, EventArgs e)
        {
            EditProfile EditPorf = new EditProfile();
            form.Controls.Clear();
            EditPorf.EditProfileFormIni(form);
        }

        private static void buttonProfile_Click(object sender, EventArgs e)
        {
            Profile Profile = new Profile();
            form.Controls.Clear();
            Profile.ProfileFormIni(form);
        }

        private static void buttonPosts_Click(object sender, EventArgs e)
        {
            YourPostsList Posts = new YourPostsList();
            form.Controls.Clear();
            Posts.PostListIni(form);
        }

        private static void buttonAddPost_Click(object sender, EventArgs e)
        {
            AddPost addPost = new AddPost();
            form.Controls.Clear();
            addPost.AddPostIni(form);
        }

        private static void buttonCommentsList_Click(object sender, EventArgs e)
        {
            CommentsList comments = new CommentsList();
            form.Controls.Clear();
            comments.ComentsListIni(form);
        }

        private static void buttonBookmarksList_Click(object sender, EventArgs e)
        {
            BookMarksList bookmarks = new BookMarksList();
            form.Controls.Clear();
            bookmarks.BookmarksListIni(form);
        }

        private static void buttonForumTagsBunList_Click(object sender, EventArgs e)
        {
            ForumTagBan forumTagBan = new ForumTagBan();
            form.Controls.Clear();
            forumTagBan.ForumTagBansIni(form);
        }

        private static void buttonTopicTagsBunList_Click(object sender, EventArgs e)
        {
            TopicTagBan topicTagBan = new TopicTagBan();
            form.Controls.Clear();
            topicTagBan.TopicTagBansIni(form);
        }

        private static void buttonSubscriptionsList_Click(object sender, EventArgs e)
        {
            SubscriprionsList subscriprions = new SubscriprionsList();
            form.Controls.Clear();
            subscriprions.SubscriprionsListIni(form);
        }

        private static void buttonForumList_Click(object sender, EventArgs e)
        {
            ForumsList forums = new ForumsList();
            form.Controls.Clear();
            forums.ForumsListIni(form);
        }
        //buttonCreate.Click += buttonCreate_Click;
    }
}
