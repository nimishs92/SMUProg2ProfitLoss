using System;
using System.Collections.Generic;
using System.Text;

namespace SMUProg2ProfitLoss
{
    public class PrintConsoleTable
    {
        public string[] Columns { get; set; }

        public List<string[]> Rows { get; set; }

        private int _maxWidth;

        public PrintConsoleTable()
        {
            this._maxWidth = Console.LargestWindowWidth - 20;
            this.Rows = new List<string[]>();
        }

        public void PrintTable()
        {
            PrintRow(true,this.Columns);
            PrintDashLine();

            for (int i = 0; i < this.Rows.Count - 1 ; i++)
            {
                PrintRow(false, this.Rows[i]);
            }

            PrintDashLine();
            PrintRow(false, this.Rows[this.Rows.Count - 1]);
        }

        private void PrintRow(bool showSeperator, params string[] columns)
        {
            int width = (this._maxWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width);
                if (showSeperator) row += "|"; else row += " ";
            }

            Console.WriteLine(row);
        }

        private string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        private void PrintDashLine()
        {
            Console.WriteLine(new string('-', this._maxWidth));
        }
        
        public void AddRow(string[] row)
        {
            this.Rows.Add(row);
        }
    }
}
