using System.Collections;
using DatabaseControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountManager : MonoBehaviour
{
    //creating a singleton (an instance that will be carried over from script to script)
    public static AccountManager instance;

    void Awake()
    {
        //make sure this object is the only instance in the scene
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public static string LoggedIn_Username { get; protected set; }      //this string can be accessed from other scripts but only an instance of this class can set its value
    private static string LoggedIn_Password = "";
    
    public static bool IsLoggedIn { get; protected set; }

    public static string LoggedIn_Data { get; protected set; }


    public delegate void onRecievedDataCallBack(string data);       //Delegates allow methods to be passed as parameters. Delegates can be used to define callback methods.

    public void logOut()
    {
        LoggedIn_Username = "";
        LoggedIn_Password = "";

        IsLoggedIn = false;

        SceneManager.LoadScene("Login");
    }



    public void logIn(string username, string password)
    {
        LoggedIn_Username = username;
        LoggedIn_Password = password;

        IsLoggedIn = true;

        SceneManager.LoadScene("MainMenu");


    }

    public void SaveData(string data)
    {
        if (IsLoggedIn)
        {
            StartCoroutine(SetData(data));
        }

    }



    IEnumerator SetData(string data)
    {
        IEnumerator e = DCF.SetUserData(LoggedIn_Username, LoggedIn_Password, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //The data string was set correctly.
        }
        else
        {
            //There was another error.
        }
    }


    public void LoadData(onRecievedDataCallBack onDataReceived)
    {
        if (IsLoggedIn)
        {
            StartCoroutine(GetData(onDataReceived));
        }
    }


    IEnumerator GetData(onRecievedDataCallBack onDataRecieved)
    {
        string data = "error";

        IEnumerator e = DCF.GetUserData(LoggedIn_Username, LoggedIn_Password); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Error")
        {
            //There was another error.
        }
        else
        {
            //The player's data was retrieved. 
            data = response;
        }

        //set the LoggedIn_data equal to the recieved data
        LoggedIn_Data = data;
        if(onDataRecieved != null)
            onDataRecieved.Invoke(data);
    }


   
}
