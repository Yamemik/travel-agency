using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency;

[Table("contracts")]
public partial class Contract
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tour_id")]
    public int TourId { get; set; }

    [Column("date_of_conclusion", TypeName = "date")]
    public DateOnly DateOfConclusion { get; set; }

    [ForeignKey("TourId")]
    [InverseProperty("Contracts")]
    public virtual Tour Tour { get; set; } = null!;

    [ForeignKey("ContractId")]
    [InverseProperty("Contracts")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
