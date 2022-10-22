using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler, IEndDragHandler 
{
    Vector3 selectedObjPos;
    RectTransform rectTransform;
    public static DragDrop instance;
    public GameObject Itemparent;
    public Transform parentPosition;
    public bool isPlaced;

    public GameObject droppedParent;
    private void Awake()
    {      
         instance = this;
         rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        parentPosition = this.transform.parent;
    }


     
    public void OnDrag(PointerEventData eventData)
    {
        isPlaced = false;
        //Itemparent.SetActive(false);
       rectTransform.anchoredPosition += eventData.delta;
      //  parentPosition = this.transform.parent;
        this.transform.SetParent(UIManager.instance.PickObj.transform);
        UIManager.instance.selectedObj = this.gameObject;
       

        
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        if(!isPlaced)
        {
            this.transform.SetParent(parentPosition);
            if(droppedParent)
            droppedParent.GetComponent<itemDrop>().IsDropped = false;
            rectTransform.anchoredPosition =Vector3.zero;
            parentPosition.gameObject.SetActive(true);
            print("return to postion" );
        }
         
    }
  


}

