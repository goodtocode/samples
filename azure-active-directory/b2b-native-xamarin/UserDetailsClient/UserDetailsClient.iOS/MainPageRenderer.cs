﻿using MyCo.Identity;
using MyCo.Identity.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace MyCo.Identity.iOS
{
    class MainPageRenderer : PageRenderer
    {
        MainPage page;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            page = e.NewElement as MainPage;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();           
        }
    }
}
