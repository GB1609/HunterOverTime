using System;
using UnityEngine;

public abstract class SelectionManager : MonoBehaviour
{
    [SerializeField] private String selectableTag = "Selectable";
    private Transform _selection;

    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            removeAction();
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                doAction(selection);
                _selection = selection;
            }
        }
    }

    public void doAction(Transform selection)
    {
        Debug.Log("INTERSECTION");
    }

    public void removeAction()
    {
        Debug.Log("INTERSECTION");
    }
}