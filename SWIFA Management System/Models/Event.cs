using System;
using System.Collections.Generic;

namespace SWIFA_Management_System.Models;

public partial class Event
{
    public int Id { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public string EventLocation { get; set; } = null!;
    public bool Current { get; set; }
}


public partial class Team
{
    public int TeamId { get; set; }

    public string School { get; set; }

    public string suffix { get; set; }

    public string Blade { get; set; }

    public string AFencer { get; set; }

    public string BFencer { get; set; }

    public string CFencer { get; set; }

    public string AltFencer { get; set; }

    public int EventId { get; set; }
}

public partial class School
{
    public int Id { get; set; }
    public string SchoolName { get; set; }
}