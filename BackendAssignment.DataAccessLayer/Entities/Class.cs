using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendAssignment.DataAccessLayer.Entities;

public partial class Class
{
    [Key]
    public int Id { get; set; }

    public string ClassName { get; set; } = null!;

    public string Grade { get; set; } = null!;

    public string TeacherInCharge { get; set; } = null!;

    [InverseProperty("Class")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
