using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //all panel reference in array
    public GameObject[] Gamepanels;

    public GameObject selectedObj;
    public GameObject PickObj;
    public InputField signUp_InputField;
    
    public InputField passcode_Inputfield;
    public Text signUp_Error_Text, passcodeErrorText;

     




    //variable is use for last click toggle in setting and message screen
    public GameObject previousToggle;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(Splash_Loading());
    }
    
/// <summary>
/// //
///  loading splash 
/// 
/// </summary>
/// <returns></returns>
    IEnumerator Splash_Loading()
    {
        Gamepanels[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        Gamepanels[1].SetActive(true);
        Gamepanels[0].SetActive(false);
    }
 /// <summary>
 /// toggle function
 /// </summary>
 /// <param name="currentToggle"></param>
   public void CheckToggle(GameObject currentToggle)
    {
        if(previousToggle)
        {
            previousToggle.SetActive(false);

        }
        previousToggle = currentToggle;
        currentToggle.SetActive(true);
    }
    
    /// <summary>
    /// Click events for all the ui buttons
    /// </summary>
    /// <param name="PanelIndex"></param>

    public void ActivePanelByID(int PanelIndex)
    {
        for (int i = 0; i < Gamepanels.Length; i++)
        {
            if (i==PanelIndex)
            {
                Gamepanels[PanelIndex].SetActive(true);
            }
            else
            {
                Gamepanels[i].SetActive(false);
            }
          
        }    
        
    }
   


  

}
