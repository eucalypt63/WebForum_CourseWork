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
        static TextBox textBoxPassword;
        static Form form = new Form();
        public Form EditProfileFormIni(Form formF)
        {
            form = formF;
            form.Size = new System.Drawing.Size(280, 240);

            Label labelName = new Label();
            Label labelSurname = new Label();
            Label labelPatronymic = new Label();

            Label labelCountry = new Label();
            Label labelCity = new Label();//для бд
            Label labelGender = new Label();//для бд
            Label labelAge = new Label();// для бд

            TextBox TextBoxName = new TextBox();
            TextBox TextBoxSurname = new TextBox();
            TextBox TextBoxPatronymic = new TextBox();

            TextBox TextBoxCountry = new TextBox();
            TextBox TextBoxCity = new TextBox();//для бд
            TextBox TextBoxGender = new TextBox();//для бд
            TextBox TextBoxAge = new TextBox();// для бд

            Button buttonSave = new Button();
            Button buttonExit = new Button();



            labelName.Text = "Name:";
            labelName.Location = new System.Drawing.Point(5, 8);
            labelName.Size = new System.Drawing.Size(100, 16);
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

            TextBoxCountry.Location = new System.Drawing.Point(labelCountry.Location.X, labelCountry.Location.Y + 15);
            TextBoxCountry.Size = new System.Drawing.Size(110, labelCountry.Size.Height);

            labelCity.Text = "City:";
            labelCity.Location = new System.Drawing.Point(labelCountry.Location.X, labelCountry.Location.Y + 40);
            labelCity.Size = labelName.Size;
            labelCity.Font = labelName.Font;

            TextBoxCity.Location = new System.Drawing.Point(labelCity.Location.X, labelCity.Location.Y + 15);
            TextBoxCity.Size = new System.Drawing.Size(110, labelCity.Size.Height);

            labelGender.Text = "Gender:";
            labelGender.Location = new System.Drawing.Point(labelCity.Location.X, labelCity.Location.Y + 40);
            labelGender.Size = labelName.Size;
            labelGender.Font = labelName.Font;

            TextBoxGender.Location = new System.Drawing.Point(labelGender.Location.X, labelGender.Location.Y + 15);
            TextBoxGender.Size = new System.Drawing.Size(110, labelGender.Size.Height);

            //

            buttonSave.Text = "Save";
            buttonSave.Location = new System.Drawing.Point(TextBoxAge.Location.X, TextBoxAge.Location.Y + 25);
            buttonSave.Size = TextBoxGender.Size;
            buttonSave.Click += buttonSave_Click;

            buttonExit.Text = "Сancel";
            buttonExit.Location = new System.Drawing.Point(TextBoxGender.Location.X, TextBoxAge.Location.Y + 25);
            buttonExit.Size = TextBoxGender.Size;
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

            form.Controls.Add(TextBoxCountry);
            form.Controls.Add(TextBoxCity);
            form.Controls.Add(TextBoxGender);
            form.Controls.Add(TextBoxAge);

            form.Controls.Add(buttonSave);
            form.Controls.Add(buttonExit);

            return formF;
        }

        private static void buttonSave_Click(object sender, EventArgs e)
        {
            //Сохранить изменения в бд
            Profile profile = new Profile();
            form.Controls.Clear();
            profile.ProfileFormIni(form);
        }
        private static void buttonCancel_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            form.Controls.Clear();
            profile.ProfileFormIni(form);
        }
    }
}
