using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("surname", TypeName = "varchar (255)")]
    public string Surname { get; set; } = null!;

    [Column("name", TypeName = "varchar (255)")]
    public string Name { get; set; } = null!;

    [Column("patronymic", TypeName = "varchar (50)")]
    public string? Patronymic { get; set; }

    [Column("role", TypeName = "varchar (15)")]
    public string Role { get; set; } = null!;

    [Column("login", TypeName = "varchar (50)")]
    public string Login { get; set; } = null!;

    [Column("password", TypeName = "varchar (10)")]
    public string Password { get; set; } = null!;

}
