using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
 
using LitJson;

public class ApiManager : MonoBehaviour
{
    public string URL_host;
     

    public string weekdetails;
    public string FreeText, paidtext;

   


    public string OTp;

    // signup api
    public void Signup_API()
    {
        //if (string.IsNullOrEmpty(UIManager.OTP_Inputfield.text))
        //{
        //    UIManager.OtpErrortxt.text = "OTP is Required";
        //}
        //else if (UIManager.OTP_Inputfield.text.Contains(" "))
        //{
        //    UIManager.OtpErrortxt.text = "Space is Not Allow in OTP";
        //}
        //else
        //{
        //    if (OTp == UIManager.OTP_Inputfield.text)
        //    {
        //        StartCoroutine(Signup());
        //        UIManager.loading_Panel.SetActive(true);
        //    }
        //    else
        //    {
        //        UIManager.OtpErrortxt.text = "OTP is incorrect please enter correct OTP";
        //    }
        //}  SoundManager.instacne.PlaySfx(SoundManager.instacne.button_sound);
     
       
          if (string.IsNullOrEmpty(UIManager.instance.signUp_InputField.text))
        {
            UIManager.instance.signUp_Error_Text.text = "Email is Required";
        }
        else if (UIManager.instance.signUp_InputField.text.Contains(" "))
        {
            UIManager.instance.signUp_Error_Text.text = "Space is Not Allow in Email Address";
        }
        else if (UIManager.instance.signUp_InputField.text.IndexOf('@') <= 0 || !UIManager.instance.signUp_InputField.text.Contains(".com"))
        {
            UIManager.instance.signUp_Error_Text.text = "Please enter correct Email Address. ";
        }
        
        else
        {
           
            print("sign up calling");
            StartCoroutine(Signup());
        }
    }

    public void OTpVerify()
    {
        
         
        if (string.IsNullOrEmpty(UIManager.instance.passcode_Inputfield.text))
        {
            UIManager.instance.passcodeErrorText.text = "OTP field is required.";
        }
        else if (UIManager.instance.passcode_Inputfield.text.Contains(" "))
        {
            UIManager.instance.passcodeErrorText.text = "Space is Not Allow in OTP";
        }
        else if (string.IsNullOrEmpty(UIManager.instance.passcode_Inputfield.text))
        {
            UIManager.instance.passcodeErrorText.text = "OTP field is required.";
        }
        else
        {
            
            print("OTP calling");
            StartCoroutine(Signup(true));
        }
    }

    private IEnumerator Signup(bool isotp = false)
    {
        print("sign up processing");
        WWWForm form = new WWWForm();
         
        form.AddField("email", UIManager.instance.signUp_InputField.text);
        
        if (isotp) 
            form.AddField("otp", UIManager.instance.passcode_Inputfield.text);
         

        using (UnityWebRequest www = UnityWebRequest.Post(URL_host + "otp-login", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                
                Debug.Log(www.error);
                //StartCoroutine(Signup());
                yield break;
            }
            else if (www.downloadHandler.text == "")
            {
               
                Debug.Log(www.error);
                //StartCoroutine(Signup());
                yield break;
            }
            else
            {
                Debug.Log(www.responseCode);
                

                Debug.Log(www.downloadHandler.text);
                JsonData jval = JsonMapper.ToObject(www.downloadHandler.text);

                if (jval["status"].ToString() == "True" || jval["status"].ToString() == "true")
                {
                    if (!isotp)
                    {
                        Debug.Log("not otp");
                       // UIManager.instance.ActivePanelByID(8);
                    }
                    else
                    {



                        UIManager.instance.ActivePanelByID(8);

                       
                    }
                    // UIManager.signupErrortxt.text = jval["message"].ToString();
                }
                else
                {
                    if (!isotp)
                    {
                        UIManager.instance.signUp_Error_Text.text = jval["message"].ToString();
                    }
                    else
                    {
                        UIManager.instance.passcodeErrorText.text = jval["message"].ToString();
                    }
                }
            }
        }
    }

}
