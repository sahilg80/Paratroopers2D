using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class PlayerModel
    {
        public bool IsGameStarted { get; private set; }
        public int PlayerScore { get; private set; }

        public void SetScore(int scoreToAdd) => PlayerScore = PlayerScore + scoreToAdd;

        public void SetGamePlayStarted(bool value) => IsGameStarted = value;
    }
}
