namespace Memphis.Actions
{
    public class ActionSetSeparators : TokenAction
    {
        public string Separators { get; set; } = string.Empty;

        private IEngineBase? mEngine = null;

        public ActionSetSeparators(IEngineBase engine, string separators = "") 
        { 
            mEngine = engine;
            Separators = separators;

            UpdateDescription();
        }

        public override void Execute(Token token)
        {
            if (mEngine == null)
                throw new Exception("Engine is null");

            if (token == null)
                throw new ArgumentNullException("Token is null");

            token.Separators = Separators;

            mEngine.Update(token);
        }

        protected override void UpdateDescription()
        {
            mDescription = GetType().Name;
        }
    }
}
