namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catalog")]
    public partial class Catalog
    {
        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        public int Price { get; set; }
    }
}
