namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class ExternalIdentity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public ExternalIdentity(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}