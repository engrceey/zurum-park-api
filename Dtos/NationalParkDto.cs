using System;
using System.ComponentModel.DataAnnotations;

namespace ZurumPark.Dtos
{
    public class NationalParkDto
    { 
        public int Id { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Established { get; set; }

    }
}