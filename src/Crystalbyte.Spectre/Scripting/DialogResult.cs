namespace Crystalbyte.Spectre.Scripting{
    public sealed class DialogResult{
        public DialogResult(){
            Message = string.Empty;
        }

        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}