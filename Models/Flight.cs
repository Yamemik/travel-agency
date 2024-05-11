using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("flights")]
public partial class Flight
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("airline_id")]
    public int AirlineId { get; set; }

    [Column("company_price")]
    public int CompanyPrice { get; set; }

    [Column("customer_price")]
    public int CustomerPrice { get; set; }

    [Column("departure_country_id")]
    public int DepartureCountryId { get; set; }

    [Column("destination_country_id")]
    public int DestinationCountryId { get; set; }

    [ForeignKey("AirlineId")]
    [InverseProperty("Flights")]
    public virtual Airline Airline { get; set; } = null!;

    [ForeignKey("DepartureCountryId")]
    [InverseProperty("FlightDepartureCountries")]
    public virtual Country DepartureCountry { get; set; } = null!;

    [ForeignKey("DestinationCountryId")]
    [InverseProperty("FlightDestinationCountries")]
    public virtual Country DestinationCountry { get; set; } = null!;

    [InverseProperty("ArrivalFlight")]
    public virtual ICollection<Tour> TourArrivalFlights { get; set; } = new List<Tour>();

    [InverseProperty("DepartureFlight")]
    public virtual ICollection<Tour> TourDepartureFlights { get; set; } = new List<Tour>();
}
