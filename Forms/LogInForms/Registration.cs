﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.forms;

namespace WebForum.Forms
{
    internal class Registration
    {
        static TextBox textBoxPassword;
        static Form form = new Form();
        public Form RegistrationFormIni(Form formF)
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

            Button buttonCreate = new Button();
            Button buttonAuthorization = new Button();

            // Установка расположения и размеров элементов
            labelHeader.Text = "Registration";
            labelHeader.Font = new System.Drawing.Font("Arial", 18);
            labelHeader.Location = new System.Drawing.Point(45, 30);
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
            buttonCreate.Text = "Create Account";
            buttonCreate.Location = new System.Drawing.Point(labelLogin.Location.X, labelPassword.Location.Y + 40);
            buttonCreate.Size = textBoxPassword.Size;
            buttonCreate.Click += buttonCreate_Click;
            
            buttonAuthorization.Text = "Authorization";
            buttonAuthorization.Location = new System.Drawing.Point(labelLogin.Location.X, buttonCreate.Location.Y + 20);
            buttonAuthorization.Size = textBoxPassword.Size;
            buttonAuthorization.Click += ButtonAuthorization_Click;

            // Добавление элементов на форму
            form.Controls.Add(labelHeader);
            form.Controls.Add(labelLogin);
            form.Controls.Add(textBoxLogin);

            form.Controls.Add(labelPassword);
            form.Controls.Add(textBoxPassword);

            form.Controls.Add(PasswordCheck);

            form.Controls.Add(buttonCreate);
            form.Controls.Add(buttonAuthorization);

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

        private static void ButtonAuthorization_Click(object sender, EventArgs e)
        {
            Authorization authorization = new Authorization();
            form.Controls.Clear();
            authorization.AuthorizationFormIni(form);
        }

        private static void buttonCreate_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            form.Controls.Clear();
            profile.ProfileFormIni(form);
        }
    }
}