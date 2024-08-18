namespace mauigridtest;

public class Noun
{
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public string AudiResource { get; set; }
    public string Gender { get; set; }

    public static List<Noun> Nouns =
    [
        new Noun { Text = "Katze", Gender = "die", ImageSource = "cat.png", AudiResource = "die katze.m4a" },
        new Noun { Text = "Hund", Gender = "der", ImageSource = "dog.png",AudiResource = "der hund.m4a" },
        new Noun() { Text = "Ei", Gender = "das", ImageSource = "egg.png", AudiResource = "das ei.m4a" },
        new Noun() { Text = "Milch", Gender = "die", ImageSource = "milk.png", AudiResource = "die milch.m4a" },
        new Noun() { Text = "Apfel", Gender = "der", ImageSource = "apple.png", AudiResource = "der apfel.m4a" }
    ];
}