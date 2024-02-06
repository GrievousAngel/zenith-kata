using Microsoft.Extensions.Logging;

namespace CheckoutKata;

public sealed class Checkout: ICheckout
{
    private readonly PricingRules pricingRules;
    private readonly ILogger<Checkout> logger;
    public Dictionary<string, int> basket { get; set; }

    public Checkout(PricingRules pricingRules, ILogger<Checkout> logger)
    {
        this.pricingRules = pricingRules;
        this.logger = logger;
        basket = new Dictionary<string, int>();
    }
    
    public void Scan(string skuId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(skuId, nameof(skuId));

        var skuPrice = pricingRules.GetPrice(skuId);

        if (skuPrice == null)
        {
            logger.LogWarning("{SkuId} not found in pricingRules", skuId);
            throw new ApplicationException($"{skuId} is not a recognised item in the PriceList");
        }
        
        if (!basket.TryAdd(skuId, 1))
        {
            // Increase the number of these items in the basket fot this sku
            basket[skuId]++;
        }
    }

    public decimal GetTotalPrice()
    {
        decimal total = 0;
        foreach (var item in basket)
        {
            var skuPrice = pricingRules.GetPrice(item.Key);

            if (skuPrice.MultiPrice == null)
            {
                total += skuPrice!.UnitPrice * item.Value;
                continue;
            }
            
            total += CalculateMultiPrice(skuPrice, item.Value);
            
        }

        return total;
    }

    private static decimal CalculateMultiPrice(Price price, int noOfSkus)
    {
        var multiPriceSize = price.MultiPrice!.Size;
        var multiPriceCount = noOfSkus / multiPriceSize;
        var remainingSkus = noOfSkus % multiPriceSize;
        return (price.MultiPrice.Price * multiPriceCount) + (price.UnitPrice * remainingSkus);
    }
    
}