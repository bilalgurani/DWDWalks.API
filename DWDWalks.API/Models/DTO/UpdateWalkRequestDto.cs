using System.ComponentModel.DataAnnotations;
using DWDWalks.API.Models.Domain;

namespace DWDWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Description has to be minimum of 3 character")]
        [MaxLength(500, ErrorMessage = "Description has to be maximum of 500 character")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50, ErrorMessage = "LengthInKm must be between 0 and 50")]
        public double LengthInKm { get; set; }
        [Required]
        public string? WalkImgUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
