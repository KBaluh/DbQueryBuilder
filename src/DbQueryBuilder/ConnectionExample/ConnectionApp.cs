using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using DbQueryBuilder;
using DbQueryBuilder.MsSql;

namespace ConnectionExample
{
    class ConnectionApp
    {
        private MsSqlDatabase Database { get; }

        public ConnectionApp()
        {
            Database = new MsSqlDatabase("Data Source=localhost;Initial Catalog=ExampleDatabase;Integrated Security=SSPI");
        }

        public void Connect()
        {
            try
            {
                Database.Connect();

                var customers = GetCustomers();
                foreach (var customer in customers)
                {
                    DisplayCustomer(customer);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Database.Disconnect();
            }
        }

        private IQueryBuilderSelect GetSelectQuery()
        {
            var selectQuery = new MsSqlQueryBuilderSelect(Database);
            selectQuery.Select(new[]
            {
                "Customer.Id",
                "Customer.FirstName"
            });
            selectQuery.From("Customer");
            return selectQuery;
        }

        private List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            var selectQuery = GetSelectQuery();
            var dataReader = Database.ExecuteResultQuery(selectQuery);
            while (dataReader.Read())
            {
                customers.Add(new Customer
                {
                    Id = dataReader.GetInt32("Id"),
                    FirstName = dataReader.GetString("FirstName")
                });
            }
            Database.CloseDataReader(dataReader);

            return customers;
        }

        private void DisplayCustomer(Customer customer)
        {
            Console.WriteLine(customer);
        }
    }
}
