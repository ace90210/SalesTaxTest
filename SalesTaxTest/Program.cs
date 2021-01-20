using SalesTaxServices.Services;
using System;

namespace SalesTaxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var salesService = new SalesTaxService();
            salesService.InitialiseConfiguration();

            var ordersToProcess = salesService.LoadOrders("Sample1");
            salesService.ProcessOrders(ordersToProcess);

            var ordersToProcess2 = salesService.LoadOrders("Sample2");
            salesService.ProcessOrders(ordersToProcess2);

            var ordersToProcess3 = salesService.LoadOrders("Sample3");
            salesService.ProcessOrders(ordersToProcess3);
        }
    }
}
