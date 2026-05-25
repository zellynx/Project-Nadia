namespace Editor.Validation
{
    public readonly struct RavenValidationResult
    {
        public readonly bool Valid;

        public readonly string Message;

        public RavenValidationResult(
            bool valid,
            string message)
        {
            Valid = valid;
            Message = message;
        }
    }
}