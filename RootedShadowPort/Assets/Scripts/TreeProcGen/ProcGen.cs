using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ProcGen : MonoBehaviour
{
    [SerializeField] GameObject Tree;
    [SerializeField] GameObject Parent;
    HashSet<Vector3> locations = new HashSet<Vector3>();

    [ContextMenu("Place Trees")]
    void PlaceTree() {
        for(int i = 0; i < 1000; i++)
        {
            float num1 = Random.Range(-168f, 168f);
            float num2 = Random.Range(-168f, 168f);
            Mathf.Round(num1);
            Mathf.Round(num2);
            Vector3 temp = new Vector3(num1, 0, num2);

            //Debug.Log("Spawning another Tree");
            
            if(!locations.Contains(temp))
            {
                GameObject newTree = (GameObject)PrefabUtility.InstantiatePrefab(Tree);
                newTree.transform.position = temp;
                newTree.transform.SetParent(Parent.transform);
                locations.Add(temp);
            }
        }
    }

    [ContextMenu("PlaceBoundryTrees")]
    void PlaceBoundryTrees()
    {
        for(int i = 0; i < 200; i++)
        {
            GameObject newTree = (GameObject)PrefabUtility.InstantiatePrefab(Tree);
            Vector3 temp = new Vector3(i*1.2f, 0,transform.position.z);
            newTree.transform.position = temp;
            newTree.transform.SetParent(Parent.transform);
        }
    }
    // Remember to go to Tree Manager, right click on Proc Gen Component and Place Tree
}
