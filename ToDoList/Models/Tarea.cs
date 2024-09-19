using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ToDoList.Models
{
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Nombre { get; set; }
        [NotNull]
        public string Descripcion { get; set; }
        public string Estado { get; set; } // "Por hacer", "En progreso", "Finalizada" 
        public string Prioridad { get; set; }
        


        private bool isSelected;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(); // Notifica de que la propiedad ha cambiado  
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
