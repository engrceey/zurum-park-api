using System;
using System.ComponentModel.DataAnnotations;

namespace ZurumPark.Entities
{
    public class NationalPark
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Pictures { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Established { get; set; }
    }
}