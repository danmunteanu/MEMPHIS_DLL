namespace Memphis.Conditions
{
    public class ConditionIsNumeric : TokenCondition
    {
        public override bool Evaluate(Token token)
        {
            if (token == null)
                return false;

            if (token.Text == null)
                return false;

            return int.TryParse(token.Text, out _);
        }

        public override string Description()
        {
            return "IsNumeric";
        }
    }
}
