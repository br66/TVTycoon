using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vision;

public class UIManagerObj : MonoBehaviour {

    #region Updating the UI
    [SerializeField]
    private GameObject player;

    private bool playerExists;
    
    #endregion

    protected void Awake()
    {
        // start up the UIManager
        //UIManager.Initialize();
    }

    void Start()
    {
    }

    public void Update()
    {
        if (playerExists == true)
        {
            // Debug.Log(UIManager.Instance._uiControllers.Count);
            UIManager.Instance.UIChannelText.text = GameManager.Instance.Player.GetComponent<Vision.Network>().Name;
        }
        else
            Debug.Log("Player does not exist.");
        
    }

    public void OnPlayerInitialze()
    {
        List<Vision.Network> networks = transform.parent.GetComponentsInChildren<Vision.Network>().ToList();
        player = networks.First(x => x.gameObject.tag == "Player").gameObject;
        playerExists = true;

        List<string> programs = player.GetComponent<Vision.Network>().AllPrograms.Select(x => x.Name).ToList();
        //UIManager.Instance.UI.Find("Shyt I own").GetComponentInChildren<Text>().text = string.Join("\n", programs.ToArray());
        GameObject List = UIManager.Instance.UI.Find("Shyt I own").GetChild(0).gameObject;
        foreach (var show in programs)
        {
            GameObject gObject = new GameObject();
            Text txt = gObject.AddComponent<Text>();
            LayoutElement element = gObject.AddComponent<LayoutElement>();
            txt.text = show;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            gObject.transform.SetParent(List.transform, false);
            element.minWidth = 190;
            element.minHeight = 60;
            gObject.GetComponent<RectTransform>().sizeDelta = new Vector2(190, 60);
        }
    }

    #region Player Interactivity
    public void OnMessageBoxClick()
    {

        UIElementMsgBox.Show("This is a message <b>box</b>", _ =>
        {
            Debug.Log("MessageBox done");
        });
    }

    public void OnMessageQuestionBoxCoroutineClick()
    {
        StartCoroutine(ShowMessageQuestionBoxCoroutine());
    }

    private IEnumerator ShowMessageQuestionBoxCoroutine()
    {
        while (true)
        {
            var handle = UIElementMsgBox.Show("Do you want to continue?", UIElementMsgBox.QuestionType.ContinueStop);
            yield return StartCoroutine(handle.WaitForHide());
            //_logger.Info(handle.ReturnValue);
            if ((UIElementMsgBox.QuestionResult)handle.ReturnValue == UIElementMsgBox.QuestionResult.Continue)
            {
                var handle2 = UIElementMsgBox.Show("Let's do it again");
                yield return StartCoroutine(handle2.WaitForHide());
            }
            else
            {
                UIElementMsgBox.Show("Done");
                yield break;
            }
        }
    }
    #endregion
}
