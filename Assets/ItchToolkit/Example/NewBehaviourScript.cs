using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public void Testing(InputField inputField)
    {
        ItchToolkit.ItchUserRequest itchUserRequest = new ItchToolkit.ItchUserRequest(inputField.text);
        Debug.Log(itchUserRequest.user.userName);
    }
}
