namespace Memphis
{

    public interface EngineBase
    {

        // Abstract method to update a token (must be implemented by subclasses)
        public abstract void Update(Token token);

        // Abstract method to change case of token (must be implemented by subclasses)
        public abstract void ChangeCase(Token token, bool upcase, bool onlyFirst, bool recursive);

        // Abstract method to check if the token is the current root
        public abstract bool IsTokenCurrentRoot(Token token);


    }

}