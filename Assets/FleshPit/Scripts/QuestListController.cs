using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using TMPro;
using UnityEngine;


public class QuestListController : NetworkBehaviour
{
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public QuestLog questLog;
    public PlayerControllerWithFreeLookCamera playerController;

    private Quest quest1;
    private Quest quest2;
    private Quest quest3;

     void Start()
    {


        // Initialize the example quests
        quest1 = new Quest("Damage 50 monster Before countdown", "0/50");
        quest2 = new Quest("Solve the puzzle", "0/1");
        quest3 = new Quest("Cure the infection", "0/1");

        // Add the example quests to the quest log
        questLog.quests.Add(quest1);
        questLog.quests.Add(quest2);
        questLog.quests.Add(quest3);

        // Display the titles and descriptions of the quests on the UI panel
        UpdateQuestPanel();
        

    }

    void Update()
    {

            playerController = NetworkClient.localPlayer.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>();
        // Check if quest 1 is complete
        if (playerController.RefreshQuestLog >= 50)
        {
            questLog.quests.Remove(quest1);
            quest1 = new Quest("Kill 50 monster Before countdown", "50/50 (Complete)");
            questLog.quests.Add(quest1);
            // You can add additional rewards or update other game systems here
        }
        // Otherwise, update the quest description
        else
        {
            // Check if the quest is already in the quest log
            if (!questLog.quests.Contains(quest1))
            {
                questLog.quests.Add(quest1);
            }
            // Update the quest description
            quest1.description = playerController.RefreshQuestLog.ToString() + "/50";
        }

        //// Update the quest log panel
        UpdateQuestPanel();
        

    }

    // Update the quest log whenever the RefreshQuestLog variable changes
    public void OnRefreshQuestLogChanged(int newvalue)
    {
        playerController.RefreshQuestLog = newvalue;
        quest1.description = playerController.RefreshQuestLog.ToString() + "/50";
        // Update the quest log panel
        UpdateQuestPanel();

    }

    // Update the quest log panel with the current quest information
    public void UpdateQuestPanel()
    {
        // Clear the existing quest information
        questTitleText.text = "";
        questDescriptionText.text = "";

        // Add the updated quest information to the UI panel
        foreach (Quest quest in questLog.quests)
        {
            // Get the quest title and description
            string questTitle = quest.title;
            string questDescription = quest.description;

            // Add the quest title and description to the UI panel
            questTitleText.text += questTitle + "\n";
            questDescriptionText.text += questDescription + "\n";
        }
    }
}
