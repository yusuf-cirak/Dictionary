using Dictionary.Domain.Entities.Common;

namespace Dictionary.Domain.Entities;

public class EntryCommentVote:BaseEntity
{
    public Guid UserId { get; set; }
    public Guid EntryCommentId { get; set; }
    public virtual EntryComment EntryComment { get; set; }
    public virtual User User { get; set; }
}