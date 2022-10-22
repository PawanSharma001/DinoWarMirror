using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
   public bool Istrue;
    public Text temp;
    public Text previousButtonText;
    public string previousText="Latest";
    public void OnClickLatestButton()
    {
        Istrue = !Istrue;
        if(Istrue)
        transform.GetChild(0).gameObject.SetActive(false);
        else
            transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SetTextToDropDown(Text currentText )
    {

        string temptext = temp.text;
       temp.text= currentText.text.ToString();
         currentText.text = temptext;
      
    }
   
}
