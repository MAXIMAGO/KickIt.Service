using MAXIMAGO.KickIt.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAXIMAGO.KickIt.Games
{
    public sealed class Team
    {
        public long Id { get; set; }

        public ICollection<PlayerTeam> PlayerTeams { get; set; }
    }
}
