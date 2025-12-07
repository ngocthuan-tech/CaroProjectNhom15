using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaroProjectNhom15.Utils;
using Firebase.Database;
using Firebase.Database.Query;
using Auth.Models;

namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    public partial class FriendsForm : Form
    {
        // Optional: you can set current user uid here when creating FriendsForm
        // e.g. new FriendsForm(currentUid)
        private readonly string? _currentUid;

        public FriendsForm(string? currentUid = null)
        {
            InitializeComponent();

            _currentUid = currentUid;

            // Wire up UI events
            Load += FriendsForm_Load;
            Btn_ExitFriends.Click += (_, __) => Close();
            Btn_DsBanBe.Click += async (_, __) => await ShowFriendsAsync();
            Btn_LoiMoiKB.Click += async (_, __) => await ShowRequestsAsync();
            Btn_timFriend.Click += async (_, __) => await SearchAndSendRequestAsync();
        }

        private async void FriendsForm_Load(object? sender, EventArgs e)
        {
            // Make controls visible when form loads
            Btn_ExitFriends.Visible = true;
            Btn_DsBanBe.Visible = true;
            Btn_LoiMoiKB.Visible = true;
            Btn_timFriend.Visible = true;

            // If we don't have current uid, try to infer it from FirebaseProvider.AuthClient
            if (string.IsNullOrEmpty(_currentUid))
            {
                try
                {
                    // The FirebaseAuthClient may expose the current user's id token/user info depending on usage.
                    // If you store current user's uid elsewhere in your app, pass it via constructor instead.
                }
                catch { /* ignore; we work without it but some features need uid */ }
            }

            // default: show friends if possible
            await ShowFriendsAsync();
        }

        private FirebaseClient Database => FirebaseProvider.Instance.Database;

        /// <summary>
        /// Load and display the current user's friends.
        /// expects DB structure:
        ///   friends/{currentUid}/{friendUid} = true
        ///   users/{uid} => user model
        /// </summary>
        private async Task ShowFriendsAsync()
        {
            panel1.Controls.Clear();

            if (string.IsNullOrEmpty(_currentUid))
            {
                // Show a helpful message in the panel so developer knows to pass current uid
                var lbl = new Label
                {
                    Text = "Current user uid not provided. Set it when creating FriendsForm to load friends.",
                    AutoSize = false,
                    Size = panel1.Size,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel1.Controls.Add(lbl);
                return;
            }

            var listControl = new ListFriends();
            listControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(listControl);

            try
            {
                var friendUids = new List<string>();

                await TryHelper.TryAsync(async () =>
                {
                    // read friend keys
                    var snapshot = await Database
                        .Child("friends")
                        .Child(_currentUid)
                        .OnceAsync<object>();

                    if (snapshot != null)
                    {
                        foreach (var item in snapshot)
                        {
                            // item.Key is friendUid
                            if (!string.IsNullOrEmpty(item.Key))
                                friendUids.Add(item.Key);
                        }
                    }
                }, "load friend list");

                var users = new List<UserModel>();
                foreach (var uid in friendUids)
                {
                    var u = await TryHelper.TryAsync(async () =>
                    {
                        var obj = await Database.Child("users").Child(uid).OnceSingleAsync<UserModel>();
                        return obj;
                    }, $"load user {uid}");

                    if (u != null)
                    {
                        users.Add(u);
                    }
                }

                listControl.SetUsers(users);

                // optional: double-click to open profile / start chat
                listControl.InnerListView.DoubleClick += (_, __) =>
                {
                    var selected = listControl.GetSelectedUser();
                    if (selected != null)
                    {
                        MessageBox.Show($"Selected friend: {selected.UserName} ({selected.FullName})", "Friend selected",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friends: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load incoming friend requests and display Accept/Decline buttons for each.
        /// expects DB structure:
        ///   friendRequests/{targetUid}/{fromUid} = true
        /// </summary>
        private async Task ShowRequestsAsync()
        {
            panel1.Controls.Clear();

            if (string.IsNullOrEmpty(_currentUid))
            {
                var lbl = new Label
                {
                    Text = "Current user uid not provided. Set it when creating FriendsForm to manage requests.",
                    AutoSize = false,
                    Size = panel1.Size,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel1.Controls.Add(lbl);
                return;
            }

            try
            {
                var requestFromUids = new List<string>();

                await TryHelper.TryAsync(async () =>
                {
                    var snapshot = await Database.Child("friendRequests").Child(_currentUid).OnceAsync<object>();
                    if (snapshot != null)
                    {
                        foreach (var item in snapshot)
                        {
                            if (!string.IsNullOrEmpty(item.Key))
                                requestFromUids.Add(item.Key);
                        }
                    }
                }, "load friend requests");

                if (requestFromUids.Count == 0)
                {
                    var lbl = new Label
                    {
                        Text = "No incoming friend requests.",
                        AutoSize = false,
                        Size = panel1.Size,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel1.Controls.Add(lbl);
                    return;
                }

                // create a vertical flow panel to show each request with buttons
                var flow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    Padding = new Padding(8)
                };

                foreach (var fromUid in requestFromUids)
                {
                    var user = await TryHelper.TryAsync(async () =>
                    {
                        var u = await Database.Child("users").Child(fromUid).OnceSingleAsync<UserModel>();
                        return u;
                    }, $"load user {fromUid}");

                    var itemPanel = new Panel
                    {
                        Width = Math.Max(300, panel1.Width - 40),
                        Height = 60,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(4)
                    };

                    var lbl = new Label
                    {
                        Text = user != null ? $"{user.UserName} — {user.FullName}" : $"(unknown user: {fromUid})",
                        Location = new Point(6, 8),
                        AutoSize = false,
                        Width = itemPanel.Width - 160,
                        Height = 44
                    };
                    itemPanel.Controls.Add(lbl);

                    var btnAccept = new Button
                    {
                        Text = "Accept",
                        Location = new Point(itemPanel.Width - 140, 10),
                        Size = new Size(60, 32)
                    };
                    var btnDecline = new Button
                    {
                        Text = "Decline",
                        Location = new Point(itemPanel.Width - 72, 10),
                        Size = new Size(60, 32)
                    };

                    // capture fromUid for handlers
                    btnAccept.Click += async (_, __) =>
                    {
                        btnAccept.Enabled = false;
                        btnDecline.Enabled = false;
                        await AcceptRequestAsync(fromUid);
                        // refresh the requests view
                        await ShowRequestsAsync();
                    };

                    btnDecline.Click += async (_, __) =>
                    {
                        btnAccept.Enabled = false;
                        btnDecline.Enabled = false;
                        await DeclineRequestAsync(fromUid);
                        await ShowRequestsAsync();
                    };

                    itemPanel.Controls.Add(btnAccept);
                    itemPanel.Controls.Add(btnDecline);

                    flow.Controls.Add(itemPanel);
                }

                panel1.Controls.Add(flow);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AcceptRequestAsync(string fromUid)
        {
            if (string.IsNullOrEmpty(_currentUid)) return;

            try
            {
                await TryHelper.TryAsync(async () =>
                {
                    // set friendship both ways
                    await Database.Child("friends").Child(_currentUid).Child(fromUid).PutAsync(true);
                    await Database.Child("friends").Child(fromUid).Child(_currentUid).PutAsync(true);

                    // remove the request
                    await Database.Child("friendRequests").Child(_currentUid).Child(fromUid).DeleteAsync();
                }, $"accept friend request from {fromUid}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DeclineRequestAsync(string fromUid)
        {
            if (string.IsNullOrEmpty(_currentUid)) return;

            try
            {
                await TryHelper.TryAsync(async () =>
                {
                    await Database.Child("friendRequests").Child(_currentUid).Child(fromUid).DeleteAsync();
                }, $"decline friend request from {fromUid}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error declining request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Search a user by username (textBox1) and allow sending a friend request from current user to that user.
        /// The right panel will display result rows with a "Gửi lời mời" button for each match.
        /// </summary>
        private async Task SearchAndSendRequestAsync()
        {
            var query = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Please enter a username to search.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(_currentUid))
            {
                MessageBox.Show("Current user uid not provided. Cannot send request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            panel1.Controls.Clear();

            try
            {
                List<FirebaseObject<UserModel>> matches = null!;
                await TryHelper.TryAsync(async () =>
                {
                    // Query users by username. The realtime client doesn't support complex queries easily,
                    // so we load all users and filter locally (ok for moderate size).
                    var all = await Database.Child("users").OnceAsync<UserModel>();
                    matches = all?.Where(x => !string.IsNullOrEmpty(x.Object?.UserName)
                                               && x.Object.UserName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                                  .ToList();
                }, $"search user {query}");

                if (matches == null || matches.Count == 0)
                {
                    var lbl = new Label
                    {
                        Text = $"No user found with username containing '{query}'.",
                        AutoSize = false,
                        Size = panel1.Size,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel1.Controls.Add(lbl);
                    return;
                }

                // create a vertical flow panel to show each match with Send Request button
                var flow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    Padding = new Padding(8)
                };

                foreach (var firebaseUser in matches)
                {
                    var targetUid = firebaseUser.Key;
                    var targetUser = firebaseUser.Object;

                    var itemPanel = new Panel
                    {
                        Width = Math.Max(320, panel1.Width - 40),
                        Height = 72,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(4)
                    };

                    var lbl = new Label
                    {
                        Text = targetUser != null ? $"{targetUser.UserName} — {targetUser.FullName}" : $"(unknown user: {targetUid})",
                        Location = new Point(8, 8),
                        AutoSize = false,
                        Width = itemPanel.Width - 160,
                        Height = 56,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    itemPanel.Controls.Add(lbl);

                    var btnSend = new Button
                    {
                        Text = "Gửi lời mời",
                        Location = new Point(itemPanel.Width - 140, 18),
                        Size = new Size(120, 36)
                    };

                    // avoid sending request to self
                    if (targetUid == _currentUid)
                    {
                        btnSend.Enabled = false;
                        btnSend.Text = "Bạn là chính bạn";
                    }

                    btnSend.Click += async (_, __) =>
                    {
                        btnSend.Enabled = false;
                        try
                        {
                            await TryHelper.TryAsync(async () =>
                            {
                                // Create request node friendRequests/{targetUid}/{fromUid} = true
                                await Database.Child("friendRequests").Child(targetUid).Child(_currentUid).PutAsync(true);
                            }, $"send friend request to {targetUid}");

                            MessageBox.Show($"Đã gửi lời mời tới {targetUser?.UserName ?? targetUid}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Optionally change button to indicate sent
                            btnSend.Text = "Đã gửi";
                            btnSend.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error sending request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnSend.Enabled = true;
                        }
                    };

                    itemPanel.Controls.Add(btnSend);
                    flow.Controls.Add(itemPanel);
                }

                panel1.Controls.Add(flow);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching/sending request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
