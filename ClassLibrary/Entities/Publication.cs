using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryInfinity.Entities;

public partial class Publication
{
    public int IdPublication { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Nickname { get; set; } = null!;
    [ForeignKey(nameof(Nickname))]
    public virtual ICollection<Comment>? Comments { get; } = new List<Comment>();

    public virtual Userweb? NicknameNavigation { get; set; } = null!;
}
