namespace Memphis.Conditions
{
    public class ConditionAlways : TokenCondition
    {
        public ConditionAlways() { }

        public override string Description()
        {
            return "Always";
        }

        public override bool Evaluate(Token arg)
        {
            //  always true
            return true;
        }
    }
}
