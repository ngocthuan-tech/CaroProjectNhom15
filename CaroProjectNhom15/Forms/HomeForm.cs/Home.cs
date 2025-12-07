using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth.Models;

namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    public partial class Home : Form
    {
        private readonly string? _currentUid;
        private readonly string? _idToken;

        public Home()
        {
            InitializeComponent();

            // Wire up the Friends button to open the FriendsForm
            Btn_Friends.Click += Btn_Friends_Click;

            // Wire up the User button to open UserForm
            Btn_user.Click += Btn_user_Click;
        }

        // New constructor that accepts a UserModel and optional idToken and fills UI
        public Home(UserModel user, string? idToken = null) : this()
        {
            _currentUid = user?.Uid;
            _idToken = idToken;

            try
            {
                Tb_tenUser.Text = !string.IsNullOrEmpty(user?.UserName) ? user.UserName : user?.Email ?? string.Empty;

                // Try to load avatar robustly (data URI, http(s), file path). Keep designer default if none/failed.
                TryLoadAvatar(user?.AvatarUrl);
            }
            catch { /* be resilient to nulls or missing data */ }
        }

        private void Btn_user_Click(object? sender, EventArgs e)
        {
            // If we don't have current uid, inform the developer/user
            if (string.IsNullOrEmpty(_currentUid))
            {
                MessageBox.Show("Current user id not available. User profile cannot be shown.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Open UserForm and pass current uid and optional idToken
            var userForm = new UserForm(_currentUid, _idToken);
            userForm.ShowDialog(this);

            // If the user logged out from UserForm, just close Home.
            // The original LoginForm (which opened Home) now has a handler to Show() itself,
            // so closing Home returns user to Login without exiting application.
            if (userForm.SignedOut)
            {
                this.Close();
                return;
            }

            // Update Home UI immediately if user changed profile in UserForm
            try
            {
                if (!string.IsNullOrEmpty(userForm.NewUserName))
                {
                    Tb_tenUser.Text = userForm.NewUserName;
                }

                if (!string.IsNullOrEmpty(userForm.NewAvatarUrl))
                {
                    TryLoadAvatar(userForm.NewAvatarUrl);
                }
            }
            catch { /* ignore UI update errors */ }
        }

        private void Btn_Friends_Click(object? sender, EventArgs e)
        {
            // Pass current uid into FriendsForm so it can load the friend list
            var friendsForm = new FriendsForm(_currentUid);
            friendsForm.ShowDialog(this);
        }

        // Robust avatar loader: supports data URI (base64), http(s) URLs and file paths.
        private void TryLoadAvatar(string? avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(avatarUrl))
                return; // keep default image from designer

            try
            {
                // Data URI (data:image/png;base64,...)
                if (avatarUrl.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                {
                    var comma = avatarUrl.IndexOf(',');
                    if (comma >= 0 && comma + 1 < avatarUrl.Length)
                    {
                        var base64 = avatarUrl.Substring(comma + 1);
                        var bytes = Convert.FromBase64String(base64);
                        using var ms = new MemoryStream(bytes);
                        var img = Image.FromStream(ms);
                        Pb_anhUser.Image = new Bitmap(img);
                    }
                    return;
                }

                // HTTP/HTTPS remote URL
                if (avatarUrl.StartsWith("http:", StringComparison.OrdinalIgnoreCase) ||
                    avatarUrl.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Pb_anhUser.LoadAsync(avatarUrl);
                    }
                    catch
                    {
                        // ignore — keep designer default
                    }
                    return;
                }

                // Local file path
                if (File.Exists(avatarUrl))
                {
                    try
                    {
                        // Load copy to avoid locking file
                        using var fs = File.OpenRead(avatarUrl);
                        var img = Image.FromStream(fs);
                        Pb_anhUser.Image = new Bitmap(img);
                    }
                    catch
                    {
                        // ignore
                    }
                    return;
                }

                // Otherwise, not recognized — keep default (designer) avatar.
            }
            catch
            {
                // swallow errors, keep default avatar
            }
        }
    }
}
