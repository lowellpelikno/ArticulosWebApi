

namespace Utilerias
{
    public class ResultBase
    {
        public bool Success { get; set; } = false;
        public string? Mensaje { get; set; }
        public int? TotalCount { get; set; }
        //public Exception? Error { get; set; }
        public List<string>? ValidationErrors { get; set; } = null;
    }
    public class ResultObject<T> : ResultBase
    {
        public T? Data { get; set; }
    }
    public class ResultList<T> : ResultBase
    {
        public List<T>? Data { get; set; }
    }
}
 