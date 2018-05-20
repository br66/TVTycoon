using System;
using System.Collections;
using UnityEngine;

public class UIElementController
{
    // from public UiDialogHandle
    public UIElement Element;
    public Action<UIElement, object> Hidden;
    public bool Visible;
    public object ReturnValue;

    public IEnumerator WaitForHide()
    {
        while (Visible)
        {
            yield return null;
        }
    }

    // from private ModalEntity
    public bool IsTemporary { get; set; }
    public RectTransform Curtain { get; set; }
    public UIManager.UIElementSettings Option { get; set; }
}
