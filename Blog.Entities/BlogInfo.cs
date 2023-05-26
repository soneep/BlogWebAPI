using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blog.Entities
{
    [Table("BlogInfo", Schema = "dbo")]
    public class BlogInfo
    {
        public BlogInfo() { 
            InsertedUtcDate = DateTime.UtcNow;
        }

        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", Order = 1)]
        public int ID { get; set; }

        [Column("BlogID", Order = 2)]        
        public Guid BlogID { get; set; }
        [Required]
        [Column("Title", Order = 3)]
        public string Title { get; set; }

        [Required]
        [Column("Content", Order = 4)]
        public string Content { get; set; }

        [Required]
        [Column("Category", Order = 5)]
        public string Category { get; set; }

        [Required]
        [Column("Author", Order = 6)]
        public string Author { get; set; }
        [JsonIgnore]
        public DateTime InsertedUtcDate { get; set; }

    }
}