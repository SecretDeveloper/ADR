using System.Linq;

namespace adr
{
    partial class Program
    {
        private class ADRCommand
        {
            public string Command;
            public string[] Arguments;

            public ADRCommand()
            {
                Command = "";
                Arguments = "".Split();
            }
            public static ADRCommand Parse(string[] args)
            {
                var command = new ADRCommand();
                if (args.Length == 0 || Utility.InvariantEquals("help", args[0]))
                {
                    command.Command = "help";
                    command.Arguments = args.Skip(1).ToArray();
                    return command;
                }

                if (Utility.InvariantEquals("init", args[0]))
                {
                    command.Command = "init";
                    command.Arguments = args.Skip(1).ToArray();
                    return command;
                }

                if (Utility.InvariantEquals("new", args[0]))
                {
                    if (args.Length == 1)
                    {
                        command.Command = "invalid";
                        command.Arguments = new string[] { "you need to supply a name for the new record", "" };
                        return command;
                    }
                    command.Command = "new";
                    command.Arguments[0] = string.Join(" ", args.Skip(1).ToArray());
                    return command;
                }

                command.Command = "invalid";
                command.Arguments[0] = string.Join(" ", args.ToArray());
                return command;
            }

        }
    }
}
