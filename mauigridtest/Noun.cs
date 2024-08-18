namespace mauigridtest;

public class Noun
{
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public string Gender { get; set; }

    public static List<Noun> Nouns =
    [
        new Noun { Text = "Katze", Gender = "die", ImageSource = "cat.png" },
        new Noun { Text = "Hund", Gender = "der", ImageSource = "dog.png" },
        new Noun() { Text = "Ei", Gender = "das", ImageSource = "egg.png" },
        new Noun() { Text = "Milch", Gender = "die", ImageSource = "milk.png" },
        new Noun() { Text = "Apfel", Gender = "der", ImageSource = "apple.png" }
    ];
}