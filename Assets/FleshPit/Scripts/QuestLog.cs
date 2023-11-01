using System;
using System.Collections.Generic;

public class Quest
{
    public string title;
    public string description;
    public bool isComplete;

    public Quest(string title, string description)
    {
        this.title = title;
        this.description = description;
        this.isComplete = false;
    }
}
[System.Serializable]

public class QuestLog
{
    public List<Quest> quests;

    public QuestLog()
    {
        quests = new List<Quest>();
    }

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
    }

    public void PrintQuests()
    {
        Console.WriteLine("Current Quests:");
        foreach (Quest quest in quests)
        {
            Console.WriteLine(quest.title + " - " + quest.description);
            Console.WriteLine("Completed: " + quest.isComplete);
        }
    }
}

public class MainClass
{
    public static void Main()
    {
        QuestLog questLog = new QuestLog();

        // Create some example quests
        //Quest quest1 = new Quest("Kill the Goblin King", "The Goblin King has been terrorizing the village. Defeat him and bring peace back to the land.");
        //Quest quest2 = new Quest("Retrieve the Lost Artifact", "The ancient artifact has been lost for centuries. It's said to have the power to control time itself. Find it before it falls into the wrong hands.");
        //Quest quest3 = new Quest("Deliver the Package", "A valuable package needs to be delivered to a wealthy client. Make sure it arrives safely.");

        // Add the quests to the quest log
        //questLog.AddQuest(quest1);
        //questLog.AddQuest(quest2);
        //questLog.AddQuest(quest3);

        //// Print out the current quests in the quest log
        //questLog.PrintQuests();

        // Mark a quest as complete

    }
}





