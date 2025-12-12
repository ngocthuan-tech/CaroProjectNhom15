// Player.cs

using System;
using System.Collections.Generic;
using System.Drawing; // Cần thiết để sử dụng kiểu dữ liệu Image
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Đảm bảo namespace này khớp với project của bạn
namespace CaroProjectNhom15.Forms.Gameplay
{
    public class Player
    {
        #region Properties
        // private string name; // Đã loại bỏ thuộc tính Name

        // Tên người chơi
        // public string Name
        // {
        //     get { return name; }
        //     set { name = value; }
        // }

        private Image mark;

        // Hình ảnh quân cờ (X hoặc O)
        public Image Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        #endregion

        // Constructor mới (chỉ cần Image mark)
        public Player(Image mark)
        {
            this.Mark = mark;
        }
    }
}