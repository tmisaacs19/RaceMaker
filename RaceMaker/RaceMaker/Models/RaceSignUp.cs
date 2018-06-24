using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceMaker.Models
{
    public class RaceSignUp
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "T-Shirt Size")]
        public string TshirtSize { get; set; }

        [Required]
        [Display(Name = "Participant Email")]
        public string Email { get; set; }

        public int RaceID { get; set; }


        [ForeignKey("RaceSignUpID")]
        public virtual ICollection<CreateRace> CreateRaces { get; set; } = new HashSet<CreateRace>();




    }
}