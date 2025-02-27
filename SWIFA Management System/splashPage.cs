namespace SWIFA_Management_System
{
    public partial class splashPage : Form
    {
        private float originalFormWidth;
        private float originalFormHeight;
        public splashPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalFormWidth = this.Width;
            originalFormHeight = this.Height;
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


    }
}
