using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

struct MPSFilesMapStruct
{
    public MPSToken Root;
    public string RenameTo;

    public MPSFilesMapStruct(MPSToken token = null, string rename = "")
    {
        Root = token;
        RenameTo = rename;
    }
};

//typedef std::map<size_t, MPSFilesMapStruct> MPSFilesToRenameMap;


public class MPSEngine : MPSTransformsContainer
{
    public MPSEngine()
    {
        m_masterToken = null;
        m_renameTo = string.Empty;
        m_selectedSubtoken = null;
        m_defaultSeparators = string.Empty;
        m_alwaysLowcaseExtension = false;
        //m_filesMap = new Dictionary<ulong, MPSFilesMapStruct>();
        m_stringsToRemove = new List<string>();
        //m_observers = new List<MPSEngineObserver>();
    }

    public void SetDefaultSeparators(string separators)
    {
        m_defaultSeparators = separators;
    }

    public string DefaultSeparators()
    {
        return m_defaultSeparators;
    }

    public void SetAlwaysLowcaseExtension(bool lowcase)
    {
        m_alwaysLowcaseExtension = lowcase;
    }

    public bool IsAlwaysLowcaseExtension()
    {
        return m_alwaysLowcaseExtension;
    }

    public void SelectMasterToken(string fileName)
    {
        //if (string.IsNullOrEmpty(fileName))
        //{
        //    m_masterToken = null;
        //    m_selectedSubtoken = null;
        //    m_renameTo = string.Empty;
        //    return;
        //}

        //var hash = fileName.GetHashCode();

        //if (m_filesMap.TryGetValue((ulong)hash, out var mapStruct))
        //{
        //    m_masterToken = mapStruct.Root;
        //    m_renameTo = mapStruct.RenameTo;
        //}
        //else
        //{
        //    m_masterToken = new MPSToken(null, fileName, m_defaultSeparators, false);
        //    var fnClean = RemoveStringsFromText(fileName);

        //    if (fnClean != fileName)
        //    {
        //        m_masterToken.InsertSubtoken(fnClean, 0);
        //        var sub = m_masterToken.SubTokens.FirstOrDefault();
        //        if (sub != null)
        //        {
        //            sub.SetSeparators(m_defaultSeparators);
        //            sub.Split();
        //            if (IsApplyTransforms())
        //                ApplyTransforms(sub);
        //        }
        //    }
        //    else
        //    {
        //        m_masterToken.Split();
        //        if (IsApplyTransforms())
        //            ApplyTransforms(m_masterToken);
        //    }

        //    m_renameTo = ReconstructOutput(m_masterToken);
        //    m_filesMap[(ulong)hash] = new MPSFilesMapStruct(m_masterToken, m_renameTo);
        //}

        //m_selectedSubtoken = m_masterToken;
    }

    public void SelectSubtoken(MPSToken token, bool updateOutput = true)
    {
        if (token == null) return;

        m_selectedSubtoken = token;

        if (updateOutput)
        {
            m_renameTo = ReconstructOutput(m_masterToken);
        }

        foreach (var observer in m_observers)
        {
            observer.Notify();
        }
    }

    public void UpdateToken(ref MPSToken token, string text, string separators, bool discard, bool forceUpdate = false)
    {
        //if (token == null) return;

        //if (token.Text != text || token.Separators != separators || forceUpdate)
        //{
        //    var parent = token.Parent;
        //    token.ClearSubtokens();
        //    token.SetParent(parent);
        //    token.SetText(text);
        //    token.SetSeparators(separators);
        //    token.SetDiscard(discard);
        //    token.Split();
        //}
        //else if (token.IsDiscard != discard || forceUpdate)
        //{
        //    if (token != m_masterToken)
        //        token.SetDiscard(discard);
        //}
    }

    public void UpdateSelectedSubtoken(string text, string separators, bool discard, bool forceUpdate = false)
    {
        //if (m_selectedSubtoken == null) return;

        //var isRoot = m_selectedSubtoken == m_masterToken;
        //UpdateToken(ref m_selectedSubtoken, text, separators, discard, forceUpdate);
        //if (isRoot) m_masterToken = m_selectedSubtoken;

        //m_renameTo = ReconstructOutput(m_masterToken);

        //var hash = m_masterToken.Text.GetHashCode();
        //if (m_filesMap.ContainsKey((ulong)hash))
        //{
        //    m_filesMap[(ulong)hash] = new MPSFilesMapStruct(m_masterToken, m_renameTo);
        //}
    }

    public void ShiftSelectedSubtoken(EMPSDirection direction)
    {
        if (m_selectedSubtoken == null) return;

        var parent = m_selectedSubtoken.Parent;
        if (parent == null) return;

        parent.ShiftSubtoken(m_selectedSubtoken, direction);
        m_renameTo = ReconstructOutput(m_masterToken);
    }

