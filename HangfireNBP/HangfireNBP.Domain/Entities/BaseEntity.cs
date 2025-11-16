namespace HangfireNBP.Domain.Entities;
public abstract class BaseEntity
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}