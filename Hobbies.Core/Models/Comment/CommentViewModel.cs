using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Core.Models.Comment
{
    public class CommentViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; } = null!;
    }
}
