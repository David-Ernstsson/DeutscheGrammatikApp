using SQLite;

namespace mauigridtest.Models;

public class Noun
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public string AudioResource { get; set; }
    public string Gender { get; set; }

    public static List<Noun> Nouns =
    [
        new Noun { Text = "Katze", Gender = "die", ImageSource = "cat.png", AudioResource = "die katze.m4a" },
        new Noun { Text = "Hund", Gender = "der", ImageSource = "dog.png",AudioResource = "der hund.m4a" },
        new Noun() { Text = "Ei", Gender = "das", ImageSource = "egg.png", AudioResource = "das ei.m4a" },
        new Noun() { Text = "Milch", Gender = "die", ImageSource = "milk.png", AudioResource = "die milch.m4a" },
        new Noun() { Text = "Apfel", Gender = "der", ImageSource = "apple.png", AudioResource = "der apfel.m4a" }
    ];
}