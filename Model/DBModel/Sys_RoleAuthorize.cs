namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_RoleAuthorize
    {
        [Key]
        [StringLength(50)]
        public string F_Id { get; set; }

        public int? F_ItemType { get; set; }

        [StringLength(50)]
        public string F_ItemId { get; set; }

        public int? F_ObjectType { get; set; }

        [StringLength(50)]
        public string F_ObjectId { get; set; }

        public int? F_SortCode { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
