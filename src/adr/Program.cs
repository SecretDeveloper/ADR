using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace adr
{
    partial class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var c = ADRCommand.Parse(args);

                if (c.Command == "invalid")
                {
                    ShowHelp(c);
                    return;
                }

                if (c.Command == "help")
                {
                    ShowHelp();
                    return;
                }

                if (c.Command == "init")
                {
                    var currentDir = Environment.CurrentDirectory;
                    var repo = DAL.InitRepository(currentDir);
                    Utility.WriteLine("Created ADR repository at:" + repo);
                    return;
                }

                if (c.Command == "new")
                {
                    var adr = new Models.ADR();
                    adr.Title = string.Join(" ", c.Arguments);
                    var recordPath = DAL.SaveADR(adr);
                    Utility.WriteLine("Creating a new record at:" + recordPath);

                    return;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLine(ex.Message);
            }
        }



        private static void ShowHelp(ADRCommand c = null)
        {
            Utility.WriteLine(@"ADR - Architecture Descision Records tool
Description: A command line tool for creating architecure decision records.  
You can read more about ADRs here https://adr.github.io/

Usage: adr [command] [options]
Commands:
  init                 initialises a repository for ADR items in the current directory.
  new [NAME]             the name of the record to be created
  help                 show this message and exit
");

            if (c != null)
                Utility.WriteLine("Invalid command: " + c.Arguments[0]);

        }
    }
}
