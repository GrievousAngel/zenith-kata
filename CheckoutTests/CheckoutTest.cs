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
    
    [Theory]
    [MemberData(nameof(BasketItems))]
    public void Scan_Sku_GetTotalPrice(List<string> skus, decimal expectedTotal)
    {
        var pricingRules = new PricingRules(
            new Dictionary<string, Price>
                    {
                        { "A", new Price(50) },
                        { "B", new Price(30) },
                        { "C", new Price(20) },
                        { "D", new Price(15) }
                    });

        var checkout = new Checkout(pricingRules, nullLogger);

        foreach (var sku in skus)
        {
            checkout.Scan(sku);
        }
        
        checkout.GetTotalPrice().ShouldBe(expectedTotal);
    }

    public static IEnumerable<object[]> BasketItems 
        => new[]
         {
            new object[] { new List<string> {"A"}, 50 },
            new object[] { new List<string> {"A", "A"}, 100 },
            new object[] { new List<string> {"A", "B", "C", "D"}, 115 },
         };

}