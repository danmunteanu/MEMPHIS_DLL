namespace Memphis.Conditions
{
    public class ConditionIsRoot : TokenCondition
    {
        EngineBase? mEngine = null;

        public ConditionIsRoot(EngineBase engine) 
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
