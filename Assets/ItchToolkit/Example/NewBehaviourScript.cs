using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public void Testing(InputField inputField)
    {
        string yourKey = "06Y5yopS5pi2AV1N7OMBoX6IaqSus2W8770r8K7g";
        string gameID = "151578";
        ItchToolkit.ItchUserRequest itchUserRequest = new ItchToolkit.ItchUserRequest(yourKey);
        Debug.Log(itchUserRequest.user.userName);
        Debug.Log("Game State: " + itchUserRequest.IsGameOwned(yourKey, gameID));
    }
}
