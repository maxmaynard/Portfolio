using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(NavMeshSurface))]
public class GameSetup : MonoBehaviour {
    GameStateManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameStateManager>();
    }

    public void GenerateNavMesh()
    {
        if (gameManager == null)
            return;

        List<MeshFilter> meshFilters = new List<MeshFilter>();
        GetComponentsInChildren<MeshFilter>(false, meshFilters);
        foreach(MeshFilter mesh in meshFilters)
        {
            if (mesh.transform.position.y >= gameManager.mainCamera.transform.position.y)
            {
                meshFilters.Remove(mesh);
            }
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Count];
        for (int i = 0; i < meshFilters.Count; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);

        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
