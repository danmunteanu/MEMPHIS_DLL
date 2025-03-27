using System.Text;

namespace Memphis.Actions
{
    public class ActionInsertText : TokenAction
    {
        public ActionInsertText(string textToAdd = "", int position = int.MaxValue) 
        {
            UpdateDescription();
        }

        public string TextToAdd {  get; set; } = string.Empty;
        public int Position { get; set; } = int.MaxValue;

        public override void Execute(Token token)
        {

        }

        protected override void UpdateDescription()
        {
            StringBuilder sb = new();
            sb.Append("InsertText (\"");
            sb.Append(TextToAdd);
            sb.Append("\") on Position ");
            sb.Append(Position);
            mDescription = sb.ToString();
        }
    }
}
