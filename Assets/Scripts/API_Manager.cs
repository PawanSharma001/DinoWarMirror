//using System.Collections;
//using UnityEngine;
//using UnityEngine.Networking;

//namespace TeenPatti
//{
//    [System.Serializable]
//    public class MusicFilesData
//    {
//        public string name;
//        public AudioClip soundclip;
//    }

//    [System.Serializable]
//    public class MusicFilesUrl
//    {
//        public string name;
//        public string URl;
//    }

//    public enum GameType
//    { TeenPatti, Poker, Ludo, SnakeLadder, Rummy };

//    public class API_Manager : MonoBehaviour
//    {
//        // Start is called before the first frame update
//        public static API_Manager instance;

//        //public UimanagerTeenPatti UIManager;
//        //public photonGameManager PGManager;
//        public string URL_host;
//        public string User_ID;
//        public string User_Name;
//        public string User_Email;
//        public int TotalPlayedGame;
//        public int TotalGameWin;
//        public int TotalCoins;
//        public int Avatar_id;
//        public GameType _gametype;

//        // public string StartTime
//        public void Start()
//        {
//            //   StartCoroutine(Getprofile());
//            //   checkTime();
//            // StartCoroutine(Login());
//        }

//        private void Awake()
//        {
//            instance = this;
//        }

//        // login api
//        public void Login_API()
//        {
//            //if (PlayerPrefs.HasKey("UserName") && PlayerPrefs.HasKey("UserPassword"))
//            //{
//            //    UIManager.loading_Panel.SetActive(true);
//            //    StartCoroutine(Login());
//            //}
//            //else 
//            //{
//            //}
//            //SoundManager.instacne.PlaySfx(SoundManager.instacne.button_sound);
//            //if (!photonGameManager.instance.IsInternetConnected() || !PhotonNetwork.IsConnectedAndReady)
//            //{
//            //    PhotonEventScript.instance.connectphoton();
//            //    photonGameManager.instance.MSgBox("Please check Your Internet");
//            //    return;
//            //}

//            if (string.IsNullOrEmpty(UIManager.login_InputField.text))
//            {
//                UIManager.LoginErrortxt.text = "Email is Required";
//            }
//            else if (UIManager.login_InputField.text.IndexOf('@') <= 0 || !UIManager.login_InputField.text.Contains(".com"))
//            {
//                UIManager.LoginErrortxt.text = "Please enter correct Email. ";
//            }
//            else if (UIManager.login_Password_Inputfield.text.Contains(" "))
//            {
//                UIManager.LoginErrortxt.text = "Space is Not Allow in Password";
//            }
//            else if (string.IsNullOrEmpty(UIManager.login_Password_Inputfield.text))
//            {
//                UIManager.LoginErrortxt.text = "Password field is required.";
//            }
//            else
//            {
//                UIManager.Waitingpanel.SetActive(true);
//                StartCoroutine(Login());
//            }
//        }

//        public string weekdetails;
//        public string FreeText, paidtext;

//        public IEnumerator Login()
//        {
//            WWWForm form = new WWWForm();
//            print(PlayerPrefs.GetString("UserName") + PlayerPrefs.GetString("UserPassword"));
//            if (PlayerPrefs.HasKey("UserName") && PlayerPrefs.HasKey("UserPassword"))
//            {
//                form.AddField("email", PlayerPrefs.GetString("UserName"));
//                form.AddField("password", PlayerPrefs.GetString("UserPassword"));
//                form.AddField("device_id", SystemInfo.deviceUniqueIdentifier.ToString());
//                form.AddField("device_type", SystemInfo.deviceType.ToString());
//                form.AddField("notification_token", DataManager.instance._token);
//                form.AddField("game_type", _gametype.ToString());
//            }
//            else
//            {
//                form.AddField("email", UIManager.login_InputField.text);
//                form.AddField("password", UIManager.login_Password_Inputfield.text);
//                form.AddField("device_id", SystemInfo.deviceUniqueIdentifier.ToString());
//                form.AddField("device_type", SystemInfo.deviceType.ToString());
//                form.AddField("notification_token", DataManager.instance._token);
//                form.AddField("game_type", _gametype.ToString());
//            }

