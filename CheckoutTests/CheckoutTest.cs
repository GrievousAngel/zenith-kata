using CheckoutKata;
using Shouldly;

namespace CheckoutTests;

public class CheckoutTest
{
    [Fact]
    public void Scan_NoSkuId_Exception()
    {
        var checkout = new Checkout(new PricingRules(new Dictionary<string, Price>()));

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

        var checkout = new Checkout(pricingRules);

        Should.Throw<ApplicationException>(
            () =>
            {
                checkout.Scan("B");
            });
    }
}