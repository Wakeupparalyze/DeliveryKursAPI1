using System;
using System.Collections.Generic;

namespace ConsoleApp1;

public partial class Order
{
    public int Id { get; set; }

    public int DishId { get; set; }

    public string Total { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public int UserId { get; set; }

    public int CourierId { get; set; }

    public string Status { get; set; } = null!;

    public string Number { get; set; } = null!;

    public virtual Courier Courier { get; set; } = null!;

    public virtual Dish Dish { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
