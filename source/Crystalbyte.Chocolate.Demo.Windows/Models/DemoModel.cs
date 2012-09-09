using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Models
{
    [Serializable]
    public sealed class DemoModel
    {
        public DemoModel() {
            WelcomeText = "Welcome to Razor!";
        }

        public string WelcomeText { get; set; }
    }
}
