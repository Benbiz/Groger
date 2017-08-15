using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Groger.WebApi.Models.Filters
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FilterOperators
    {
        Eq,
        Le,
        Ge,
        Lt,
        Gt,
        Like
    }
}