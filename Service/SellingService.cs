using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace McpSitecoreAiExample;

public class SellingService
{
    public async Task<List<Selling>> GetAllSellingsAsync()
    {

        var random = new Random();
        var productNames = new[] { "Laptop", "Mouse", "Keyboard", "Monitor", "USB Cable", "Charger", "Tablet", "Phone", "Printer" };
        var suppliers = new[] { "TechCorp", "GadgetHub", "Electronica", "ITSupplyCo" };
        var buyers = new[] { "John Doe", "Jane Smith", "Alice", "Bob", "Michael" };
        var paymentMethods = new[] { "Cash", "Card", "UPI", "Net Banking" };
        var remarksList = new[] { "Fast delivery", "Discount applied", "No warranty", "Urgent", "Regular order" };

        var sellingList = new List<Selling>();

        for (int i = 0; i < 100; i++)
        {
            var product = productNames[random.Next(productNames.Length)];
            var quantity = random.Next(1, 10);
            var price = Math.Round(random.NextDouble() * 9000 + 100, 2);
            var soldDate = DateTime.Today.AddDays(-random.Next(0, 30));
            var supplier = suppliers[random.Next(suppliers.Length)];
            var buyer = buyers[random.Next(buyers.Length)];
            var payment = paymentMethods[random.Next(paymentMethods.Length)];
            var remark = remarksList[random.Next(remarksList.Length)];

            var selling = new Selling
            {
                ProductName = product,
                Quantity = quantity,
                PricePerUnit = price,
                SoldDate = soldDate,
                SupplierName = supplier,
                BuyerName = buyer,
                PaymentMethod = payment,
                Remarks = remark
            };

            sellingList.Add(selling);
        }

        return sellingList;
    }

}

[JsonSerializable(typeof(List<Selling>))]
internal sealed partial class SellingContext : JsonSerializerContext {

}