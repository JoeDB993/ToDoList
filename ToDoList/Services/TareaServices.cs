using SQLite;
using ToDoList.Models;


namespace ToDoList.Services
{
    public class TareaServices
    {
        private readonly SQLiteConnection dbConnection;

        public TareaServices()
        {
            // Construir ruta
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tarea.db3");
            // Inicializar el objeto
            dbConnection = new SQLiteConnection(dbPath);

            // Crear la tabla de tareas
            dbConnection.CreateTable<Tarea>();

            
        }

        /// <summary>
        /// Lista de todas las tareas
        /// </summary>
        /// <returns>Una lista de tareas</returns>
        public List<Tarea> GetAll()
        {
            var res = dbConnection.Table<Tarea>().ToList();
            return res;
        }

        /// <summary>
        /// Guarda un registro en la base de datos
        /// </summary>
        /// <param name="tarea">Objeto con los datos de la tarea a guardar</param>
        /// <returns>Cantidad de registros ingresados</returns>
        public int Insert(Tarea tarea)
        {
            return dbConnection.Insert(tarea);
        }

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="tarea">Objeto con los datos de la tarea a actualizar</param>
        /// <returns>Cantidad de registros actualizados</returns>
        public int Update(Tarea tarea)
        {
            return dbConnection.Update(tarea);
        }

        /// <summary>
        /// Elimina un registro de la base de datos
        /// </summary>
        /// <param name="tarea">Objeto con los datos a eliminar</param>
        /// <returns>Cantidad de registros eliminados</returns>
        public int Delete(Tarea tarea)
        {
            return dbConnection.Delete(tarea);
        }
    }
}
