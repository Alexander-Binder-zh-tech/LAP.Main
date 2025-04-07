using Microsoft.AspNetCore.Mvc;

namespace Lap.Model.Pagination;

[ModelBinder(BinderType = typeof(MetadataValueModelBinder))]
public class ColumnFilter
{
    public required string ColumnName { get; set; }
    public required string Value { get; set; }
}