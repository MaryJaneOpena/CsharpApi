using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpApi
{
    public partial class Add_Books : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Add_Books()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                HttpResponseMessage response = await client.GetAsync("http://localhost/phpapi/booksapi.php");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtBooktitle_TextChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txtGenre_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a book object
                var book = new
                {
                    title = txtBooktitle.Text,
                    authorID = int.Parse(txtAuthor.Text), // Assuming author ID is inputted as an integer
                    genre = txtGenre.Text,
                    publishedDate = dateTimePicker1.Value.ToString("yyyy-MM-dd")
                };

                // Serialize the book object to JSON using Newtonsoft.Json
                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Post the JSON to the API
                HttpResponseMessage response = await client.PostAsync("http://localhost/phpapi/booksapi.php", content);
                response.EnsureSuccessStatusCode();

                // Read and display the response
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var otherforms = new users();
            this.Hide();
            otherforms.Show();
        }
    }
}
