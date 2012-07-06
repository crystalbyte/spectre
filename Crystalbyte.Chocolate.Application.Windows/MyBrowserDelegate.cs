#region Namespace Directives

using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    internal class MyBrowserDelegate : BrowserDelegate {
        protected override void OnGeolocationRequested(GeolocationRequestedEventArgs e) {
            base.OnGeolocationRequested(e);
        }

        protected override void OnPageLoading(PageLoadingEventArgs e) {
            base.OnPageLoading(e);
        }

        protected override void OnPageLoaded(PageLoadedEventArgs e) {
            base.OnPageLoaded(e);
        }

        protected override void OnNavigated(NavigatedEventArgs e) {
            base.OnNavigated(e);
        }

        protected override void OnGeolocationRequestCanceled(GeolocationRequestCanceledEventArgs e)
        {
            base.OnGeolocationRequestCanceled(e);
        }
    }
}