//            using (UnityWebRequest www = UnityWebRequest.Post(URL_host + "login", form))
//            {
//                Debug.Log("proges" + www.downloadProgress);
//                yield return www.SendWebRequest();

//                if (www.isNetworkError)
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    Debug.Log(www.error);
//                    StartCoroutine(Login());
//                    yield break;
//                }
//                else if (www.downloadHandler.text == "")
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    Debug.Log("Datat" + www.error);
//                    StartCoroutine(Login());
//                    yield break;
//                }
//                else
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    Debug.Log(www.downloadHandler.text);
//                    Debug.Log(www.responseCode);
//                    JsonData jval = JsonMapper.ToObject(www.downloadHandler.text);
//                    UIManager.LoginErrortxt.text = "";
//                    //  print(jval["status"].ToString());
//                    if (jval["status"].ToString() == "True" || jval["status"].ToString() == "true")
//                    {
//                        DataManager.instance._loginData = JsonConvert.DeserializeObject<LoginData>(www.downloadHandler.text);
//                        DataManager.instance.LoadJson();
//                        DataManager.instance.GetUsers(GetUserType.Get_Friends, () => { });
//                        User_ID = jval["data"]["id"].ToString();
//                        User_Name = jval["data"]["name"].ToString();
//                        User_Email = jval["data"]["email"].ToString();
//                        TotalPlayedGame = int.Parse(jval["data"]["total_game"].ToString());
//                        TotalGameWin = int.Parse(jval["data"]["total_win"].ToString());
//                        TotalCoins = int.Parse(jval["data"]["total_coin"].ToString());
//                        UIManager.SetPlayerName(User_Name);
//                        UIManager.SetPlayerCoins(TotalCoins);
//                        // LeaderBoard();
//                        Avatar_id = int.Parse(jval["data"]["avtar_id"].ToString());
//                        print("datat is calling" + Avatar_id);
//                        UIManager.LoginErrortxt.text = "";
//                        UIManager.activepanelbyid(0);
//                        UIManager.SetProfile(Avatar_id);
//                        UIManager.SetPlayerData();

//                        if (!PlayerPrefs.HasKey("UserName") && !PlayerPrefs.HasKey("UserPassword"))
//                        {
//                            PlayerPrefs.SetString("UserName", UIManager.login_InputField.text);
//                            PlayerPrefs.SetString("UserPassword", UIManager.login_Password_Inputfield.text);
//                        }
//                        UIManager.ClearAllText();
//                        //    StartCoroutine(Getprofile());
//                    }
//                    else
//                    {
//                        UIManager.LoginErrortxt.text = jval["message"].ToString();
//                        UIManager.activepanelbyid(3);
//                        PlayerPrefs.DeleteKey("UserName");
//                        PlayerPrefs.DeleteKey("UserPassword");
//                    }
//                }
//            }
//        }

//        public string OTp;

