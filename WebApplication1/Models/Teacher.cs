using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Teacher")]
public partial class Teacher
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    public int? Age { get; set; }

    [InverseProperty("Teacher")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
