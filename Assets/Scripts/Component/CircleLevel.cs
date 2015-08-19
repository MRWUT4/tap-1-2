using UnityEngine;

public class CircleLevel : MonoBehaviour
{
    private Setup setup;
    private Proxy proxy;
    private TextMesh textMesh;

    public GameObject circle;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        setup = gameObject.GetComponent<Setup>();
        proxy = setup.proxy as Proxy;

        textMesh = circle.GetComponentInChildren<TextMesh>();
        textMesh.text = proxy.level.ToString();
    }
}