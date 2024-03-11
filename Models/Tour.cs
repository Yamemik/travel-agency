using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

[Table("tours")]
public partial class Tour
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("hotel_id")]
    public int HotelId { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("departure_flight_id")]
    public int DepartureFlightId { get; set; }

    [Column("arrival_flight_id")]
    public int ArrivalFlightId { get; set; }

    [Column("start_date", TypeName = "date")]
    public DateOnly StartDate { get; set; }

    [Column("finish_date", TypeName = "date")]
    public DateOnly FinishDate { get; set; }

    [Column("company_service_cost")]
    public int CompanyServiceCost { get; set; }

    [Column("number_of_free_seats")]
    public int NumberOfFreeSeats { get; set; }

    [ForeignKey("ArrivalFlightId")]
    [InverseProperty("TourArrivalFlights")]
    public virtual Flight ArrivalFlight { get; set; } = null!;

    [InverseProperty("Tour")]
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    [ForeignKey("CountryId")]
    [InverseProperty("Tours")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("DepartureFlightId")]
    [InverseProperty("TourDepartureFlights")]
    public virtual Flight DepartureFlight { get; set; } = null!;

    [ForeignKey("HotelId")]
    [InverseProperty("Tours")]
    public virtual Hotel Hotel { get; set; } = null!;
}
