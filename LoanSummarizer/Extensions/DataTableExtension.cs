using System.Data;

namespace LoanSummarizer.Extensions;

public static class DataTableExtension
{
    public static void Dump(this DataTable dataTable)
    {
        void GenerateHorizontalLine()
        {
            // Print the horizontal line delimiter
            Console.WriteLine(new string('-', dataTable.Columns.Count * 23));
        }
        
        GenerateHorizontalLine();

        // Print the column names
        foreach (DataColumn column in dataTable.Columns)
        {
            Console.Write("| " + column.ColumnName.PadRight(20) + " ");
        }

        Console.WriteLine("|");

        GenerateHorizontalLine();
    
        // Print the data rows
        foreach (DataRow row in dataTable.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                Console.Write("| " + item?.ToString()?.PadRight(20) + " ");
            }

            Console.WriteLine("|");
        }

        GenerateHorizontalLine();
    }
}