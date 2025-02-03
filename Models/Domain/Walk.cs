﻿using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models.Domain
{
    public class Walk
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }


        // Navagation Properties
        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }





    }
}
