using System;

namespace WebTemplate.MVC.ViewModels
{
    public class Checkbox
    {
        public Checkbox()
        {
        }

        public Checkbox(string name, string value, bool isChecked)
        {
            IsChecked = isChecked;
            Name = name;
            Value = value;
        }

        public string Value { get; set; }
        public string Name { get; private set; }
        public bool IsChecked { get; set; }

        public bool Match(string value)
        {
            return IsChecked && Value.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}