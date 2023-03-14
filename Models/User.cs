using System;
using System.Collections.Generic;

namespace OnlineShopAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Headorder> Headorders { get; } = new List<Headorder>();
}
