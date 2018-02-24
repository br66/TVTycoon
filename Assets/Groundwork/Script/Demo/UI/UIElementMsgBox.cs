using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// orig. UiMessageBox
public class UIElementMsgBox : UIElement
{
    public enum QuestionType
    {
        Ok,
        OkCancel,
        RetryCancel,
        YesNo,
        ContinueStop
    }

    public enum QuestionResult
    {
        Close,
        Ok,
        Cancel,
        Retry,
        Yes,
        No,
        Continue,
        Stop
    }

    public delegate void QuestionResultDelegate(QuestionResult result);

    public Text MessageText;
    public Button[] Buttons;

    // show a Message box
    public static UIElementController Show(string msg, QuestionResultDelegate callback = null)
    {
        UIElementController controller; // = new UIElementController();
        {
            var builtin = UIManager.Instance.FindFromTemplate("MessageBox");
            if (builtin != null)
            {
                // get version from hierarchy
                controller = UIManager.Instance.ShowElementTemplate(builtin.gameObject);
            }
            else
            {
                // get version from resources folder
                // this cannot run; there is no resources folder
                //var msgBoxPrefab = Resources.Load("MessageBox") as GameObject;
                //controller = UIManager.Instance.ShowModalPrefab(msgBoxPrefab);
                controller = UIManager.Instance.ShowElementTemplate(builtin.gameObject);
            }
        }
        // In the future, there will be flying cars and this callback will be called (Ex. UIManagerSamele.cs line 20)
        // when the hidden command is told to run.
        if (callback != null)
            controller.Hidden += (dlg, returnValue) => callback((QuestionResult)returnValue);

        var msgBox = (UIElementMsgBox)controller.Element;
        msgBox.MessageText.text = msg;
        msgBox.Buttons[0].onClick.AddListener(msgBox.OnMessageBoxOkButtonClick);

        return controller;
    }

    private void OnMessageBoxOkButtonClick()
    {
        Hide(QuestionResult.Ok);
    }

    public static UIElementController Show(
       string msg, QuestionType questionType,
       QuestionResultDelegate callback = null, string customOkName = null)
    {
        UIElementController controller;
        {
            var builtin = UIManager.Instance.FindFromTemplate("QuestionBox");
            if (builtin != null)
            {
                controller = UIManager.Instance.ShowElementTemplate(builtin.gameObject);
            }
            else
            {
                // var msgBoxPrefab = Resources.Load("MessageQuestionBox") as GameObject;
                //controller = UIManager.Instance.ShowModalPrefab(msgBoxPrefab);
                controller = UIManager.Instance.ShowElementTemplate(builtin.gameObject);
            }
        }

        if (callback != null)
            controller.Hidden += (dlg, returnValue) => callback((QuestionResult)returnValue);

        var msgBox = (UIElementMsgBox)controller.Element;
        msgBox.MessageText.text = msg;

        var b0 = msgBox.Buttons[0];
        var b0Text = b0.transform.Find("Text").GetComponent<Text>();
        var b1 = msgBox.Buttons[1];
        var b1Text = b1.transform.Find("Text").GetComponent<Text>();

        b1.gameObject.SetActive(questionType != QuestionType.Ok);

        switch (questionType)
        {
            case QuestionType.Ok:
                b0Text.text = customOkName ?? "Ok";
                b0.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Ok));
                b1.gameObject.SetActive(false);
                break;

            case QuestionType.OkCancel:
                b0Text.text = customOkName ?? "Ok";
                b0.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Ok));
                b1Text.text = "Cancel";
                b1.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Cancel));
                break;

            case QuestionType.RetryCancel:
                b0Text.text = "Retry";
                b0.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Retry));
                b1Text.text = "Cancel";
                b1.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Cancel));
                break;

            case QuestionType.YesNo:
                b0Text.text = "Yes";
                b0.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Yes));
                b1Text.text = "No";
                b1.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.No));
                break;

            case QuestionType.ContinueStop:
                b0Text.text = "Continue";
                b0.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Continue));
                b1Text.text = "Stop";
                b1.onClick.AddListener(() => msgBox.OnQuestionBoxButtonClick(QuestionResult.Stop));
                break;
        }

        return controller;
    }

    private void OnQuestionBoxButtonClick(QuestionResult result)
    {
        Hide(result);
    }
}
