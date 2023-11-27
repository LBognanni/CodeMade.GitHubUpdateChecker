using Newtonsoft.Json.Linq;
using System.Reflection;

namespace CodeMade.GithubUpdateChecker;

public class FileBasedTempData : ITempData
{
    private readonly string _fileName;

    public FileBasedTempData() : this(Assembly.GetExecutingAssembly().GetName().Name + "_updates.tmp")
    {
    }

    public FileBasedTempData(string fileName)
    {
        _fileName = fileName;
        EnsureEmpty();
    }

    private string FileName => Path.Combine(Path.GetTempPath(), _fileName);

    internal void EnsureEmpty()
    {
        if (File.Exists(FileName))
        {
            File.Delete(FileName);
        }
    }

    public void Write<T>(string what, T value)
    {
        JObject data = new JObject();
        if (File.Exists(FileName))
        {
            var contents = File.ReadAllText(FileName);
            data = JObject.Parse(contents);
        }

        if (IsStraightValue<T>())
        {
            data[what] = JObject.FromObject(new ValueWrapper<T>(value));
        }
        else
        {
            data[what] = JObject.FromObject(value!);
        }

        File.WriteAllText(FileName, data.ToString());
    }

    private static bool IsStraightValue<T>() => !typeof(T).IsClass || typeof(T) == typeof(string);

    public T? Read<T>(string what)
    {
        if (!File.Exists(FileName))
            return default;

        var json = File.ReadAllText(FileName);
        var o = JObject.Parse(json);
        var prop = o[what];
        if (prop == null)
            return default;

        if (IsStraightValue<T>())
        {
            return prop.ToObject<ValueWrapper<T>>()!.Value;
        }
        else
        {
            return prop.ToObject<T>();
        }
    }

    class ValueWrapper<T>
    {
        public T Value { get; set; }

        public ValueWrapper(T data)
        {
            Value = data;
        }
    }
}
