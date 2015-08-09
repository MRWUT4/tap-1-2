using UnityEngine;

public class ClearLevel : MonoBehaviour
{
    private Setup setup;
    private Proxy proxy;
    // private State state;
    // private LevelVO levelVO;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initLevelVO();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        // state = gameObject.GetComponent<StateInfo>().state;
        // proxy = state.proxy as Proxy;
        // levelVO = proxy.levelVO;
        // proxy = Proxy.instance;
        setup = gameObject.GetComponent<Setup>();
        proxy = setup.proxy;
    }


    /** LevelVO functions.*/
    private void initLevelVO()
    {
    	proxy.levelVO = proxy.GetLevelVO();
    }
}