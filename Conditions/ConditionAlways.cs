namespace MEMPHIS.Conditions
{
    internal class ConditionAlways : TokenCondition
    {
        public ConditionAlways() { }

        public override string Description()
        {
            return "Always";
        }

        public override bool Evaluate(MPSToken arg)
        {
            //  always true
            return true;
        }
    }
}
