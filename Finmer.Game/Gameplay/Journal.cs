/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents the player's quest journal.
    /// </summary>
    public class Journal
    {

        private readonly Dictionary<Guid, int> m_Quests = new Dictionary<Guid, int>();

        public Journal(PropertyBag template = null)
        {
            DeserializeProperties(template ?? new PropertyBag());
        }

        /// <summary>
        /// Adds or updates the specified quest, and sets it to the specified stage.
        /// </summary>
        public void SetQuestStage(AssetJournal quest, int stage)
        {
            SetQuestStage(quest.ID, stage);
            GameUI.Instance.Log("Your journal has been updated.", Theme.LogColorOrange);
        }

        /// <summary>
        /// Returns the current stage of the specified quest. Throws if the quest is not in the journal.
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        public int GetQuestStage(AssetJournal quest)
        {
            return m_Quests[quest.ID]; // throws if not exists...
        }

        /// <summary>
        /// Removes the specified quest from the journal.
        /// </summary>
        public void CloseQuest(AssetJournal quest)
        {
            m_Quests.Remove(quest.ID);
        }

        /// <summary>
        /// Returns an enumerator that can be used to obtain all quests listed in the journal.
        /// </summary>
        public IEnumerable<AssetJournal> GetAllQuests()
        {
            foreach (KeyValuePair<Guid, int> pair in m_Quests)
            {
                var quest = GameController.Content.GetAssetByID(pair.Key) as AssetJournal;
                if (quest == null) throw new KeyNotFoundException("No matching AssetJournal found for GUID " + pair.Key);

                yield return quest;
            }
        }

        /// <summary>
        /// Saves the journal's state to a new PropertyBag and returns it.
        /// </summary>
        public PropertyBag SerializeProperties()
        {
            var props = new PropertyBag();
            props.SetInt("count", m_Quests.Count);

            var i = 0;
            foreach (KeyValuePair<Guid, int> pair in m_Quests)
            {
                props.SetBytes("guid_" + i, pair.Key.ToByteArray());
                props.SetInt("stage_" + i, pair.Value);
                i++;
            }

            return props;
        }

        private void SetQuestStage(Guid id, int stage)
        {
            if (m_Quests.ContainsKey(id))
                m_Quests[id] = stage;
            else
                m_Quests.Add(id, stage);
        }

        private void DeserializeProperties(PropertyBag template)
        {
            m_Quests.Clear();

            int count = template.GetInt("count");
            for (var i = 0; i < count; i++)
            {
                var guid = new Guid(template.GetBytes("guid_" + i));
                int stage = template.GetInt("stage_" + i);

                m_Quests.Add(guid, stage);
            }
        }

    }

}
