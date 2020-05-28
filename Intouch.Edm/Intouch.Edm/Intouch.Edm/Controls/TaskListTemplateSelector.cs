using Intouch.Edm.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace Intouch.Edm.Controls
{
    internal class TaskListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ObservableRangeCollection<Announcement>)((ListView)container).ItemsSource).IndexOf(item as Announcement) % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}