namespace Memphis
{
    // Methods for error message handling

    public partial class Engine
    {
        public void PushMessage(string msg)
            => mMessages.Enqueue(msg);

        public bool HasMessages()
            => mMessages.Count > 0;

        public string? PopMessage()
        {
            if (HasMessages())
            {
                string msg = mMessages.Dequeue();
                return msg;
            }
            return null;
        }

        public void ClearMessages()
            => mMessages.Clear();

        // Protected queue for storing status & error messages
        protected readonly Queue<string> mMessages = new Queue<string>();
    }
}
