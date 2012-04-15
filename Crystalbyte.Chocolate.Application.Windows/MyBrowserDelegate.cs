using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate
{
    class MyBrowserDelegate : BrowserDelegate
    {
        public MyBrowserDelegate() {
            
        }

        protected override void OnGeolocationRequested(GeolocationRequestedEventArgs e)
        {
            base.OnGeolocationRequested(e);
        }

        protected override void OnPageLoading(PageLoadingEventArgs e)
        {
            base.OnPageLoading(e);
        }

        protected override void OnPageLoaded(PageLoadedEventArgs e)
        {
            base.OnPageLoaded(e);
        }

        protected override void OnNavigated(NavigatedEventArgs e)
        {
            base.OnNavigated(e);
        }
    }
}
