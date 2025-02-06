namespace Memphis
{

    public enum EMPSDirection
    {
        Left,
        Right
    }

    public class MPSToken
    {
        private MPSToken? _parent;
        private string _text;
        private string _separators;
        private bool _discard;
        private List<MPSToken> mSubtokens = new();

        // Constructor
        public MPSToken(MPSToken? parent = null, string text = "", string separators = "", bool discard = false)
        {
            _parent = parent;
            _text = text;
            _separators = separators;
            _discard = discard;
        }

        // Destructor (for cleanup)
        ~MPSToken()
        {
            CleanupToken();
        }

        // Setters and Getters
        public MPSToken? Parent { get { return _parent; } set { _parent = value; } }
        public string Text { get => _text; set => _text = value; }
        public string Separators { get => _separators; set { _separators = value; if (string.IsNullOrEmpty(_separators)) CleanupToken(); } }
        public bool Discard { get => _discard; set => SetDiscard(value); }
        public IReadOnlyList<MPSToken> Subtokens => mSubtokens.AsReadOnly();

        public void InsertSubtoken(string text, int pos = int.MaxValue)
        {
            var newToken = new MPSToken(this, text);
            if (pos == int.MaxValue || pos >= mSubtokens.Count)
            {
                mSubtokens.Add(newToken);
            }
            else
            {
                mSubtokens.Insert(pos, newToken);
            }
        }

        public void RemoveSubtoken(MPSToken token)
            => mSubtokens.Remove(token);

        public void ClearSubtokens()
            => mSubtokens.Clear();

        public bool IsSubtoken(MPSToken token)
            => mSubtokens.Contains(token);

        public void ShiftSubtoken(MPSToken subToken, EMPSDirection direction)
        {
            if (subToken == null || mSubtokens.Count == 0) return;

            int index = mSubtokens.IndexOf(subToken);
            if (index == -1) return;

            if (direction == EMPSDirection.Left && index > 0)
            {
                // Swap with previous
                var temp = mSubtokens[index - 1];
                mSubtokens[index - 1] = mSubtokens[index];
                mSubtokens[index] = temp;
            }
            else if (direction == EMPSDirection.Right && index < mSubtokens.Count - 1)
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
                var subToken = new MPSToken(this, token, _separators, _discard);
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
        public MPSToken? FindFirstLeafSubtoken(bool includeDiscarded)
        {
            return FindFirstLeafSubtoken(this, includeDiscarded);
        }

        public MPSToken? FindLastLeafSubtoken(bool includeDiscarded)
        {
            return FindLastLeafSubtoken(this, includeDiscarded);
        }

        private MPSToken? FindFirstLeafSubtoken(MPSToken token, bool includeDiscarded)
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

        private MPSToken? FindLastLeafSubtoken(MPSToken token, bool includeDiscarded)
        {
            if (token == null) return null;
            if (token.mSubtokens.Count == 0) return token;

            MPSToken? last = null;
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