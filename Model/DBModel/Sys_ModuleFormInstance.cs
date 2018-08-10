namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_ModuleFormInstance
    {
        [Key]
        [StringLength(50)]
        public string F_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string F_FormId { get; set; }

        [StringLength(50)]
        public string F_ObjectId { get; set; }

        public string F_InstanceJson { get; set; }

        public int? F_SortCode { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
