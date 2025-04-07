namespace Lap.Model
{
    public class TableColumn<T>
    {
        public string? Header { get; set; }
        public Func<T, object>? CellTemplate { get; set; }

    }
}
