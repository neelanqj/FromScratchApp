using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FromScratchApp.Models
{
    public class NoteCreateViewModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(50)]
        [Display(Name = "The Title")]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        [DataType(DataType.MultilineText)]

        public string Description { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
