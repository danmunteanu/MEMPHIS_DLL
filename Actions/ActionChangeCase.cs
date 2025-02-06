namespace Memphis.Actions
{
    public class ActionChangeCase : TokenAction
    {
        public bool Upcase { get; set; } = true;
        public bool OnlyFirst { get; set; } = true;
        public bool Recursive { get; set; } = true;

        private MPSEngineBase _engineBase;

        public ActionChangeCase(MPSEngineBase engineBase, bool upcase, bool all, bool recursive)
        {
            _engineBase = engineBase;

            Upcase = upcase;
            OnlyFirst = !all;
            Recursive = recursive;
        }

        public override void Execute(MPSToken token)
        {
            if (token == null)
                return;

            bool isRoot = _engineBase.IsTokenCurrentRoot(token);
            if (!string.IsNullOrEmpty(token.Text))
            {
                if (!isRoot || (isRoot && !token.Subtokens.Any()))
                {
                    if (OnlyFirst)
                    {

                    }
                    else
                    {

                    }
                }
            }

            //std::wstring text = token->text();
            //if (!text.empty())
            //{
            //    int(*fun_case)(int) = m_upcase ? toupper : tolower;

            //    if (!is_root || (is_root && token->count_subtokens() == 0))
            //    {
            //        if (m_only_first)
            //        {
            //            //  find first letter
            //            size_t idx = 0;
            //            for (; idx < text.length(); ++idx)
            //            {
            //                if ((text[idx] >= 'a' && text[idx] <= 'z') ||
            //                    (text[idx] >= 'A' && text[idx] <= 'Z'))
            //                    break;
            //            }
            //            if (idx < text.length())
            //            {
            //                text[idx] = fun_case(text[idx]);
            //                token->set_text(text);
            //            }
            //        }
            //        else
            //        {
            //            std::transform(text.begin(), text.end(), text.begin(), fun_case);
            //            token->set_text(text);
            //        }
            //    }
            //    if (m_recursive)
            //    {
            //        MPSTokensContainer::iterator iter = token->sub_tokens_begin();
            //        for (; iter != token->sub_tokens_end(); ++iter)
            //        {
            //            this->operator ()(*iter);
            //        }
            //    }
            //}
        }

        public override string ToString()
        {
            return "ChangeCase";
        }
    }

}
