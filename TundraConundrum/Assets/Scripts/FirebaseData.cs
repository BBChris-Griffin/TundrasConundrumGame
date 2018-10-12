using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;

public class FirebaseData : MonoBehaviour {

    public string userID;
    public string roomID;

	// Use this for initialization
	void Start () {
        GetData(userID, roomID);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetData(string userID, string roomID)
    {
        Firebase firebase = Firebase.CreateNew("https://tundrasconundrum-31ea5.firebaseio.com", "");
        Firebase user = firebase.Child("users");
        user.OnGetSuccess += GetOKHandler;
        user.GetValue("print=pretty");
        user.OnGetSuccess -= GetOKHandler;

    }

    void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        Debug.Log("[OK] Get from key: <" + sender.FullKey + ">");
        Debug.Log("[OK] Raw Json: " + snapshot.RawJson);

        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        if (keys != null)
            foreach (string key in keys)
            {
                Debug.Log(key + " = " + dict[key].ToString());
            }
    }
}