﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class Technologies
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; } = "SYSTEM";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; } // Make this nullable

        public DateTime? UpdatedDate { get; set; } // Make this nullable

        [NotMapped] // Add this attribute to ignore in EF Core
        [JsonIgnore] // Add this attribute to ignore during JSON deserialization
        public Departments Departments { get; set; }
    }
    public class TechnologiesDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The DepartmentId field is required.")]
        public string DepartmentId { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

}
