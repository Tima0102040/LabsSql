using System;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace ConsoleApp3
{
    class Program
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        static void Main()
        {
            Program pr = new Program();
            char command;
            char key;
            do
            {
                Console.WriteLine("Menu of command");
                Console.WriteLine("1. Show Connection Information");
                Console.WriteLine("2. SELECT");
                Console.WriteLine("3. INSERT");
                Console.WriteLine("4. DELETE");
                Console.WriteLine("5. UPDATE");
                Console.WriteLine();
                Console.WriteLine("Select of command, press number of key");
                command = (char) Console.Read();
                Console.ReadLine();
                Console.WriteLine(new String('-', 25));
                switch (command)
                {
                    case '1':
                        pr.FirstTask();
                        break;
                    case '2':
                        pr.SecondTask();
                        break;
                    case '3':
                        pr.ThirdTask();
                        break;
                    case '4':
                        pr.FourthTask();
                        break;
                    case '5':
                        pr.FifthTask();
                        break;
                    default:
                        Console.WriteLine("Wrong command");
                        break;
                }

                Console.WriteLine("Continue y/n");
                key = (char) Console.Read();
                Console.ReadLine();
                Console.Clear();
            } while (key != 'n');
        }
        
        public void FirstTask()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection is open");
                Console.WriteLine("\t Connection Source: " + connection.DataSource);
                Console.WriteLine("\t Database Name: " + connection.Database);
                Console.WriteLine("\t Server Version: " + connection.ServerVersion);
                Console.WriteLine("\t Connection Condition: " + connection.State);
                Console.WriteLine("\t User Id: " + connection.ClientConnectionId);
                connection.Close();
            }
        }
        
        public void SecondTask()
        {
            string sql = "SELECT * FROM Client";

            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine(new string('-', 10 + 20 + 30 + 30 + 5));
                    Console.WriteLine("{0,10} {1,20} {2,30} {3,30}", reader.GetName(0), reader.GetName(0), reader.GetName(0), reader.GetName(0));
                    Console.WriteLine(new string('-', 10 + 20 + 30 + 30 + 5));

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object FName = reader.GetValue(1);
                        object Lname = reader.GetValue(2);
                        object PhoneNumber = reader.GetValue(7);

                        Console.WriteLine("{0,10} {1,20} {2,30} {3,30} ", id, FName, Lname, PhoneNumber);
                    }
                }
                reader.Close();
            }

            Console.Read();
        }

        public void ThirdTask()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine("Enter First Name: ");
                    string fName = Console.ReadLine();
                    Console.WriteLine("Enter Last Name: ");
                    string lName = Console.ReadLine();
                    Console.WriteLine("Enter Second Name: ");
                    string sName = Console.ReadLine();
                    Console.WriteLine("Enter Passport: ");
                    string passport = Console.ReadLine();
                    Console.WriteLine("Enter Date Of Birth: ");
                    string birth = Console.ReadLine();
                    Console.WriteLine("Enter City: ");
                    string city = Console.ReadLine();
                    Console.WriteLine("Enter Phone number: ");
                    string phone = Console.ReadLine();

                    string sqlExpression = String.Format(
                        "INSERT INTO [dbo].[Client] VALUES ({0},{1},{2},{3},{4},{5},{6})",
                        "'" + fName + "'", "'" + lName + "'", "'" + sName + "'", "'" + passport + "'",
                        "'" + birth + "'",
                        "'" + city + "'", "'" + phone + "'");

                    SqlCommand cmd = new SqlCommand(sqlExpression, connection);
                    int number = cmd.ExecuteNonQuery();
                    Console.WriteLine("Was added number of objects {0}: ", number);
                }

                connection.Close();
            }
        }
        
        public void FourthTask()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Enter Client Id: ");
                int Id = Int32.Parse(Console.ReadLine());

                string sql = String.Format("DELETE FROM Client WHERE ClientId= {0}", Id);
                SqlCommand cmd = new SqlCommand(sql, connection);
                int number = cmd.ExecuteNonQuery();
                Console.WriteLine("Number of deleted rows: " + number);

                connection.Close();
            }
        }

        public void FifthTask()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Enter Client Id: ");
                int id = Int32.Parse(Console.ReadLine());
                
                Console.WriteLine("Enter new Phone number: ");
                string phone = Console.ReadLine();

                string sql = String.Format("UPDATE Client SET ClientPhone = {0} WHERE ClientId = {1}", phone, id);

                SqlCommand cmd = new SqlCommand(sql, connection);
                int number = cmd.ExecuteNonQuery();

                Console.WriteLine("Updated rows: " + number);
            }
        }
    }
}

