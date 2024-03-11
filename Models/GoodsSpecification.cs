using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Keyless]
[Table("goods_specifications")]
public partial class GoodsSpecification
{
    [Column("contract_id")]
    public int ContractId { get; set; }

    [Column("flight_id")]
    public int FlightId { get; set; }

    [Column("shipping_price_for_company")]
    public int ShippingPriceForCompany { get; set; }

    [Column("shipping_price_for_customer")]
    public int ShippingPriceForCustomer { get; set; }

    [Column("weight")]
    public int Weight { get; set; }

    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; } = null!;

    [ForeignKey("FlightId")]
    public virtual Flight Flight { get; set; } = null!;
}
