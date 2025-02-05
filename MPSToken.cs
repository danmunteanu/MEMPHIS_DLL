using System;
using System.Collections.Generic;

public enum EMPSDirection
{
    Left,
    Right
}

public class MPSToken
{
    private MPSToken _parent;
    private string _text;
    private string _separators;
    private bool _discard;
    private List<MPSToken> _subtokens;

    // Constructor
    public MPSToken(MPSToken parent = null, string text = "", string separators = "", bool discard = false)
    {
        _parent = parent;
        _text = text;
        _separators = separators;
        _discard = discard;
        _subtokens = new List<MPSToken>();
    }

    // Destructor (for cleanup)
    ~MPSToken()
    {
        CleanupToken();
    }

    // Setters and Getters
    public MPSToken Parent { get { return _parent; } set { _parent = value; } }
    public string Text { get => _text; set => _text = value; }
    public string Separators { get => _separators; set { _separators = value; if (string.IsNullOrEmpty(_separators)) CleanupToken(); } }
    public bool Discard { get => _discard; set => SetDiscard(value); }
    public IReadOnlyList<MPSToken> Subtokens => _subtokens.AsReadOnly();

    // Subtoken management
    public void AddSubtoken(string text, int pos = int.MaxValue)
    {
        var newToken = new MPSToken(this, text);
        if (pos == int.MaxValue || pos >= _subtokens.Count)
        {
            _subtokens.Add(newToken);
        }
        else
        {
            _subtokens.Insert(pos, newToken);
        }
    }

    public void RemoveSubtoken(MPSToken token)
    {
        _subtokens.Remove(token);
    }

    public void ClearSubtokens()
    {
        _subtokens.Clear();
    }

    public bool IsSubtoken(MPSToken token) => _subtokens.Contains(token);

    public void ShiftSubtoken(MPSToken subToken, EMPSDirection direction)
    {
        if (subToken == null || _subtokens.Count == 0) return;

        int index = _subtokens.IndexOf(subToken);
        if (index == -1) return;

        if (direction == EMPSDirection.Left && index > 0)
        {
            // Swap with previous
            var temp = _subtokens[index - 1];
            _subtokens[index - 1] = _subtokens[index];
            _subtokens[index] = temp;
        }
        else if (direction == EMPSDirection.Right && index < _subtokens.Count - 1)
        {
            // Swap with next
            var temp = _subtokens[index + 1];
            _subtokens[index + 1] = _subtokens[index];
            _subtokens[index] = temp;
        }
    }

    public void Split()
    {
        if (string.IsNullOrEmpty(_separators)) return;

        var tokens = _text.Split(new[] { _separators }, StringSplitOptions.None);
        CleanupToken();

        foreach (var token in tokens)
        {
            var subToken = new MPSToken(this, token, _separators, _discard);
            _subtokens.Add(subToken);
        }
    }

    // Cleanup and discard handling
    private void CleanupToken()
    {
        foreach (var subToken in _subtokens)
        {
            subToken.CleanupToken();
        }

        _subtokens.Clear();
    }

    private void SetDiscard(bool discard)
    {
        _discard = discard;
        foreach (var subToken in _subtokens)
        {
            subToken.SetDiscard(discard);
        }
    }

    // Utility methods for finding leaf nodes
    public MPSToken FindFirstLeafSubtoken(bool includeDiscarded)
    {
        return FindFirstLeafSubtoken(this, includeDiscarded);
    }

    public MPSToken FindLastLeafSubtoken(bool includeDiscarded)
    {
        return FindLastLeafSubtoken(this, includeDiscarded);
    }

    private MPSToken FindFirstLeafSubtoken(MPSToken token, bool includeDiscarded)
    {
        if (token == null) return null;
        if (token._subtokens.Count == 0) return token;

        foreach (var subToken in token._subtokens)
        {
            if (includeDiscarded || !subToken._discard)
            {
                return FindFirstLeafSubtoken(subToken, includeDiscarded);
            }
        }

        return null;
    }

    private MPSToken FindLastLeafSubtoken(MPSToken token, bool includeDiscarded)
    {
        if (token == null) return null;
        if (token._subtokens.Count == 0) return token;

        MPSToken last = null;
        foreach (var subToken in token._subtokens)
        {
            if (includeDiscarded || !subToken._discard)
            {
                last = FindLastLeafSubtoken(subToken, includeDiscarded);
            }
        }

        return last;
    }
}
