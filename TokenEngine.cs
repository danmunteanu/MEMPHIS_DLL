namespace Memphis
{
    struct FileRenameInfo
    {
        public Token? Root;
        public string RenameTo;

        public FileRenameInfo(Token? token = null, string rename = "")
        {
            Root = token;
            RenameTo = rename;
        }
    };

    public partial class TokenEngine : TransformContainer
    {
        // Fields
        private Token? mMasterToken;
        private Token? mSelectedSubtoken;

        private string? mRenameTo;
        public string? RenameTo { get => mRenameTo; }

        private List<string> mStringsToRemove = new();

        public string DefaultSeparators { get; set; } = string.Empty;

        private bool AlwaysLowcaseExtension { get; set; } = false;

        //  Map of files to rename
        private Dictionary<ulong, FileRenameInfo> mRenamesMap = new();

        //  List of engine observers
        private List<IEngineObserver> mObservers = new();

        public bool ApplyTransforms { get; set; } = true;

        public TokenEngine()
        {
            //  Default settings
            mSelectedSubtoken = null;
            DefaultSeparators = string.Empty;
            AlwaysLowcaseExtension = false;
        }

        public void ApplyTransformsToToken(Token token)
        {
            if (token == null)
                return;

            foreach (var transform in mTransforms)
            {
                if (transform.Enabled)
                    transform.Apply(token);
            }

            //  apply on subtokens
            if (token.Subtokens.Any())
            {
                foreach (var subToken in token.Subtokens)
                    ApplyTransformsToToken(subToken);
            }
        }

        public void SelectMasterToken(string fileName)
        {
            //  Did we get an empty file name?
            if (string.IsNullOrEmpty(fileName))
            {
                mMasterToken = null;
                mSelectedSubtoken = null;
                mRenameTo = string.Empty;
                return;
            }

            //  Compute hash for file name
            var hash = fileName.GetHashCode();

            //  Was it already added to the map?
            if (mRenamesMap.TryGetValue((ulong)hash, out var mapStruct))
            {
                //  load master token and rename to from the map
                mMasterToken = mapStruct.Root;
                mRenameTo = mapStruct.RenameTo;
            }
            else
            {
                //  Must add a new master token
                mMasterToken = new Token(null, fileName, DefaultSeparators, false);

                //  Remove predefined strings from the file name

                Token? tokenToSplit = mMasterToken;

                //  Remove predefined strings first
                //var cleanFileName = RemoveStringsFromText(fileName);
                var cleanFileName = fileName;

                //  if the file name was modified, insert it as the first subtoken
                if (cleanFileName != fileName)
                    tokenToSplit = mMasterToken.InsertSubtoken(cleanFileName);

                if (tokenToSplit != null)
                {
                    tokenToSplit.Separators = DefaultSeparators;
                    tokenToSplit.Split();

                    //  Apply the transforms chain
                    if (ApplyTransforms)
                        ApplyTransformsToToken(tokenToSplit);
                }

                mRenameTo = ReconstructOutput(mMasterToken);
                mRenamesMap[(ulong)hash] = new FileRenameInfo(mMasterToken, mRenameTo);
            }

            //  Select master token
            mSelectedSubtoken = mMasterToken;
        }

        public void SelectSubtoken(Token token, bool updateOutput = true)
        {
            if (token == null) return;

            mSelectedSubtoken = token;

            if (updateOutput)
            {
                mRenameTo = ReconstructOutput(mMasterToken);
            }

            foreach (var observer in mObservers)
            {
                observer.Notify();
            }
        }

        public void UpdateToken(
            ref Token token,
            string text,
            string separators,
            bool discard,
            bool forceUpdate = false)
        {
            if (token == null) return;

            if (token.Text != text || token.Separators != separators || forceUpdate)
            {
                var parent = token.Parent;
                token.ClearSubtokens();
                token.Parent = parent;
                token.Text = text;
                token.Separators = separators;
                token.Discard = discard;
                token.Split();
            }
            else if (token.Discard != discard || forceUpdate)
            {
                if (token != mMasterToken)
                    token.Discard = discard;
            }
        }

        public void UpdateSelectedSubtoken(string text, string separators, bool discard, bool forceUpdate = false)
        {
            //if (m_selectedSubtoken == null) return;

            //var isRoot = m_selectedSubtoken == mMasterToken;
            //UpdateToken(ref m_selectedSubtoken, text, separators, discard, forceUpdate);
            //if (isRoot) mMasterToken = m_selectedSubtoken;

            //mRenameTo = ReconstructOutput(mMasterToken);

            //var hash = mMasterToken.Text.GetHashCode();
            //if (mRenameMap.ContainsKey((ulong)hash))
            //{
            //    mRenameMap[(ulong)hash] = new MPSFilesMapStruct(mMasterToken, mRenameTo);
            //}
        }

        public void ShiftSelectedSubtoken(EShiftDirection direction)
        {
            if (mSelectedSubtoken == null) return;

            var parent = mSelectedSubtoken.Parent;
            if (parent == null) return;

            parent.ShiftSubtoken(mSelectedSubtoken, direction);
            mRenameTo = ReconstructOutput(mMasterToken);
        }

        public void ChangeCase(bool upcase, bool onlyFirst, bool recursive)
        {
            //var hash = mMasterToken.Text.GetHashCode();
            //if (mRenameMap.ContainsKey((ulong)hash))
            //{
            //    ChangeCase(m_selectedSubtoken, upcase, onlyFirst, recursive);
            //    mRenameTo = ReconstructOutput(mMasterToken);
            //    mRenameMap[(ulong)hash] = new MPSFilesMapStruct(mMasterToken, mRenameTo);
            //}
        }

        public void InsertText(string textToInsert, EShiftDirection direction)
        {
            if (mSelectedSubtoken == null) return;

            if (mSelectedSubtoken == mMasterToken) return;

            var parent = mSelectedSubtoken.Parent;
            if (parent == null) return;

            var subtokens = (parent.Subtokens as IList<Token>);

            var selTokenPos = subtokens?.IndexOf(mSelectedSubtoken) ?? int.MaxValue;
            if (direction == EShiftDirection.Left)
                parent.InsertSubtoken(textToInsert, selTokenPos);
            else
                parent.InsertSubtoken(textToInsert, selTokenPos + 1);

            mRenameTo = ReconstructOutput(mMasterToken);
        }

        public void ChangeCase(Token token, bool upcase, bool onlyFirst, bool recursive)
        {
            //var changeCaseAction = new MPSActionChangeCase(this, upcase, !onlyFirst, recursive);
            //changeCaseAction.Apply(token);
        }

        public string ReconstructOutput(Token token)
        {
            string name = string.Empty;
            string separator = string.Empty;

            if (token != null)
            {
                if (!token.Subtokens.Any())
                {
                    if (!token.Discard)
                    {
                        var isFirst = token == mMasterToken.FindFirstLeafSubtoken(false);
                        var isLast = token == mMasterToken.FindLastLeafSubtoken(false);
                        separator = isFirst ? string.Empty : (isLast ? "." : " ");
                        name += separator + token.Text;
                    }
                }
                else
                {
                    foreach (var subToken in token.Subtokens)
                    {
                        name += ReconstructOutput(subToken);
                    }
                }
            }

            return name;
        }

        public bool HasRenameTo(string fileName, out string renameTo)
        {
            renameTo = string.Empty;
            var hash = fileName.GetHashCode();

            if (mRenamesMap.ContainsKey((ulong)hash))
            {
                renameTo = mRenamesMap[(ulong)hash].RenameTo;
                return true;
            }

            return false;
        }

        public bool HasFilesToRename()
        {
            return mRenamesMap.Values.Any(
                entry => entry.Root.Text != entry.RenameTo
            );
        }

        public void ClearFilesMap()
            => mRenamesMap.Clear();

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
                    if (mRenamesMap.ContainsKey((ulong)hash))
                    {
                        var record = mRenamesMap[(ulong)hash];
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
            foreach (var entry in mRenamesMap)
            {
                var srcFile = entry.Value.Root.Text;
                var dstFile = entry.Value.RenameTo;
                RenameOne(path, srcFile, dstFile, true);
            }
        }

        public void AddObserver(IEngineObserver observer)
            => mObservers.Add(observer);

        public void ClearObservers()
            => mObservers.Clear();

        public void Update(Token token)
        {
            //Custom update logic for MPSEngine
        }

        public bool IsTokenCurrentRoot(Token token)
        {
            return token == mMasterToken;
        }

    }
}
