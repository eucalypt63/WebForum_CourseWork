using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.Forms.WebLists;

namespace WebForum.Forms.SettingTopic
{
    internal class AddComment
    {
        static TextBox TextBoxText;

        static Form form = new Form();
        static MySqlConnection connection;
        static int Id;
        static int PostId;

        public void AddCommentIni(Form formF, MySqlConnection connectionF, int id, int postId)
        {
            form = formF;
            connection = connectionF;
            Id = id;
            PostId = postId;

            string queryPostName = $"SELECT Post_Title FROM Post WHERE Post_id = {PostId}";
            string Name = "";
            using (MySqlCommand PostCommand = new MySqlCommand(queryPostName, connection))
            {
                Name = (string)PostCommand.ExecuteScalar();
                PostCommand.Dispose();
            }

            Label PostTitle = new Label();
            PostTitle.Text = "Add comment to the post: " + Name;
            PostTitle.Location = new System.Drawing.Point(5, 5);
            PostTitle.Size = new System.Drawing.Size(form.Width, 20);
            PostTitle.Font = new System.Drawing.Font("Arial", 10);
            form.Controls.Add(PostTitle);

            Label LabelText = new Label();
            LabelText.Text = "Text:";
            LabelText.Location = new System.Drawing.Point(0, 25);
            LabelText.Size = new System.Drawing.Size(form.Width, 15);
            form.Controls.Add(LabelText);

            TextBoxText = new TextBox();
            TextBoxText.Multiline = true;
            TextBoxText.Location = new System.Drawing.Point(0, 40);
            TextBoxText.Size = new System.Drawing.Size(form.Width - 17, 170);
            form.Controls.Add(TextBoxText);

            Button AddComments = new Button();
            AddComments.Location = new System.Drawing.Point(120, 215);
            AddComments.Size = new System.Drawing.Size(90, 25);
            AddComments.Text = "Add Comment";
            AddComments.Click += buttonSave_Click;
            form.Controls.Add(AddComments);

            Button Cancele = new Button();
            Cancele.Location = new System.Drawing.Point(AddComments.Location.X + AddComments.Size.Width + 10, AddComments.Location.Y);
            Cancele.Size = AddComments.Size;
            Cancele.Text = "Cancele";
            Cancele.Click += buttonCancel_Click;
            form.Controls.Add(Cancele);
        }

        private static void buttonSave_Click(object sender, EventArgs e)
        {
            if (TextBoxText.Text != "")
            {   try
                {
                    string query = $"INSERT INTO Comment (Com_Text, Com_Profile, Com_Post, Com_Date, Com_id) SELECT \'{TextBoxText.Text}\', {Id}, {PostId}, CURDATE(), (SELECT MAX(Com_id) + 1 FROM (SELECT * FROM Comment) AS temp);";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                catch
                {
                    string query = $"INSERT INTO Comment (Com_Text, Com_Profile, Com_Post, Com_Date, Com_id) SELECT \'{TextBoxText.Text}\', {Id}, {PostId}, CURDATE(), 0;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }

                PostInf profile = new PostInf();
                form.Controls.Clear();
                profile.PostInfIni(form, connection, Id, PostId);
            }
        }
        private static void buttonCancel_Click(object sender, EventArgs e)
        {
            PostInf profile = new PostInf();
            form.Controls.Clear();
            profile.PostInfIni(form, connection, Id, PostId);
        }
    }
}
