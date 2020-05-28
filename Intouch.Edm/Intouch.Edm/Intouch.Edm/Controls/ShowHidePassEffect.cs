using Xamarin.Forms;

namespace Intouch.Edm.Controls
{
    public class ShowHidePassEffect : RoutingEffect
    {
        public string EntryText { get; set; }

        public ShowHidePassEffect() : base("Xamarin.ShowHidePassEffect")
        {
        }
    }
}