namespace WpfApp3
{
    #region

    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    #endregion

    public class EmployeeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var bindingGroup = (BindingGroup)value;

            if (bindingGroup.Items.Count == 2)
            {
                var employee = (EmployeeModel)bindingGroup.Items[1];

                var firstname = (string)bindingGroup.GetValue(employee, "Firstname");
                var lastname = (string)bindingGroup.GetValue(employee, "Lastname");

                if (firstname.Length == 0) return new ValidationResult(false, "Firstname can not be empty.");

                if (lastname.Length == 0) return new ValidationResult(false, "Lastname can not be empty.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidationGroup
    {
    }
}