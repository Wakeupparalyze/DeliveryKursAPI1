using System;
using System.Collections.Generic;

namespace ConsoleApp1;

public partial class Dish
{
    public int Id { get; set; }

    public string Cost { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
