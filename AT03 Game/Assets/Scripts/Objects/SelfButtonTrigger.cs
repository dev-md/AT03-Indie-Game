using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfButtonTrigger : MonoBehaviour
{
    public Transform playerTran;
    public Camera playerCamera;
    private bool disableSCR;

    private MeshFilter meshChange;

    // Start is called before the first frame update
    void Start()
    {
        playerTran.GetChild(0).TryGetComponent<Camera>(out playerCamera);
        disableSCR = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (disableSCR == false)
        {
            if ((Vector3.Distance(playerTran.transform.position, this.transform.position) < 2.5f))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitTarget, 3f);
                    if (hitTarget.transform == this.transform)
                    {
                        ButtonEventManger.confrimIncreaseTotal.Invoke(1);
                        disableSCR = true;

                        //this is temp
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}