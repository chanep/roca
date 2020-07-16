using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;
using Clutch.Diagnostics.EntityFramework;

namespace Cno.Roca.BackEnd.Tests.Materials
{
    public class EfListener : IDbTracingListener
    {
        public void CommandExecuting(DbTracingContext context)
        {
            //Console.WriteLine("Excecuting: " + context.Command.CommandText);
        }

        public void CommandFinished(DbTracingContext context)
        {
            //Console.WriteLine("Finished: " + context.Command.CommandText);
        }

        public void ReaderFinished(DbTracingContext context)
        {
            //Console.WriteLine("Reader: " + context.Command.CommandText);
            //Console.WriteLine();
        }

        public void CommandFailed(DbTracingContext context)
        {
            Console.WriteLine("Failed: ");
            LogCommand(context);
        }

        public void CommandExecuted(DbTracingContext context)
        {
            Console.WriteLine("Excecuted: ");
            LogTransaction(context);
            LogCommand(context);
        }


        private void LogTransaction(DbTracingContext context)
        {
            var tx = context.Command.Transaction;
            if (tx != null)
            {
                Console.WriteLine("Tx Local: " + tx.GetHashCode() + "; Isolation: " + tx.IsolationLevel);
            }

            if (Transaction.Current != null)
                Console.WriteLine("Tx dist: " + Transaction.Current.TransactionInformation.DistributedIdentifier);
        }

        private void LogCommand(DbTracingContext context)
        {
            Console.WriteLine(context.Command.CommandText);
            foreach (DbParameter param in context.Command.Parameters)
            {
                Console.WriteLine("   " + param.ParameterName + ": " + param.Value);
            }

            Console.WriteLine();
        }
    }

}
