using Android.Support.Design.Widget;
using Android.Views;
using Intouch.Edm.Droid.Renderers.TabbedDemo.Droid;
using Intouch.Edm.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MainPage), typeof(MyTabbedPageRenderer))]

namespace Intouch.Edm.Droid.Renderers
{
    namespace TabbedDemo.Droid
    {
        public class MyTabbedPageRenderer : TabbedPageRenderer
        {
            public MyTabbedPageRenderer(Android.Content.Context context) : base(context)
            {
            }

            protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
            {
                base.OnElementChanged(e);

                if (e.OldElement == null && e.NewElement != null)
                {
                    for (int i = 0; i <= this.ViewGroup.ChildCount - 1; i++)
                    {
                        var childView = this.ViewGroup.GetChildAt(i);
                        if (childView is ViewGroup viewGroup)
                        {
                            for (int j = 0; j <= viewGroup.ChildCount - 1; j++)
                            {
                                var childRelativeLayoutView = viewGroup.GetChildAt(j);
                                bool isLayoutView = childRelativeLayoutView is BottomNavigationView;
                                if (isLayoutView)
                                {
                                    ((BottomNavigationView)childRelativeLayoutView).ItemIconTintList = null;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}