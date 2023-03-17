using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryInfinity.Entities;

public partial class Comment
{
    public int IdComment { get; set; }

    public string Comment1 { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public DateTime Date { get; set; }

    public int FkIdPublication { get; set; }
    [ForeignKey(nameof(FkIdPublication))]
    public virtual Publication? FkIdPublicationNavigation { get; set; } = null!;
}
