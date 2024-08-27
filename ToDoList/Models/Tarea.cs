using SQLite;

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
    }
}
