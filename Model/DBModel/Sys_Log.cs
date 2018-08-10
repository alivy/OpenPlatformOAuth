namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_Log
    {
        [Key]
        [StringLength(50)]
        public string F_Id { get; set; }

        public DateTime? F_Date { get; set; }

        [StringLength(50)]
        public string F_Account { get; set; }

        [StringLength(50)]
        public string F_NickName { get; set; }

        [StringLength(50)]
        public string F_Type { get; set; }

        [StringLength(50)]
        public string F_IPAddress { get; set; }

        [StringLength(50)]
        public string F_IPAddressName { get; set; }

        [StringLength(50)]
        public string F_ModuleId { get; set; }

        [StringLength(50)]
        public string F_ModuleName { get; set; }

        public bool? F_Result { get; set; }

        [StringLength(500)]
        public string F_Description { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
