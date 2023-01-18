---
sidebar_position: 4
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

# Journals

Journals represent notes that can be added to the player's journal. They are intended to be used for questlines, to guide the player and help them remember details, though the feature is not technically limited to that.

One journal asset represents an evolving narrative through multiple text entries. Only one text entry can be presented to the player at any one time: if you update the journal to a different entry, the old one disappears.

## Properties

The following properties are available when editing Journal assets:

Property|Description
---|---
Quest Summary|Title of the journal. Displayed above the active journal entry.
Asset GUID|Unique ID number of the asset. For advanced users.
Entries List|The list of all entries that make up this journal. You can have any number of entries per journal, though only one is active at a time.
Entry ID|A number that identifies this entry or 'stage'. When updating the journal, you use this ID to tell the game which entry to show.
Entry Text|The text contents of the selected entry, as it will be shown to the player.

## Showing journals

Given a Journal asset named 'MyQuest', you would use it like so:

```mdx-code-block
<Tabs groupId="script-lang">
<TabItem value="visual" label="Visual Script">
```

```finmervis
-- Add or update a journal to a specific stage (10)
Update Quest 'MyQuest' to Stage 10

-- Remove the journal from the Character Sheet
Remove Quest 'MyQuest'
```

```mdx-code-block
</TabItem>
<TabItem value="lua" label="Lua Script">
```

```lua
-- Add or update a journal to a specific stage (10)
Journal.Update("MyQuest", 10)

-- Remove the journal from the Character Sheet
Journal.Close("MyQuest")
```

```mdx-code-block
</TabItem>
</Tabs>
```

When changing the current stage of a particular journal - whether or not it was already in the Character Sheet - only the last-set stage will take effect. So only one entry will be shown per journal asset.
