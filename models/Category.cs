using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPDP_Stefan.models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Numele nu poate fi mai lung de 50 caractere")]
        public string Name { get; set; }

        public Category Parent_category { get; set; }
     
    }
}
