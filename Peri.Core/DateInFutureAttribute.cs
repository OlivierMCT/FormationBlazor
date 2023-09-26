using System.ComponentModel.DataAnnotations;

namespace Peri.Core {
    public class DateInFutureAttribute : ValidationAttribute {
        public bool IncludeToday { get; set; }

        public override bool IsValid(object? value) {
            if(value == null) return true;

            DateTime dateToValid = (DateTime)value;
            if (IncludeToday)
                return dateToValid.Date >= DateTime.Today;
            return dateToValid >= DateTime.Now;
        }
    }
}