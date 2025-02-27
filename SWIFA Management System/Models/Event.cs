using System;
using System.Collections.Generic;

namespace SWIFA_Management_System.Models;

public partial class Event
{
    public int Id { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime EventDate { get; set; }
}
