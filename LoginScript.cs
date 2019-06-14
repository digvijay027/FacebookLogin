using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {


	// public GameObject Panel_Add;
	  public Text FB_userName;
	 public Image FB_useerDp;

	void Awake()
	{
		FB.Init (SetInit, OnHideUnity);
		// Panel_Add.SetActive(false);
	}

	void SetInit()
	{
		if (FB.IsLoggedIn) 
		{
			Debug.Log ("Logged In");
		} 
		else
		{
			Debug.Log (" Not Logged In");
		}
		DealWithFbMenus(FB.IsLoggedIn);

	}

	void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown) 
		{
			Time.timeScale = 0;
		}
		else 
		{
			Time.timeScale = 1;

		}
	}

	public void FBLogin()
	{
		List<string> permission = new List<string> ();
		permission.Add ("Public_Profile");
		FB.LogInWithReadPermissions (permission, AuthCallBack);
	}

	void AuthCallBack(IResult result)
	{
		if (result.Error != null) {
			Debug.Log (result.Error);
		} 
		else 
		{
			if (FB.IsLoggedIn) 
			{
				Debug.Log ("Logged In");
			} 
			else
			{
				Debug.Log (" Not Logged In");
			}

			DealWithFbMenus (FB.IsLoggedIn);
		}
	}


	void DealWithFbMenus(bool IsLoggedIn)
	{
		if (IsLoggedIn) 
		{
			FB.API("/me?fields=first_name",HttpMethod.GET,DisplayUserName);
			FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfielPic);

		}
		else 
		{
			
		}
	}

	void DisplayUserName(IResult result)
	{
		if (result.Error == null) {
			string name = ""+result.ResultDictionary["first_name"];
			//  FB_userName.text = name;
			Debug.Log (name);
		}
		else 
		{
			Debug.Log (result.Error);
		}
	}


	void DisplayProfielPic(IGraphResult result)
	{
		if (result.Texture!= null) 
		{
			Debug.Log("Profile Pic");
			FB_useerDp.sprite = Sprite.Create(result.Texture,new Rect(0,0,128,128),new Vector2());
		}
		else 
		{
			Debug.Log (result.Error);
		}
	}


}
