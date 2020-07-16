using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Clutch.Diagnostics.EntityFramework;

namespace Cno.Roca.Web.RocaSite.Tests
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
            LogCommand(context);
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
