using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class itemDrop : MonoBehaviour  ,IDropHandler
{
    public bool IsDropped;

    

    public void OnDrop(PointerEventData eventData)
    {
        //if(eventData.pointerDrag!= null)
        // {
        //    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        // }
     
        if (IsDropped == false)
        {
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.GetComponent<DragDrop>().isPlaced = true;
            eventData.pointerDrag.GetComponent<DragDrop>().droppedParent = this.gameObject;
            IsDropped = true;
            
        }
      /*  else
        {
           IsDropped = false;
            eventData.pointerDrag.GetComponent<DragDrop>().isPlaced = false;
            //eventData.pointerDrag.transform.SetParent();
            //DragDrop.instance.Itemparent.SetActive(true);
        }*/


    }

 
}
