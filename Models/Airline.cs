using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("airlines")]
public partial class Airline
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar (255)")]
    public string Name { get; set; } = null!;

    [InverseProperty("Airline")]
    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
