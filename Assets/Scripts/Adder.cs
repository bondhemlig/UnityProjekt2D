/*
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Adder : MonoBehaviour
{
    public List<MonoScript> behavioursToAdd = new List<MonoScript>();
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> items = FindObjectsOfType(typeof(GameObject)).OfType<GameObject>().ToList();
        foreach (GameObject item in items)
        {
            if (item.GetComponent<Button>() != null)
            {
                foreach (MonoScript script in behavioursToAdd)
                {                    
                    item.AddComponent(script.GetClass());
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/