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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            // Wire up the Friends button to open the FriendsForm
            Btn_Friends.Click += Btn_Friends_Click;
        }

        // New constructor that accepts a UserModel and fills UI
        public Home(UserModel user) : this()
        {
            try
            {
                Tb_tenUser.Text = !string.IsNullOrEmpty(user?.UserName) ? user.UserName : user?.Email ?? string.Empty;

                if (!string.IsNullOrEmpty(user?.AvatarUrl) && Uri.IsWellFormedUriString(user.AvatarUrl, UriKind.Absolute))
                {
                    try
                    {
                        // Load avatar asynchronously; if it fails, ignore
                        Pb_anhUser.Load(user.AvatarUrl);
                    }
                    catch { /* ignore image load errors */ }
                }
            }
            catch { /* be resilient to nulls or missing data */ }
        }

        private void Btn_Friends_Click(object? sender, EventArgs e)
        {
            // NOTE: If you keep the current user's UID somewhere (Auth service),
            // pass it into the FriendsForm constructor. For now we attempt to open
            // without an explicit uid; FriendsForm will try to work with FirebaseProvider.
            var friendsForm = new FriendsForm();
            friendsForm.ShowDialog(this);
        }
    }
}
