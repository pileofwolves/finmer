/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
            GameUI.Instance.Log("Your journal has been updated.", Theme.LogColorNotification);
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
            foreach (var pair in m_Quests)
            {
                var quest = GameController.Content.GetAssetByID(pair.Key) as AssetJournal;
                if (quest == null)
                    throw new KeyNotFoundException("No matching AssetJournal found for GUID " + pair.Key);

                yield return quest;
            }
        }

        /// <summary>
        /// Saves the journal's state to a new PropertyBag and returns it.
        /// </summary>
        public PropertyBag SerializeProperties()
        {
            var props = new PropertyBag();

            // Entry count
            props.SetInt(SaveData.k_Journal_Count, m_Quests.Count);

            // Individual entries
            var i = 0;
            foreach (var pair in m_Quests)
            {
                props.SetBytes(SaveData.CombineBase(SaveData.k_Journal_EntryGuid, i), pair.Key.ToByteArray());
                props.SetInt(SaveData.CombineBase(SaveData.k_Journal_EntryStage, i), pair.Value);
                i++;
            }

            return props;
        }

        private void SetQuestStage(Guid id, int stage)
        {
            // Add or update this quest
            if (m_Quests.ContainsKey(id))
                m_Quests[id] = stage;
            else
                m_Quests.Add(id, stage);
        }

        private void DeserializeProperties(PropertyBag template)
        {
            // Ensure different states cannot mix
            m_Quests.Clear();

            // Read the number of entries
            int count = template.GetInt(SaveData.k_Journal_Count);
            for (var i = 0; i < count; i++)
            {
                // Read individual entries
                var guid = new Guid(template.GetBytes(SaveData.CombineBase(SaveData.k_Journal_EntryGuid, i)));
                int stage = template.GetInt(SaveData.CombineBase(SaveData.k_Journal_EntryStage, i));

                m_Quests.Add(guid, stage);
            }
        }

    }

}
