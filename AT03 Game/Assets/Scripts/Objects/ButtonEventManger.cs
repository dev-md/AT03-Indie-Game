using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManger : MonoBehaviour
{
    private static ButtonEventManger Instance;

    public delegate int increaseTotal(int addMore);
    public static increaseTotal confrimIncreaseTotal;
    //confrimIncreaseTotal.Invoke(1);

    public Transform playerTran;
    private Camera playerCamera;
    private bool disableSCR;

    private int totalMax;

    private void Awake()
    {
        confrimIncreaseTotal += AddTotal;
    }
    private void Start()
    {
        playerTran.GetChild(0).TryGetComponent<Camera>(out playerCamera);
        disableSCR = false;
    }

    void Update()
    {
        if (disableSCR == false)
        {
            if ((Vector3.Distance(playerTran.transform.position, this.transform.position) < 2.5f))
            {
                if (Input.GetKeyDown("e"))
                {
                    Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitTarget, 3f);
                    if (hitTarget.transform == this.transform)
                    {
                        if(totalMax > 2)
                        {

                            disableSCR = true;

                            this.gameObject.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Boo");
                        }
                    }
                }
            }
        }
    }

    private int AddTotal(int num)
    {
        totalMax += num;
        return totalMax;
    }
}
