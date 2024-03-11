using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("countries")]
public partial class Country
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar (255)")]
    public string Name { get; set; } = null!;

    [InverseProperty("Country")]
    public virtual ICollection<Excursion> Excursions { get; set; } = new List<Excursion>();

    [InverseProperty("DepartureCountry")]
    public virtual ICollection<Flight> FlightDepartureCountries { get; set; } = new List<Flight>();

    [InverseProperty("DestinationCountry")]
    public virtual ICollection<Flight> FlightDestinationCountries { get; set; } = new List<Flight>();

    [InverseProperty("Country")]
    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
