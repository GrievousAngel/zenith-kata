namespace CheckoutKata;

interface ICheckout
{
    void Scan(string item);
    int GetTotalPrice();
}