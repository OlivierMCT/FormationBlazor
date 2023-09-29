using System.ComponentModel.DataAnnotations;

namespace Peri.Core {
    public class RequiredWithAttribute : ValidationAttribute {
        private readonly string _otherPropertyName;

        public RequiredWithAttribute(string otherPropertyName) {
            this._otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            var objecType = validationContext.ObjectType;
            var otherProperty = objecType.GetProperty(_otherPropertyName)
                ?? throw new InvalidOperationException($"no property [{_otherPropertyName}] in class {objecType.Name}");

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            if (value == null && otherValue != null)
                return new ValidationResult(ErrorMessage);            

            return ValidationResult.Success;
        }
    }
}
