namespace EFTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sys_User_Auths
    {
        public int ID { get; set; }

        public int USERID { get; set; }

        [Required]
        [StringLength(50)]
        public string IDENTITYTYPE { get; set; }

        [Required]
        [StringLength(50)]
        public string IDENTIFIER { get; set; }

        [Required]
        [StringLength(50)]
        public string CREDENTIAL { get; set; }

        public int IFVERIFIED { get; set; }
    }
}
