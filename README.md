## Overview
Memphis is an old project of mine. It was supposed to help me with renaming mp3 files and it helped me maintain my music library. 
While I moved away from the initial technologies I used to develop it (Qt and C++), I also stopped using it.
But its original idea kept buzzing in my head and time and time again, I would find myself going down the memory lane and opening it up.

These are not the original files of the project, but a remake of it, this time in C#. It supports my intention to get my C# skills up to speed.
So, I'm recreating my cherished Memphis from scratch. 
Well, almost. REALITY_FRAMEWORKS_DLL already implements part of the functionality, so I can simply reuse what's there.

Anyway, it's still a work in progress and nothing is functioning, yet. 

Peace out!

### Token
Token is a node in the Tokens tree. Can have subtokens. Splits itself based on separators.

### TokenEngine
When a file gets selected, the TokenEngine class "remembers" which files was selected. Woks as a hub for all Token-related operations.

### Transform
Pairs a TokenCondition and TokenAction together.

### TransformsContainer
Contains Transforms and provides an interface for adding / removing / clearing Transforms.
