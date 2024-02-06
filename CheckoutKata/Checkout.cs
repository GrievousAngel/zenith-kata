namespace CheckoutKata;

public sealed class Checkout: ICheckout
{
    private readonly PricingRules pricingRules;

    public Checkout(PricingRules pricingRules)
    {
        this.pricingRules = pricingRules;
    }
    
    public void Scan(string skuId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(skuId, nameof(skuId));

        var skuPrice = pricingRules.GetPrice(skuId);

        if (skuPrice == null)
        {
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