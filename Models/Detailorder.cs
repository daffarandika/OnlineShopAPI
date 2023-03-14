using System;
using System.Collections.Generic;

namespace OnlineShopAPI.Models;

public partial class Detailorder
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Qty { get; set; }

    public virtual Headorder Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
