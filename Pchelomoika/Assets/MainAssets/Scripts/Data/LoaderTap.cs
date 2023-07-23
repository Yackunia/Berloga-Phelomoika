using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderTap : MonoBehaviour
{
    private Loader loader;
    [SerializeField] private int id;
    private void Start()
    {
        loader = GameObject.FindObjectOfType<Loader>();
    }
    private void OnMouseDown()
    {
        loader.LoadScene(id);
    }
}
