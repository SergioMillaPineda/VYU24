﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkersManagementAdminMode.Infrastructure.Contracts.Entities
{
    public partial class TeamEntity
    {
        public TeamEntity()
        {
            ITWorkersTable = new HashSet<ITWorkerEntity>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int idManager { get; set; }

        [ForeignKey("idManager")]
        [InverseProperty("TeamsTable")]
        public virtual ITWorkerEntity idManagerNavigation { get; set; }
        [InverseProperty("idTeamNavigation")]
        public virtual ICollection<ITWorkerEntity> ITWorkersTable { get; set; }
    }
}