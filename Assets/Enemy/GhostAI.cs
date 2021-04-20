using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode()]
public class GhostAI : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/Enemies/Ghost")]
    public static void AddGhost()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("GameObject/Enemies/Ghost"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }

}
