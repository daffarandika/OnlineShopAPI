using System;
using System.Collections.Generic;

namespace OnlineShopAPI.Models;

public partial class Discount
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime ExpiredAt { get; set; }

    public virtual ICollection<Headorder> Headorders { get; } = new List<Headorder>();
}
