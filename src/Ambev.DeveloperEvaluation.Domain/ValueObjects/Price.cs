namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Price
{
    public decimal Value { get; private set; }

    public Price(decimal value)
    {
        if (value < 0) 
            throw new ArgumentException("The price cannot be negative.");

        Value = value;
    }
}