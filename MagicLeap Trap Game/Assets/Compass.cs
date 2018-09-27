using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ML_Compass
{
    public class Compass : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
            compass3D = GameObject.Find("Compass3D");
            if (compass3D.GetComponent<MeshRenderer>().enabled)
            {
                compass3D.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        Vector3 dirToTarget2D;
        Vector3 cameraDirToTarget;
        GameObject compass3D;
        public List<GameObject> compasses = new List<GameObject>();
        public List<GameObject> targetList = new List<GameObject>();
        Camera mainCamera;
        public GameObject target;
        public GameObject uiCanvas;

        //points given compass at given target
        public void MLCompass(int compassID, int targetID)
        {
            //determines direction vector based on rotation to target relative to camera
            compass3D.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * .5f, Screen.height * .5f, 0.0f));
            compass3D.transform.LookAt(targetList[targetID].transform);
            compass3D.transform.Rotate(-mainCamera.transform.eulerAngles);
            cameraDirToTarget = compass3D.transform.forward - Vector3.zero;

            //takes direction vector and projects it onto plane
            //before rotating fixed amount based on the direction
            dirToTarget2D = Vector3.ProjectOnPlane(cameraDirToTarget, uiCanvas.GetComponent<Canvas>().transform.forward);
            dirToTarget2D = dirToTarget2D.normalized;

            compasses[compassID].transform.parent.transform.rotation = Quaternion.LookRotation(dirToTarget2D);
        }

        //points given compass at given target
        public void MLCompass()
        {
            //determines direction vector based on rotation to target relative to camera
            compass3D.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * .5f, Screen.height * .5f, 0.0f));
            compass3D.transform.LookAt(target.transform);
            compass3D.transform.Rotate(-mainCamera.transform.eulerAngles);
            cameraDirToTarget = compass3D.transform.forward - Vector3.zero;

            //takes direction vector and projects it onto plane
            //before rotating fixed amount based on the direction
            dirToTarget2D = Vector3.ProjectOnPlane(cameraDirToTarget, uiCanvas.GetComponent<Canvas>().transform.forward);
            dirToTarget2D = dirToTarget2D.normalized;

            compasses[0].transform.parent.transform.rotation = Quaternion.LookRotation(dirToTarget2D);
        }

    }
}
