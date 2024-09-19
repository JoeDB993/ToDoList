using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    public partial class TareaMainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Tarea> tareaCollection = new ObservableCollection<Tarea>();

        private readonly TareaServices TareaServices;

        public TareaMainViewModel()
        {
            TareaServices = new TareaServices();
        }

        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        public void GetAll()
        {
            var getAll = TareaServices.GetAll();
            if (getAll.Count > 0)
            {
                TareaCollection.Clear();
                foreach (var tarea in getAll)
                {
                    TareaCollection.Add(tarea);
                }
            }
        }

        [RelayCommand]
        private async Task goToAddTareaForm()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new AddTareaForm());
        }

        [RelayCommand]
        private async Task SelectTarea(Tarea tarea)
        {
            try
            {
                string actualizar = "Actualizar";
                string eliminar = "Eliminar";
                string res = await App.Current!.MainPage!.DisplayActionSheet("OPCIONES", "Cancelar", null, actualizar, eliminar);
                if (res == actualizar)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddTareaForm(tarea));
                }
                else if (res == eliminar)
                {
                    bool respuesta = await App.Current!.MainPage!.DisplayAlert("ELIMINAR TAREA", "¿Desea eliminar el registro de la tarea?", "Si", "No");
                    if (respuesta)
                    {
                        int del = TareaServices.Delete(tarea);
                        if (del > 0)
                        {
                            TareaCollection.Remove(tarea);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        // Eliminar todas las tareas seleccionadas
        [RelayCommand]
        private void EliminarTareasSeleccionadas()
        {
            var tareasAEliminar = TareaCollection.Where(t => t.IsSelected).ToList();
            if (tareasAEliminar.Count == 0)
            {
                Alerta("ADVERTENCIA", "No hay tareas seleccionadas para eliminar.");
                return;
            }

            foreach (var tarea in tareasAEliminar)
            {
                int del = TareaServices.Delete(tarea);
                if (del > 0)
                {
                    TareaCollection.Remove(tarea);
                }
            }
        }

        // Actualizar la tarea seleccionada
        [RelayCommand]
        private async Task ActualizarTareaSeleccionada()
        {
            
            var tareasSeleccionadas = TareaCollection.Where(t => t.IsSelected).ToList();

            
            if (!tareasSeleccionadas.Any())
            {
                Alerta("ADVERTENCIA", "No hay tareas seleccionadas para actualizar.");
                return;
            }

            
            if (tareasSeleccionadas.Count > 1)
            {
                Alerta("ADVERTENCIA", "Solo puede seleccionar una tarea para actualizar.");
                return;
            }

            
            var tareaSeleccionada = tareasSeleccionadas.First();
            await App.Current.MainPage.Navigation.PushAsync(new AddTareaForm(tareaSeleccionada));
        }
    }
}

