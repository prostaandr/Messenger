using Messenger.Client.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Messenger.Client.WPF.Helpers
{
    public class ChatDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SenderText { get; set; }
        public DataTemplate ReciverText { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = item as MessageViemModel;

            if (message.IsSender)
            {
                return SenderText;
            }
            else return ReciverText;
        }
    }
}
