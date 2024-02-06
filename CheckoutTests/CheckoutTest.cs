using CheckoutKata;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;

namespace CheckoutTests;

public class CheckoutTest
{
    private readonly NullLogger<Checkout> nullLogger = NullLogger<Checkout>.Instance;
    
    [Fact]
    public void Scan_NoSkuId_Exception()
    {
        var checkout = new Checkout(new PricingRules(new Dictionary<string, Price>()), nullLogger);

        Should.Throw<ArgumentException>(
            () =>
            {
                checkout.Scan("");
            });
    }

    [Fact]
    public void Scan_SkuIdMissing_Exception()
    {
        var pricingRules = new PricingRules(new Dictionary<string, Price> { { "A", new Price(50) } });

        var checkout = new Checkout(pricingRules, nullLogger);

        Should.Throw<ApplicationException>(
            () =>
            {
                checkout.Scan("B");
            });
    }
}