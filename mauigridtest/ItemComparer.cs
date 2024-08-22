using mauigridtest.Models;

namespace mauigridtest;

public class ItemComparer : IEqualityComparer<WiktionaryNoun>
{
    public bool Equals(WiktionaryNoun x, WiktionaryNoun y)
    {
        // Check if both objects are null
        if (x == null && y == null) return true;

        // Check if one of the objects is null
        if (x == null || y == null) return false;

        // Compare the Name properties
        return x.Lemma == y.Lemma;
    }

    public int GetHashCode(WiktionaryNoun obj)
    {
        // Use the Name property to calculate the hash code
        return obj.Lemma == null ? 0 : obj.Lemma.GetHashCode();
    }
}