    public void ChangeCase(bool upcase, bool onlyFirst, bool recursive)
    {
        //var hash = m_masterToken.Text.GetHashCode();
        //if (m_filesMap.ContainsKey((ulong)hash))
        //{
        //    ChangeCase(m_selectedSubtoken, upcase, onlyFirst, recursive);
        //    m_renameTo = ReconstructOutput(m_masterToken);
        //    m_filesMap[(ulong)hash] = new MPSFilesMapStruct(m_masterToken, m_renameTo);
        //}
    }

    public void InsertText(string textToInsert, EMPSDirection direction)
    {
        //if (m_selectedSubtoken == null) return;

        //if (m_selectedSubtoken == m_masterToken) return;

        //var parent = m_selectedSubtoken.Parent;
        //if (parent == null) return;

        //var selTokenPos = parent.SubTokens.IndexOf(m_selectedSubtoken);
        //if (direction == EMPSDirection.Left)
        //    parent.InsertSubtoken(textToInsert, selTokenPos);
        //else
        //    parent.InsertSubtoken(textToInsert, selTokenPos + 1);

        //m_renameTo = ReconstructOutput(m_masterToken);
    }

    public void ChangeCase(MPSToken token, bool upcase, bool onlyFirst, bool recursive)
    {
        //var changeCaseAction = new MPSActionChangeCase(this, upcase, !onlyFirst, recursive);
        //changeCaseAction.Apply(token);
    }

    public string ReconstructOutput(MPSToken token)
    {
        string name = string.Empty;
        //string separator = string.Empty;

        //if (token != null)
        //{
        //    if (!token.SubTokens.Any())
        //    {
        //        if (!token.IsDiscard)
        //        {
        //            var isFirst = token == m_masterToken.FindFirstLeafSubtoken(false);
        //            var isLast = token == m_masterToken.FindLastLeafSubtoken(false);
        //            separator = isFirst ? string.Empty : (isLast ? "." : " ");
        //            name += separator + token.Text;
        //        }
        //    }
        //    else
        //    {
        //        foreach (var subToken in token.SubTokens)
        //        {
        //            name += ReconstructOutput(subToken);
        //        }
        //    }
        //}

        return name;
    }

    public bool HasRenameTo(string fileName, out string renameTo)
    {
        renameTo = string.Empty;
        //var hash = fileName.GetHashCode();

        //if (m_filesMap.ContainsKey((ulong)hash))
        //{
        //    renameTo = m_filesMap[(ulong)hash].RenameTo;
        //    return true;
        //}

        return false;
    }

    public bool HasFilesToRename()
    {
        return false;
        //return m_filesMap.Values.Any(entry => entry.Root.Text != entry.RenameTo);
    }

    public void ClearFilesMap() => m_filesMap.Clear();

    public bool RenameOne(string path, string srcFile, string dstFile, bool updateMapEntry = false)
    {
        var srcPath = System.IO.Path.Combine(path, srcFile);
        var dstPath = System.IO.Path.Combine(path, dstFile);

        if (!System.IO.File.Exists(srcPath))
        {
            return false;
        }

        if (System.IO.File.Exists(dstPath) && !string.Equals(srcFile, dstFile, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        try
        {
            System.IO.File.Move(srcPath, dstPath);

            if (updateMapEntry)
            {
                var hash = srcFile.GetHashCode();
                if (m_filesMap.ContainsKey((ulong)hash))
                {
                    var record = m_filesMap[(ulong)hash];
                    record.Root.Text = dstFile;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public void RenameAll(string path)
    {
        foreach (var entry in m_filesMap)
        {
            var srcFile = entry.Value.Root.Text;
            var dstFile = entry.Value.RenameTo;
            RenameOne(path, srcFile, dstFile, true);
        }
    }

    public void AddObserver(IEngineObserver observer)
    {
        //m_observers.Add(observer);
    }

    public void ClearObservers()
    {
        m_observers.Clear();
    }

    //public override void Update(MPSToken token)
    //{
        // Custom update logic for MPSEngine
    //}

    //public override bool IsTokenCurrentRoot(MPSToken token)
    //{
    //    return token == m_masterToken;
    //}

    // Fields
    private MPSToken m_masterToken;
    private string m_renameTo;
    private MPSToken m_selectedSubtoken;
    private string m_defaultSeparators;
    private bool m_alwaysLowcaseExtension;
    private List<string> m_stringsToRemove;
    private List<IEngineObserver> m_observers;
    private Dictionary<ulong, MPSFilesMapStruct> m_filesMap;
}
