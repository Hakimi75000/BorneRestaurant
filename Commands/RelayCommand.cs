// Permet d'utiliser le type Action
using System;

// Permet d'utiliser l'interface ICommand de WPF
using System.Windows.Input;

namespace BorneRestaurant.Commands
{
    // Classe générique permettant d'exécuter une action
    // lorsqu'un bouton est cliqué.
    // Elle est utilisée dans le pattern MVVM.
    public class RelayCommand : ICommand
    {
        // Stocke la méthode à exécuter
        private readonly Action execute;


    // Constructeur
    // Reçoit la méthode qui devra être exécutée
    public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        // Indique si la commande peut être exécutée.
        // Ici on retourne toujours true,
        // donc le bouton est toujours actif.
        public bool CanExecute(object parameter)
        {
            return true;
        }

        // Méthode exécutée lorsque la commande est appelée
        // (par exemple lorsqu'un utilisateur clique sur un bouton)
        public void Execute(object parameter)
        {
            execute();
        }

        // Événement utilisé par WPF pour savoir
        // si l'état de la commande a changé.
        // Dans ce projet il n'est pas utilisé.
        public event EventHandler CanExecuteChanged;
    }


}
