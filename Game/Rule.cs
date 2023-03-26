namespace Game
{
    public static class Rule
    {
        public static int GetResult(
            Dictionary<string, string> moves,
            KeyValuePair<string, string> userMove,
            KeyValuePair<string, string> compMove)
        {
            if (moves.WhoILoseToIfIHave(userMove).Contains(compMove.Value))
            {
                return 1;
            }

            if (moves.WhoIWinIfIHave(userMove).Contains(compMove.Value))
            {
                return -1;
            }

            return 0;
        }

        public static List<string> WhoIWinIfIHave(
            this Dictionary<string, string> moves,
            KeyValuePair<string, string> userMove)
        {
            var halfRound = (moves.Count - 1) / 2;
            var userPickIndex = Convert.ToInt32(userMove.Key);
            var startIndex =
                userPickIndex + halfRound >= moves.Count
                    ? userPickIndex + halfRound - moves.Count
                    : userPickIndex + halfRound;

            var result = new List<string>();

            for (var i = startIndex; halfRound > 0; halfRound--, i++)
            {
                if (i >= moves.Count)
                {
                    i = 0;
                }

                result.Add(moves[(i + 1).ToString()]);
            }

            return result;
        }

        public static List<string> WhoILoseToIfIHave(
            this Dictionary<string, string> moves,
            KeyValuePair<string, string> userMove)
        {
            var result = new List<string>();
            var doTimes = (moves.Count - 1) / 2;

            for (var i = Convert.ToInt32(userMove.Key); doTimes > 0; doTimes--, i++)
            {
                if (i >= moves.Count)
                {
                    i = 0;
                }

                result.Add(moves[(i + 1).ToString()]);
            }

            return result;
        }
    }
}
