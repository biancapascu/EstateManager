namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History")]
    public partial class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdHistory { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }
    }
}
