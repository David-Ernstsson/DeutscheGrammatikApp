using System.Globalization;

namespace mauigridtest.Models;

public class GameNoun
{
    public int Id { get; set; }
    public string Singular { get; set; }
    public string Plural { get; set; }
    public string ImagePath
    {
        get
        {
            var singularAlphaNumerical = Singular.Replace("ä", "ae")
                .Replace("ö", "oe")
                .Replace("ü", "ue")
                .Replace("ß", "ss")
                .Replace("Ä", "Ae")
                .Replace("Ö", "Oe")
                .Replace("Ü", "Ue")
                .Replace("é", "ee")
                .Replace("É", "Ee")
                .Replace("-", "_")
                ;

            return $"{Gender}_{singularAlphaNumerical}.png".ToLower(CultureInfo.InvariantCulture);
        }
    }

    public string? AudioPath { get; set; }
    public string Gender { get; set; }

    public GameNoun(string singular, string genus, string plural)
    {
        Singular = singular;
        Gender = genus switch
        {
            "m" => "der",
            "f" => "die",
            "n" => "das",
            _ => throw new ArgumentException("Invalid genus", nameof(genus))
        };
        Plural = plural;
    }

    public GameNoun()
    {

    }
}