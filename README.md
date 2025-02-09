## Overview
Memphis is an old project of mine. It was supposed to help me with renaming mp3 files and it helped me maintain my music library. 
While I moved away from the initial technologies I used to develop it (Qt and C++), I also stopped using it.
But its original idea kept buzzing in my head and time and time again, I would find myself going down the memory lane and opening it up.

These are not the original files of the project, but a remake of it, this time in C#. It supports my intention to get my C# skills up to speed.
So, I'm recreating my cherished Memphis from scratch. 
Well, almost. REALITY_FRAMEWORKS_DLL already implements part of the functionality, so I can reuse what's there.

Anyway, it's still a work in progress and nothing is functioning, yet. 

Peace out!

Until it's ready, here's what each of the classes are responsible with:

### Token
Token is a node in the Tokens tree. It can have subtokens. It can split itself, based on separators.

### TokenEngine
It remembers all the files that were selected. It's supposed to work in tandem with a list of files (FilesListComponent, anyone? It's in COMMON_FORMS).
When a file gets selected, the TokenEngine will "remember" which files was selected and stores it in a map.
Woks as a hub for all Token-related operations.

### Transform
Pairs a TokenCondition and TokenAction together.

### TransformsContainer
Container of Transforms and provides an interface for adding / removing / clearing Transforms.
