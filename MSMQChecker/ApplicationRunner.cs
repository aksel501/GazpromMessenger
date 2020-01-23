using GazpromMessenger.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;

namespace MSMQChecker
{
    class ApplicationRunner
    {
        private static bool keepRunning = true;
        internal static void Run()
        {
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
                Console.WriteLine("Stopping Application");
                e.Cancel = true;
                keepRunning = false;
            };

            while (keepRunning)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Checking MSMQ");
                using (MSMQManager mSMQManager = new MSMQManager(Configuration.configuration))
                {
                    int count = mSMQManager.Count();
                    if (count != 0)
                    {
                        Console.WriteLine($"There are {count} Messages to Receive");
                        using (ApplicationDbContext db = new ApplicationDbContext())
                        {
                            Console.WriteLine("Starting Transaction");
                            using (var dbContextTransaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    Console.WriteLine("Inserting Messages to DB");
                                    while (mSMQManager.Count() > 0)
                                    {
                                        Message myMessage = mSMQManager.ReceiveMessage();
                                        int result = db.Database.ExecuteSqlInterpolated($"INSERT INTO Messages (Description, CreateTime) VALUES ({myMessage.Description}, {myMessage.CreateTime})");
                                        if (result != 1)
                                        {
                                            throw new Exception("Failed to insert message to DB");
                                        }
                                    }
                                    Console.WriteLine("All Inserts SUccessful. Committing Transacation");
                                    dbContextTransaction.Commit();
                                }
                                catch
                                {
                                    Console.WriteLine("An Insert Failed. Rolling Back Transaction");
                                    dbContextTransaction.Rollback();
                                    throw;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are No Messages to Receive");
                    }
                }
                Console.WriteLine("Sleeping for 10 Seconds");
                Console.WriteLine("--------------------------------------------");
                Thread.Sleep(10000);
            }
        }
    }
}