//        // signup api
//        public void Signup_API()
//        {
//            //if (string.IsNullOrEmpty(UIManager.OTP_Inputfield.text))
//            //{
//            //    UIManager.OtpErrortxt.text = "OTP is Required";
//            //}
//            //else if (UIManager.OTP_Inputfield.text.Contains(" "))
//            //{
//            //    UIManager.OtpErrortxt.text = "Space is Not Allow in OTP";
//            //}
//            //else
//            //{
//            //    if (OTp == UIManager.OTP_Inputfield.text)
//            //    {
//            //        StartCoroutine(Signup());
//            //        UIManager.loading_Panel.SetActive(true);
//            //    }
//            //    else
//            //    {
//            //        UIManager.OtpErrortxt.text = "OTP is incorrect please enter correct OTP";
//            //    }
//            //}  SoundManager.instacne.PlaySfx(SoundManager.instacne.button_sound);
//            SoundManager.instacne.PlaySfx(SoundManager.instacne.button_sound);
//            if (!photonGameManager.instance.IsInternetConnected() || !PhotonNetwork.IsConnectedAndReady)
//            {
//                PhotonEventScript.instance.connectphoton();
//                photonGameManager.instance.MSgBox("Please check Your Internet");
//                return;
//            }
//            if (string.IsNullOrEmpty(UIManager.Signup_Name_InputFiled.text))
//            {
//                UIManager.signupErrortxt.text = "Name is Required";
//            }
//            else if (UIManager.Signup_Name_InputFiled.text.StartsWith(" "))
//            {
//                UIManager.signupErrortxt.text = "Space is Not Allow in Name";
//            }
//            else if (UIManager.Signup_Name_InputFiled.text.Length < 3)
//            {
//                UIManager.signupErrortxt.text = "Minimum length should be 2 and maximum length should be 25 characters. ";
//            }
//            else if (string.IsNullOrEmpty(UIManager.Signup_Email_InputFiled.text))
//            {
//                UIManager.signupErrortxt.text = "Email is Required";
//            }
//            else if (UIManager.Signup_Email_InputFiled.text.Contains(" "))
//            {
//                UIManager.signupErrortxt.text = "Space is Not Allow in Email Address";
//            }
//            else if (UIManager.Signup_Email_InputFiled.text.IndexOf('@') <= 0 || !UIManager.Signup_Email_InputFiled.text.Contains(".com"))
//            {
//                UIManager.signupErrortxt.text = "Please enter correct Email Address. ";
//            }
//            else if (string.IsNullOrEmpty(UIManager.Signup_Number_InputFiled.text))
//            {
//                UIManager.signupErrortxt.text = "Mobile number field is required.";
//            }
//            else if (UIManager.Signup_Number_InputFiled.text.Contains(" "))
//            {
//                UIManager.signupErrortxt.text = "Space is Not Allow in Password";
//            }
//            else if (string.IsNullOrEmpty(UIManager.Signup_Password_InputFiled.text))
//            {
//                UIManager.signupErrortxt.text = "Password field is required.";
//            }
//            else if (UIManager.Signup_Password_InputFiled.text.Contains(" "))
//            {
//                UIManager.signupErrortxt.text = "Space is Not Allow in Password";
//            }
//            else if (UIManager.Signup_Password_InputFiled.text.Length <= 6)
//            {
//                UIManager.signupErrortxt.text = "Password Field required 7 to 15 characters.";
//            }
//            else if (string.IsNullOrEmpty(UIManager.Signup_ConfirmPass_InputFiled.text))
//            {
//                UIManager.signupErrortxt.text = "Confirm password field is required.";
//            }
//            else if (UIManager.Signup_ConfirmPass_InputFiled.text.Contains(" "))
//            {
//                UIManager.signupErrortxt.text = "Space is Not Allow in Confirm Password";
//            }
//            else if (UIManager.Signup_Password_InputFiled.text != UIManager.Signup_ConfirmPass_InputFiled.text)
//            {
//                UIManager.signupErrortxt.text = " Password and confirm password does not match.";
//            }
//            else
//            {
//                UIManager.Waitingpanel.SetActive(true);
//                print("sign up calling");
//                StartCoroutine(Signup());
//            }
//        }

//        public void OTpVerify()
//        {
//            SoundManager.instacne.PlaySfx(SoundManager.instacne.button_sound);
//            if (!photonGameManager.instance.IsInternetConnected() || !PhotonNetwork.IsConnectedAndReady)
//            {
//                PhotonEventScript.instance.connectphoton();
//                photonGameManager.instance.MSgBox("Please check Your Internet");
//                return;
//            }
//            if (string.IsNullOrEmpty(UIManager.Otp_InputField.text))
//            {
//                UIManager.OtpErrortxt.text = "OTP field is required.";
//            }
//            else if (UIManager.Otp_InputField.text.Contains(" "))
//            {
//                UIManager.OtpErrortxt.text = "Space is Not Allow in OTP";
//            }
//            else if (string.IsNullOrEmpty(UIManager.Otp_InputField.text))
//            {
//                UIManager.OtpErrortxt.text = "OTP field is required.";
//            }
//            else
//            {
//                UIManager.Waitingpanel.SetActive(true);
//                print("OTP calling");
//                StartCoroutine(Signup(true));
//            }
//        }

