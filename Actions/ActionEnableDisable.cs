namespace Memphis.Actions
{
    public class ActionEnableDisable : TokenAction
    {
        public bool Disable { get; set; } = false;  //  Enable by default

        EngineBase? Engine { get; set; } = null;

        public ActionEnableDisable(EngineBase engineBase, bool disable = false)
        {
            Disable = disable;
            Engine = engineBase;
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
    }
}
