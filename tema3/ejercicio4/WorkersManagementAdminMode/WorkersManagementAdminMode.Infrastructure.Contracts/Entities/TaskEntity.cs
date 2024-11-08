﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkersManagementAdminMode.Infrastructure.Contracts.Entities
{
    public partial class TaskEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string Technology { get; set; }
        [Required]
        [StringLength(10)]
        public string Status { get; set; }
        public int? idWorker { get; set; }

        [ForeignKey("idWorker")]
        [InverseProperty("TasksTable")]
        public virtual ITWorkerEntity idWorkerNavigation { get; set; }
    }
}