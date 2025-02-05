using System;
using System.Collections.Generic;

public abstract class MPSEngineBase
{
    // Destructor equivalent in C#
    ~MPSEngineBase()
    {
        // Any necessary cleanup would go here
    }

    // Abstract method to update a token (must be implemented by subclasses)
    public abstract void Update(MPSToken token);

    // Abstract method to change case of token (must be implemented by subclasses)
    public abstract void ChangeCase(MPSToken token, bool upcase, bool onlyFirst, bool recursive);

    // Abstract method to check if the token is the current root
    public abstract bool IsTokenCurrentRoot(MPSToken token);

    // Methods for message handling
    public void PushMessage(string msg)
    {
        mMessages.Enqueue(msg);
    }

    public bool HasMessages()
    {
        return mMessages.Count > 0;
    }

    public string PopMessage()
    {
        if (HasMessages())
        {
            string msg = mMessages.Dequeue();
            return msg;
        }
        return null;
    }

    public void ClearMessages()
    {
        mMessages.Clear();
    }

    // Protected queue for storing status & error messages
    protected readonly Queue<string> mMessages = new Queue<string>();
}
