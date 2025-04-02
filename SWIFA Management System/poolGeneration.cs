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
        private int _dragIndex = -1;
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
                lb.DragOver += poolListBox_DragOver;

                int row = i / 3;
                int col = i % 3;

                poolsLayout.Controls.Add(lb, col, row);
                poolListBoxes.Add(lb);
            }
        }

        private void poolListBox_DragOver(object sender, DragEventArgs e)
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
            if (draggedItem == null) return;

            // If user dropped in the same list, reorder instead of cross-list move
            if (currentDragSource == targetListBox)
            {
                // If _dragIndex is invalid, do nothing
                if (_dragIndex < 0) return;

                // Convert mouse coords to list coords to find the drop index
                Point dropPoint = targetListBox.PointToClient(new Point(e.X, e.Y));
                int dropIndex = targetListBox.IndexFromPoint(dropPoint);
                if (dropIndex < 0)
                    dropIndex = targetListBox.Items.Count - 1;

                // Remove the item from the old location
                targetListBox.Items.RemoveAt(_dragIndex);

                // If removing an item from above shifts indices, adjust if needed
                if (dropIndex > targetListBox.Items.Count)
                    dropIndex = targetListBox.Items.Count;

                // Insert the item at the new index
                targetListBox.Items.Insert(dropIndex, draggedItem);
            }
            else
            {
                // Different list => cross-list move
                targetListBox.Items.Add(draggedItem);

                // If you're removing from 'selectedBladeList' or a pool box
                // handle that as you currently do
                selectedBladeList.Items.Remove(draggedItem);

                if (currentDragSource != null && currentDragSource != targetListBox)
                {
                    currentDragSource.Items.Remove(draggedItem);
                }
            }

            // Reset _dragIndex after finishing
            _dragIndex = -1;
        }

        private void poolListBox_MouseDown(object sender, MouseEventArgs e)
        {
            currentDragSource = (ListBox)sender;

            _dragIndex = currentDragSource.IndexFromPoint(e.Location);
            if (_dragIndex < 0)
            {
                return;
            }
            if (currentDragSource.SelectedItem != null)
            {
                DoDragDrop(currentDragSource.SelectedItem, DragDropEffects.Move);
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (selectedBladeList.Items.Count > 0)
            {
                MessageBox.Show("Not all teams are assigned.");
                return;
            }

            using (var db = new EventsDatabaseContext())
            {
                for (int i = 0; i < poolListBoxes.Count; i++)
                {
                    var lb = poolListBoxes[i];
                    var newPool = new Pool
                    {
                        Blade = bladeSelection.SelectedItem.ToString(),
                        PoolNum = i + 1,
                        EventId = _eventId
                    };
                    db.Pools.Add(newPool);
                    db.SaveChanges();

                    for (int j = 0; j < lb.Items.Count; j++)
                    {
                        var team = (Team)lb.Items[j];
                        var dbTeam = db.Teams.Find(team.TeamId);
                        if (dbTeam != null)
                        {
                            dbTeam.SeedinPool = j + 1;
                            dbTeam.PoolId = newPool.PoolId;
                        }
                    }
                    db.SaveChanges();
                }
            }
            MessageBox.Show("Pools generated successfully");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void poolsLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
