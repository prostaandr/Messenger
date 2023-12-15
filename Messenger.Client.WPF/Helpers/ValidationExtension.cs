using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.WPF.Helpers
{
    public static class ValidationExtension
    {
        public static Validator Rules(this string content) => new Validator(content); 
    }
}
