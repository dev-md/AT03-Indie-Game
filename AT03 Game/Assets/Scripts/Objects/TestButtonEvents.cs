using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonEvents : MonoBehaviour, IInteractable
{
    #region Base vars
    delegate void TestInteractionDelegate();
    TestInteractionDelegate testInteractionDelegate = delegate { };

    #endregion

    // Start is called before the first frame update
    private void OnEnable()
    {
        testInteractionDelegate += TestMethod;
    }

    private void OnDisable()
    {
        testInteractionDelegate -= TestMethod;
    }

    public void OnInteract()
    {
        testInteractionDelegate.Invoke();
    }

    private void TestMethod()
    {
        Debug.Log("AHOY");
    }
}
