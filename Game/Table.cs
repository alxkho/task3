using ConsoleTables;

namespace Game
{
    public class Table
    {
        public static void DisplayHelp(Dictionary<string, string> moves)
        {
            var table = new ConsoleTable(Dialogs.Table.TableColumns);

            foreach (var move in moves)
            {
                var winnerList = string.Join(", ", moves.WhoIWinIfIHave(move));
                var loserList = string.Join(", ", moves.WhoILoseToIfIHave(move));

                table.AddRow(move.Value, winnerList, move.Value, loserList);
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
