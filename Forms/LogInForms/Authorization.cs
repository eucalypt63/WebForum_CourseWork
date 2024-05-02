﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;
using Label = System.Windows.Forms.Label;

namespace WebForum.forms
{
    internal class Authorization
    {
        static TextBox textBoxPassword;
        static Form form = new Form();
        public Form AuthorizationFormIni(Form formF)
        {
            form = formF;
            form.Size = new System.Drawing.Size(250, 300);
            TextBox textBoxLogin = new TextBox();

            textBoxPassword = new TextBox();
            textBoxPassword.PasswordChar = '*';

            Label labelLogin = new Label();
            Label labelPassword = new Label();
            Label labelHeader = new Label();

            CheckBox PasswordCheck = new CheckBox();

            Button buttonEnter = new Button();
            Button buttonRegistration = new Button();

            // Установка расположения и размеров элементов
            labelHeader.Text = "Authorization";
            labelHeader.Font = new System.Drawing.Font("Arial", 18);
            labelHeader.Location = new System.Drawing.Point(40, 30);
            labelHeader.Size = new System.Drawing.Size(160, 35);

            //Панель Логина
            labelLogin.Text = "Login:";
            labelLogin.Location = new System.Drawing.Point(60, 70);
            labelLogin.Size = new System.Drawing.Size(110, 15);

            textBoxLogin.Location = new System.Drawing.Point(labelLogin.Location.X, labelLogin.Location.Y + 15);
            textBoxLogin.Size = new System.Drawing.Size(110, textBoxLogin.Size.Height);

            //Панель пароля
            labelPassword.Text = "Password:";
            labelPassword.Location = new System.Drawing.Point(labelLogin.Location.X, labelLogin.Location.Y + 45);
            labelPassword.Size = labelLogin.Size;

            textBoxPassword.Location = new System.Drawing.Point(labelPassword.Location.X, labelPassword.Location.Y + 15);
            textBoxPassword.Size = new System.Drawing.Size(textBoxLogin.Size.Width, textBoxPassword.Size.Height);
            
            //Check Box
            PasswordCheck.Location = new System.Drawing.Point(textBoxPassword.Location.X + textBoxPassword.Size.Width + 5, textBoxPassword.Location.Y - 2);
            PasswordCheck.CheckedChanged += CheckBox_CheckedChanged;

            //Панель кнопок
            buttonEnter.Text = "Enter";
            buttonEnter.Location = new System.Drawing.Point(labelLogin.Location.X, labelPassword.Location.Y + 40);
            buttonEnter.Size = textBoxPassword.Size;
            buttonEnter.Click += buttonEnter_Click;

            buttonRegistration.Text = "Registration";
            buttonRegistration.Location = new System.Drawing.Point(labelLogin.Location.X, buttonEnter.Location.Y + 20);
            buttonRegistration.Size = textBoxPassword.Size;
            buttonRegistration.Click += ButtonRegistration_Click;

            // Добавление элементов на форму
            form.Controls.Add(labelHeader);
            form.Controls.Add(labelLogin);
            form.Controls.Add(textBoxLogin);

            form.Controls.Add(labelPassword);
            form.Controls.Add(textBoxPassword);

            form.Controls.Add(PasswordCheck);

            form.Controls.Add(buttonEnter);
            form.Controls.Add(buttonRegistration);

            return form;
        }

        private static void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '*';
            }
        }

        private static void ButtonRegistration_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            form.Controls.Clear();
            registration.RegistrationFormIni(form);
        }

        private static void buttonEnter_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            form.Controls.Clear();
            profile.ProfileFormIni(form);
        }
    }
}
