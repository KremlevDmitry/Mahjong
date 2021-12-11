using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chip : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 _shiftVector = new Vector3(.07f, -.07f, 0f);

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position += _shiftVector;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position -= _shiftVector;
    }
}
