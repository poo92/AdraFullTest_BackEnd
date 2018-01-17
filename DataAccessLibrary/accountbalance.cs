namespace DataAccessLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("accountbalance")]
    public partial class accountbalance
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int year { get; set; }

        [Key]
        [Column(Order = 1)]
        public int month { get; set; }

        public double? rnd { get; set; }

        public double? canteen { get; set; }

        public double? ceocar { get; set; }

        public double? marketing { get; set; }

        public double? parking { get; set; }

        public int? uid { get; set; }
    }
}
