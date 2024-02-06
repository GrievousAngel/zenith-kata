namespace CheckoutKata;

public class PricingRules(Dictionary<string, Price> PriceList)
{
    private Dictionary<string, Price> PriceList { get; } = PriceList;

    public Price? GetPrice(string skuId)
    {
        return PriceList.ContainsKey(skuId) 
            ? PriceList[skuId] 
            : null;
    }
}

public record Price(decimal UnitPrice, MultiPriced? MultiPrice = null);

public record MultiPriced(decimal Price, int Size);