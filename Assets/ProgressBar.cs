using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform,false);
    }

    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public int maximum;
    public int minimum;
    public float current;
    public Image mask;
    public Image fill;
    public Color fill_color;
    public Color mask_color;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }


    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        mask.fillAmount = currentOffset / maximumOffset;
        fill.color = fill_color;
        mask.color = mask_color;
    }

    public void UpdateCurrent(float newCur)
    {
        current = newCur;
    }

    public void UpdateMax(int newMax)
    {
        maximum = newMax;
    }

    public void UpdateMin(int newMin)
    {
        minimum = newMin;
    }
}
