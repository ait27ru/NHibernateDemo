using System;
using System.Linq;
using System.Reflection;
using NHibernate;
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

            //cfg.DataBaseIntegration(x =>
            //{
            //    x.ConnectionStringName = "DemoConnection";
            //    x.Driver<SqlClientDriver>();
            //    x.Dialect<MsSql2012Dialect>();
            //});
            //cfg.AddAssembly(Assembly.GetExecutingAssembly());

            cfg.Configure();

            cfg.DataBaseIntegration(x =>
            {
                x.Timeout = 10;
                x.LogSqlInConsole = true;
            });

            cfg.SessionFactory().GenerateStatistics();

            var sessionFactory = cfg.BuildSessionFactory();

            //SaveAndModifyCustomer(sessionFactory);

            SaveAndRead(sessionFactory);

            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }

        private static void SaveAndModifyCustomer(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
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
                    var newCustomer = CreateCustomer();

                    var customerQuery = from customer in session.Query<Customer>()
                        where
                        (customer.FirstName == newCustomer.FirstName) && (customer.LastName == newCustomer.LastName)
                        select customer;

                    if (!customerQuery.Any())
                    {
                        session.Save(newCustomer);
                        Console.WriteLine(
                            $"Created a new Customer '{newCustomer.FirstName} {newCustomer.LastName}' with Id ({newCustomer.Id}).");
                    }
                    else
                    {
                        var existingCustomer = customerQuery.First();

                        Console.WriteLine(
                            $"Found a customer '{existingCustomer.FirstName} {existingCustomer.LastName}'. Options: 1) to update 2) delete");
                        var option = Console.ReadLine();

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
            }
        }

        private static void SaveAndRead(ISessionFactory sessionFactory)
        {
            int newId;

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var newCustomer = CreateCustomer();
                    newCustomer.LastName = "Gerrard";
                    newCustomer.MemberSince = null;
                    Console.WriteLine("Before saving:");
                    Console.WriteLine(newCustomer);
                    session.Save(newCustomer);
                    newId = newCustomer.Id;
                    tx.Commit();
                }
            }

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var customer = session.Load<Customer>(newId);
                    Console.WriteLine("After saving:");
                    Console.WriteLine(customer);
                    tx.Commit();
                }
            }
        }

        private static Customer CreateCustomer()
        {
            return new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                AverageRating = 10.12345678,
                Points = 100,
                HasGoldStatus = true,
                MemberSince = new DateTime(2012, 1, 1),
                CreditRating = CustomerCreditRating.Good,
                CreditRatingText = CustomerCreditRating.Good,
                Address = new Location
                {
                    Street = "123 Somewhere St",
                    City = "Nowhere",
                    Province = "Alberta",
                    Country = "Canada"
                }
            };
        }
    }
}