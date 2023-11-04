IGame game = new GuessGameLoose(
    Console.In,
    Console.Out,
    ConfigurationManager.AppSettings,
    Random.Shared,
    TimeProvider.System,
    CultureInfo.CurrentUICulture,
    Resources.ResourceManager);
game.Play();
