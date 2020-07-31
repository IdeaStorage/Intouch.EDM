using Intouch.Edm.Controls;
using Intouch.Edm.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]

namespace Intouch.Edm.iOS
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TextAlignment = UITextAlignment.Center;
                Control.BackgroundColor = UIColor.LightGray;
                Control.TextColor = UIColor.Black;
                Control.Layer.CornerRadius = 15;
                Control.Layer.MasksToBounds = true;
            }
        }
    }
}