using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kayitsistemi2.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string Content { get; set; }

        //Creator
        public string IdentityCreatorId { get; set; }

        //Owner
        public string IdentityUserId { get; set; }

        public bool TaskStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
