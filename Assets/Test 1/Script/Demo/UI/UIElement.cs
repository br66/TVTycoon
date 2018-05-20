using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all UI Elements.  orig. UiDialog
public class UIElement : MonoBehaviour
{
    /*
     * What to do when a UIElement is showing 
     * It is recommended this be defined by a child of this class.
     * Object in parameter could be anything
     */
    public virtual void OnShow(object anything) { }

    /*
     * What to do when a UIElement is hidden 
     * It is recommended this be defined by a child of this class.
     * Object in parameter could be anything
     */
    public virtual void OnHide() { }

    /*
     * Hides the UIElement
     * It is recommended this be defined by a child of this class.
     * Object in parameter could be anything, usually the answer/button the user selected
     */
    public void Hide(object UserSelection = null)
    {
        UIManager.Instance.HideElement(this, UserSelection);
    }

}
