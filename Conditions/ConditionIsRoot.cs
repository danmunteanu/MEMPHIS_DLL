namespace Memphis.Conditions
{
    public class ConditionIsRoot : TokenCondition
    {
        IEngineBase? mEngine = null;

        public ConditionIsRoot(IEngineBase engine) 
        { 
            mEngine = engine;
        }

        public override bool Evaluate(Token token)
        {
            if (mEngine == null)
                throw new Exception("Engine is null");

            if (token == null)
                throw new ArgumentNullException("Token is null");

            return mEngine.IsTokenCurrentRoot(token);
        }

        public override string Description()
        {
            return "IS ROOT";
        }
    }
}
