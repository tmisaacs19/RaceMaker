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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Race Date")]
        public DateTime RaceDate { get; set; }

        [Required]
        [Display(Name = "Race Distance")]
        public int RaceDistance { get; set; }

        [Required]
        [Display(Name = "Race Fee")]
        public double RaceCost { get; set; }

        [Display(Name = "Race Description")]
        public string RaceDescription { get; set; }
        [Required]
        [Display(Name = "Race Administrator Email")]
        public string AdminEmail { get; set; }

        [Display(Name = "Race Administrator Password")]
        public string AdminPassword { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        [Display(Name = "Additional Information")]
        public string AdditionalInformation { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public double CenterLat { get; set; }
        public double CenterLong { get; set; }
        public double CourseLat { get; set; }
        public double CourseLng { get; set; }
        public double OriginLat { get; set; }
        public double OriginLng { get; set; }
        public double DestinationLat { get; set; }
        public double DestinationLng { get; set; }

        [ForeignKey("CreateRaceID")]
        public virtual ICollection<RaceSignUp> RaceSignUps { get; set; } = new HashSet<RaceSignUp>();

        public List<RaceCoursePoint> RaceCoursePoints { get; set; }

        //[ForeignKey("RacePointNumber")]
        /*public virtual ICollection<RaceCoursePoint> RaceCoursePoint { get; set; }*/ //= new HashSet<RaceCoursePoint>();

        

    }
}