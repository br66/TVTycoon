using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager
{
    public static UIManager Instance { get; private set; }

    // ref to heads-up display
    private RectTransform _uiRoot;

    // ref to the Menu
    private RectTransform _menuRoot;

    // ref to templates for UI Elements kept in hierarchy for quicker loading
    // they are enabled when needed
    // orig. _dialogRoot
    private RectTransform _templatesRoot;

    public RectTransform UI { get { return _uiRoot; } }
    public RectTransform Menu { get { return _menuRoot; } }
    public RectTransform Templates { get { return _templatesRoot; } }

    [Flags]
    public enum UIElementSettings
    {
        None,
        ShowCurtain = 1,
    }

    private RectTransform _inputBlocker;
    private int _inputBlockCount;
    private int _curtainCount;
    private RectTransform _fadingOutCurtain;

    public List<UIElementController> _uiControllers = new List<UIElementController>();

    public static void Initialize()
    {
        Instance = new UIManager();
    }

    public UIManager()
    {
        // ref to Canvas
        _uiRoot = GameObject.Find("UI").GetComponent<RectTransform>();
        // ref to buttons that will trigger the UI Modals
        _menuRoot = GameObject.Find("UI/Menu").GetComponent<RectTransform>();
        // ref to UI Modals (initially disabled)
        _templatesRoot = GameObject.Find("UI/Templates").GetComponent<RectTransform>();
    }

    public RectTransform CanvasRoot
    {
        get { return _uiRoot; }
    }

    public RectTransform MenuRoot
    {
        get { return _menuRoot; }
    }

    public RectTransform TemplatesRoot
    {
        get { return _templatesRoot; }
    }

    // Finds one of the four dialog modals by name
    public RectTransform FindFromTemplate(string name)
    {
        var transform = _templatesRoot.Find(name);

        // return its transform if not null, else it has a rectTransform, else it's actually null
        return transform != null ? transform.GetComponent<RectTransform>() : null;
    }

    public bool InputBlocked
    {
        get { return _inputBlockCount > 0; }
    }

    // creates a gameObject made to black out the background to put emphasis on the new UIElement
    // returns its rectTransform
    private RectTransform CreateCurtain(Color color, RectTransform parent)
    {
        var go = new GameObject("Curtain");
        var rt = go.AddComponent<RectTransform>();
        var image = go.AddComponent<Image>();
        image.color = color;
        rt.SetParent(parent, false);
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        return rt;
    }

    public void ShowInputBlocker()
    {
        if (_inputBlockCount == 0)
        {
            if (_inputBlocker != null)
            {
                //Debug.Log("Blocker already exists on ShowInputBlocker count={0}", _inputBlockCount);
                return;
            }

            _inputBlocker = CreateCurtain(new Color(0, 0, 0, 0.5f), _uiRoot);
        }

        _inputBlockCount++;
    }

    public void HideInputBlocker()
    {
        if (_inputBlockCount <= 0)
        {
            //Debug.Log("Invalid count on HideInputBlocker count={0}", _inputBlockCount);
            return;
        }

        _inputBlockCount--;

        if (_inputBlockCount == 0)
        {
            UnityEngine.Object.Destroy(_inputBlocker.gameObject);
            _inputBlocker = null;
        }
    }

    public bool CurtainVisible
    {
        get { return _curtainCount > 0; }
    }

    // this function is used.
    public UIElementController ShowElementTemplate(GameObject gameObject, object param = null,
                                      UIElementSettings option = UIElementSettings.ShowCurtain)
    {
        //Debug.Log("ShowModalTemplate({0})", gameObject.transform.name);

        // creates a copy of the gameObject (2nd parameter) and makes it a child
        // of the 1st parameter and names it "dialogGo"
        // might change parent
        var dialogGo = UIManagementStatic.AddChild(_templatesRoot.gameObject, gameObject);
        dialogGo.SetActive(true);

        var dialog = dialogGo.GetComponent<UIElement>();
        return ShowElementInternal(dialog, true, param, option);
    }

    // Brings down curtain, shows Dialog, sends Dialog info into a handle, 
    // creates Modal entity based on Dialog and adds to list
    private UIElementController ShowElementInternal(UIElement element, bool isTemporary, object param, UIElementSettings option)
    {
        // create curtain for blocking input
        RectTransform curtain = null;

        // if we are instructed to show the black curtain and there is already one fading out
        if ((option & UIElementSettings.ShowCurtain) != 0 && _fadingOutCurtain != null)
        {
            // When there is a curtain fading out, reuse it to reduce flickering
            curtain = _fadingOutCurtain;

            // Kill the curtain
            DOTween.Kill(curtain.GetComponentInChildren<Image>());
            _fadingOutCurtain = null;
        }
        else
        {
            // if not, create the curtain and set its parent
            curtain = CreateCurtain(new Color(0, 0, 0, 0.5f), _templatesRoot);
        }

        curtain.SetSiblingIndex(element.GetComponent<RectTransform>().GetSiblingIndex());

        // unnecessary but it works as workaround for setting sibling index
        element.GetComponent<RectTransform>().SetSiblingIndex(curtain.GetSiblingIndex() + 1);

        // if im supposed to show curtain, show curtain
        if ((option & UIElementSettings.ShowCurtain) != 0)
        {
            _curtainCount += 1;
            var image = curtain.GetComponentInChildren<Image>();
            //image.DOColor(new Color(0, 0, 0, 0.7f), 0.15f).SetUpdate(true);
        }

        // fade in dialog
        var dialogRt = element.GetComponent<RectTransform>();
        dialogRt.localScale = Vector3.one * 1.1f;
        dialogRt.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutCubic).SetUpdate(true);

        var dialogCg = element.GetComponent<CanvasGroup>();
        //dialogCg.alpha = 0.2f;
        //dialogCg.GetComponentInChildren<CanvasRenderer>().GetMaterial().DOFade(1f, 0.2f).SetEase(Ease.OutCubic).SetUpdate(true);
        //CanvasRenderer[] rends = dialogCg.GetComponentsInChildren<CanvasRenderer>();

        /*for (int i = 0; i < rends.Length; i++)
        {
            rends[i].GetMaterial().DOFade(1f, 0.2f).SetEase(Ease.OutCubic).SetUpdate(true);
        }*/
        
        /*_uiControllers.Add(new UIElementController
        {
            Element = element,
            Visible = true,
            IsTemporary = isTemporary,
            Curtain = curtain,
            Option = option
        });*/

        UIElementController controller = new UIElementController
        {
            Element = element,
            Visible = true,
            IsTemporary = isTemporary,
            Curtain = curtain,
            Option = option
        };

        _uiControllers.Add(controller);

        // does nothing, but could do something if needed i guess
        element.OnShow(param);

        return controller;
    }

    internal bool HideElement(UIElement element, object returnValue)
    {
        //_logger.InfoFormat("HideModal({0})", dialog.name);

        // Find the index# of the modal that has the dialog specified in the parameter.
        var i = _uiControllers.FindIndex(m => m.Element == element);
        if (i == -1)
        {
            //_logger.Info("Failed to hide modal because not shown");
            return false;
        }

        // save the index number and use it to find/save the entity
        var controller = _uiControllers[i];
        // remove it from the modal list
        _uiControllers.RemoveAt(i);

        // trigger UIElement.OnHide
        var uiElement = controller.Element.GetComponent<UIElement>();
        if (uiElement != null)
            uiElement.OnHide();

        // remove dialog
        var dialogCg = element.GetComponent<CanvasGroup>();
        // after the fade, if the ent. is temporary, destroy it
        // or if not temporary, just disable it
        //dialogCg.GetComponentInChildren<CanvasRenderer>().GetMaterial().DOFade(0f, 0.2f).SetEase(Ease.OutCubic).SetUpdate(true).OnComplete(() =>
       // {
            if (controller.IsTemporary)
            {
                UnityEngine.Object.Destroy(controller.Element.gameObject);
            }
            else
            {
                controller.Element.gameObject.SetActive(false);
            }
        //});

        // if the dialog had a curtain...
        if (controller.Curtain != null)
        {
            // if black curtain was included with the modal...
            if ((controller.Option & UIElementSettings.ShowCurtain) != 0)
            {
                // get the curtain, use it for fading out
                _curtainCount -= 1;
                _fadingOutCurtain = controller.Curtain;

                // get the curtain's image
                var image = controller.Curtain.GetComponentInChildren<Image>();

                // after the animation, remove curtain
                /*image.material.DOColor(new Color(0, 0, 0, 0), 0.1f).SetEase(Ease.InQuad).SetUpdate(true)
                     .OnComplete(() =>
                     {
                         if (_fadingOutCurtain == controller.Curtain)
                             _fadingOutCurtain = null;
                         UnityEngine.Object.Destroy(controller.Curtain.gameObject);
                     });*/
                if (_fadingOutCurtain == controller.Curtain)
                    _fadingOutCurtain = null;
                UnityEngine.Object.Destroy(controller.Curtain.gameObject);
            }
            else
            {
                // if it didn't, destroy it??? because each modal has a curtain regardless
                UnityEngine.Object.Destroy(controller.Curtain.gameObject);
            }
        }

        // handle is no longer visible
        controller.Visible = false;

        // save return value
        controller.ReturnValue = returnValue;

        // trigger Hidden event already defined in 
        // trigger previously defined 'future function'
        if (controller.Hidden != null)
            controller.Hidden(controller.Element, returnValue);

        controller = null;

        return true;
    }

    public UIElementController ShowElement<T>(T element, object param = null,
                                   UIElementSettings option = UIElementSettings.ShowCurtain)
    where T : UIElement
    {
        //_logger.InfoFormat("ShowModal({0})", dialog.GetType().Name);

        if (element.gameObject.activeSelf)
        {
            //_logger.InfoFormat("Failed to show modal because already shown");
            return null;
        }

        element.gameObject.SetActive(true);
        return ShowElementInternal(element, false, param, option);
    }

    public UIElementController ShowElementRoot<T>(object param = null,
                                       UIElementSettings option = UIElementSettings.ShowCurtain)
    where T : UIElement
    {
        var dialogGo = FindFromTemplate(typeof(T).Name);
        if (dialogGo == null)
            throw new Exception("ShowModalRoot not found: " + typeof(T).Name);

        var dialog = dialogGo.GetComponent<T>();
        if (dialog == null)
            throw new Exception("ShowModalRoot type mismatched: " + typeof(T).Name);

        return ShowElement(dialog, param, option);
    }
}
