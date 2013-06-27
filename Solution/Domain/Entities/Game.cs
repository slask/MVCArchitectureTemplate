﻿using System;

namespace Domain.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public DateTime? PlayDate { get; set; }
        public string Location { get; set; }
        
        public int Player1Score { get; set; }
        public virtual ScrabblePlayer Player1 { get; set; }

        public int Player2Score { get; set; }
        public virtual ScrabblePlayer Player2 { get; set; }

        //public bool IsWin
        //{
        //    get { return PlayerScore > OpponentScore; }
        //}
    }
}
