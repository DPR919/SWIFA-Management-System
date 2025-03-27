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

    public string? AltFencer { get; set; }

    public int EventId { get; set; }

    public int? PoolId { get; set; }

    public int? SeedinPool { get; set; }

    public override string ToString()
    {
        return School + " " + suffix;
    }
}

public partial class School
{
    public int Id { get; set; }
    public string SchoolName { get; set; }
}

public partial class Pool
{
    public int PoolId { get; set; }
    public string Blade { get; set; }
    public int PoolNum { get; set; }
    public int EventId { get; set; }
}

public partial class Match
{
    public int MatchId { get; set; }
    public int TeamLeftId { get; set; }
    public int TeamRightId { get; set; }
    public string FencerLeft { get; set; }

    public string FencerLeftStrip { get; set; }
    public string FencerRight { get; set; }
    public string FencerRightStrip { get; set; }
    public string ScoreLeft { get; set; }
    public string ScoreRight { get; set; }
    public bool PoolMatch { get; set; }
    public bool DEMatch { get; set; }
    public int? PoolId { get; set; }
    public int? DEId { get; set; }
}