﻿using Intouch.Edm.iOS.Renderers;
using Intouch.Edm.Views;
using Plugin.Badge.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MainPage), typeof(MyTabbedPageRenderer))]

namespace Intouch.Edm.iOS.Renderers
{
    public class MyTabbedPageRenderer : BadgedTabbedPageRenderer
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