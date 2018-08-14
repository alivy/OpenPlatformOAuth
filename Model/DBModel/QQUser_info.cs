namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QQUser_info
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string OpenId { get; set; }

        [StringLength(50)]
        public string Nickname { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string Province { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Year { get; set; }

        [StringLength(1024)]
        public string Figureurl { get; set; }

        [StringLength(1024)]
        public string Figureurl_2 { get; set; }

        [StringLength(1024)]
        public string Figureurl_qq_2 { get; set; }
    }
}
