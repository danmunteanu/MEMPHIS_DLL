using System;

namespace Memphis.Conditions
{
    public class ConditionEquals : TokenCondition
    {
        public override bool Evaluate(Token token)
        {
            //if (!token)
            //    return false;

            //BoostSeparator separ(L";");
            //BoostTokenizer tokenizer(m_text, separ);
            //BoostTokenizer::iterator iter = tokenizer.begin();
            //bool equals = false;
            //for (; iter != tokenizer.end(); ++iter)
            //{
            //    if (m_case_sensitive)
            //        equals = token->text() == (*iter);
            //    else
            //        equals = boost::to_upper_copy(token->text()) == boost::to_upper_copy(*iter);

            //    if (equals)
            //        break;
            //}
            //return equals;

            return false;
        }

        //public override string ToString()
        public override string Description()
        {
            return "Equals";
        }
    }
}
