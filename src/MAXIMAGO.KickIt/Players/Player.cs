using MAXIMAGO.KickIt.Games;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MAXIMAGO.KickIt.Players
{
    public class Player
    {
        public long Id { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
    }
}
