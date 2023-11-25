---
sidebar_position: 11
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';
import CaptionedImage from '@site/src/components/CaptionedImage';

# 2 - A Friendly Chat

The quest start is ready to go; now let's make a character who provides the player with information on the rest of the quest! The player will meet this character in North Finmer's inn.

## Patching the inn scene

To start, add another scene asset. Name it `TQ01_Patch_Inn`. Just as we did before for the notice board, mark this scene as a patch, and target `Scene_TownInn`. We'll want to inject **as choices at the end** of the **InnTalkRoot node**. Finally, add a Choice node to this patch with the following settings:

	Unique Key:		TQ01_Tobias
	Button Text:	Dingo
	Tooltip:		Talk to the dingo on the bench

For the general layout of our conversation, we're going to go with Tobias asking whether we're here for the notice, introducing ourselves, and having him explain the situation before we ask questions. To start, we're going to add a node off of the choice node we just made, then another pair of choice/state nodes from that.

:::note
For simplicity, we're going to omit the simple 'show a single message' scripts from now on. We'll list the string table key that you need to print. Like in the last part, either use the 'Show Message' visual script command, or the `Log()` Lua function.
:::

The first state node:

	Unique Key:		TQ01_Tobias_Meet
	Actions Taken:	Log 'TQ01_MEET'

The next choice node:

	Unique Key:		TQ01_Continue
	Button Text:	Continue
	Tooltip:		Shake paws and introduce yourself.

And the next state node:

	Unique Key:		TQ01_Tobias_Explain
	Actions Taken:	Log 'TQ01_EXPLAIN'

Now, let's go back to our `TQ01_Strings` string table and add the two sets we just referenced in the scene, to give the scene nodes we just created some actual text to work with. Add in the sets and strings as follows:

`TQ01_MEET`:

> You sit beside the dingo at the bench, staring at the wall ahead. Out of the corner of your eye, you catch the dingo turn to face you. "You here about the notice?" he softly utters. You nod, to which the canid smiles slightly. "No need to act so suspicious, there's nothing shady going on here. Tobias Natobi, pleasure to make your acquaintance."

`TQ01_EXPLAIN`:

> "{player.name}, eh? Well, you seem like someone who knows how to handle themselves." Tobias pulls out a rough sketch of some intricate design. "This is a pendant that's been passed down through my family for generations. When my father died, it belonged to me. However, a false friend of mine made off with it, probably to sell it for a pretty coin. I need you to track him down, and retrieve this pendant for me."

