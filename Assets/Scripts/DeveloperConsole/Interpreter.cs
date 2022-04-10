namespace DenizYanar.DeveloperConsoleSystem
{
    /// <summary>
    /// Example
    /// [Command_Name] [p1] [p2] [p3]
    /// A command with 3 parameter
    /// </summary>
    public static class Interpreter
    {
        public static string[] ParseTheCommand(string command)
        {
            return string.Empty == command ? new string[] { } : command.Split(' ');
        }

        public static ConsoleCommand NameToCommand(string commandName, ConsoleCommandTable commandList)
        {
            if (!commandList.Commands.ContainsKey(commandName)) return null;
            
            var command = commandList.Commands[commandName];
            return command;
        }
        
        
    }
}
