using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManger : MonoBehaviour, IInteractable
{
    //private static ButtonEventManger Instance;

    public delegate int increaseTotal(int addMore);
    public static increaseTotal confrimIncreaseTotal;

    //confrimIncreaseTotal.Invoke(1);
    private bool disableSCR;
    private int totalMax;
    private Outline outlineObject;
    [SerializeField] private GameObject _player;

    private List<GameObject> listChildren = new List<GameObject>();
    public int numChild { get; private set; }

    private void Awake()
    {
        confrimIncreaseTotal += AddTotal;
        outlineObject = GetComponent<Outline>();
        outlineObject.enabled = false;

        numChild = this.transform.childCount;
        for (int i = 0; i < numChild; i++)
        {
            listChildren.Add(this.transform.GetChild(i).gameObject);

        }

    }
    private void Start()
    {
        disableSCR = false;
    }

    private int AddTotal(int num)
    {
        totalMax += num;
        return totalMax;
    }

    public void OnInteract()
    {
        if (totalMax >= numChild)
        {
            disableSCR = true;
            confrimIncreaseTotal.Invoke(-totalMax);
        }
        else
        {
            Debug.Log("Boo");
        }
    }

    private void MessageBounce(GameObject gameObj)
    {
        gameObj.SendMessage("TotalBox",numChild);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < 3f)
        {
            outlineObject.enabled = true;
        }
        else
        {
            outlineObject.enabled = false;
        }
        
        if (disableSCR)
        {
            this.gameObject.SetActive(false);
        }
    }
}
