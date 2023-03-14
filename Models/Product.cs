using System;
using System.Collections.Generic;

namespace OnlineShopAPI.Models;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public int Stock { get; set; }

    public DateTime DateAdded { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Detailorder> Detailorders { get; } = new List<Detailorder>();
}
