namespace Memphis.Actions
{
    public class ActionReplaceText : TokenAction
    {
        public string Search {  get; set; } = string.Empty;

        public string Replace {  get; set; } = string.Empty;

        private IEngineBase? mEngine = null;

        public ActionReplaceText(IEngineBase engine, string search = "", string replace = "")
        {
            mEngine = engine;
            Search = search;
            Replace = replace;

            UpdateDescription();
        }
        public override void Execute(Token token)
        {
            if (token == null)
                return;

            if (string.IsNullOrEmpty(token.Text))
                return;

            string result = token.Text.Replace(Search, Replace);
            token.Text = result;
        }

        protected override void UpdateDescription()
        {
            mDescription = GetType().Name;
        }
    }
}
