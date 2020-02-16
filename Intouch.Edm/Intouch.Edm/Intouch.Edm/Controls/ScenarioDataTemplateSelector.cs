using Intouch.Edm.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Intouch.Edm.Controls
{
    class ScenarioDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ObservableRangeCollection<Scenario>)((ListView)container).ItemsSource).IndexOf(item as Scenario) % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}