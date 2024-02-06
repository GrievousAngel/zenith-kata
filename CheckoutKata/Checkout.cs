using Microsoft.Extensions.Logging;

namespace CheckoutKata;

public sealed class Checkout: ICheckout
{
    private readonly PricingRules pricingRules;
    private readonly ILogger<Checkout> logger;

    public Checkout(PricingRules pricingRules, ILogger<Checkout> logger)
    {
        this.pricingRules = pricingRules;
        this.logger = logger;
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
        
        // TODO: Add to basket
        
        throw new NotImplementedException();
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }
}