using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("students")]
public partial class Student
{
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [NotMapped]
    public int Id { get; set; }

    [StringLength(11)]
    public string? TeL { get; set; }

    [StringLength(30)]
    public string? Email { get; set; }

    public int? TeacherId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("Students")]
    public virtual Teacher? Teacher { get; set; }
}
