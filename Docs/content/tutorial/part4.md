---
sidebar_position: 13
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';
import CaptionedImage from '@site/src/components/CaptionedImage';

# 4 - Final Touches

To wrap up the quest, the player must find Tobias again and return the item to him. He had suggested that he would still be located at the inn, so we can reuse the `TQ01_Patch_Inn` scene we created and add a new dialogue tree.

## Returning the pendant

In `TQ01_Patch_Inn`, off of Root, create another choice node, this one now set up like:

	Unique Key:			TQ01_Return
	Button Text:		Tobias
	Tooltip:			Report your success to Tobias.
	Appears When:
		Visual Script:
		(If ALL of the following is TRUE:)
		Number Variable 'TQ01_PROGRESS' Equals 3.00
		
		Lua:
		return Storage.GetNumber("TQ01_PROGRESS") == 3

Observe how we ensure that the `TQ01_PROGRESS` variable was set to 3, which is done by the `TQ01_To_Road` node in the last part. We only want Tobias to show when we complete his quest, and we will increment it once again here to prevent this wrap-up sequence from being done twice. So we have two choice nodes for striking up a conversation with Tobias - though keep in mind that due to their 'Appears When' scripts, only one will be visible at a time.

Now, let's finish the quest up. Create a new state node off of `TQ01_Return:`

<Tabs groupId="script-lang">
<TabItem value="visual" label="Visual Script">

	Unique Key:			TQ01_Complete_Quest
	Actions Taken:
		-- Print message
		Show Message: TQ01_COMPLETION

		-- Prevent player from completing the quest again
		Set Number Variable TQ01_PROGRESS to 4.00

		-- Remove the journal entry and the quest item
		Remove Quest 'TQ01'
		Remove TQ01_I_Pendant from Inventory

		-- Grant rewards
		Modify Player Money by 55
		Grant 50 XP to Player

</TabItem>
<TabItem value="lua" label="Lua Script">

	Unique Key:			TQ01_Complete_Quest

```lua
-- Actions Taken
	-- Print message
	Log("TQ01_COMPLETION")

	-- Prevent player from completing the quest again
	Storage.SetNumber("TQ01_PROGRESS", 4)

	-- Remove the journal entry and the quest item
	Journal.Close("TQ01")
	Player:TakeItem("TQ01_I_Pendant")

	-- Grant rewards
	Player:ModifyMoney(55)
	Player:AwardXP(50)
```

</TabItem>
</Tabs>

`TQ01_COMPLETION`:

> "You found it? Wonderful! Let me see!" You pull the pendant out for the dingo to investigate. You see him sigh in relief. "Ahh, perfect. Thanks again {player.name}, I really appreciate it. Here, as agreed, a fair payment, for services rendered." Tobias slides a purse of coin to you, to which you're quick to pocket it. "And now, our business is concluded, and I shall now take my leave. Take care in this world, {player.name}, and I hope to see you soon."

We close out the journal, remove the pendant from our inventory, get paid, and go on our way. Like the investigation conversation, let's wrap it up with a simple choice node. Don't forget to add a link back to `Main` afterwards, so the player can exit out of the conversation. The choice node is built like this:

	Unique Key:			TQ01_Exit
	Button Text:		Continue
	Tooltip:			And that's that!
	
This completes the `TQ01_Patch_Inn` scene! With the second conversation added, the scene should now look like this:

<CaptionedImage
	src={require("/images/Tutorial04InnComplete.png")}
	caption="The final layout of TQ01_Patch_Inn." />

## String mappings: a little spice

There is one last thing we can do to add bit more personality to our quest. Go back to the TQ01_CR_Wolf creature asset. Notice the String Mappings field? This feature lets you 'override' the default combat-related strings that the game will use when this creature is used in combat. Let's create a new String Table asset, called TQ01_Combat. Add the following entries to that table:

`TQ01_WOLF_DIGEST`:

> The wolf tries to call out to its pack from within your stomach, but the churning and groaning from within drowns its final words out.

`TQ01_WOLF_DIGEST_POV`:

> A resounding belch from the wolf signifies the last of your available air. The victorious canine saunters over the the abandoned campsite and flops down on its side, curling up beside its distended stomach to fall asleep. You, however, are far less comfortable. With your dwindling energy and air, you pass out within the caustic confines of the wolf's stomach.

`TQ01_WOLF_KILL`:

> You manage to land a good, solid hit on the wolf, and you hear a sickening crack as the canine collapses on the ground.

`TQ01_WOLF_STRUGGLE`:

> The wolf frantically fights to escape your stomach, though it seems to be of no avail.

> The wolf whines and whimpers in its desperate attempt to free itself from your belly.

Notice how `TQ01_WOLF_STRUGGLE` has multiple strings. Enter each one on a new line. This allows for randomization of messages playing: the game will automatically select a random one from this string set whenever `TQ01_WOLF_STRUGGLE` is used.

With our strings created, let's add some string remapping to the wolf creature. Open `TQ01_CR_Wolf` in the editor again, and in the bottom right corner in the section labeled 'String Mappings', create four string mappings like so:

	String Used:		VORE_EXT_STRUGGLE
	Situation:			Player targets NPC
	Replacement:		TQ01_WOLF_STRUGGLE

&nbsp;

	String Used:		KILL_DIGESTED
	Situation:			Player targets NPC
	Replacement:		TQ01_WOLF_DIGEST

&nbsp;

	String Used:		KILL_GENERIC
	Situation:			Player targets NPC
	Replacement:		TQ01_WOLF_KILL

&nbsp;

	String Used:		KILL_DIGESTED
	Situation:			NPC targets Player
	Replacement:		TQ01_WOLF_DIGEST_POV

Now we have some special text shown when the player wins the fight and/or devour the wolf to win. In the event that the wolf eats the player, we'll also have a dedicated string mapping to cover that situation. As you can see, these are what you use to customize the combat system's text output to your liking. You can also override basic attacks (`ATTACK_HIT`, `ATTACK_MISS`), grapples (`GRAPPLE_HIT`, `GRAPPLE_MISS`), and more.

## Publishing your work

Congratulations! You now have a fully-functional quest that plugs in neatly alongside Finmer's base game.

If you like, you could package your module and send it to someone else. You can do so by clicking the 📦 `Publish Furball` button on the Toolbar. If you'd like to read more about how publishing modules works (and what the heck a _furball_ is), check the page on [Packaging Modules](/getting-started/packaging).

## Final words

In this guide, we have covered how to make a new quest: the player will gather information, get into a fight, and save the day. A job well done. We have demonstrated the various asset types and features of the editor. Hopefully, you now have some familiarity with the editor and a feel for how to set things up. From here, you can easily add more content to your new quest, or adjust it for a different setting or location.

We would love to see your creations in the community; please feel free to share your custom content. The community is also a great place  to share questions and feedback about the game or the editor, or to request and discuss new features and systems.

Have fun and be creative!
