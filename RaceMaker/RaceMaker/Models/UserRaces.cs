using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceMaker.Models
{
    public class UserRaces
    {
        [ForeignKey("CreateRace")]
        public int RaceID { get; set; }
        public CreateRace CreateRace { get; set; }


        [ForeignKey("RaceSignUp")]
        public int UserID { get; set; }
        public RaceSignUp RaceSignUp { get; set; }

        [Key]
        public int UserRaceID { get; set; }

    }
}