using Intouch.Edm.Models;
using MvvmHelpers;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Intouch.Edm.Controls
{
    class AlternateColorDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ObservableRangeCollection<TaskItem>)((ListView)container).ItemsSource).IndexOf(item as TaskItem) % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}