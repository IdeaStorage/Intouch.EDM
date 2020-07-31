using Intouch.Edm.iOS.Renderers;
using Intouch.Edm.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MainPage), typeof(MyTabbedPageRenderer))]

namespace Intouch.Edm.iOS.Renderers
{
    public class MyTabbedPageRenderer : TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var vcs = ViewControllers;

            foreach (UIViewController vc in vcs)
            {
                vc.TabBarItem.ImageInsets = new UIEdgeInsets(-5, -5, -5, -5);
                if (vc.TabBarItem.Image != null)
                {
                    vc.TabBarItem.Image = vc.TabBarItem.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                }
                if (vc.TabBarItem.SelectedImage != null)
                {
                    vc.TabBarItem.SelectedImage = vc.TabBarItem.SelectedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                }
            }
        }
    }
}