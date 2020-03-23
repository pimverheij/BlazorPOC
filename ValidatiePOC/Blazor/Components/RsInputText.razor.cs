using System;
using System.Globalization;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

// https://chrissainty.com/building-custom-input-components-for-blazor-using-inputbase
// https://github.com/chrissainty/BuildingCustomInputComponentsForBlazorUsingInputBase
namespace ValidatiePOC.Blazor.Components
{
    public class RsInputTextBase<T> : InputBase<T>
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public Expression<Func<T>> ValidationFor { get; set; }

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (typeof(T) == typeof(string))
            {
                result = (T) (value as object);
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(T) == typeof(int))
            {
                int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue);
                result = (T) (object) parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(T) == typeof(decimal))
            {
                decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var parsedValue);
                result = (T) (object) parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(T).IsEnum)
            {
                // There's no non-generic Enum.TryParse (https://github.com/dotnet/corefx/issues/692)
                try
                {
                    result = (T) Enum.Parse(typeof(T), value);
                    validationErrorMessage = null;

                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";

                    return false;
                }
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(T)}'.");
        }
    }
}