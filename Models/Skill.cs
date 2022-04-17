using System;
using System.ComponentModel.DataAnnotations;


namespace DevPath.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Skill title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        //[StringLength(250, ErrorMessage = "Developer property cannot be longer than 250 characters.")]
        //public string Developer { get; set; }
        //public int? CompanyID { get; set; }
        //public virtual Company Company { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]

        public DateTime? ReleaseDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Added")]

        public DateTime? DateAdded { get; set; } = DateTime.Now;
        [DataType(DataType.Url)]
        public string Repository { get; set; }
        [DataType(DataType.Url)]
        public string Documentation { get; set; }
        //public virtual ICollection<Course> Courses { get; set; }
        //public virtual ICollection<DocCompletion> DocCompletions { get; set; }
        //public virtual ICollection<Flashcard> Flashcards { get; set; }
    }
}