﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotosWebApp.Models.Admin
{
    public class AllUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public Hobby Hobby { get; set; }

        public string? ProfileImage { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }

    public enum Hobby
    {
        Sports,
        Reading,
        Movies,
        coding,
        singing,
    }
}
