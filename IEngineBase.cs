namespace Memphis
{
    public interface IEngineBase
    {

        // Abstract method to update a token (must be implemented by subclasses)
        public void Update(Token token);

        // Abstract method to change case of token (must be implemented by subclasses)
        public void ChangeCase(Token token, bool upcase, bool onlyFirst, bool recursive);

        // Abstract method to check if the token is the current root
        public bool IsTokenCurrentRoot(Token token);


    }

}