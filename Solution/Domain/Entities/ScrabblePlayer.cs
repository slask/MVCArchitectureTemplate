using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ScrabblePlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? JoinDate { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string Email { get; set; }
        public List<Game> MatchHistory { get; set; }

        //public double CalculateAverageScore()
        //{
        //    return MatchHistory.Average(game => game.PlayerScore);
        //}

        //public int CountNumberOfWins()
        //{
        //    return MatchHistory.Sum(game => game.IsWin ? 1 : 0);
        //}

        //public int CountNumberOfLoses()
        //{
        //    return MatchHistory.Sum(game => game.IsWin ? 0 : 1);
        //}

        //public Game GetHighScoreGame()
        //{
        //    int maximumScore = MatchHistory.Max(game => game.PlayerScore);
        //    return (from game in MatchHistory
        //            where game.PlayerScore == maximumScore
        //            select game).First();
        //}
    }
}