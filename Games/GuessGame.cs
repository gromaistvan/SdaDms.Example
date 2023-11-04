namespace SdaDms.Example.Games;

public sealed class GuessGame: IGame
{
    private int Parse(string? line, int defaultValue) => line is { Length: > 0 } && int.TryParse(line, out int n) ? n : defaultValue;

    private void GameImplementation()
    {
        int max = Parse(ConfigurationManager.AppSettings["MaxGuessNumber"], 100);
        int secret = Random.Shared.Next(1, max + 1);
        int guess;
        DateTimeOffset start = DateTimeOffset.Now;
        do
        {
            Console.Write(Resources.ResourceManager.GetString("Guess", CultureInfo.CurrentUICulture) ?? "", max);
            string? line = Console.ReadLine();
            guess = Parse(line, 0);
            if (guess < secret)
            {
                Console.WriteLine(Resources.ResourceManager.GetString("Bigger", CultureInfo.CurrentUICulture));
            }
            else if (guess > secret)
            {
                Console.WriteLine(Resources.ResourceManager.GetString("Smaller", CultureInfo.CurrentUICulture));
            }
        }
        while (guess != secret);
        DateTimeOffset end = DateTimeOffset.Now;
        Console.Write(Resources.ResourceManager.GetString("End", CultureInfo.CurrentUICulture) ?? "", end - start);
    }

    void IGame.Play() => GameImplementation();
}