using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPDP_Stefan.models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer ten 50 char")]
        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public DateTime ModifiedAt { get; set; }

        public Location Location { get; set; }

        public string InventoryNumber { get; set; }

        public DateTime CreationDate { get; set; }



    }
}
