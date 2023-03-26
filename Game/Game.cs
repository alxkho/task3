using System.Collections;

namespace Game
{
    public class Game
    {
        private readonly Dictionary<string, string> _moves = new();
        private readonly Dictionary<string, string> _gameUserMove = new();
        private HMAC _hmac = new();

        public void StartGame(List<string> args)
        {
            if (!IsArgsCorrect(args))
            {
                return;
            }

            Initialize(args);

            var replay = true;
            while (replay)
            {
                var compMove = GetComputerMove();
                var userMove = GetUserChoise();

                switch (userMove.Key)
                {
                    case "0":
                        Console.WriteLine(Dialogs.Goodbye);

                        return;

                    case "?":
                        Table.DisplayHelp(_gameUserMove);

                        break;

                    default:
                        replay = RunGameRound(userMove, compMove);

                        break;
                }
            }

            Console.WriteLine(Dialogs.Goodbye);
        }

        private bool RunGameRound(KeyValuePair<string, string> userMove, KeyValuePair<string, string> compMove)
        {
            Console.WriteLine($"Your move: {userMove.Value}");
            Console.WriteLine($"Computer move: {compMove.Value}");

            DisplayWhoWins(userMove, compMove);

            _hmac.DisplaySaltAfterRound();

            Console.WriteLine(Dialogs.AnotherGame);
            var userAnswer = Console.ReadLine()?.Trim();
            var loop = userAnswer == "1";

            return loop;
        }

        private void Initialize(List<string> options)
        {
            for (var i = 1; i <= options.Count; i++)
            {
                _gameUserMove.Add(i.ToString(), options[i - 1]);
                _moves.Add(i.ToString(), options[i - 1]);
            }

            _moves.Add("0", "exit");
            _moves.Add("?", "help");
        }

        private KeyValuePair<string, string> GetUserChoise()
        {
            GetAvailableMoves();

            while (true)
            {
                Console.Write(Dialogs.EnterMove);

                var move = Console.ReadLine();

                if (move != null && !_moves.ContainsKey(move))
                {
                    Console.WriteLine(Dialogs.Errors.NonExistentMove);

                    continue;
                }

                return _moves.Single(r => r.Key == move);
            }
        }

        private KeyValuePair<string, string> GetComputerMove()
        {
            _hmac.GenerateKey();

            var move = new Random().Next(1, _gameUserMove.Count) + 1;
            var compMove = _moves.Single(r => r.Key == move.ToString());

            var saltedComputerMove = _hmac.GenerateHash(compMove.Value);
            Console.WriteLine(Dialogs.Crypt.HMAC + saltedComputerMove);

            return compMove;
        }

        private void GetAvailableMoves()
        {
            Console.WriteLine(Dialogs.AvailableMoves);

            foreach (var move in _moves)
            {
                Console.Write(move.Key + "-" + move.Value + "\n");
            }
        }

        private void DisplayWhoWins(KeyValuePair<string, string> userMove, KeyValuePair<string, string> compMove)
        {
            switch (Rule.GetResult(_gameUserMove, userMove, compMove))
            {
                case -1:
                    Console.WriteLine(Dialogs.Lose);

                    return;
                case 0:
                    Console.WriteLine(Dialogs.Draw);

                    return;
                case 1:
                    Console.WriteLine(Dialogs.Win);

                    return;
            }
        }

        private static bool IsArgsCorrect(IReadOnlyCollection<string> args)
        {
            var result = false;

            Console.ForegroundColor = ConsoleColor.Red;

            if (args.Count < 3)
            {
                Console.WriteLine(Dialogs.Errors.SmallNumberOfArgs);
            }
            else if (args.Count % 2 == 0)
            {
                Console.WriteLine(Dialogs.Errors.EvenCountOfArgs);
            }
            else if (args.Count != args.Distinct().Count())
            {
                Console.WriteLine(Dialogs.Errors.NonUniqueOptions);
            }
            else
            {
                result = true;
            }

            Console.ResetColor();

            return result;
        }
    }
}
