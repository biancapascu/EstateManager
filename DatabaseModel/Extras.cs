namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Extras
    {
        [Key]
        public int IdExtra { get; set; }

        public int IdRecord { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public virtual Catalog Catalog { get; set; }

        public virtual Records Records { get; set; }
    }
}
