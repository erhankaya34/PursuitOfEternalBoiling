using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private Dictionary<int, List<Quest>> questsByScene = new Dictionary<int, List<Quest>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeQuests();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeQuests()
    {
        questsByScene.Add(0, new List<Quest> {
            new Quest("Move Around", "Use WASD to move in all directions."),
            new Quest("Shoot Targets", "Destroy all targets in the area."),
            new Quest("Enter the Gate", "Proceed through the gate to the next city.")
        });
        // Add quests for other scenes...
    }

    public List<Quest> GetQuestsForCurrentScene(int sceneIndex)
    {
        if (questsByScene.ContainsKey(sceneIndex))
            return questsByScene[sceneIndex];
        else
            return new List<Quest>();
    }

    public void CompleteQuest(string questName)
    {
        var quests = questsByScene[UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex];
        Quest quest = quests.Find(q => q.questName == questName);
        if (quest != null && !quest.isComplete)
        {
            quest.CompleteQuest();
            QuestUI.Instance.UpdateQuestDisplay();
        }
    }
}