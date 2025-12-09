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
        // Tùy chọn: bạn có thể thiết lập uid của người dùng hiện tại ở đây khi tạo FriendsForm
        // ví dụ: new FriendsForm(currentUid)
        private readonly string? _currentUid;

        public FriendsForm(string? currentUid = null)
        {
            InitializeComponent();

            _currentUid = currentUid;

            // Gắn sự kiện UI
            Load += FriendsForm_Load;
            Btn_ExitFriends.Click += (_, __) => Close();
            Btn_DsBanBe.Click += async (_, __) => await ShowFriendsAsync();
            Btn_LoiMoiKB.Click += async (_, __) => await ShowRequestsAsync();
            Btn_timFriend.Click += async (_, __) => await SearchAndSendRequestAsync();
        }

        private async void FriendsForm_Load(object? sender, EventArgs e)
        {
            // Hiển thị các control khi form load
            Btn_ExitFriends.Visible = true;
            Btn_DsBanBe.Visible = true;
            Btn_LoiMoiKB.Visible = true;
            Btn_timFriend.Visible = true;

            // Nếu không có uid hiện tại, thử suy luận từ FirebaseProvider.AuthClient
            if (string.IsNullOrEmpty(_currentUid))
            {
                try
                {
                    // FirebaseAuthClient có thể tiết lộ id token/thông tin người dùng hiện tại tùy thuộc vào cách sử dụng.
                    // Nếu bạn lưu uid người dùng hiện tại ở nơi khác trong ứng dụng, hãy truyền nó qua constructor.
                }
                catch { /* Bỏ qua; ta vẫn làm việc được nhưng một số tính năng cần uid */ }
            }

            // Mặc định: hiển thị danh sách bạn bè nếu có thể
            await ShowFriendsAsync();
        }

        private FirebaseClient Database => FirebaseProvider.Instance.Database;

        /// <summary>
        /// Tải và hiển thị danh sách bạn bè của người dùng hiện tại.
        /// Cấu trúc DB dự kiến:
        ///    friends/{currentUid}/{friendUid} = true
        ///    users/{uid} => user model
        /// </summary>
        private async Task ShowFriendsAsync()
        {
            panel1.Controls.Clear();

            if (string.IsNullOrEmpty(_currentUid))
            {
                // Hiển thị một thông báo hữu ích trong panel để nhà phát triển biết cần truyền current uid
                var lbl = new Label
                {
                    Text = "Uid người dùng hiện tại chưa được cung cấp. Hãy thiết lập nó khi tạo FriendsForm để tải bạn bè.",
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
                    // đọc các key bạn bè
                    var snapshot = await Database
                        .Child("friends")
                        .Child(_currentUid)
                        .OnceAsync<object>();

                    if (snapshot != null)
                    {
                        foreach (var item in snapshot)
                        {
                            // item.Key là friendUid
                            if (!string.IsNullOrEmpty(item.Key))
                                friendUids.Add(item.Key);
                        }
                    }
                }, "tải danh sách bạn bè"); // load friend list

                var users = new List<UserModel>();
                foreach (var uid in friendUids)
                {
                    var u = await TryHelper.TryAsync(async () =>
                    {
                        var obj = await Database.Child("users").Child(uid).OnceSingleAsync<UserModel>();
                        return obj;
                    }, $"tải người dùng {uid}"); // load user {uid}

                    if (u != null)
                    {
                        users.Add(u);
                    }
                }

                listControl.SetUsers(users);

                // tùy chọn: double-click để mở hồ sơ / bắt đầu trò chuyện
                listControl.InnerListView.DoubleClick += (_, __) =>
                {
                    var selected = listControl.GetSelectedUser();
                    if (selected != null)
                    {
                        MessageBox.Show($"Bạn bè được chọn: {selected.UserName} ({selected.FullName})", "Chọn bạn bè",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải bạn bè: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải các lời mời kết bạn đến và hiển thị các nút Chấp nhận/Từ chối cho mỗi lời mời.
        /// Cấu trúc DB dự kiến:
        ///    friendRequests/{targetUid}/{fromUid} = true
        /// </summary>
        private async Task ShowRequestsAsync()
        {
            panel1.Controls.Clear();

            if (string.IsNullOrEmpty(_currentUid))
            {
                var lbl = new Label
                {
                    Text = "Uid người dùng hiện tại chưa được cung cấp. Hãy thiết lập nó khi tạo FriendsForm để quản lý lời mời.",
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
                }, "tải lời mời kết bạn"); // load friend requests

                if (requestFromUids.Count == 0)
                {
                    var lbl = new Label
                    {
                        Text = "Không có lời mời kết bạn nào đến.",
                        AutoSize = false,
                        Size = panel1.Size,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel1.Controls.Add(lbl);
                    return;
                }

                // tạo một flow panel dọc để hiển thị từng lời mời với các nút
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
                    }, $"tải người dùng {fromUid}"); // load user {fromUid}

                    var itemPanel = new Panel
                    {
                        Width = Math.Max(300, panel1.Width - 40),
                        Height = 60,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(4)
                    };

                    var lbl = new Label
                    {
                        Text = user != null ? $"{user.UserName} — {user.FullName}" : $"(người dùng không rõ: {fromUid})", // (unknown user: {fromUid})
                        Location = new Point(6, 8),
                        AutoSize = false,
                        Width = itemPanel.Width - 160,
                        Height = 44
                    };
                    itemPanel.Controls.Add(lbl);

                    var btnAccept = new Button
                    {
                        Text = "Chấp nhận", // Accept
                        Location = new Point(itemPanel.Width - 140, 20),
                        Size = new Size(60, 32)
                    };
                    var btnDecline = new Button
                    {
                        Text = "Từ chối", // Decline
                        Location = new Point(itemPanel.Width - 72, 20),
                        Size = new Size(60, 32)
                    };

                    // bắt fromUid cho các trình xử lý
                    btnAccept.Click += async (_, __) =>
                    {
                        btnAccept.Enabled = false;
                        btnDecline.Enabled = false;
                        await AcceptRequestAsync(fromUid);
                        // làm mới chế độ xem lời mời
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
                MessageBox.Show($"Lỗi khi tải lời mời: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AcceptRequestAsync(string fromUid)
        {
            if (string.IsNullOrEmpty(_currentUid)) return;

            try
            {
                await TryHelper.TryAsync(async () =>
                {
                    // thiết lập tình bạn hai chiều
                    await Database.Child("friends").Child(_currentUid).Child(fromUid).PutAsync(true);
                    await Database.Child("friends").Child(fromUid).Child(_currentUid).PutAsync(true);

                    // xóa lời mời
                    await Database.Child("friendRequests").Child(_currentUid).Child(fromUid).DeleteAsync();
                }, $"chấp nhận lời mời kết bạn từ {fromUid}"); // accept friend request from {fromUid}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chấp nhận lời mời: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                }, $"từ chối lời mời kết bạn từ {fromUid}"); // decline friend request from {fromUid}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi từ chối lời mời: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm kiếm người dùng bằng tên người dùng (textBox1) và cho phép gửi lời mời kết bạn từ người dùng hiện tại đến người dùng đó.
        /// Panel bên phải sẽ hiển thị các hàng kết quả với nút "Gửi lời mời" cho mỗi kết quả phù hợp.
        /// </summary>
        private async Task SearchAndSendRequestAsync()
        {
            var query = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng để tìm kiếm.", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information); // Please enter a username to search.
                return;
            }

            if (string.IsNullOrEmpty(_currentUid))
            {
                MessageBox.Show("Uid người dùng hiện tại chưa được cung cấp. Không thể gửi lời mời.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); // Current user uid not provided. Cannot send request.
                return;
            }

            panel1.Controls.Clear();

            try
            {
                List<FirebaseObject<UserModel>> matches = null!;
                await TryHelper.TryAsync(async () =>
                {
                    // Truy vấn người dùng theo tên người dùng. Client realtime không hỗ trợ truy vấn phức tạp dễ dàng,
                    // nên ta tải tất cả người dùng và lọc cục bộ (ổn đối với kích thước vừa phải).
                    var all = await Database.Child("users").OnceAsync<UserModel>();
                    matches = all?.Where(x => !string.IsNullOrEmpty(x.Object?.UserName)
                                                     && x.Object.UserName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                                     .ToList();
                }, $"tìm kiếm người dùng {query}"); // search user {query}

                if (matches == null || matches.Count == 0)
                {
                    var lbl = new Label
                    {
                        Text = $"Không tìm thấy người dùng nào có tên chứa '{query}'.", // No user found with username containing '{query}'.
                        AutoSize = false,
                        Size = panel1.Size,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel1.Controls.Add(lbl);
                    return;
                }

                // tạo một flow panel dọc để hiển thị mỗi kết quả phù hợp với nút Gửi lời mời
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
                        Text = targetUser != null ? $"{targetUser.UserName} — {targetUser.FullName}" : $"(người dùng không rõ: {targetUid})", // (unknown user: {targetUid})
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

                    // tránh gửi lời mời cho chính mình
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
                                // Tạo node lời mời friendRequests/{targetUid}/{fromUid} = true
                                await Database.Child("friendRequests").Child(targetUid).Child(_currentUid).PutAsync(true);
                            }, $"gửi lời mời kết bạn tới {targetUid}"); // send friend request to {targetUid}

                            MessageBox.Show($"Đã gửi lời mời tới {targetUser?.UserName ?? targetUid}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information); // Success

                            // Tùy chọn thay đổi nút để báo đã gửi
                            btnSend.Text = "Đã gửi";
                            btnSend.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi gửi lời mời: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Lỗi khi tìm kiếm/gửi lời mời: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}