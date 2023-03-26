namespace Game
{
    public static class Dialogs
    {
        public static class Errors
        {
            public const string SmallNumberOfArgs = "Error: small number of arguments. Please, enter more then 2 arguments";
            public const string EvenCountOfArgs = "Error: even count of arguments. Please, enter an odd count of arguments";
            public const string NonExistentMove = "Error: this move does't exist. Please, choose another.";
            public const string NonUniqueOptions = "Error: non unique options. Please, insert correct arguments";
        }

        public const string Lose = "You Lose :(";
        public const string Draw = "Friendship wins!";
        public const string Win = "Congrats! You Win!";
        public const string AvailableMoves = "Available moves:";
        public const string EnterMove = "Enter your move: ";
        public const string AnotherGame = "You want play another game? 0 - no, 1 - yes";
        public const string Goodbye = "Goodbye";

        public static class Table
        {
            public static readonly string[] TableColumns = { "I pick ", " so I win ", " but I have draw with ", " and I lose to" };
        }

        public static class Crypt
        {
            public const string HMAC = "HMAC: ";
            public const string HMACKey = "HAMC key: ";
        }
    }
}
