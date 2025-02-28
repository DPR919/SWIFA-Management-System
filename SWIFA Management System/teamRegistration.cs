using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWIFA_Management_System.Models;

namespace SWIFA_Management_System
{
    public partial class teamRegistration : Form
    {
        public teamRegistration()
        {
            InitializeComponent();
            LoadSchoolsIntoComboBox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void AFencerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dropdownschool = schoolCombo.Text ?? "";
            string suffix = suffixText.Text;
            string blade = bladeCombo.Text;
            string manualSchool = manualSchoolInput.Text ?? "";

            string aFencer = AFencerName.Text ?? "";
            string bFencer = BFencerName.Text ?? "";
            string cFencer = CFencerName.Text ?? "";
            string altFencer = AltFencerName.Text ?? "";

            string finalSchool = !string.IsNullOrWhiteSpace(manualSchool)
                ? manualSchool
                : dropdownschool;

            if (!string.IsNullOrWhiteSpace(manualSchool))
            {
                using (var db = new EventsDatabaseContext())
                {
                    var existingSchool = db.Schools.FirstOrDefault(s => s.SchoolName == manualSchool);

                    if (existingSchool == null)
                    {
                        var newSchool = new School
                        {
                            SchoolName = manualSchool
                        };
                        db.Schools.Add(newSchool);
                        db.SaveChanges();
                    }
                }
                LoadSchoolsIntoComboBox();
            }

            var newTeam = new Team
            {
                School = finalSchool,
                suffix = suffix,
                Blade = blade,
                AFencer = aFencer,
                BFencer = bFencer,
                CFencer = cFencer,
                AltFencer = altFencer
            };

            using (var db = new EventsDatabaseContext())
            {
                db.Teams.Add(newTeam);
                db.SaveChanges();
            }
            MessageBox.Show("Team has been registered successfully!");
            clearFields();
        }

        private void teamRegistration_Load(object sender, EventArgs e)
        {

        }

        private void LoadSchoolsIntoComboBox()
        {
            using (var db = new EventsDatabaseContext())
            {
                var schools = db.Schools
                    .Select(s => s.SchoolName)
                    .OrderBy(name => name)
                    .ToList();

            schoolCombo.Items.Clear();
            foreach (var schoolName in schools)
                {
                    schoolCombo.Items.Add(schoolName);
                }

            }
        }

        private void clearFields()
        {
            schoolCombo.Text = "";
            suffixText.Text = "";
            bladeCombo.Text = "";
            manualSchoolInput.Text = "";
            AFencerName.Text = "";
            BFencerName.Text = "";
            CFencerName.Text = "";
            AltFencerName.Text = "";
        }
    }
}
