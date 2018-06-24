using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaceMaker.Models
{
    public class RaceCoursePoint
    {
        [Key]
        public int ID { get; set; }
        public int PointNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public int CreateRaceID { get; set; }
        [ForeignKey("CreateRaceID")]
        public CreateRace CreateRace { get; set; }

    }
}