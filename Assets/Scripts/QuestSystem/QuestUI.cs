using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public static QuestUI Instance { get; private set; }

    public GameObject questItemPrefab;
    public Transform questListParent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateQuestDisplay()
    {
        ClearQuestDisplay();
        var quests = QuestManager.Instance.GetQuestsForCurrentScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        foreach (Quest quest in quests)
        {
            GameObject questItemGO = Instantiate(questItemPrefab, questListParent);
            questItemGO.GetComponentInChildren<Text>().text = $"{quest.questName}: {quest.description}";
            questItemGO.transform.Find("CompleteIcon").gameObject.SetActive(quest.isComplete);
        }
    }

    private void ClearQuestDisplay()
    {
        foreach (Transform child in questListParent)
        {
            Destroy(child.gameObject);
        }
    }
}