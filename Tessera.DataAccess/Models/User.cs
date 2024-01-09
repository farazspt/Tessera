using System;
using System.Collections.Generic;

namespace Tessera.DataAccess.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual Employee IdNavigation { get; set; } = null!;
}
