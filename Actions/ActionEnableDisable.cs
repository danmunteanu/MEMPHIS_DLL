namespace Memphis.Actions
{
    public class ActionEnableDisable : TokenAction
    {
        public bool Disable { get; set; } = false;  //  Enable by default

        IEngineBase? Engine { get; set; } = null;

        public ActionEnableDisable(IEngineBase engineBase, bool disable = false)
        {
            Disable = disable;
            Engine = engineBase;

            UpdateDescription();
        }

        public override void Execute(Token token)
        {
            if (token == null)
                return;

            if (Engine == null)
                return;

            if (!Engine.IsTokenCurrentRoot(token))
                token.Discard = Disable;
        }

        protected override void UpdateDescription()
        {
            mDescription = GetType().Name;
        }
    }
}
