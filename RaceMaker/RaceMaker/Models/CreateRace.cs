using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceMaker.Models
{
    public class CreateRace
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Race Name")]
        public string RaceName { get; set; }

        [Required]
        [Display(Name = "Race Location")]
        public string RaceLocation { get; set; }

        [Required]
        [Display(Name = "Race Date and Time")]
        public DateTime RaceDate { get; set; }

        [Required]
        [Display(Name = "Race Distance")]
        public int RaceDistance { get; set;}

        [Required]
        [Display(Name = "Race Fee")]
        public double RaceCost { get; set; }

        [Display(Name = "Race Description")]
        public string RaceDescription { get; set; }
        [Required]
        [Display(Name = "Race Administrator Email")]
        public string AdminEmail { get; set; }
        [Required]
        [Display(Name = "Race Administrator Password")]
        public string AdminPassword { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

    }
}