using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace WebForum.Forms.SettingTopic
{
    internal class AddPost
    {
        static Form form = new Form();
        static MySqlConnection connection;
        static int Id;

        static ComboBox comboBoxForum = new ComboBox();
        static ComboBox comboBoxTopic = new ComboBox();
        static TextBox Title = new TextBox();
        static TextBox Text = new TextBox();

        public void AddPostIni(Form formF, MySqlConnection connectionF, int id)
        {
            form = formF;
            connection = connectionF;
            Id = id;
            form.Size = new System.Drawing.Size(440, 320);


            Label labelForum = new Label();
            Label labelTitle = new Label();
            Label labelTopic = new Label();

            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Size = new System.Drawing.Size(424, 50);
            panel.BorderStyle = BorderStyle.FixedSingle;

            panel.Controls.Add(comboBoxForum);
            panel.Controls.Add(comboBoxTopic);
            panel.Controls.Add(Title);

            panel.Controls.Add(labelForum);
            panel.Controls.Add(labelTitle);
            panel.Controls.Add(labelTopic);

           
            comboBoxForum.Location = new System.Drawing.Point(30, 20);
            comboBoxForum.Size = new System.Drawing.Size(100, 15);
            comboBoxForum.SelectedIndexChanged += new EventHandler(comboBox_Forum_SelectedIndexChanged);
            labelForum.Location = new System.Drawing.Point(comboBoxForum.Location.X, comboBoxForum.Location.Y - 15);
            labelForum.Text = "Forums:";
            labelForum.Size = comboBoxForum.Size;

            comboBoxForum.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTopic.DropDownStyle = ComboBoxStyle.DropDownList;

            //
            string query = "SELECT For_Name FROM Forum";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ForumName = reader.GetString("For_Name");
                comboBoxForum.Items.Add(ForumName);
            }
            comboBoxForum.DropDownStyle = ComboBoxStyle.DropDownList;
            reader.Close();
            command.Dispose();

            //
            Title.Location = new System.Drawing.Point(160, comboBoxForum.Location.Y);
            Title.Size = comboBoxForum.Size;
            labelTitle.Location = new System.Drawing.Point(Title.Location.X, Title.Location.Y - 15);
            labelTitle.Text = "Title:";
            labelTitle.Size = comboBoxForum.Size;

            comboBoxTopic.Location = new System.Drawing.Point(290, comboBoxForum.Location.Y);
            comboBoxTopic.Size = comboBoxForum.Size;
            labelTopic.Location = new System.Drawing.Point(comboBoxTopic.Location.X, comboBoxTopic.Location.Y - 15);
            labelTopic.Text = "Topics:";
            labelTopic.Size = comboBoxForum.Size;

           
            Text.Multiline = true; // Разрешить многострочный ввод текста
            Text.WordWrap = true;
            Text.Location = new System.Drawing.Point(0, 50);
            Text.Size = new System.Drawing.Size(424, 200);


            Button buttonSave = new Button();
            Button buttonExit = new Button();

            buttonSave.Text = "Save";
            buttonSave.Location = new System.Drawing.Point(110, 250);
            buttonSave.Size = new System.Drawing.Size(100, 30);
            buttonSave.Click += buttonSave_Click;

            buttonExit.Text = "Сancel";
            buttonExit.Location = new System.Drawing.Point(210, buttonSave.Location.Y);
            buttonExit.Size = buttonSave.Size;
            buttonExit.Click += buttonCancel_Click;

            form.Controls.Add(panel);
            form.Controls.Add(buttonSave);
            form.Controls.Add(buttonExit);
            form.Controls.Add(Text);
        }

        private static void buttonSave_Click(object sender, EventArgs e)
        {
            
            if (Text.Text != "" && comboBoxForum.SelectedItem != null && comboBoxTopic.SelectedItem != null && Title.Text != "")
            {

                string IdTopicQuery = "SELECT Top_id FROM Topic WHERE Top_Name = @TopicName";
                int TopicId;
                using (MySqlCommand cityCommand = new MySqlCommand(IdTopicQuery, connection))
                {
                    cityCommand.Parameters.AddWithValue("@TopicName", comboBoxTopic.SelectedItem.ToString());
                    TopicId = (int)cityCommand.ExecuteScalar();
                    cityCommand.Dispose();
                }

                string query = "INSERT INTO Post (Post_id, Post_Title, Post_Text, Post_Topic, Post_Profile) VALUES (@postId, @title, @postText, @topic, @profile)";
                
                string getMaxIdQuery = "SELECT MAX(Post_id) FROM Post";
                int postId;
                using (MySqlCommand getMaxIdCommand = new MySqlCommand(getMaxIdQuery, connection))
                {                  
                    try
                    {
                        object result = getMaxIdCommand.ExecuteScalar();
                        postId = Convert.ToInt32(result) + 1;
                    }
                    catch
                    {
                        postId = 0;
                    }
                   
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@postId", postId);
                    command.Parameters.AddWithValue("@title", Title.Text);
                    command.Parameters.AddWithValue("@postText", Text.Text);
                    command.Parameters.AddWithValue("@topic", Convert.ToString(TopicId));
                    command.Parameters.AddWithValue("@profile", Convert.ToString(Id));
                    command.ExecuteNonQuery();
                }

                Profile Profile = new Profile();
                form.Controls.Clear();
                Profile.ProfileFormIni(form, connection, Id);
            }
            
        }
        private static void buttonCancel_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            form.Controls.Clear();
            profile.ProfileFormIni(form, connection, Id);
        }

        private void comboBox_Forum_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedForum = comboBoxForum.SelectedItem.ToString();
            string countryIdQuery = $"SELECT For_id FROM Forum WHERE For_Name = '{selectedForum}'";
            MySqlCommand ForumIdCommand = new MySqlCommand(countryIdQuery, connection);
            int ForumId = Convert.ToInt32(ForumIdCommand.ExecuteScalar());

            string TopicQuery = $"SELECT Top_Name FROM Topic WHERE Top_Forum = {ForumId}";
            MySqlCommand TopicCommand = new MySqlCommand(TopicQuery, connection);
            MySqlDataReader reader = TopicCommand.ExecuteReader();

            comboBoxTopic.Items.Clear();
            comboBoxTopic.Text = "";
            while (reader.Read())
            {
                string cityName = reader.GetString("Top_Name");
                comboBoxTopic.Items.Add(cityName);
            }

            reader.Close();
            ForumIdCommand.Dispose();
            TopicCommand.Dispose();
        }
    }
}