//        private IEnumerator Signup(bool isotp = false)
//        {
//            print("sign up processing");
//            WWWForm form = new WWWForm();
//            form.AddField("name", UIManager.Signup_Name_InputFiled.text);
//            form.AddField("email", UIManager.Signup_Email_InputFiled.text);
//            form.AddField("mobile", UIManager.Signup_Number_InputFiled.text);
//            form.AddField("password", UIManager.Signup_Password_InputFiled.text);
//            if (isotp) form.AddField("otp", UIManager.Otp_InputField.text);
//            form.AddField("confirm_password", UIManager.Signup_ConfirmPass_InputFiled.text);
//            form.AddField("device_id", SystemInfo.deviceUniqueIdentifier.ToString());
//            form.AddField("device_type", SystemInfo.deviceType.ToString());
//            form.AddField("notification_token", DataManager.instance._token);
//            form.AddField("game_type", _gametype.ToString());

//            using (UnityWebRequest www = UnityWebRequest.Post(URL_host + "registration", form))
//            {
//                yield return www.SendWebRequest();

//                if (www.isNetworkError)
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    Debug.Log(www.error);
//                    //StartCoroutine(Signup());
//                    yield break;
//                }
//                else if (www.downloadHandler.text == "")
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    Debug.Log(www.error);
//                    //StartCoroutine(Signup());
//                    yield break;
//                }
//                else
//                {
//                    Debug.Log(www.responseCode);
//                    UIManager.Waitingpanel.SetActive(false);

//                    Debug.Log(www.downloadHandler.text);
//                    JsonData jval = JsonMapper.ToObject(www.downloadHandler.text);

//                    if (jval["status"].ToString() == "True" || jval["status"].ToString() == "true")
//                    {
//                        if (!isotp)
//                        {
//                            UIManager.activepanelbyid(6);
//                        }
//                        else
//                        {
//                            User_ID = jval["data"]["id"].ToString();
//                            User_Name = jval["data"]["name"].ToString();
//                            TotalPlayedGame = int.Parse(jval["data"]["total_game"].ToString());
//                            TotalGameWin = int.Parse(jval["data"]["total_win"].ToString());
//                            TotalCoins = int.Parse(jval["data"]["total_coin"].ToString());
//                            Avatar_id = int.Parse(jval["data"]["avtar_id"].ToString());

//                            UIManager.SetProfile(Avatar_id);

//                            DataManager.instance._loginData = JsonConvert.DeserializeObject<LoginData>(www.downloadHandler.text);
//                            DataManager.instance.LoadJson();
//                            DataManager.instance.GetUsers(GetUserType.Get_Friends, () => { });
//                            UIManager.activepanelbyid(0);
//                            UIManager.SetPlayerName(User_Name);
//                            UIManager.SetPlayerCoins(TotalCoins);
//                            UIManager.SetPlayerData();
//                            // LeaderBoard();
//                            if (!PlayerPrefs.HasKey("UserName") && !PlayerPrefs.HasKey("UserPassword"))
//                            {
//                                PlayerPrefs.SetString("UserName", UIManager.Signup_Email_InputFiled.text);
//                                PlayerPrefs.SetString("UserPassword", UIManager.Signup_Password_InputFiled.text);
//                            }
//                            UIManager.ClearAllText();
//                            Avatar_id = 1;
//                            StartCoroutine(UpdateDataIEnum());
//                        }
//                        // UIManager.signupErrortxt.text = jval["message"].ToString();
//                    }
//                    else
//                    {
//                        if (!isotp)
//                        {
//                            UIManager.signupErrortxt.text = jval["message"].ToString();
//                        }
//                        else
//                        {
//                            UIManager.OtpErrortxt.text = jval["message"].ToString();
//                        }
//                    }
//                }
//            }
//        }

//        public IEnumerator Getprofile()
//        {
//            WWWForm form = new WWWForm();
//            form.AddField("id", User_ID);

//            using (UnityWebRequest www = UnityWebRequest.Post(URL_host + "get-profile", form))
//            {
//                www.SetRequestHeader("Authorization", "Token " + DataManager.instance._loginData.session_token);
//                yield return www.SendWebRequest();

//                if (www.isNetworkError)
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    Debug.Log(www.error);
//                    StartCoroutine(Login());
//                    yield break;
//                }
//                else if (www.downloadHandler.text == "")
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    StartCoroutine(Login());
//                    yield break;
//                }
//                else
//                {
//                    UIManager.Waitingpanel.SetActive(false);
//                    Debug.Log(www.downloadHandler.text);
//                }
//            }
//        }

