using Newtonsoft.Json;
using Serilog;

namespace SeperateConcernsLogger.Utilities;


public static class WLogger
{

    private static string _template = "{existing_data}{updated_data}{log_type}{message}{user_id}{created_at}";
    private static string _now = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
    private static string _success = "Başarılı";

    /// <summary>
    /// Create işleminde başarılı ya da hatalı log kaydı atar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="existing_data"></param>
    /// <param name="userId"></param>
    public static void Create<T>(T existing_data, int userId, string errorMessage = null)
    {
        if (errorMessage == null)
            Log.Information(_template, JsonConvert.SerializeObject(existing_data), null, LogType.Create.ToString(), _success, userId, _now);
        else
            Log.Error(_template, JsonConvert.SerializeObject(existing_data), null, LogType.Create.ToString(), errorMessage, userId, _now);
    }

    /// <summary>
    /// Update işleminde başarılı ya da hatalı log kaydı atar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="existing_data"></param>
    /// <param name="updated_data"></param>
    /// <param name="userId"></param>
    public static void Update<T>(T existing_data, T updated_data, int userId, string errorMessage = null)
    {
        if (errorMessage == null)
            Log.Information(_template, JsonConvert.SerializeObject(existing_data), JsonConvert.SerializeObject(updated_data), LogType.Update.ToString(), _success, userId, _now);
        else
            Log.Error(_template, JsonConvert.SerializeObject(existing_data), JsonConvert.SerializeObject(updated_data), LogType.Update.ToString(), errorMessage, userId, _now);
    }

    /// <summary>
    /// Silme işleminde başarılı ya da hatalı log kaydı atar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="existing_data"></param>
    /// <param name="userId"></param>
    public static void Delete<T>(T existing_data, int userId, string errorMessage = null)
    {
        if (errorMessage == null)
            Log.Information(_template, JsonConvert.SerializeObject(existing_data), null, LogType.Delete.ToString(), _success, userId, _now);
        else
            Log.Error(_template, JsonConvert.SerializeObject(existing_data), null, LogType.Delete.ToString(), errorMessage, userId, _now);
    }
}