using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Groger.WebApi.Models.Filters
{
    public class RestFilter<T> where T : class
    {
        [Required]
        public string Property { get; set; }

        [Required]
        public FilterOperators Operator { get; set; }

        public string Value { get; set; }

        public object GetValue()
        {
            PropertyInfo propinfo = GetProperty();
            if (propinfo == null)
                return null;
            Type t = propinfo.PropertyType;

            try
            {
                return (JsonConvert.DeserializeObject(Value, t));
            }
            catch (Exception e)
            {
                try
                {
                    return Convert.ChangeType(Value, t);
                }
                catch { return null; }
            }
        }

        private PropertyInfo _prop = null;
        public PropertyInfo GetProperty()
        {
            if (_prop != null)
                return _prop;
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {

                if (String.Equals(property.Name, Property,
                   StringComparison.OrdinalIgnoreCase))
                {
                    _prop = property;
                    return _prop;
                }
            }
            return null;
        }

        public bool Verify(T entity)
        {
            PropertyInfo info = GetProperty();
            if (info == null)
                return true;

            object ActualValue = info.GetValue(entity);
            object FilterValue = GetValue();

            IComparable CompareActual = ActualValue as IComparable;

            switch (Operator)
            {
                case FilterOperators.Eq:
                    return ActualValue == FilterValue || (ActualValue != null && ActualValue.Equals(FilterValue));
                case FilterOperators.Le:
                case FilterOperators.Ge:
                case FilterOperators.Lt:
                case FilterOperators.Gt:
                    if (CompareActual == null)
                        return false;
                    break;
                case FilterOperators.Like:
                    if ((ActualValue as string) == (FilterValue as string))
                        return true;
                    break;
            }

            switch (Operator)
            {
                case FilterOperators.Le:
                    return CompareActual.CompareTo(FilterValue) <= 0;
                case FilterOperators.Ge:
                    return CompareActual.CompareTo(FilterValue) >= 0;
                case FilterOperators.Lt:
                    return CompareActual.CompareTo(FilterValue) < 0;
                case FilterOperators.Gt:
                    return CompareActual.CompareTo(FilterValue) > 0;
                case FilterOperators.Like:
                    string ActualString = ActualValue as string;
                    string FilterString = FilterValue as string;
                    return ActualString != null && FilterString != null && ActualString.IndexOf(FilterString, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
            return true;
        }
    }
}