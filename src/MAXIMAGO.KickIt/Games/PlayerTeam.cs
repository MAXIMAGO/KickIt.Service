using MAXIMAGO.KickIt.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAXIMAGO.KickIt.Games
{
    public class PlayerTeam
    {
        public long PlayerId { get; set; }
        public Player Player { get; set; }

        public long TeamId { get; set; }
        public Team Team { get; set; }
    }
}
