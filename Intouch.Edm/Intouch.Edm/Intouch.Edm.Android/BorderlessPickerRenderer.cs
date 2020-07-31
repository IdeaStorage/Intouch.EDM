using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Intouch.Edm.Controls;
using Intouch.Edm.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace Intouch.Edm.Droid
{

    public class BorderlessPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        public BorderlessPickerRenderer(Android.Content.Context context) : base(context)
        {
            AutoPackage = false;
        }
        BorderlessPicker element;
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (BorderlessPicker)this.Element;

            if (Control != null)
            {
                Control.Background = AddBorderToPicker();
                Control.Gravity = GravityFlags.CenterHorizontal;
               // Control.SetShadowLayer(10.0f, 0.0f, 2.0f, Android.Graphics.Color.Black);
            }
        }

        public LayerDrawable AddBorderToPicker()
        {
            float[] outerR = new float[8];
            outerR[0] = 45;
            outerR[1] = 45;
            outerR[2] = 45;
            outerR[3] = 45;
            outerR[4] = 45;
            outerR[5] = 45;
            outerR[6] = 45;
            outerR[7] = 45;

            ShapeDrawable pickerBorder = new ShapeDrawable(new RoundRectShape(outerR,null, null));
       
            pickerBorder.Paint.Color = Android.Graphics.Color.LightGray;
             
            pickerBorder.Paint.SetStyle(Paint.Style.FillAndStroke);
            pickerBorder.SetPadding(5, 5, 5, 5);
         
            Drawable[] layers = { pickerBorder };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
            
            return layerDrawable;
        }
    }

}