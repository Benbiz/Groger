using Groger.WebApi.Models.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Groger.WebApi.Models
{
    public class RestQueryParams<Entity> where Entity : class
    {
        private IEnumerable<RestFilter<Entity>> filters = null;
        public IEnumerable<RestFilter<Entity>> Filters
        {
            get
            {
                try
                {
                    if (this.Filter != null && this.Filter.Length != 0)
                    {
                        if (filters == null)
                            filters = JsonConvert.DeserializeObject<List<RestFilter<Entity>>>(Filter);
                        return filters;
                    }
                    else if (filters == null)
                        filters = new List<RestFilter<Entity>>();
                    return filters;
                }
                catch
                {
                    if (filters == null)
                        filters = new List<RestFilter<Entity>>();
                    return filters;
                }
            }
        }

        public string Filter { get; set; }

        [JsonIgnore]
        public string Sorters { get; set; }

        public bool IsOk(Entity e)
        {
            foreach (RestFilter<Entity> filter in Filters)
            {
                if (!filter.Verify(e))
                    return false;
            }
            return true;
        }
    }
}