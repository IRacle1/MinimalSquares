namespace MinimalSquares.ConsoleCommands
{
    public abstract class BaseCommand
    {
        protected BaseCommand(string name, string[] aliases, string description)
        {
            Name = name;
            Aliases = aliases;
            Description = description;
        }

        public string Name { get; }
        public string[] Aliases { get; }
        public string Description { get; }

        public abstract void Handle();

        protected string BuildMessage(string msg)
        {
            return "[" + Name + "] " + msg;
        }

        protected void Success()
        {
            CommandManager.WriteLineText(BuildMessage("Успешно!"), CommandStatus.Success);
        }

        protected void Abort()
        {
            CommandManager.WriteLineText(BuildMessage("Прервано"), CommandStatus.Exit);
        }
    }

    public enum CommandStatus
    {
        None,
        Invalid,
        Exit,
        Success
    }
}
