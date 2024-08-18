using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mauigridtest;

[INotifyPropertyChanged]
public partial class MainViewModel
{
    [ObservableProperty]
    private string name;
    partial void OnNameChanging(string value)
    {
        Debug.WriteLine($"Name is about to change to {value}");
    }
    partial void OnNameChanged(string value)
    {
        Debug.WriteLine($"Name has changed to {value}");
    }
}