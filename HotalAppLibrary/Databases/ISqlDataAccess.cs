namespace HotalAppLibrary.Databases
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatement, U parameter, string connectionStringName, bool isStoredProcedure = false);

        ////  **********************        Yevhen Answer       **********************  ////
        List<T> LoadData1<T>(string sqlStatement, object parameter, string connectionStringName, bool isStoredProcedure = false);

        void SaveData<T>(string sqlStatement, T parameter, string connectionStringName, bool isStoredProcedure = false);
    }
}