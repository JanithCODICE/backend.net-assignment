using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendAssignment.DataAccessLayer.Entities;

[PrimaryKey("Id", "StudentId", "SubjectId")]
public partial class StudentSubject
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Key]
    [Column("studentId")]
    public int StudentId { get; set; }

    [Key]
    [Column("subjectId")]
    public int SubjectId { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentSubjects")]
    public virtual Student Student { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("StudentSubjects")]
    public virtual Subject Subject { get; set; } = null!;
}
