namespace DataAccessLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [Key]
        public int uid { get; set; }

        [StringLength(200)]
        public string username { get; set; }

        [StringLength(200)]
        public string password { get; set; }

        public int? userType { get; set; }

        [StringLength(200)]
        public string fname { get; set; }

        [StringLength(200)]
        public string lname { get; set; }

        [StringLength(200)]
        public string salt { get; set; }
    }
}
