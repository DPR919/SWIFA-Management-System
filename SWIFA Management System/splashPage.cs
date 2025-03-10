using System.Diagnostics;

namespace SWIFA_Management_System
{
    public partial class splashPage : Form
    {
        public splashPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            eventCreation eventCreation = new eventCreation();
            eventCreation.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentEvents currentEvents = new currentEvents();
            currentEvents.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pastEvents pastEvents = new pastEvents();
            pastEvents.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url = "https://pschimelman.wixsite.com/swifa";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open url: " + ex.Message);
            }
        }
    }
}
