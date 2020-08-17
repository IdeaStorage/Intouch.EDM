using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Views;
using Intouch.Edm.Droid.Renderers.TabbedDemo.Droid;
using Intouch.Edm.Models;
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

                if (ViewGroup != null && ViewGroup.ChildCount > 0)
                {
                    BottomNavigationMenuView bottomNavigationMenuView = FindChildOfType<BottomNavigationMenuView>(ViewGroup);

                    if (bottomNavigationMenuView != null)
                    {
                        var shiftMode = bottomNavigationMenuView.Class.GetDeclaredField("mShiftingMode");

                        shiftMode.Accessible = true;
                        shiftMode.SetBoolean(bottomNavigationMenuView, false);
                        shiftMode.Accessible = false;
                        shiftMode.Dispose();

                        for (var i = 0; i < bottomNavigationMenuView.ChildCount; i++)
                        {
                            BottomNavigationItemView item = bottomNavigationMenuView.GetChildAt(i) as BottomNavigationItemView;
                            if (item == null)
                            {
                                continue;
                            }

                            item.SetShiftingMode(false);
                            item.SetChecked(item.ItemData.IsChecked);
                        }

                        if (bottomNavigationMenuView.ChildCount > 0)
                        {
                            bottomNavigationMenuView.UpdateMenuView();
                        }
                    }
                }
            }

            private T FindChildOfType<T>(ViewGroup viewGroup) where T : Android.Views.View
            {
                if (viewGroup == null || viewGroup.ChildCount == 0) return null;

                for (var i = 0; i < viewGroup.ChildCount; i++)
                {
                    var child = viewGroup.GetChildAt(i);

                    T typedChild = child as T;
                    if (typedChild != null) return typedChild;

                    if (!(child is ViewGroup)) continue;

                    var result = FindChildOfType<T>(child as ViewGroup);

                    if (result != null) return result;
                }

                return null;
            }
        }
    }
}