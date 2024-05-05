using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.forms;
using WebForum.Forms.ProfilLists;
using WebForum.Forms.SettingTopic;
using WebForum.Forms.WebLists;
using MySql.Data.MySqlClient;
using System.IO.Ports;

namespace WebForum.Forms
{
    internal class Profile
    {
        static Form form = new Form();
        static MySqlConnection connection;
        static int Id;
        public void ProfileFormIni(Form formF, MySqlConnection connectionF, int id)
        {
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
            string P_Age = "";
            int P_Country = -1;
            int P_City = -1;
            int P_Gender = -1;
            string query = $"SELECT P_Name, P_Country, P_City, P_Login, P_Surname, P_Patronymic, P_Data_of_Registration, P_Gender, P_Age FROM Profile WHERE P_id = {Id}";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string P_Name = reader.GetString("P_Name");
                        string P_Surname = reader.GetString("P_Surname");
                        string P_Patronymic = reader.GetString("P_Patronymic");
                        string P_Data_of_Registration = reader.GetString("P_Data_of_Registration");
                        P_Age = reader.GetString("P_Age");
                        string P_Login = reader.GetString("P_Login");

                        P_Country = reader.GetInt32("P_Country");
                        P_City = reader.GetInt32("P_City");
                        P_Gender = reader.GetInt32("P_Gender");


                        labelLoginName.Text = P_Login;
                        labelLoginName.Font = new System.Drawing.Font("Arial", 14);
                        labelLoginName.Location = new System.Drawing.Point(0, 0);
                        labelLoginName.Size = new System.Drawing.Size(200, 20);

                        lableDataReg.Text = "Registration date: " + P_Data_of_Registration;
                        lableDataReg.Location = new System.Drawing.Point(3, 20);
                        lableDataReg.Size = new System.Drawing.Size(panelButt.Size.Width - 4, 30);

                        labelName.Text = "Name:  " + P_Name;
                        labelName.Location = new System.Drawing.Point(0, 8);
                        labelName.Size = new System.Drawing.Size(200, 20);
                        labelName.Font = new System.Drawing.Font("Arial", 10);

                        labelSurname.Text = "Surname:  " + P_Surname;
                        labelSurname.Location = new System.Drawing.Point(labelName.Location.X, labelName.Location.Y + 20);
                        labelSurname.Size = labelName.Size;
                        labelSurname.Font = labelName.Font;

                        labelPatronymic.Text = "Patronymic:  " + P_Patronymic;
                        labelPatronymic.Location = new System.Drawing.Point(labelSurname.Location.X, labelSurname.Location.Y + 20);
                        labelPatronymic.Size = labelName.Size;
                        labelPatronymic.Font = labelName.Font;
                    }
                }
            }

            string cityQuery = "SELECT City_Name FROM City WHERE City_id = @CityId";
            string countryQuery = "SELECT C_Country_Name FROM Country WHERE C_id = @CountryId";
            string genderQuery = "SELECT Gen_Name FROM Gender WHERE Gen_id = @GenderName";

            string cityName;
            string CountryName;
            string GenderName;
            try
            {
                using (MySqlCommand cityCommand = new MySqlCommand(cityQuery, connection))
                {
                    cityCommand.Parameters.AddWithValue("@CityId", P_City.ToString());
                    cityName = (string)cityCommand.ExecuteScalar();
                    cityCommand.Dispose();
                }
            }
            catch
            {
                cityName = "";
            }

            try
            {
                using (MySqlCommand countryCommand = new MySqlCommand(countryQuery, connection))
                {
                    countryCommand.Parameters.AddWithValue("@CountryId", P_Country.ToString());
                    CountryName = (string)countryCommand.ExecuteScalar();
                    countryCommand.Dispose();
                }
            }
            catch
            {
                CountryName = "";
            }

            try
            {
                using (MySqlCommand genderCommand = new MySqlCommand(genderQuery, connection))
                {
                    genderCommand.Parameters.AddWithValue("@GenderName", P_Gender.ToString());
                    GenderName = (string)genderCommand.ExecuteScalar();
                    genderCommand.Dispose();
                }
            }
            catch
            {
                GenderName = "";
            }

            labelCountry.Text = "Country:  " + CountryName;
            labelCountry.Location = new System.Drawing.Point(labelPatronymic.Location.X, labelPatronymic.Location.Y + 20);
            labelCountry.Size = labelName.Size;
            labelCountry.Font = labelName.Font;

            labelCity.Text = "City:  " + cityName;
            labelCity.Location = new System.Drawing.Point(labelCountry.Location.X, labelCountry.Location.Y + 20);
            labelCity.Size = labelName.Size;
            labelCity.Font = labelName.Font;

            labelGender.Text = "Gender:  " + GenderName;
            labelGender.Location = new System.Drawing.Point(labelCity.Location.X, labelCity.Location.Y + 20);
            labelGender.Size = labelName.Size;
            labelGender.Font = labelName.Font;

            labelAge.Text = "Age:  " + P_Age;
            labelAge.Location = new System.Drawing.Point(labelGender.Location.X, labelGender.Location.Y + 20);
            labelAge.Size = labelName.Size;
            labelAge.Font = labelName.Font;

            Panel panelProfileInf = new Panel();
            panelProfileInf.BorderStyle = BorderStyle.Fixed3D;
            panelProfileInf.Location = new System.Drawing.Point(210, 70);
            panelProfileInf.Size = new System.Drawing.Size(200, 170);



            lableHeaderInf.Text = "Profile Information:";
            lableHeaderInf.Location = new System.Drawing.Point(205, 40);
            lableHeaderInf.Size = new System.Drawing.Size(300, 30);
            lableHeaderInf.Font = new System.Drawing.Font("Arial", 18);



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
            buttonSubscriptions.Location = new System.Drawing.Point(buttonProfileSettings.Location.X + 95, buttonProfileSettings.Location.Y);
            buttonSubscriptions.Size = buttonProfileSettings.Size;
            buttonSubscriptions.Click += buttonSubscriptionsList_Click;
            //
            buttonBookmarkSettings.Text = "Bookmarks";
            buttonBookmarkSettings.Location = new System.Drawing.Point(buttonProfileSettings.Location.X, buttonProfileSettings.Location.Y + 30);
            buttonBookmarkSettings.Size = buttonProfileSettings.Size;
            buttonBookmarkSettings.Click += buttonBookmarksList_Click;

            buttonCommentsSettings.Text = "Your Comments";
            buttonCommentsSettings.Location = new System.Drawing.Point(buttonSubscriptions.Location.X, buttonSubscriptions.Location.Y + 30);
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

            //
            form.Controls.Add(panelButt);
            form.Controls.Add(panelHeader);
            form.Controls.Add(panelProfileInf);
        }

        private static void buttonExit_Click(object sender, EventArgs e)
        {
            Authorization authorization = new Authorization();
            form.Controls.Clear();
            authorization.AuthorizationFormIni(form, connection);
        }

        private static void buttonEditProfile_Click(object sender, EventArgs e)
        {
            EditProfile EditPorf = new EditProfile();
            form.Controls.Clear();
            EditPorf.EditProfileFormIni(form, connection, Id);
        }

        private static void buttonProfile_Click(object sender, EventArgs e)
        {
            Profile Profile = new Profile();
            form.Controls.Clear();
            Profile.ProfileFormIni(form, connection, Id);
        }

        private static void buttonPosts_Click(object sender, EventArgs e)
        {
            YourPostsList Posts = new YourPostsList();
            form.Controls.Clear();
            Posts.PostListIni(form, connection, Id, Id);
        }

        private static void buttonAddPost_Click(object sender, EventArgs e)
        {
            AddPost addPost = new AddPost();
            form.Controls.Clear();
            addPost.AddPostIni(form, connection, Id);
        }

        private static void buttonCommentsList_Click(object sender, EventArgs e)
        {
            CommentsList comments = new CommentsList();
            form.Controls.Clear();
            comments.ComentsListIni(form, connection, Id, Id);
        }

        private static void buttonBookmarksList_Click(object sender, EventArgs e)
        {
            BookMarksList bookmarks = new BookMarksList();
            form.Controls.Clear();
            bookmarks.BookmarksListIni(form, connection, Id, Id);
        }

        private static void buttonSubscriptionsList_Click(object sender, EventArgs e)
        {
            SubscriprionsList subscriprions = new SubscriprionsList();
            form.Controls.Clear();
            subscriprions.SubscriprionsListIni(form, connection, Id, Id);
        }

        private static void buttonForumList_Click(object sender, EventArgs e)
        {
            ForumsList forums = new ForumsList();
            form.Controls.Clear();
            forums.ForumsListIni(form, connection, Id);
        }

        private static void buttonUserList_Click(object sender, EventArgs e)
        {
            UsersList UserList = new UsersList();
            form.Controls.Clear();
            UserList.UsersListIni(form, connection, Id);
        }
        //buttonCreate.Click += buttonCreate_Click;
    }
}
