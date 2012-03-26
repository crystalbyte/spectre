using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Crystalbyte.Chocolate.UI;
using ChocolateApp = Crystalbyte.Chocolate.UI.App;

namespace orgAnice.Chocolate
{
    public sealed class WindowModelView {
        private readonly AppContext _context;

        public WindowModelView() {
            var window = Application.Current.MainWindow as IRenderTarget;
            _context = new AppContext(window, new WindowDelegate());
        }

        public void Run() {
            ChocolateApp.Run(_context);
        }
    }
}
