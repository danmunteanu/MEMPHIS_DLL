namespace Memphis
{
    //  Contains Transforms and provides an interface for handling them
    public class TransformsContainer
    {
        //  List of transforms
        protected List<TokenTransform> mTransforms = new();

        public IReadOnlyList<TokenTransform> Transforms
            => mTransforms.AsReadOnly();

        public void AddTransform(TokenTransform transform)
            => mTransforms.Add(transform);

        public void AddTransform(TokenCondition cond, TokenAction action)
            => mTransforms.Add(new TokenTransform(cond, action));

        public void ClearTransforms()
            => mTransforms.Clear();
    }
}
