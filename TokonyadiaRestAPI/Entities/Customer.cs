using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TokonyadiaRestAPI.Entities;

[Table(name: "m_customer")]
public class Customer
{
    [Key, Column(name: "id")] public Guid Id { get; set; }

    [Column(name: "customer_name", TypeName = "NVarchar(48)")]
    public string? CustomerName { get; set; }

    [Column(name: "phone_number", TypeName = "NVarchar(14)")]
    public string? PhoneNumber { get; set; }

    [Column(name: "address", TypeName = "NVarchar(100)")]
    public string? Address { get; set; }
    public virtual UserCredential UserCredentials { get; set; }
   
}