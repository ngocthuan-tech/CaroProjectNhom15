using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth.Models;

namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    public partial class ListFriends : UserControl
    {
        public ListFriends()
        {
            InitializeComponent();
            // Configure listView for simple details view
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Username", 140);
            listView1.Columns.Add("Full name", 180);
            listView1.FullRowSelect = true;
        }

        /// <summary>
        /// Populate the list control with a collection of users.
        /// </summary>
        public void SetUsers(List<UserModel> users)
        {
            listView1.Items.Clear();

            if (users == null || users.Count == 0)
                return;

            foreach (var u in users)
            {
                var li = new ListViewItem(u.UserName ?? "");
                li.SubItems.Add(u.FullName ?? "");
                li.Tag = u; // keep model for selection usage
                listView1.Items.Add(li);
            }
        }

        /// <summary>
        /// Return the selected user model or null if none selected.
        /// </summary>
        public UserModel? GetSelectedUser()
        {
            if (listView1.SelectedItems.Count == 0) return null;
            return listView1.SelectedItems[0].Tag as UserModel;
        }

        /// <summary>
        /// Expose the underlying ListView if caller wants to attach events.
        /// </summary>
        public ListView InnerListView => listView1;
    }
}
