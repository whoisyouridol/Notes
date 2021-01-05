using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Notes.Areas.Identity.Data;

namespace Notes.Data
{
    public class Notes
    {
        [Key]
        public int NotesId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }


        [Column(TypeName = "nvarchar(500)")]
        public string Content { get; set; }


        //adding time fits automatically
        [Column(TypeName = "datetime")]
        public DateTime? TimeAdded { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string Author { get; set; }

        public string UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual ApplicationUser User { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string FilePath { get; set; }

    }
}
