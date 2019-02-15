using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItchToolkitExample : MonoBehaviour
{
    public string clientID = "";
    public string yourToken = "";
    private string userToken;

    public GameObject profilePageObject;
    public Text profileInfortext;
    public RawImage avatarImage;
    public Text gameDRMText;

    private ItchToolkit.ItchUserRequest userRequest;

    public void GetOAuthToken()
    {
        if (string.IsNullOrEmpty(clientID))
            throw new Exception("Error: Cannot proceed without a clientID\n clientID can be entered under \"" + this.gameObject.name + "\", GameObject in Scene Hierarchy.");
        else
            ItchToolkit.OAuthRequest.RequestOAuthApiKey(clientID);
    }

    public void VerifyOAuthToken(InputField tokenInputField)
    {
        if (ItchToolkit.OAuthRequest.IsApiKeyValid(tokenInputField.text))
        {
            profilePageObject.SetActive(true);
            LoadProfileInfo(tokenInputField.text);
        }
        else
        {
            throw new Exception("Error: Token is not valid, confirm details and try again.");
        }
    }

    private void LoadProfileInfo(string userKey)
    {
        userRequest = new ItchToolkit.ItchUserRequest(userKey);
        profileInfortext.text = "Display Name: " + userRequest.user.displayName + "\n";
        profileInfortext.text += "User ID: " + userRequest.user.userID + "\n";
        profileInfortext.text += "Developer: " + userRequest.user.isDeveloper + "\n";
        profileInfortext.text += "Gamer: " + userRequest.user.isGamer + "\n";
        profileInfortext.text += "Press User: " + userRequest.user.isPressUser;
        if (!string.IsNullOrEmpty(userRequest.user.avatarURL))
        {
            Texture2D loadTex = new Texture2D(avatarImage.mainTexture.width, avatarImage.mainTexture.height);
            loadTex.LoadImage(userRequest.GetAvatarByteArray());
            avatarImage.texture = loadTex;
        }
    }

    public void CheckDRM(InputField gameIdInputField)
    {
        if (string.IsNullOrEmpty(gameIdInputField.text))
            throw new Exception("Error: Cannot proceed without a gameID");
        if (string.IsNullOrEmpty(yourToken))
            throw new Exception("Error: Cannot proceed without a yourToken\n yourToken can be entered under \"" + this.gameObject.name + "\", GameObject in Scene Hierarchy.");
        ItchToolkit.DownloadKeyRequest downloadKeyRequest = new ItchToolkit.DownloadKeyRequest(yourToken, gameIdInputField.text, userRequest.user.userID.ToString());
        if (downloadKeyRequest.error == ItchToolkit.DownloadKeyRequest.RequestType.ErrorType.NULL)
            gameDRMText.text = "Game Owned";
        else
            gameDRMText.text = downloadKeyRequest.error.ToString();
    }

    public void ToggleEnable(GameObject gameObject)
    {
       gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
