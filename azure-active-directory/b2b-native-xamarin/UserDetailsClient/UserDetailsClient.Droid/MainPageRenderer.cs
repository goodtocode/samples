using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Identity.Client;
using Xamarin.Forms.Platform.Android;
using MyCo.Identity;
using Xamarin.Forms;
using MyCo.Identity.Droid;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace MyCo.Identity.Droid
{
    class MainPageRenderer : PageRenderer
    {
        public MainPageRenderer(Context context) : base(context)
        {

        }
        MainPage page;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            page = e.NewElement as MainPage;
            var activity = this.Context as Activity;           
        }

    }
}