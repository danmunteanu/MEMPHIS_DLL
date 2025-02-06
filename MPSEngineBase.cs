namespace Memphis
{

    public interface MPSEngineBase
    {

        // Abstract method to update a token (must be implemented by subclasses)
        public abstract void Update(MPSToken token);

        // Abstract method to change case of token (must be implemented by subclasses)
        public abstract void ChangeCase(MPSToken token, bool upcase, bool onlyFirst, bool recursive);

        // Abstract method to check if the token is the current root
        public abstract bool IsTokenCurrentRoot(MPSToken token);


    }

}