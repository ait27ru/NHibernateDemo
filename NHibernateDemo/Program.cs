using System;
using System.Linq;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;
using NHibernate.Util;

namespace NHibernateDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionStringName = "DemoConnection";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
            });

            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            var sessionFactory = cfg.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                // get Customers using Criteria
                var customers = session.CreateCriteria<Customer>()
                    .List<Customer>();
                customers.ForEach(c => Console.WriteLine($"{c.FirstName,-10} {c.LastName}"));

                // get Customers using LINQ
                Console.WriteLine();

                var customers2 = from customer in session.Query<Customer>()
                    orderby customer.LastName
                    select customer;
                customers2.ForEach(c => Console.WriteLine($"{c.FirstName,-10} {c.LastName}"));

                // create a new Customer

                var customerQuery = from customer in session.Query<Customer>()
                    where customer.FirstName == "James" && customer.LastName == "Bond"
                    select customer;

                if (!customerQuery.Any())
                {
                    var newCustomer = new Customer { FirstName = "James", LastName = "Bond" };
                    session.Save(newCustomer);
                    Console.WriteLine($"Created a new Customer '{newCustomer.FirstName} {newCustomer.LastName}' with Id ({newCustomer.Id}).");
                }
                else
                {

                    Console.WriteLine("Found a customer. Options: 1) to update 2) delete");
                    var option = Console.ReadLine();

                    var existingCustomer = customerQuery.First();

                    switch (option)
                    {
                        case "1":
                            existingCustomer.LastName = "Blond";
                            session.Update(existingCustomer);
                            Console.WriteLine("Updated existing Customer.");
                            break;
                        case "2":
                            session.Delete(existingCustomer);
                            Console.WriteLine("Deleted existing Customer.");
                            break;
                        default:
                            Console.WriteLine($"Option '{option}' is not recognised. Nothing done.");
                            break;
                    }

                }

                tx.Commit();
            }
            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }
    }
}