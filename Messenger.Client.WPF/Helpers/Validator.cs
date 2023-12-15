using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Messenger.Client.WPF.Helpers
{
    public class Validator
    {
        private string _content;
        private List<string> _errors { get; set; } 

        public Validator(string content)
        {
            _content = content;
            _errors = new List<string>();
        }

        public Validator NotEmpty()
        {
            if (_content == null) _content = "";
            if (_content == "") _errors.Add("Значение не может быть пустым");
            return this;
        }

        public Validator MinCharacters(int count)
        {
            if (_content.Length < count) _errors.Add($"Длина строки должна быть больше либо равна: {count}"); 
            return this;
        }

        public Validator MaxCharacters(int count)
        {
            if (_content.Length > count) _errors.Add($"Длина строки должна быть меньше либо равна:  {count}");
            return this;
        }

        public Validator Compliance(string value, string title)
        {
            if (_content != value) _errors.Add($"Значение не подтвержденно в поле \"{title}\"");
            return this;
        }

        public List<string> Validate()
        {
            if (_errors.Count > 0) return _errors;
            return new List<string>();
        }
    }
}
