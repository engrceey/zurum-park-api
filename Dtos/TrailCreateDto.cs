using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZurumPark.Entities;
using static ZurumPark.Entities.Trail;

namespace ZurumPark.Dtos
{
    
    public class TrailCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
       
    }
}