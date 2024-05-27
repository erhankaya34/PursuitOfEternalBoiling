[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public bool isComplete;

    public Quest(string name, string desc)
    {
        questName = name;
        description = desc;
        isComplete = false;
    }

    public void CompleteQuest()
    {
        isComplete = true;
    }
}