using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInteraction : MonoBehaviour
{
    private float dist = 3f;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitTarget, dist) == true)
        {
            if (hitTarget.transform.TryGetComponent(out IHoverInteraction interaction) == true)
            {
                interaction.HoldInter();
            }
        }
    }
}

public interface IHoverInteraction
{
    void HoldInter();
}