using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.Forms.ProfilLists;

namespace WebForum.Forms.WebLists
{
    internal class UserProfile
    {
        static Button buttonSubscribe;

        static Form form = new Form();
        static MySqlConnection connection;
        static int UserId;
        static int Id;

        public void UserProfileIni(Form formF, MySqlConnection connectionF,int id, int Userid)
        {
            Id = id;
            UserId = Userid;
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
            Label lableDataReg = new Label();
            Button buttonSubscriptions = new Button();
            Button buttonBookmarkSettings = new Button();
            Button buttonPosts = new Button();
            buttonSubscribe = new Button();

            //Инцилизация остальных элементов
            Label lableHeaderInf = new Label();
            Label labelName = new Label();
            Label labelSurname = new Label();
            Label labelPatronymic = new Label();

            Label labelCountry = new Label();
            Label labelCity = new Label();
            Label labelGender = new Label();
            Label labelAge = new Label();
            string P_Age = "";
            int P_Country = -1;
            int P_City = -1;
            int P_Gender = -1;
            string query = $"SELECT P_Name, P_Country, P_City, P_Login, P_Surname, P_Patronymic, P_Data_of_Registration, P_Gender, P_Age FROM Profile WHERE P_id = {UserId}";
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
            panelButt.Controls.Add(buttonSubscriptions);
            panelButt.Controls.Add(buttonBookmarkSettings);
            panelButt.Controls.Add(buttonPosts);
            

            buttonSubscriptions.Text = "Subscription"; 
            buttonSubscriptions.Location = new System.Drawing.Point(5, 45);
            buttonSubscriptions.Size = new System.Drawing.Size(90, 25);
            buttonSubscriptions.Click += buttonSubscriptionsList_Click;
            
            buttonBookmarkSettings.Text = "Bookmarks";
            buttonBookmarkSettings.Location = new System.Drawing.Point(buttonSubscriptions.Location.X + 95, buttonSubscriptions.Location.Y);
            buttonBookmarkSettings.Size = buttonSubscriptions.Size;
            buttonBookmarkSettings.Click += buttonBookmarksList_Click;

            //

            buttonPosts.Text = "Posts";
            buttonPosts.Location = new System.Drawing.Point(buttonSubscriptions.Location.X, buttonSubscriptions.Location.Y + 30);
            buttonPosts.Size = buttonSubscriptions.Size;
            buttonPosts.Click += buttonPosts_Click;

            if (Id != UserId)
            {
                string querySub = "SELECT COUNT(*) FROM Subscriptions WHERE Sub_Profile = @id AND Sub_Subsciption_profile = @userId";

                using (MySqlCommand command = new MySqlCommand(querySub, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@userId", UserId);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        buttonSubscribe.Text = "Unsubscribe";
                    }
                    else
                    {
                        buttonSubscribe.Text = "Subscribe";
                    }
                }
                buttonSubscribe.Location = new System.Drawing.Point(buttonBookmarkSettings.Location.X, buttonBookmarkSettings.Location.Y + 30);
                buttonSubscribe.Size = buttonSubscriptions.Size;

                buttonSubscribe.Click += buttonSubscribe_Click;
                panelButt.Controls.Add(buttonSubscribe);
            }
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

        private static void buttonSubscribe_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) FROM Subscriptions WHERE Sub_Profile = @id AND Sub_Subsciption_profile = @userId";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@userId", UserId);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    // Удалить элемент из таблицы
                    string deleteQuery = "DELETE FROM Subscriptions WHERE Sub_Profile = @id AND Sub_Subsciption_profile = @userId";
                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@id", Id);
                        deleteCommand.Parameters.AddWithValue("@userId", UserId);
                        deleteCommand.ExecuteNonQuery();
                    }
                    buttonSubscribe.Text = "Subscribe";
                }
                else
                {
                    // Добавить элемент в таблицу
                    string insertQuery = "INSERT INTO Subscriptions (Sub_Profile, Sub_Subsciption_profile) VALUES (@id, @userId)";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@id", Id);
                        insertCommand.Parameters.AddWithValue("@userId", UserId);
                        insertCommand.ExecuteNonQuery();
                    }
                    buttonSubscribe.Text = "Unsubscribe";
                }
            }
        }

        private static void buttonSubscriptionsList_Click(object sender, EventArgs e)
        {
            SubscriprionsList subscriprions = new SubscriprionsList();
            form.Controls.Clear();
            subscriprions.SubscriprionsListIni(form, connection, Id, UserId);
        }

        private static void buttonPosts_Click(object sender, EventArgs e)
        {
            YourPostsList Posts = new YourPostsList();
            form.Controls.Clear();
            Posts.PostListIni(form, connection, Id, UserId);
        }

        
        private static void buttonBookmarksList_Click(object sender, EventArgs e)
        {
            BookMarksList bookmarks = new BookMarksList();
            form.Controls.Clear();
            bookmarks.BookmarksListIni(form, connection, Id, UserId);
        }
    }
}
