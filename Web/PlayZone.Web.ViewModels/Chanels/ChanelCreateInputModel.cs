﻿namespace PlayZone.Web.ViewModels.Chanels
{
    using System.ComponentModel.DataAnnotations;

    public class ChanelCreateInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is Required!")]
        [MaxLength(60, ErrorMessage = "Length cannot be longer than 60 characters!")]
        [MinLength(4, ErrorMessage = "Length cannot be shorter than 4 characters!")]
        public string Title { get; set; }

        [MaxLength(6000, ErrorMessage = "Description can't be more than 6000 characters!)")]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
