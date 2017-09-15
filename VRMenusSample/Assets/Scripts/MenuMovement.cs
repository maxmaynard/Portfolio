using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuMovement : MonoBehaviour {
    //variable allowing for manipulation of main menu
    public GameObject MenuCanvas;
    //indicates which selectable objects is currently being manipulated
    public GameObject CurrentSelected;
    //used to indicate and access the game object that forms the crosshairs infront of camera
    public GameObject AimSpot;
    //used to reference the camera
    public Camera Camera;
    //used to reference and activate the material menu to change colors
    public GameObject MaterialMenu;

    //These variables are used to calculate the position of the menus
    Vector3 PlayerSpot;
    Vector3 CurrentObjectSpot;
    Vector3 MenuSpawnLocation;
    Vector3 MenuFloatHeight;

    //used to determine object properties being manipulated and collisions during moving
    public bool movingObject = false;
    public bool IgnoringObjectCollisions = false;
    public bool SpinObject = false;
    public bool Recoloring = false;
    public bool ScaleObject = false;
	
    // Sets variables; be sure to check that these game objects are in the game with these names to proceed.
    // Also initializes menu position so it spawns in the intended manner
	void Start () {
        MenuCanvas = gameObject.transform.Find("Menu").gameObject;
        MenuFloatHeight = new Vector3(0, 3, 0);

        PlayerSpot = transform.position;
        CurrentObjectSpot = new Vector3(0,0,0);

        MenuSpawnLocation = Vector3.Lerp(PlayerSpot, CurrentObjectSpot, 0.35f);
        MenuCanvas.transform.position = MenuSpawnLocation;
    }
	
	// Provides control scheme for selecting and highlighting objects
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 fwd = Camera.transform.TransformDirection(Vector3.forward);
            
            //Enables Menu options
            if (MenuCanvas.activeSelf)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(AimSpot.transform.position, fwd, out hit))
                    {
                        if (hit.transform.name == "Move")
                        {
                            print("move");
                            movingObject = true;
                        }
                        if (hit.transform.name == "Resize")
                        {
                            print("resize");
                            ScaleObject = true;
                        }
                        if (hit.transform.name == "Rotate")
                        {
                            print("rotate");
                            SpinObject = true;
                        }
                        if (hit.transform.name == "Recolor")
                        {
                            print("recolor");
                            Recoloring = true;
                        }
                    }
                }
            }

            //On hit of a manipulatable object via raycast, open menu, highlight it and set object up to be changed
            if (Physics.Raycast(AimSpot.transform.position, fwd, out hit))
            {
                if (hit.transform.tag == "Object")
                {
                    DeselectObject(CurrentSelected);

                    CurrentSelected = hit.transform.gameObject;
                    ActivateMainMenu();
                    CurrentSelected.GetComponent<MeshRenderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");                    
                }
            }
            
            
        }

        //Deselects objects and turns off menus
        if (Input.GetMouseButtonDown(1))
        {
            DeselectObject(CurrentSelected);
            Debug.Log("Deselected");
        }

        //Manages movement for objects around the world
        if (movingObject)
        {
            Physics.IgnoreCollision(CurrentSelected.GetComponent<Collider>(), GetComponent<Collider>(), true);
            IgnoringObjectCollisions = true;

            RaycastHit movePoint;
            Vector3 movingForward = Camera.transform.TransformDirection(Vector3.forward);
            if(Physics.Raycast(transform.position, movingForward, out movePoint))
            {
                CurrentSelected.transform.position = Vector3.MoveTowards(CurrentSelected.transform.position, movePoint.point, 5f * Time.deltaTime);
            }
        }

        //Manages rotating objects in the world
        if (SpinObject)
        {
            RotateObject(CurrentSelected);
        }

        //Manages recoloring objects in the world
        if (Recoloring)
        {
            RecolorObject(CurrentSelected);
        }

        //Manages growing objects in the world
        if (ScaleObject)
        {
            ResizeObject(CurrentSelected);
        }

        //Menus always face the player
        if (MenuCanvas.activeSelf)
        {
            MenuCanvas.transform.LookAt(transform);
        }
        if (MaterialMenu.activeSelf)
        {
            MaterialMenu.transform.LookAt(transform);
        }
	}


    //Activates the player menu for recoloring, resizing, and moving objects, as well as calculates its position in worldspace
    void ActivateMainMenu()
    {
        if (CurrentSelected == null)
        {
            return;
        }
        
        if (!MenuCanvas.activeSelf)
        {
            MenuCanvas.SetActive(true);
            PlayerSpot = transform.position;
            CurrentObjectSpot = CurrentSelected.transform.position;

            MenuSpawnLocation = Vector3.Lerp(PlayerSpot, CurrentObjectSpot, 0.35f);
            //MenuSpawnLocation.y = MenuFloatHeight.y;

            MenuCanvas.transform.SetParent(null);
            MenuCanvas.transform.position = MenuSpawnLocation;

            Debug.Log("we're active");
        }
       

    }

    // Activates the material menu for recoloring and calculates it's position in worldspace
    void ActivateMaterialMenu()
    {
        if (CurrentSelected == null)
        {
            return;
        }

        if (!MaterialMenu.activeSelf)
        {
            MaterialMenu.SetActive(true);
            PlayerSpot = transform.position;
            CurrentObjectSpot = CurrentSelected.transform.position;

            MenuSpawnLocation = Vector3.Lerp(PlayerSpot, CurrentObjectSpot, .2f);;

            MaterialMenu.transform.position = MenuSpawnLocation;
            MaterialMenu.transform.SetParent(null);
            Debug.Log("we're active");
            MenuCanvas.SetActive(false);
            print(MaterialMenu.transform.position);
        }


    }

    //deselects objects and sets the menu inactive, 
    //turns off all menus
    //returns player/object collision if material properties allow
    //cancels rotation if active
    //cancels scaling if active
    void DeselectObject(GameObject Selection)
    {
        if (IgnoringObjectCollisions)
        {
            Physics.IgnoreCollision(CurrentSelected.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
        if (movingObject)
        {
            movingObject = false;
        }
        if (SpinObject)
        {
            SpinObject = false;
        }
        if (ScaleObject)
        {
            ScaleObject = false;
        }
        if (Recoloring)
        {
            MaterialMenu.SetActive(false);
            Recoloring = false;
        }
            if (Selection != null)
        {
            if (MenuCanvas.activeSelf)
            {
                MenuCanvas.SetActive(false);

                print("we're deactivated");
            }
            Selection.GetComponent<MeshRenderer>().material.shader = Shader.Find("Diffuse");
            Selection = null;
        }
        
    }

    //manages resizing of objects based on mouse (later controller) position
    void ResizeObject(GameObject Selection)
    {
        RaycastHit scalingObject;
        Vector3 scaleTarget = Camera.transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, scaleTarget, out scalingObject))
        {
            if(scalingObject.point.x > Selection.transform.position.x && scalingObject.point.y > Selection.transform.position.y)
            {
                Vector3 grow = new Vector3(.01f, .01f, .01f);
                Selection.transform.localScale += grow;
            }
            if (scalingObject.point.x < Selection.transform.position.x && scalingObject.point.y < Selection.transform.position.y)
            {
                Vector3 grow = new Vector3(-.01f, -.01f, -.01f);
                Selection.transform.localScale += grow;
            }
        }
    }

    //opens recolor menu to select a new material
    void RecolorObject(GameObject Selection)
    {
        ActivateMaterialMenu();
        MaterialMenu.SetActive(true);
        RaycastHit ColorSelector;
        Vector3 ColorSelection = Camera.transform.TransformDirection(Vector3.forward);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(AimSpot.transform.position, ColorSelection, out ColorSelector))
            {
                print("pew");
                if (ColorSelector.transform.tag == "Color")
                {
                    Selection.GetComponent<MeshRenderer>().material = ColorSelector.transform.gameObject.GetComponent<MeshRenderer>().material;
                }
            }
        }
    }

    // rotates objects based on looking
    void RotateObject(GameObject Selection)
    {
        RaycastHit TurnObject;
        Vector3 TurningObject = Camera.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, TurningObject, out TurnObject, 10f))
        {
            Selection.transform.LookAt(TurnObject.point);
        }
    }

}
