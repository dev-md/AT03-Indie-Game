using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            print("we close");
            Application.Quit();
        }
    }
}
