
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public partial class AddTareaFormViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;


        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string descripcion;
        
        [ObservableProperty]
        private string estado;

        public ObservableCollection<string> Estados { get; } = new ObservableCollection<string>
        {
            "Por hacer",
            "En proceso",
            "Terminada"
        };

        
        private readonly TareaServices TareaServices;


        /// <summary>
        /// Este constructor nos va a servir para cuando vamos a crear un registro
        /// </summary>
        public AddTareaFormViewModel()
        {
            TareaServices = new TareaServices();
        }


        /// <summary>
        /// Constructor que se utiliza al editar un empleado
        /// </summary>
        /// <param name="Tarea">Objeto con datos a editar</param>
        public AddTareaFormViewModel(Tarea Tarea)
        {
            TareaServices = new TareaServices();
            Id = Tarea.Id;
            Nombre = Tarea.Nombre;
            Descripcion = Tarea.Descripcion;
            Estado = Tarea.Estado;
        }

        /// <summary>
        /// Muestra un mensaje de alerta personalizado
        /// </summary>
        /// <param name="Titulo">Título de la alerta, por ejemmplo, ERROR, ADVERTENCIA, etc </param>
        /// <param name="Mensaje">Mensaje a mostrar en la alerta</param>
        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }
        
        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                //Izquierda: Atriburo del Objeto
                //Derecha: Atributo del view Model
                Tarea Tarea = new Tarea
                {
                    Id = Id,
                    Nombre = Nombre,
                    Descripcion = Descripcion,
                    Estado = Estado
                };


                if (Id == 0)
                {
                    TareaServices.Insert(Tarea);
                }
                else 
                {
                    TareaServices.Update(Tarea);
                }
                await App.Current!.MainPage!.Navigation.PopAsync();

            }
            catch (Exception ex) 
            {
                Alerta("ERROR", ex.Message);
            }
        }
    }
}
