using SWIFA_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWIFA_Management_System
{
    public partial class poolGeneration : Form
    {
        private int _eventId;
        private List<ListBox> poolListBoxes = new List<ListBox>();
        private ListBox currentDragSource;
        public poolGeneration(int eventId)
        {
            InitializeComponent();
            _eventId = eventId;
        }

        private void poolGeneration_Load(object sender, EventArgs e)
        {
            selectedBladeList.Visible = false;
        }

        private void bladeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBlade = bladeSelection.SelectedItem.ToString();

            using (var db = new EventsDatabaseContext())
            {
                var teams = db.Teams.Where(t => t.EventId == _eventId && t.Blade == selectedBlade)
                    .OrderBy(t => t.School).ThenBy(t => t.suffix).ToList();
                selectedBladeList.Items.Clear();
                foreach (var team in teams)
                {
                    selectedBladeList.Items.Add(team);
                }
            }
            selectedBladeList.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var lb in poolListBoxes)
            {
                this.Controls.Remove(lb);
                lb.Dispose();
            }
            poolListBoxes.Clear();

            int poolCount = int.Parse(numPoolSelection.SelectedItem.ToString());

            poolsLayout.Controls.Clear();

            for (int i = 0; i < poolCount; i++)
            {
                var lb = new ListBox
                {
                    Dock = DockStyle.Fill
                };
                lb.AllowDrop = true;
                lb.DragEnter += poolListBox_DragEnter;
                lb.DragDrop += poolListBox_DragDrop;
                lb.MouseDown += poolListBox_MouseDown;

                int row = i / 3;
                int col = i % 3;

                poolsLayout.Controls.Add(lb, col, row);
            }
        }

        private void selectedBladeList_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedBladeList.SelectedItem != null)
            {
                DoDragDrop(selectedBladeList.SelectedItem, DragDropEffects.Move);
            }
        }

        private void poolListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Team)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void poolListBox_DragDrop(object sender, DragEventArgs e)
        {
            var targetListBox = (ListBox)sender;

            var draggedItem = e.Data.GetData(typeof(Team)) as Team;

            if (draggedItem != null)
            {
                targetListBox.Items.Add(draggedItem);

                selectedBladeList.Items.Remove(draggedItem);

                if (currentDragSource != null && currentDragSource != targetListBox)
                {
                    currentDragSource.Items.Remove(draggedItem);
                }
            }
        }

        private void poolListBox_MouseDown(object sender, MouseEventArgs e)
        {
            currentDragSource = (ListBox)sender;
            if (currentDragSource.SelectedItem != null)
            {
                DoDragDrop(currentDragSource.SelectedItem, DragDropEffects.Move);
            }
        }

    }
}
