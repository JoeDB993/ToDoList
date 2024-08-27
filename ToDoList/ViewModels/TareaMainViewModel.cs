

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
        //Colección de datos para interactuar con la vista
        [ObservableProperty]
        private ObservableCollection<Tarea> tareaCollection = new ObservableCollection<Tarea>();

        //Llamamos a la clase en donde configuramos la base de datos
        private readonly TareaServices TareaServices;

        //Constructor
        public TareaMainViewModel()
        {
            TareaServices = new TareaServices();
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


        /// <summary>
        /// Muestra el listado de empleados
        /// </summary>
        public void GetAll()
        {
            var getAll = TareaServices.GetAll();

            //Validar si la lista tiene registros
            if (getAll.Count > 0)
            {
                TareaCollection.Clear();
                foreach (var tarea in getAll)
                {
                    TareaCollection.Add(tarea);
                }
            
            }

        }

        /// <summary>
        /// Redirecciona a la lista de agregar, editar tarea
        /// </summary>
        /// <returns>Vista de agregar/editar empleado</returns>
        [RelayCommand]
        private async Task goToAddTareaForm() 
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new AddTareaForm());
        }


        /// <summary>
        /// Muestra las opciones al seleccionar un registro de tarea
        /// </summary>
        /// <param name="tarea">Objeto seleccionado para actualizar o eliminar el registro</param>
        /// <returns>Opciones al seleccionar el registro de empleado</returns>
        [RelayCommand]
        private async Task selectTarea(Tarea tarea)
        {
            try
            {
                string actualizar = "Actualizar";
                string eliminar = "Eliminar");
                //ALerta de Consulta y dicha alerta trae una respuesta
                string res = await App.Current!.MainPage!.DisplayActionSheet("OPCIONES", "Cancelar", null, actualizar, eliminar);
                if (res == actualizar)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddTareaForm(tarea));
                } else if (res == eliminar) 
                {
                    bool respuesta = await App.Current!.MainPage!.DisplayAlert("ELIMINAR TAREA", "¿Desea eliminar el registro de la tarea?", "Si","No");
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
                Alerta ("ERROR", ex.Message);
            }      
        }

    }
}
