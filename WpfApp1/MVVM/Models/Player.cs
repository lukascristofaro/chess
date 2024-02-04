using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.MVVM.Models
{
    internal class Player
    {
        private int currentPlayer = 1; // 1 for white, 2 for black

        public int GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public void SwitchTurn()
        {
            currentPlayer = (currentPlayer == 1) ? 2 : 1;
        }
    }
}
