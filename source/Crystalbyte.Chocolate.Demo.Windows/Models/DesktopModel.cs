using System;

namespace Crystalbyte.Chocolate.Models
{
    [Serializable]
    public sealed class DesktopModel
    {
        public DesktopModel() {
            WelcomeText = "Welcome to Razor!";
        }

        public string WelcomeText { get; set; }
    }
}
