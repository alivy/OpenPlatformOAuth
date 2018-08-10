namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_Module
    {
        [Key]
        [StringLength(50)]
        public string F_Id { get; set; }

        [StringLength(50)]
        public string F_ParentId { get; set; }

        public int? F_Layers { get; set; }

        [StringLength(50)]
        public string F_EnCode { get; set; }

        [StringLength(50)]
        public string F_FullName { get; set; }

        [StringLength(50)]
        public string F_Icon { get; set; }

        [StringLength(500)]
        public string F_UrlAddress { get; set; }

        [StringLength(50)]
        public string F_Target { get; set; }

        public bool? F_IsMenu { get; set; }

        public bool? F_IsExpand { get; set; }

        public bool? F_IsPublic { get; set; }

        public bool? F_AllowEdit { get; set; }

        public bool? F_AllowDelete { get; set; }

        public int? F_SortCode { get; set; }

        public bool? F_DeleteMark { get; set; }

        public bool? F_EnabledMark { get; set; }

        [StringLength(500)]
        public string F_Description { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        [StringLength(50)]
        public string F_CreatorUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }

        [StringLength(50)]
        public string F_LastModifyUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }

        [StringLength(50)]
        public string F_DeleteUserId { get; set; }
    }
}
