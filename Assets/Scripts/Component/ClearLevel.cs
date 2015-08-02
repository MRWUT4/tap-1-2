using UnityEngine;

public class ClearLevel : MonoBehaviour
{
    private State state;
    private Proxy proxy;
    // private LevelVO levelVO;


    /**
     * Component interface.
     */

    public void Awake()
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
        state = gameObject.GetComponent<StateInfo>().state;
        proxy = state.proxy as Proxy;
        // levelVO = proxy.levelVO;
    }


    /** LevelVO functions.*/
    private void initLevelVO()
    {
    	proxy.levelVO = proxy.GetLevelVO();
    }
}