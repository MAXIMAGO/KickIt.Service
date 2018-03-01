using System;
using System.Collections.Generic;
using System.Text;

namespace MAXIMAGO.KickIt.Games
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Game
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public Team Home { get; set; }

        public Team Guest { get; set; }

        public IEnumerable<Set> Sets { get; set; }
    }
}
