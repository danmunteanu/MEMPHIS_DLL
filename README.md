## Overview

### Token
Token is a node in the Tokens tree. Can have subtokens. Splits itself based on separators.

### TokenEngine
When a file gets selected, the TokenEngine class "remembers" which files was selected. Woks as a hub for all Token-related operations.

### Transform
Pairs a TokenCondition and TokenAction together.

### TransformsContainer
Contains Transforms and provides an interface for adding / removing / clearing Transforms.
