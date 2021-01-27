using System;
using System.Collections.Generic;
using System.Text;

namespace SMUProg2ProfitLoss
{
    /// <summary>
    /// Class to read and validate inputs from Comand line.
    /// </summary>
    public class InputReader
    {
        /// <summary>
        /// Read and validate Numeric input
        /// </summary>
        /// <returns></returns>
        public long ReadNumericInput()
        {
            string inputText = Console.ReadLine();
            long inputNumber;

            // try to extract numeric value from string input. 
            if (Int64.TryParse(inputText, out inputNumber)) return inputNumber;
            throw new Exception("The input should be a number.");
        }

        /// <summary>
        /// Read and validate Currency input
        /// </summary>
        /// <returns></returns>
        public long ReadCurrencyInput()
        {
            string inputText = Console.ReadLine();
            long inputNumber;

            if (!inputText.Trim().StartsWith("$")) throw new Exception("The input should start with $ sign");

            // remove $ sign and conver to number.
            inputText = inputText.Replace("$", "");

            // try to extract numeric value from string input. 
            if (Int64.TryParse(inputText, out inputNumber)) return inputNumber;
            throw new Exception("The input should be a number.");
        }

        /// <summary>
        /// Read string input
        /// </summary>
        /// <returns></returns>
        public string ReadStringInput()
        {
            string inputText = Console.ReadLine();

            if (string.IsNullOrEmpty(inputText)) throw new Exception("Empty string entered.");
            return inputText;
        }
    }
}
