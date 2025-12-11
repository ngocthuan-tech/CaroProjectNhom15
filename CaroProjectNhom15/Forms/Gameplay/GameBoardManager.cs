using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CaroProjectNhom15.Forms.Gameplay
{
    public class GameBoardManager
    {
        #region Properties
        private Panel board;
        public Panel Board { get => Board; set => Board=value; }
        #endregion

        #region Initialize
        public GameBoardManager(Panel board)
        {
            this.Board = board;
        }
        #endregion

        #region Methods
        public void DrawBoard()
        {
            Button oldButton = new Button() { Width = 0, Location = new Point(0,0) };
            for (int i = 0; i < GameCons.CHESS_BOARD_SIZE; i++)
            {
                for (int j = 0; i < GameCons.CHESS_BOARD_SIZE; j++)
                {
                    Button Btn = new Button()
                    {
                        Width = GameCons.CHESS_WIDTH,
                        Height = GameCons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + GameCons.CHESS_WIDTH, oldButton.Location.Y)
                    };
                    Board.Controls.Add(Btn);
                    oldButton = Btn;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + GameCons.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }
        #endregion
    }
}
