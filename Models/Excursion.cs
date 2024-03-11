using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("excursions")]
public partial class Excursion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("location", TypeName = "varchar (255)")]
    public string Location { get; set; } = null!;

    [Column("company_price")]
    public int CompanyPrice { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Excursions")]
    public virtual Country Country { get; set; } = null!;
}
