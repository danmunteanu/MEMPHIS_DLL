## Overview
Memphis is an old project of mine. It was supposed to help me rename mp3 files and maintain my music library. 

While I moved away from the initial technologies I used to develop it (Qt and C++), I also stopped using it.

But the main idea kept buzzing in my head and time and time again, I would find myself going down the memory lane and opening up the initial project.

These are not the original files of the project, but a remake of it, this time in C#. 

It also supports my intention to get my C# skills up to speed.

So, I'm recreating my cherished Memphis from scratch. 

Well, almost. REALITY_FRAMEWORKS_DLL already implements part of the functionality, so I will reuse what's in there.

Anyway, it's still a work in progress and not much is finished, yet. 

Until it's ready, here's a class overview:

### Token
Token is a node in the Tokens tree. It can have subtokens. It can split itself, based on separators.

### TokenEngine
It remembers all the files that were selected. It's supposed to work in tandem with FilesListComponent from COMMON_FORMS.
When a file gets selected, the TokenEngine will "remember" which files was selected and stores it in a map.
Woks as a hub for all Token-related operations.

### Transform
Pairs a TokenCondition and TokenAction together.

### TransformsContainer
Container of Transforms and provides an interface for adding / removing / clearing Transforms.