//        //// forGotPAss Api
//        public void ForgotPass_API()
//        {
//            SoundManager.instacne.PlaySfx(SoundManager.instacne.button_sound);
//            if (!photonGameManager.instance.IsInternetConnected() || !PhotonNetwork.IsConnectedAndReady)
//            {
//                PhotonEventScript.instance.connectphoton();
//                photonGameManager.instance.MSgBox("Please check Your Internet");
//                return;
//            }
//            if (string.IsNullOrEmpty(UIManager.Forgot_Pass_InputField.text))
//            {
//                UIManager.ForGotErrortxt.text = "Email Id is Required";
//            }
//            else if (UIManager.Forgot_Pass_InputField.text.IndexOf('@') <= 0 || !UIManager.Forgot_Pass_InputField.text.Contains(".com"))
//            {
//                UIManager.ForGotErrortxt.text = "Please enter correct Email Id";
//            }
//            else if (UIManager.Forgot_Pass_InputField.text.Contains(" "))
//            {
//                UIManager.ForGotErrortxt.text = "Space is Not Allow in Email Address";
//            }
//            else
//            {
//                UIManager.Waitingpanel.SetActive(true);
//                StartCoroutine(ForgotPassIEnum());
//            }
//        }

//        private IEnumerator ForgotPassIEnum()
//        {
//            WWWForm form = new WWWForm();
//            form.AddField("email", UIManager.Forgot_Pass_InputField.text);

//            using (UnityWebRequest www = UnityWebRequest.Post(URL_host + "forgot", form))
//            {
//                www.SetRequestHeader("Authorization", "Token " + DataManager.instance._loginData.session_token);
//                yield return www.SendWebRequest();

//                if (www.isNetworkError)
//                {
//                    Debug.Log(www.error);
//                }
//                else
//                if (www.downloadHandler.text == "")
//                {
//                    //UIManager.loading_Panel.SetActive(false);
//                    StartCoroutine(ForgotPassIEnum());
//                    yield break;
//                }
//                else
//                {
//                    Debug.Log(www.downloadHandler.text);
//                    JsonData jval = JsonMapper.ToObject(www.downloadHandler.text);

//                    UIManager.Waitingpanel.SetActive(false);

//                    if (jval["status"].ToString() == "True")
//                    {
//                        UIManager.ForGotErrortxt.text = jval["message"].ToString();
//                    }
//                    else
//                    {
//                        UIManager.ForGotErrortxt.text = jval["message"].ToString();
//                    }
//                }
//            }
//        }

//        //// Update Api
//        public void UpdateData()
//        {
//            SoundManager.instacne.PlaySfx(SoundManager.instacne.button_sound);
//            if (!photonGameManager.instance.IsInternetConnected() || !PhotonNetwork.IsConnectedAndReady)
//            {
//                PhotonEventScript.instance.connectphoton();
//                photonGameManager.instance.MSgBox("Please check Your Internet");
//                return;
//            }

//            if (string.IsNullOrEmpty(UIManager.UpdateName_InputFiled.text))
//            {
//                UIManager.updateerrortxt.text = "Name is Required";
//            }
//            else if (UIManager.UpdateName_InputFiled.text.StartsWith(" "))
//            {
//                UIManager.updateerrortxt.text = "Space is Not Allow in Name";
//            }
//            else if (UIManager.UpdateName_InputFiled.text.Length < 3)
//            {
//                UIManager.updateerrortxt.text = "Minimum length should be 2 and maximum length should be 25 characters. ";
//            }
//            else
//            {
//                UIManager.Waitingpanel.SetActive(true);
//                User_Name = UIManager.UpdateName_InputFiled.text;
//                StartCoroutine(UpdateDataIEnum());
//            }
//        }

//        public IEnumerator UpdateDataIEnum()
//        {
//            WWWForm form = new WWWForm();
//            form.AddField("id", User_ID);
//            form.AddField("name", User_Name);
//            form.AddField("total_game", TotalPlayedGame);
//            form.AddField("total_win", TotalGameWin);
//            form.AddField("total_coin", TotalCoins.ToString());
//            form.AddField("avtar_id", Avatar_id.ToString());

