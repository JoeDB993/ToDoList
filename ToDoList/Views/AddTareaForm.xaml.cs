using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Views;

public partial class AddTareaForm : ContentPage
{
	private AddTareaFormViewModel ViewModel;

	public AddTareaForm()
	{
        InitializeComponent();
		ViewModel = new AddTareaFormViewModel();
		BindingContext = ViewModel;
	
	}
	public AddTareaForm(Tarea tarea)
	{
		InitializeComponent();
		ViewModel = new AddTareaFormViewModel(tarea);
		BindingContext = ViewModel;

	}
}