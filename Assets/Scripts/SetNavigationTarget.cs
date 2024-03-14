using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SetNavigationTarget : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown navigationTargetDropDown;
    [SerializeField]
    private List<Target> navigationTargetObject = new List<Target>();

    //private LineRenderer line;
    //private NavMeshPath path;
    private Vector3 targetPosition = Vector3.zero;

    //private bool lineToggle = false;

    [Header("Line Renderer obj")]
    [SerializeField] private NavMeshAgent lineAgent; 
    [SerializeField] private PathRenderer _pathRenderer;


    private void Start()
    {
        //path = new NavMeshPath();
        //line = transform.GetComponent<LineRenderer>();
        //line.enabled = lineToggle;
    }

    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            float distance = Vector3.Distance(lineAgent.transform.position, targetPosition);
            if (distance < 0.5f)
            {
                _pathRenderer.StopRender();
                print("Stop");
                //targetPosition = Vector3.zero;
                lineAgent.ResetPath();
            }

            lineAgent.destination = targetPosition;
            print("Move");

            
            //NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
            //for (int i = 0; i < path.corners.Length - 1; i++)
            //    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            //line.positionCount = path.corners.Length;
            //Vector3[] calculatedPathAndOffset = AddLineOffset();
            //line.SetPositions(calculatedPathAndOffset);
        }
    }

    public void SetCurrentNavigationTarget(int selectedValue)
    {
        targetPosition = Vector3.zero;
        string selectedText = navigationTargetDropDown.options[selectedValue].text;
        Target currentTarget = navigationTargetObject.Find(x => x.Name.ToLower().Equals(selectedText.ToLower()));
        if (currentTarget != null)
        {
            //if (!line.enabled)
            //{
            //    ToggleVisibility();
            //}
            targetPosition = currentTarget.PositionObject.transform.position;
            _pathRenderer.StartRender();
        }
    }
} 
