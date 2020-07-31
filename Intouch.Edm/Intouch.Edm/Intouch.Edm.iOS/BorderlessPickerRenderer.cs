using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Intouch.Edm.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Intouch.Edm.iOS
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
        BorderlessPicker element;
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (BorderlessPicker)this.Element;

            if (Control != null)
            {
                Control.Layer.CornerRadius = 5;
            }
        }
    }
}