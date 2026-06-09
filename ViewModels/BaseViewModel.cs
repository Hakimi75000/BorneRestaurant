// Permet d'utiliser INotifyPropertyChanged
using System.ComponentModel;

// Permet de récupérer automatiquement le nom
// de la propriété qui a changé
using System.Runtime.CompilerServices;

namespace BorneRestaurant.ViewModels
{
    // Classe de base utilisée par tous les ViewModels.
    // Elle permet de notifier l'interface lorsqu'une donnée change.
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Événement utilisé par WPF pour être informé
        // qu'une propriété a été modifiée.
        public event PropertyChangedEventHandler PropertyChanged;

    // Méthode appelée lorsqu'une propriété change.
    protected void OnPropertyChanged(
        [CallerMemberName] string propertyName = null)
        {
            // Déclenche l'événement PropertyChanged
            // afin que l'interface se mette à jour automatiquement.
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }

}
