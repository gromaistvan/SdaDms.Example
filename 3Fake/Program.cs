const string  input = """
                      1
                      99
                      10
                      73
                      """;
const string output = """
                      [Guess(100)]
                      [Bigger]
                      [Guess(100)]
                      [Smaller]
                      [Guess(100)]
                      [Bigger]
                      [Guess(100)]
                      [End(00:01)]
                      """;
var recorded = new StringBuilder();
var reader = new StringReader(input);
var writer = new StringWriter(recorded);
var settings = new NameValueCollection
{
    ["MaxGuessNumber"] = "100"
};
var random = new Random(0);
var time = new FakeTimeProvider
{
    AutoAdvanceAmount = TimeSpan.FromSeconds(1.0)
};
var culture = new CultureInfo("iv");
using (var fakeResources = new ResourceWriter(Path.Combine(".", "Fake.resources")))
{
    fakeResources.AddResource(nameof(Resources.Guess), "[Guess({0:D})]\r\n");
    fakeResources.AddResource(nameof(Resources.Bigger), "[Bigger]");
    fakeResources.AddResource(nameof(Resources.Smaller), "[Smaller]");
    fakeResources.AddResource(nameof(Resources.End), "[End({0:mm\\:ss})]");
}
var resources = ResourceManager.CreateFileBasedResourceManager("Fake", resourceDir: ".", usingResourceSet: null);
IGame game = new GuessGameLoose(
    reader, 
    writer, 
    settings, 
    random, 
    time,
    culture,
    resources);
game.Play();
if (output != $"{recorded}") throw new InvalidOperationException("Test failed!");
