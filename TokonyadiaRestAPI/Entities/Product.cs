using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TokonyadiaEF.Entities;

[Table(name: "m_product")]
public class Product
{
    [Key, Column(name: "id")] public Guid Id { get; set; }

    [Column(name: "product_name", TypeName = "NVarchar(50)")]
    public string ProductName { get; set; }

    [Column(name: "description", TypeName = "NVarchar(100)")]
    public string Description { get; set; }

    public virtual ICollection<ProductPrice> ProductPrices { get; set; }

}