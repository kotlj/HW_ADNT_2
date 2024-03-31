using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace HW_ADNT_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connect = "Data Source=DESKTOP-V5OB79V;Initial Catalog=warehouse;Integrated Security=True";
            using(SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                SqlCommand cmd = null;
                string choise = "";

                while(true)
                {
                    Console.WriteLine("Choose:\n1 - Insert new product\n2 - Insert new supplier\n3 - Insert new type\n4 - Update an product" +
                        "\n5 - Update an Supplyer;\n6 - Update an Type\n7 - Delete product\n8 - Delete supplyer\n9 - Delete Type" +
                        "\n10 - Show supplyer with most stored products\n11 - Show supplyer with least stored products" +
                        "\n12 - Show type of most stored product\n13 - Show type with least stored products" +
                        "\n14 - Show products what was supplyed more then [n] days ago" +
                        "\n0 - Exit");
                    choise = Console.ReadLine();
                    if (choise == "0")
                    {
                        break;
                    }
                    if (choise == "1")
                    {
                        cmd = new SqlCommand("INSERT INTO Products (Name, Type, Supplier, Last_supply, Amount) VALUES (@Name, @Type, @Supplier, @Last_supply, @Amount)", conn);

                        Console.WriteLine("Enter name of product:\n");
                        string name = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@Name", $"{name}");

                        Console.WriteLine("Enter Type of Product:\n");
                        string type = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@Type", $"{type}");

                        Console.WriteLine("Enter Supplier of product:\n");
                        string supplyer = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@Supplier", $"{supplyer}");

                        Console.WriteLine("Enter date of last supply(year-month-day, all in numbers):\n");
                        string date = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@Last_supply", $"{date}");

                        Console.WriteLine("Enter Amount of product:\n");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        cmd.Parameters.AddWithValue("@Amount", $"{amount}");
                    }
                    else if (choise == "2")
                    {
                        cmd = new SqlCommand("INSERT INTO Supplyer (Name, Info) VALUES (@Name, @Info)", conn);

                        Console.WriteLine("Enter name of Supplyer:\n");
                        string name = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@Name", $"{name}");

                        Console.WriteLine("Enter info about Supplyer:\n");
                        string info = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@Info", $"{info}");
                    }
                    else if (choise == "3")
                    {
                        cmd = new SqlCommand("INSERT INTO Type (Name) VALUES (@Name)", conn);

                        Console.WriteLine("Enter Name of type:\n");
                        string name = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@Name", $"{name}");
                    }
                    else if (choise == "4")
                    {
                        Console.WriteLine("Enter ID of product you want to change:\n");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter name of column you want update(Name, Type, Supplyer, Last_supply, Amount):\n");
                        string col = Console.ReadLine();
                        cmd = new SqlCommand($"UPDATE Products SET {col} = @{col} WHERE ID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", $"{ID}");

                        Console.WriteLine("Enter new value for col(Amount is an int type, and Date wryte like 'year-month-day'):\n");
                        string value = Console.ReadLine();
                        if (int.TryParse(value, out int res))
                        {
                            cmd.Parameters.AddWithValue($"@{col}", $"{res}");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue($"@{col}", $"{value}");
                        }
                    }
                    else if (choise == "5")
                    {
                        Console.WriteLine("Enter ID of supplyer you want to change:\n");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter name of column you want update(Name, Info):\n");
                        string col = Console.ReadLine();
                        cmd = new SqlCommand($"UPDATE Supplyer SET {col} = @{col} WHERE ID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", $"{ID}");

                        Console.WriteLine("Enter new value for col:\n");
                        string value = Console.ReadLine();
                        cmd.Parameters.AddWithValue($"@{col}", $"{value}");
                    }
                    else if (choise == "6")
                    {
                        Console.WriteLine("Enter ID of type you want to change:\n");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter name of column you want update(Name):\n");
                        string col = Console.ReadLine();
                        cmd = new SqlCommand($"UPDATE Type SET {col} = @{col} WHERE ID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", $"{ID}");

                        Console.WriteLine("Enter new value for col:\n");
                        string value = Console.ReadLine();
                        cmd.Parameters.AddWithValue($"@{col}", $"{value}");
                    }
                    else if (choise == "7")
                    {
                        Console.WriteLine("Enter ID of product you want delete:\n");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        cmd = new SqlCommand("DELETE FROM Products WHERE ID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", $"{ID}");
                    }
                    else if (choise == "8")
                    {
                        Console.WriteLine("Enter ID of supplyer you want delete:\n");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        cmd = new SqlCommand("DELETE FROM Supplyer WHERE ID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", $"{ID}");
                    }
                    else if (choise == "9")
                    {
                        Console.WriteLine("Enter ID of type you want delete:\n");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        cmd = new SqlCommand("DELETE FROM Type WHERE ID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", $"{ID}");
                    }
                    else if (choise == "10")
                    {
                        cmd = new SqlCommand("SELECT TOP 1 Supplier FROM Products\r\nORDER BY Amount DESC", conn);
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader[i] + " ");
                                    }
                                    Console.WriteLine('\n');
                                }
                            }
                        }
                    }
                    else if (choise == "11")
                    {
                        cmd = new SqlCommand("SELECT TOP 1 Supplier FROM Products\r\nORDER BY Amount ASC", conn);
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader[i] + " ");
                                    }
                                    Console.WriteLine('\n');
                                }
                            }
                        }
                    }
                    else if (choise == "12")
                    {
                        cmd = new SqlCommand("SELECT TOP 1 Type FROM Products\r\nORDER BY Amount DESC", conn);
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader[i] + " ");
                                    }
                                    Console.WriteLine('\n');
                                }
                            }
                        }
                    }
                    else if (choise == "13")
                    {
                        cmd = new SqlCommand("SELECT TOP 1 Type FROM Products\r\nORDER BY Amount ASC", conn);
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader[i] + " ");
                                    }
                                    Console.WriteLine('\n');
                                }
                            }
                        }
                    }
                    else if (choise == "14")
                    {
                        Console.WriteLine("Enter number of days:\n");
                        int days = Convert.ToInt32(Console.ReadLine());
                        DateTime dateTime = DateTime.Now;
                        dateTime = dateTime.AddDays(-days);
                        cmd = new SqlCommand($"SELECT Supplier FROM Products\r\nWHERE Last_supply < '{dateTime.Year}-{dateTime.Month}-{dateTime.Day}'", conn);
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader[i] + " ");
                                    }
                                    Console.WriteLine('\n');
                                }
                            }
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
