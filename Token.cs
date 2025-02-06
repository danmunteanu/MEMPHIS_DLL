namespace Memphis
{
    public enum EShiftDirection
    {
        Left,
        Right
    }

    public class Token
    {
        private Token? _parent;
        private string _text;
        private string _separators;
        private bool _discard;
        private List<Token> mSubtokens = new();

        // Constructor
        public Token(Token? parent = null, string text = "", string separators = "", bool discard = false)
        {
            _parent = parent;
            _text = text;
            _separators = separators;
            _discard = discard;
        }

        // Destructor (for cleanup)
        ~Token()
        {
            CleanupToken();
        }

        // Setters and Getters
        public Token? Parent { get { return _parent; } set { _parent = value; } }
        public string Text { get => _text; set => _text = value; }
        public string Separators { get => _separators; set { _separators = value; if (string.IsNullOrEmpty(_separators)) CleanupToken(); } }
        public bool Discard { get => _discard; set => SetDiscard(value); }
        public IReadOnlyList<Token> Subtokens => mSubtokens.AsReadOnly();

        public void InsertSubtoken(string text, int pos = int.MaxValue)
        {
            var newToken = new Token(this, text);
            if (pos == int.MaxValue || pos >= mSubtokens.Count)
            {
                mSubtokens.Add(newToken);
            }
            else
            {
                mSubtokens.Insert(pos, newToken);
            }
        }

        public void RemoveSubtoken(Token token)
            => mSubtokens.Remove(token);

        public void ClearSubtokens()
            => mSubtokens.Clear();

        public bool IsSubtoken(Token token)
            => mSubtokens.Contains(token);

        public void ShiftSubtoken(Token subToken, EShiftDirection direction)
        {
            if (subToken == null || mSubtokens.Count == 0) return;

            int index = mSubtokens.IndexOf(subToken);
            if (index == -1) return;

            if (direction == EShiftDirection.Left && index > 0)
            {
                // Swap with previous
                var temp = mSubtokens[index - 1];
                mSubtokens[index - 1] = mSubtokens[index];
                mSubtokens[index] = temp;
            }
            else if (direction == EShiftDirection.Right && index < mSubtokens.Count - 1)
            {
                // Swap with next
                var temp = mSubtokens[index + 1];
                mSubtokens[index + 1] = mSubtokens[index];
                mSubtokens[index] = temp;
            }
        }

        public void Split()
        {
            if (string.IsNullOrEmpty(_separators)) return;

            var tokens = _text.Split(
                new[] { _separators },
                StringSplitOptions.None
            );
            CleanupToken();

            foreach (var token in tokens)
            {
                var subToken = new Token(this, token, _separators, _discard);
                mSubtokens.Add(subToken);
            }
        }

        // Cleanup and discard handling
        private void CleanupToken()
        {
            foreach (var subToken in mSubtokens)
            {
                subToken.CleanupToken();
            }

            mSubtokens.Clear();
        }

        private void SetDiscard(bool discard)
        {
            _discard = discard;
            foreach (var subToken in mSubtokens)
            {
                subToken.SetDiscard(discard);
            }
        }

        // Utility methods for finding leaf nodes
        public Token? FindFirstLeafSubtoken(bool includeDiscarded)
        {
            return FindFirstLeafSubtoken(this, includeDiscarded);
        }

        public Token? FindLastLeafSubtoken(bool includeDiscarded)
        {
            return FindLastLeafSubtoken(this, includeDiscarded);
        }

        private Token? FindFirstLeafSubtoken(Token token, bool includeDiscarded)
        {
            if (token == null) return null;
            if (token.mSubtokens.Count == 0) return token;

            foreach (var subToken in token.mSubtokens)
            {
                if (includeDiscarded || !subToken._discard)
                {
                    return FindFirstLeafSubtoken(subToken, includeDiscarded);
                }
            }

            return null;
        }

        private Token? FindLastLeafSubtoken(Token token, bool includeDiscarded)
        {
            if (token == null) return null;
            if (token.mSubtokens.Count == 0) return token;

            Token? last = null;
            foreach (var subToken in token.mSubtokens)
            {
                if (includeDiscarded || !subToken._discard)
                {
                    last = FindLastLeafSubtoken(subToken, includeDiscarded);
                }
            }

            return last;
        }
    }

}