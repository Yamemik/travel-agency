using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Keyless]
[Table("visited_excursions")]
public partial class VisitedExcursion
{
    [Column("contract_id")]
    public int ContractId { get; set; }

    [Column("excursion_id")]
    public int ExcursionId { get; set; }

    [Column("customer_price")]
    public int CustomerPrice { get; set; }

    [Column("date_of_visit", TypeName = "date")]
    public DateOnly DateOfVisit { get; set; }

    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; } = null!;

    [ForeignKey("ExcursionId")]
    public virtual Excursion Excursion { get; set; } = null!;
}
