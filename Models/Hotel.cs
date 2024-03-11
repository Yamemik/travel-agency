using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("hotels")]
public partial class Hotel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar (255)")]
    public string Name { get; set; } = null!;

    [Column("company_price")]
    public int CompanyPrice { get; set; }

    [Column("customer_price")]
    public int CustomerPrice { get; set; }

    [InverseProperty("Hotel")]
    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
