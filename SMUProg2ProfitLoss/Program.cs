using System;
using System.Collections.Generic;
using System.Linq;

namespace SMUProg2ProfitLoss
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Read number of products
                InputReader inputReader = new InputReader();
                Console.Write("Enter Number of Products: ");
                long numberOfProducts = inputReader.ReadNumericInput();

                // Initlaize arrays
                string[] productNames = new string[numberOfProducts];
                long[] purchases = new long[numberOfProducts];
                long[] sales = new long[numberOfProducts];
                long[] purchaseCosts = new long[numberOfProducts];
                long[] sellingPrice = new long[numberOfProducts];
                double[] productProfitLossPercentage = new double[numberOfProducts];


                for (int i = 0; i < numberOfProducts; i++)
                {
                    Console.WriteLine();
                    // Read product Info
                    // Read product name
                    Console.Write("Enter the name of the product: ");
                    productNames[i] = inputReader.ReadStringInput();

                    // Read product purchases 
                    Console.Write("Enter the number of {0} purchased: ", productNames[i]);
                    purchases[i] = inputReader.ReadNumericInput();

                    // Read product sold
                    Console.Write("Enter the number of {0} sold: ", productNames[i]);
                    sales[i] = inputReader.ReadNumericInput();

                    // Read product purchase cost
                    Console.Write("Enter the cost of each {0}: ", productNames[i]);
                    purchaseCosts[i] = inputReader.ReadCurrencyInput();

                    // Read product selling cost
                    Console.Write("Enter the selling price of each {0}: ", productNames[i]);
                    sellingPrice[i] = inputReader.ReadCurrencyInput();

                    // Calculate profit or loss 
                    productProfitLossPercentage[i] = CalculateProductProfit(purchaseCosts[i] * purchases[i], sellingPrice[i] * sales[i]);
                }

                // Sort acc to profit
                Dictionary<long, double> dictProfitLoss = new Dictionary<long, double>();
                for (int i = 0; i < numberOfProducts; i++)
                {
                    dictProfitLoss.Add(i, productProfitLossPercentage[i]);
                }

                PrintConsoleTable consoleTable = new PrintConsoleTable();
                consoleTable.Columns = new string[] { "", "Name", "Purchases", "Sales", "Cost", "Selling Price", "Total Purchase", "Total Sales", "P/L%", "P/L/BE" };
                

                int serialNumber = 1;
                long totalPurchase = 0;
                long totalSales = 0;
                double totalProfitPercentage = 0.0; 
                foreach (KeyValuePair<long, double> profit in dictProfitLoss.OrderByDescending(key => key.Value))
                {
                    consoleTable.AddRow(new string[] {
                        serialNumber++.ToString(),
                        productNames[profit.Key],
                        purchases[profit.Key].ToString("F"),
                        sales[profit.Key].ToString("F"),
                        "$" + purchaseCosts[profit.Key].ToString("F"),
                        "$" + sellingPrice[profit.Key].ToString("F"),
                        "$" + (purchaseCosts[profit.Key] * purchases[profit.Key]).ToString("F"),
                        "$" + (sellingPrice[profit.Key] * sales[profit.Key]).ToString("F"),
                        productProfitLossPercentage[profit.Key].ToString("F") + "%",
                        productProfitLossPercentage[profit.Key] > 0 ? "Profit": productProfitLossPercentage[profit.Key] == 0 ? "Break-Even" : "Loss"
                    });
                    totalPurchase += purchaseCosts[profit.Key] * purchases[profit.Key];
                    totalSales += sellingPrice[profit.Key] * sales[profit.Key];
                }

                totalProfitPercentage = ((totalSales - totalPurchase - 0.0) / totalPurchase) * 100;
                consoleTable.AddRow(new string[] {
                    "Net",
                    "Profit/Loss",
                    "",
                    "",
                    "",
                    "", 
                    "$" + totalPurchase.ToString("F"),
                    "$" + totalSales.ToString("F"),
                    totalProfitPercentage.ToString("F") + "%",
                    totalProfitPercentage > 0 ? "Profit": totalProfitPercentage == 0 ? "Break-Even" : "Loss"
                });

                Console.WriteLine();
                consoleTable.PrintTable();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private static double CalculateProductProfit(long purchaseCosts, long sellingCosts)
        {
            if (purchaseCosts <= 0) throw new Exception("Invalid purchase costs");
            return ((sellingCosts - purchaseCosts - 0.0) / purchaseCosts) * 100;
        }
    }
}
