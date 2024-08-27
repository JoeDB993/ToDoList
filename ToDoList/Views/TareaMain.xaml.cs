using ToDoList.ViewModels;

namespace ToDoList.Views;

public partial class TareaMain : ContentPage
{
	private TareaMainViewModel ViewModel;
	public TareaMain()
	{
		InitializeComponent();
		ViewModel = new TareaMainViewModel();
		this.BindingContext = ViewModel;
	}

	/// <summary>
	/// Permite a los desarrolladores de aplicaciones personalizar el comportamiento inmediatamente antes de que la página sea visible
	/// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
		ViewModel.GetAll();
    }
}