:::tip
Notice the `{player.name}` bit in the above string? That'll be replaced with the player's chosen nickname. There are many more variables like this available, like species, pronouns and more - check [Grammar & Contexts](/asset-types/string-tables#grammar--contexts) for details.
:::

## Building a dialogue tree

Now that we have our basic introduction, it's time for the player to ask some detailed questions. Some topics to discuss:

- the identity of this friend,
- where we could find them,
- and what's in it for us for helping Tobias.

Let's create some choices to ask these questions. Create three choice nodes off of `TQ01_Tobias_Explain` with these settings:

	Unique Key:		TQ01_Choice_Friend
	Button Text:	Friend
	Tooltip:		Ask about this "friend".
	Appears When:
		Visual Script:
		(If ALL of the following is TRUE:)
		Flag 'TQ01_DISCUSSED_FRIEND' Equals False
		
		Lua:
		return not Storage.GetFlag("TQ01_DISCUSSED_FRIEND")

&nbsp;

	Unique Key:		TQ01_Choice_Location
	Button Text:	Location
	Tooltip:		Ask where you might be able to find the thief.
	Appears When:
		Visual Script:
		(If ALL of the following is TRUE:)
		Flag 'TQ01_DISCUSSED_LOCATION' Equals False
		
		Lua:
		return not Storage.GetFlag("TQ01_DISCUSSED_LOCATION")

&nbsp;

	Unique Key:		TQ01_Choice_Reward
	Button Text:	Reward
	Tooltip:		Ask what you can expect for helping out.
	Appears When:
		Visual Script:
		(If ALL of the following is TRUE:)
		Flag 'TQ01_DISCUSSED_REWARD' Equals False
		
		Lua:
		return not Storage.GetFlag("TQ01_DISCUSSED_REWARD")

The 'Appears When' scripts ensure that the player can only select each choice once.

Add new state nodes to the three choices we just created, one for each choice:

	Parent:			TQ01_Choice_Friend
	Unique Key:		TQ01_Expo_Friend
	Actions Taken:
		Visual Script:
		Log: TQ01_INFO_FRIEND
		Set Flag TQ01_DISCUSSED_FRIEND to True
		
		Lua:
		Log("TQ01_INFO_FRIEND")
		Storage.SetFlag("TQ01_DISCUSSED_FRIEND", true)

&nbsp;

	Parent:			TQ01_Choice_Location
	Unique Key:		TQ01_Expo_Location
	Actions Taken:
		Visual Script:
		Log: TQ01_INFO_LOCATION
		Set Flag TQ01_DISCUSSED_LOCATION to True
		
		Lua:
		Log("TQ01_INFO_LOCATION")
		Storage.SetFlag("TQ01_DISCUSSED_LOCATION", true)

&nbsp;

	Parent:			TQ01_Choice_Reward
	Unique Key:		TQ01_Expo_Reward
	Actions Taken:
		Visual Script:
		Log: TQ01_INFO_REWARD
		Set Flag TQ01_DISCUSSED_REWARD to True
		
		Lua:
		Log("TQ01_INFO_REWARD")
		Storage.SetFlag("TQ01_DISCUSSED_REWARD", true)

Observe how the state nodes set the flags that the choice nodes check for.

With the scripts ready to go, let's head back to our `TQ01_Strings` string table and add the new string sets the scenes are now using:

`TQ01_INFO_FRIEND`:

> "Theo. He's a rat, and I mean that in every sense of the word. Dad always said he was untrustworthy, but somehow I kept believing my father was too harsh on him. Guess the old man was right again, after all."

`TQ01_INFO_LOCATION`:

> "I don't know where exactly he might be, but I bet the rodent is somewhere to the west of town. Probably has a hideout or something he's camped in."

`TQ01_INFO_REWARD`:

> The dingo sighs, "Shoulda known you'd ask that. Look, dad also left a bit of an inheritance, If you bring the pendant back to me, I'll give you a fair sum in return. If the rodent has already sold the pendant, I'll cover the cost to get it back."

Finally, to keep the conversation contained, we'll need to link back to all of our other nodes. So on each state node, add a link back to the **other two choices**. Now the conversation can loop from one topic to another, like this:

<CaptionedImage
	src={require("/images/Tutorial02DialogueLinks.png")}
	caption="Each topic should link back to the others, so the player can visit all of them in any order." />

:::tip
You can make a link quickly by right-clicking on the link target, then dragging it to the node where the link should be, and releasing. So for example, right-click `TQ01_Choice_Location` and drag it to `TQ01_Expo_Reward`.
:::

So, your links so far should look like this:

- `TQ01_Expo_Friend` links to `TQ01_Choice_Location` and `TQ01_Choice_Reward`,
- `TQ01_Expo_Location` links to `TQ01_Choice_Friend` and `TQ01_Choice_Reward`,
- `TQ01_Expo_Reward` links to `TQ01_Choice_Friend` and `TQ01_Choice_Location`.

## Progressing the quest

Finally, we'll want the conversation to end eventually, so we need a final choice node to wrap up the conversation. Let's create another 'discussion' topic, just like we did above, that progresses the quest after the player has viewed all the other topics:

	Parent:			TQ01_Tobias_Explain
	Unique Key:		TQ01_Choice_End
	Button Text:	Accept
	Tooltip:		That should be everything for now.
	Appears When:
		Visual Script:
		(If ALL of the following is TRUE:)
		Flag 'TQ01_DISCUSSED_FRIEND' Equals True
		Flag 'TQ01_DISCUSSED_LOCATION' Equals True
		Flag 'TQ01_DISCUSSED_REWARD' Equals True
		
		Lua:
		return
		    Storage.GetFlag("TQ01_DISCUSSED_FRIEND") and
		    Storage.GetFlag("TQ01_DISCUSSED_LOCATION") and
		    Storage.GetFlag("TQ01_DISCUSSED_REWARD")

And the accompanying state node:

	Unique Key:		TQ01_Expo_End
	Actions Taken:
		Visual Script:
			Log: TQ01_INFO_END
			Update Quest 'TQ01' to Stage 1
			Set Number Variable TQ01_PROGRESS to 2.00
		
		Lua:
			Log("TQ01_INFO_END")
			Journal.Update("TQ01", 1)
			Storage.SetNumber("TQ01_PROGRESS", 2)

Add another choice to say goodbye and wrap the conversation up fully:

	Unique Key:		TQ01_Expo_Accept
	Button Text:	Continue
	Tooltip:		Tell him you'll get the job done.

Lastly, add a link to `Main`, to ensure the player can actually return to the inn and continue playing. You'll also want to link to your new `TQ01_Choice_End` node from all three investigation options, to ensure that the 'Accept' button shows up as soon as the player finishes asking about all three topics, regardless of the order in which they were discussed.

Add the following to the `TQ01_Strings` table:

`TQ01_INFO_END`:

> With a nod, you stand up from the bench. "Please hurry back," Tobias says, "that pendant means a lot to me and my family."

Open up the `TQ01` journal asset again. Let's add a new entry with ID 1, with text that indicates the player has accepted the quest, like so:

> Tobias instructed me to look for Theo to the west side of the town. I should see about heading that way when I'm able.

Did you notice that in the `TQ01_Expo_End` state node, we changed the quest state variable (`TQ01_PROGRESS`) again? We'll use this so that the player cannot restart this discussion with Tobias, now that they've already done it once. The notice board set the variable to `1`, and the discussion sets it to `2`.

Let's go back to the `TQ01_Patch_Inn` scene. To ensure the player can only view this conversation once, we'll adjust the `TQ01_Tobias` node by adding an 'Appears When' script. The node should now look like this:

	Unique Key:		TQ01_Tobias
	Button Text:	Dingo
	Tooltip:		Talk to the dingo on the bench
	Appears When:
		Visual Script:
		(If ALL of the following is TRUE:)
		Number Variable 'TQ01_PROGRESS' Equals 1.00

		Lua:
		return Storage.GetNumber("TQ01_PROGRESS") == 1

Congratulations, we're all done! You've built a dialogue tree, with multiple topics the player can ask about in any order. After putting everything together, your `TQ01_Patch_Inn` scene should now look like this:

<CaptionedImage
	src={require("/images/Tutorial02InnPartial.png")}
	caption="The final result: this is what your inn scene should look like." />

That wraps up the investigation portion of the quest. The player can now visit the inn and discuss the quest with Tobias. We're now ready to start heading out of the town to the west to look for Theo and retrieve the missing pendant.