//            using (UnityWebRequest www = UnityWebRequest.Post(URL_host + "update-profile", form))
//            {
//                www.SetRequestHeader("Authorization", "Token " + DataManager.instance._loginData.session_token);
//                yield return www.SendWebRequest();

//                if (www.isNetworkError)
//                {
//                    Debug.Log(www.error);
//                }
//                else
//                if (www.downloadHandler.text == "")
//                {
//                    //UIManager.loading_Panel.SetActive(false);
//                    StartCoroutine(UpdateDataIEnum());
//                    yield break;
//                }
//                else
//                {
//                    JsonData jval = JsonMapper.ToObject(www.downloadHandler.text);
//                    if (jval["status"].ToString() == "True" || jval["status"].ToString() == "True")
//                    {
//                        Debug.Log(www.downloadHandler.text);

//                        User_Name = jval["data"]["name"].ToString();
//                        TotalPlayedGame = int.Parse(jval["data"]["total_game"].ToString());
//                        TotalGameWin = int.Parse(jval["data"]["total_win"].ToString());
//                        TotalCoins = int.Parse(jval["data"]["total_coin"].ToString());
//                        Avatar_id = int.Parse(jval["data"]["avtar_id"].ToString());
//                        Avatar_id = int.Parse(jval["data"]["avtar_id"].ToString());
//                        UIManager.SetPlayerCoins(TotalCoins);
//                        UIManager.SetProfile(Avatar_id);
//                        UIManager.SetPlayerName(User_Name);
//                        UIManager.Waitingpanel.SetActive(false);
//                        UIManager.SetPlayerData();
//                    }
//                }
//            }
//        }

//        public void LeaderBoard()
//        {
//            // print(UIManager.LeaderboardContent.transform.childCount + "LeaderBoard");
//            if (UIManager.LeaderboardContent.transform.childCount > 0)
//            {
//                for (int i = 0; i < UIManager.LeaderboardContent.transform.childCount; i++)
//                {
//                    //  print(i);
//                    Destroy(UIManager.LeaderboardContent.transform.GetChild(i).gameObject);
//                }
//            }
//            //  print(UIManager.LeaderboardContent.transform.childCount + "LeaderBoard after ");
//            UIManager.Waitingpanel.SetActive(true);
//            StartCoroutine(LeaderBoardENUM());
//        }

//        private IEnumerator LeaderBoardENUM()
//        {
//            WWWForm form = new WWWForm();
//            using (UnityWebRequest www = UnityWebRequest.Post(URL_host + "leaderboard", form))
//            {
//                www.SetRequestHeader("Authorization", "Token " + DataManager.instance._loginData.session_token);
//                yield return www.SendWebRequest();

//                if (www.isNetworkError)
//                {
//                    Debug.Log(www.error);

//                    StartCoroutine(LeaderBoardENUM());
//                    yield break;
//                }
//                else
//                if (www.downloadHandler.text == "")
//                {
//                    StartCoroutine(LeaderBoardENUM());
//                    yield break;
//                }
//                else
//                {
//                    Debug.Log(www.downloadHandler.text);
//                    JsonData jval = JsonMapper.ToObject(www.downloadHandler.text);
//                    UIManager.Waitingpanel.SetActive(false);
//                    ;
//                    if (jval["status"].ToString() == "True")
//                    {
//                        for (int i = 0; i < jval["data"].Count; i++)
//                        {
//                            // print("players name" + jval["data"][i]["name"].ToString());
//                            GameObject obj = Instantiate(UIManager.PlayerDetailsPrefab, UIManager.LeaderboardContent.transform);
//                            // obj.transform.SetParent(UIManager.LeaderboardContent.transform);
//                            obj.GetComponent<LeaderBoardDetails>().PlayerNametxt.text = jval["data"][i]["name"].ToString();
//                            obj.GetComponent<LeaderBoardDetails>().playerTotalCoinsTxt.text = jval["data"][i]["total_coin"].ToString();
//                            obj.GetComponent<LeaderBoardDetails>().SerNotxt.text = (i + 1).ToString();
//                        }
//                    }
//                }
//            }
//        }
//    }
//}