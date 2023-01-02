namespace HotalAppLibrary.Databases
{
    public interface ISqliteDataAccess
    {
        List<T> LoadDate<T, U>(string sqlStatment, U parameters, string connectionStringName);
        void SaveData<T>(string sqlStatment, T parameter, string connectionStringName);
    }
}