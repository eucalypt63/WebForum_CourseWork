using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebForum.Forms
{

    internal class EditProfile
    {
        static MySqlConnection connection;
        static ComboBox comboBoxCountry = new ComboBox();
        static ComboBox comboBoxCity = new ComboBox();
        static TextBox TextBoxName = new TextBox();
        static TextBox TextBoxSurname = new TextBox();
        static TextBox TextBoxPatronymic = new TextBox();
        static ComboBox comboBoxGender = new ComboBox();//для бд
        static TextBox TextBoxAge = new TextBox();// для бд

        static Form form = new Form();
        static int Id;
        public void EditProfileFormIni(Form formF, MySqlConnection connectionF, int id)
        {
            Id = id;
            connection = connectionF;
            form = formF;
            form.Size = new System.Drawing.Size(280, 240);

            comboBoxCity.Items.Clear();
            comboBoxCountry.Items.Clear();
            Label labelName = new Label();
            Label labelSurname = new Label();
            Label labelPatronymic = new Label();

            Label labelCountry = new Label();
            Label labelCity = new Label();//для бд
            Label labelGender = new Label();//для бд
            Label labelAge = new Label();// для бд



            Button buttonSave = new Button();
            Button buttonExit = new Button();



            labelName.Text = "Name:";
            labelName.Location = new System.Drawing.Point(5, 8);
            labelName.Size = new System.Drawing.Size(100, 15);
            labelName.Font = new System.Drawing.Font("Arial", 10);

            TextBoxName.Location = new System.Drawing.Point(labelName.Location.X, labelName.Location.Y + 15);
            TextBoxName.Size = new System.Drawing.Size(110, labelName.Size.Height);

            labelSurname.Text = "Surname:";
            labelSurname.Location = new System.Drawing.Point(labelName.Location.X, labelName.Location.Y + 40);
            labelSurname.Size = labelName.Size;
            labelSurname.Font = labelName.Font;

            TextBoxSurname.Location = new System.Drawing.Point(labelSurname.Location.X, labelSurname.Location.Y + 15);
            TextBoxSurname.Size = new System.Drawing.Size(110, labelSurname.Size.Height);

            labelPatronymic.Text = "Patronymic:";
            labelPatronymic.Location = new System.Drawing.Point(labelSurname.Location.X, labelSurname.Location.Y + 40);
            labelPatronymic.Size = labelName.Size;
            labelPatronymic.Font = labelName.Font;

            TextBoxPatronymic.Location = new System.Drawing.Point(labelPatronymic.Location.X, labelPatronymic.Location.Y + 15);
            TextBoxPatronymic.Size = new System.Drawing.Size(110, labelPatronymic.Size.Height);

            labelAge.Text = "Age:";
            labelAge.Location = new System.Drawing.Point(labelPatronymic.Location.X, labelPatronymic.Location.Y + 40);
            labelAge.Size = labelName.Size;
            labelAge.Font = labelName.Font;

            TextBoxAge.Location = new System.Drawing.Point(labelAge.Location.X, labelAge.Location.Y + 15);
            TextBoxAge.Size = new System.Drawing.Size(110, labelAge.Size.Height);

            //

            labelCountry.Text = "Country:";
            labelCountry.Location = new System.Drawing.Point(labelName.Location.X + 130, labelName.Location.Y);
            labelCountry.Size = labelName.Size;
            labelCountry.Font = labelName.Font;

            comboBoxCountry.Location = new System.Drawing.Point(labelCountry.Location.X, labelCountry.Location.Y + 15);
            comboBoxCountry.Size = new System.Drawing.Size(110, labelCountry.Size.Height);
            comboBoxCountry.SelectedIndexChanged += new EventHandler(comboBoxCountry_SelectedIndexChanged);

            string query = "SELECT C_Country_Name FROM Country";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string countryName = reader.GetString("C_Country_Name");
                comboBoxCountry.Items.Add(countryName);
            }
            comboBoxCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            reader.Close();
            command.Dispose();

            labelCity.Text = "City:";
            labelCity.Location = new System.Drawing.Point(labelCountry.Location.X, labelCountry.Location.Y + 40);
            labelCity.Size = labelName.Size;
            labelCity.Font = labelName.Font;

            comboBoxCity.Location = new System.Drawing.Point(labelCity.Location.X, labelCity.Location.Y + 15);
            comboBoxCity.Size = new System.Drawing.Size(110, labelCity.Size.Height);
            comboBoxCity.DropDownStyle = ComboBoxStyle.DropDownList;

            labelGender.Text = "Gender:";
            labelGender.Location = new System.Drawing.Point(labelCity.Location.X, labelCity.Location.Y + 40);
            labelGender.Size = labelName.Size;
            labelGender.Font = labelName.Font;

            comboBoxGender.Location = new System.Drawing.Point(labelGender.Location.X, labelGender.Location.Y + 15);
            comboBoxGender.Size = new System.Drawing.Size(110, labelGender.Size.Height);

            query = "SELECT Gen_Name FROM Gender";
            command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string countryName = reader.GetString("Gen_Name");
                comboBoxGender.Items.Add(countryName);
            }
            reader.Close();
            command.Dispose();
            comboBoxGender.DropDownStyle = ComboBoxStyle.DropDownList;

            //

            buttonSave.Text = "Save";
            buttonSave.Location = new System.Drawing.Point(TextBoxAge.Location.X, TextBoxAge.Location.Y + 25);
            buttonSave.Size = comboBoxGender.Size;
            buttonSave.Click += buttonSave_Click;

            buttonExit.Text = "Сancel";
            buttonExit.Location = new System.Drawing.Point(comboBoxGender.Location.X, TextBoxAge.Location.Y + 25);
            buttonExit.Size = comboBoxGender.Size;
            buttonExit.Click += buttonCancel_Click;

            form.Controls.Add(labelName);
            form.Controls.Add(labelSurname);
            form.Controls.Add(labelPatronymic);
            form.Controls.Add(labelCountry);
            form.Controls.Add(labelCity);
            form.Controls.Add(labelGender);
            form.Controls.Add(labelAge);

            form.Controls.Add(TextBoxName);
            form.Controls.Add(TextBoxSurname);
            form.Controls.Add(TextBoxPatronymic);

            form.Controls.Add(comboBoxCountry);
            form.Controls.Add(comboBoxCity);
            form.Controls.Add(comboBoxGender);
            form.Controls.Add(TextBoxAge);

            form.Controls.Add(buttonSave);
            form.Controls.Add(buttonExit);
        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountry = comboBoxCountry.SelectedItem.ToString();
            string countryIdQuery = $"SELECT C_id FROM Country WHERE C_Country_Name = '{selectedCountry}'";
            MySqlCommand countryIdCommand = new MySqlCommand(countryIdQuery, connection);
            int countryId = Convert.ToInt32(countryIdCommand.ExecuteScalar());
            string citiesQuery = $"SELECT City_Name FROM City WHERE City_Country_id = {countryId}";
            MySqlCommand citiesCommand = new MySqlCommand(citiesQuery, connection);
            MySqlDataReader reader = citiesCommand.ExecuteReader();
            comboBoxCity.Items.Clear();
            comboBoxCity.Text = "";
            while (reader.Read())
            {
                string cityName = reader.GetString("City_Name");
                comboBoxCity.Items.Add(cityName);
            }
            reader.Close();
            countryIdCommand.Dispose();
            citiesCommand.Dispose();
        }

        private static void buttonSave_Click(object sender, EventArgs e)
        {
            if (TextBoxName.Text != "" && comboBoxCountry.SelectedItem != null && comboBoxCity.SelectedItem != null && TextBoxSurname.Text != "" && TextBoxPatronymic.Text != "" && comboBoxGender.SelectedItem != null && TextBoxAge.Text != "")
            {
                string query = $"UPDATE Profile SET P_Name = @Name, P_Country = @Country, P_City = @City, P_Surname = @Surname, P_Patronymic = @Patronymic, P_Gender = @Gender, P_Age = @Age WHERE P_id = {Id}";
                MySqlCommand command = new MySqlCommand(query, connection);

                string cityQuery = "SELECT City_id FROM City WHERE City_Name = @CityName";
                string countryQuery = "SELECT C_id FROM Country WHERE C_Country_Name = @CountryName";
                string genderQuery = "SELECT Gen_id FROM Gender WHERE Gen_Name = @GenderName";

                int cityId;
                int countryId;
                int GenId;
                using (MySqlCommand cityCommand = new MySqlCommand(cityQuery, connection))
                {
                    cityCommand.Parameters.AddWithValue("@CityName", comboBoxCity.SelectedItem.ToString());
                    cityId = (int)cityCommand.ExecuteScalar();
                    cityCommand.Dispose();
                }

                // Получение id страны
                using (MySqlCommand countryCommand = new MySqlCommand(countryQuery, connection))
                {
                    countryCommand.Parameters.AddWithValue("@CountryName", comboBoxCountry.SelectedItem.ToString());
                    countryId = (int)countryCommand.ExecuteScalar();
                    countryCommand.Dispose();
                }

                using (MySqlCommand genderCommand = new MySqlCommand(genderQuery, connection))
                {
                    genderCommand.Parameters.AddWithValue("@GenderName", comboBoxGender.SelectedItem.ToString());
                    GenId = (int)genderCommand.ExecuteScalar();
                    genderCommand.Dispose();
                }



                command.Parameters.AddWithValue("@Name", TextBoxName.Text);
                command.Parameters.AddWithValue("@Country", countryId.ToString());
                command.Parameters.AddWithValue("@City", cityId.ToString());
                command.Parameters.AddWithValue("@Surname", TextBoxSurname.Text);
                command.Parameters.AddWithValue("@Patronymic", TextBoxPatronymic.Text);
                command.Parameters.AddWithValue("@Gender", GenId.ToString());
                command.Parameters.AddWithValue("@Age", TextBoxAge.Text);
                command.ExecuteNonQuery();
                command.Dispose();

                //Сохранить изменения в бд
                Profile profile = new Profile();
                form.Controls.Clear();
                profile.ProfileFormIni(form, connection, Id);
            }
        }
        private static void buttonCancel_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            form.Controls.Clear();
            profile.ProfileFormIni(form, connection, Id);
        }
    }
}
