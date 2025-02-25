using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Helpers;

public class ExternalIdentityBuilder
{
    private Guid _id;
    private string _name;

    public ExternalIdentityBuilder()
    {
        _id = Guid.NewGuid();
        _name = "Identity";
    }

    public static ExternalIdentityBuilder AnExternalIdentity() => new();

    public ExternalIdentity Build() => new(_id, _name);
}