namespace HotalAppLibrary.Databases
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatement, U parameter, string connectionStringName, bool isStoredProcedure = false);
        void SaveData<T>(string sqlStatement, T parameter, string connectionStringName, bool isStoredProcedure = false);
    }
}