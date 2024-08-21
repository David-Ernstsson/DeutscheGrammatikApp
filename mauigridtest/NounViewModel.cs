using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using mauigridtest.Models;

namespace mauigridtest;

public partial class NounViewModel : ObservableObject
{
    [ObservableProperty]
    private Noun currentNoun;

    [ObservableProperty]
    private string someText;

    partial void OnCurrentNounChanging(Noun value)
    {
        Debug.WriteLine($"Name is about to change to {value}");
    }

    partial void OnCurrentNounChanged(Noun value)
    {
        Debug.WriteLine($"Name has changed to {value}");
    }
}