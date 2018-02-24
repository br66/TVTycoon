using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManagerObj : MonoBehaviour {

    #region For updating the UI
    [SerializeField]
    private GameObject player;
    #endregion

    protected void Awake()
    {
        // start up the UIManager
        UIManager.Initialize();
    }

    public void Update()
    {
       // Debug.Log(UIManager.Instance._uiControllers.Count);
    }

    public void OnPlayerInitialze()
    {
        List<Vision.Network> networks = transform.parent.GetComponentsInChildren<Vision.Network>().ToList();
        player = networks.First(x => x.gameObject.tag == "Player").gameObject;
    }

    #region UI updating

    #endregion

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
