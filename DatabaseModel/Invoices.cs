namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoices
    {
        [Key]
        public int IdInvoice { get; set; }

        public int IdRecord { get; set; }

        public int? Total { get; set; }

        public virtual Records Records { get; set; }
    }
}
