using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private float dist = 3f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Use"))
        { 
            //Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit hitTarget, dist);
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitTarget, dist) == true)
            {
                if(hitTarget.transform.TryGetComponent(out IInteractable interaction) == true)
                {
                    interaction.OnInteract();
                }
            }
        }
    }
}


public interface IInteractable
{
    void OnInteract();
}