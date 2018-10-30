using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthZoneUi : MonoBehaviour
{

    public GameObject yButton;
    public GameObject wKey;

    void Update()
    {
        if (Managers.PlayerManager.Instance.canEnterStealthZone)
        {
            if (Input.GetJoystickNames().Length < 1)
            {
				
				wKey.SetActive(true);
				yButton.SetActive(false);
            }
			 if (Input.GetJoystickNames().Length >= 1)
            {
				Debug.Log(Input.GetJoystickNames().Length);
				wKey.SetActive(false);
				yButton.SetActive(true);
            }
        }
		if (!Managers.PlayerManager.Instance.canEnterStealthZone)
		{
			if (Input.GetJoystickNames().Length == 0)
            {
				wKey.SetActive(false);
				yButton.SetActive(false);
            }
			 if (Input.GetJoystickNames().Length >= 1)
            {
				wKey.SetActive(false);
				yButton.SetActive(false);
            }
		}
    }
}
