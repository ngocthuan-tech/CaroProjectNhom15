using System.Collections.Generic;

namespace CaroProjectNhom15.Models
{
    public class GameRoomModel
    {
        public List<List<string>> Board { get; set; } = new List<List<string>>()
        {
            new List<string> { "", "", "" },
            new List<string> { "", "", "" },
            new List<string> { "", "", "" }
        };

        public string CurrentTurn { get; set; } = "X";
        public string Winner { get; set; } = null;
        public string ID { get; set; }
    }
}