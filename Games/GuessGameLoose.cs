namespace SdaDms.Example.Games;

public sealed class GuessGameLoose: IGame
{
    private TextReader Input { get; }

    private TextWriter Output { get; }

    public NameValueCollection Settings { get; }

    private Random Random { get; }

    private TimeProvider Time { get; }

    public CultureInfo Culture { get; }

    private ResourceManager Resources { get; }

    public GuessGameLoose(
        TextReader input,
        TextWriter output,
        NameValueCollection settings,
        Random random,
        TimeProvider time,
        CultureInfo culture,
        ResourceManager resources)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        Output = output ?? throw new ArgumentNullException(nameof(output));
        Input = input ?? throw new ArgumentNullException(nameof(input));
        Random = random ?? throw new ArgumentNullException(nameof(random));
        Time = time ?? throw new ArgumentNullException(nameof(time));
        Culture = culture ?? throw new ArgumentNullException(nameof(culture));
        Resources = resources ?? throw new ArgumentNullException(nameof(resources));
    }

    private static int Parse(string? line, int defaultValue) => line is { Length: > 0 } && int.TryParse(line, out int n) ? n : defaultValue;

    private void GameImplementation()
    {
        int max = Parse(Settings["MaxGuessNumber"], 100);
        int secret = Random.Next(1, max + 1);
        int guess;
        DateTimeOffset start = Time.GetLocalNow();
        do
        {
            Output.Write(Resources.GetString("Guess", Culture) ?? "", max);
            string? line = Input.ReadLine();
            guess = Parse(line, 0);
            if (guess < secret)
            {
                Output.WriteLine(Resources.GetString("Bigger", Culture));
            }
            else if (guess > secret)
            {
                Output.WriteLine(Resources.GetString("Smaller", Culture));
            }
        }
        while (guess != secret);
        DateTimeOffset end = Time.GetLocalNow();
        Output.Write(Resources.GetString("End", Culture) ?? "", end - start);
    }

    void IGame.Play() => GameImplementation();
}