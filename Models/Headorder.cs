using System;
using System.Collections.Generic;

namespace OnlineShopAPI.Models;

public partial class Headorder
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? DiscountId { get; set; }

    public DateTime Date { get; set; }

    public string Payment { get; set; } = null!;

    public string? Bank { get; set; }

    public string? CardNumber { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Detailorder> Detailorders { get; } = new List<Detailorder>();

    public virtual Discount? Discount { get; set; }

    public virtual User User { get; set; } = null!;
}
