using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("customers")]
public partial class Customer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("surname", TypeName = "varchar (255)")]
    public string Surname { get; set; } = null!;

    [Column("name", TypeName = "varchar (255)")]
    public string Name { get; set; } = null!;

    [Column("patronymic", TypeName = "varchar (255)")]
    public string? Patronymic { get; set; }

    [Column("gender", TypeName = "varchar (35)")]
    public string Gender { get; set; } = null!;

    [Column("passport_number", TypeName = "varchar (35)")]
    public string PassportNumber { get; set; } = null!;

    [Column("birth_date", TypeName = "date")]
    public DateOnly BirthDate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Customers")]
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
