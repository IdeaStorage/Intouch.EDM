using Intouch.Edm.iOS.Renderers;
using Intouch.Edm.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScenarioListPage), typeof(InlineTabbedPageRenderer))]

namespace Intouch.Edm.iOS.Renderers
{
    public class InlineTabbedPageRenderer : TabbedRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (TabBar.Items != null)
            {
                var items = TabBar.Items;
                foreach (var t in items)
                {
                    var txtFont = new UITextAttributes()
                    {
                        Font = UIFont.SystemFontOfSize(20),
                        TextColor = Color.FromHex("#ffffff").ToUIColor()
                    };

                    t.SetTitleTextAttributes(txtFont, UIControlState.Normal);
                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach (var vc in ViewControllers)
            {
                vc.TabBarItem.TitlePositionAdjustment = new UIOffset(0, -10);
            }
        }
    }
}