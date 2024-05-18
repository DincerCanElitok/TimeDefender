using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private Camera cam;
    [SerializeField]private GameObject selectedObject;
    private Vector3 originalPosition;
    private GameObject originalRoom;
    private bool isDragging = false;
    private Vector3 offset;
    public LayerMask draggableLayer;
    public LayerMask dropAreaLayer;
    public float maxRaycastDistance = 100f;
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        MouseInputHandler();
        if (isDragging)
        {
            DragDefenderAgent();
        }

    }
    private void MouseInputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectDefenderAgent();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                DropDefenderAgent();
            }
        }
    }
    private void SelectDefenderAgent()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, maxRaycastDistance, draggableLayer);
        if (hit.collider != null && hit.transform.CompareTag("Agent"))
        {
            Debug.Log("Agent selected");
            selectedObject = hit.transform.gameObject;
            originalPosition = selectedObject.transform.position;
            originalRoom = selectedObject.transform.parent.gameObject;
            selectedObject.GetComponent<DefenderAgent>().LeaveRoom();
            offset = selectedObject.transform.position - (Vector3)hit.point;
            isDragging = true;
        }
    }
    private void DragDefenderAgent()
    {
        if(selectedObject != null)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            selectedObject.transform.position = mousePosition + offset;
        }
    }
    private void DropDefenderAgent()
    {
        if (selectedObject != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, maxRaycastDistance, dropAreaLayer);

            if (hit.collider != null && hit.transform.CompareTag("Room"))
            {
                selectedObject.transform.position = (Vector3)hit.point + offset;
                DefenderAgent defenderAgent = selectedObject.GetComponent<DefenderAgent>();
                defenderAgent.SetRoom(hit.transform.gameObject);
            }
            else
            {
                selectedObject.transform.position = originalPosition;
                DefenderAgent defenderAgent = selectedObject.GetComponent<DefenderAgent>();
                defenderAgent.SetRoom(originalRoom);
                
            }

            selectedObject = null;
            isDragging = false;
        }          
    }
}
