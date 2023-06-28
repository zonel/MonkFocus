using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MonkFocusApp.ViewModels;

/// <summary>
///     This is a base class that all ViewModels inherit from. It implements the INotifyPropertyChanged interface and
///     provides a method for raising the PropertyChanged event.
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    ///     Indicates that a property has changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}