using UnityEngine;

public class GravitateCircle : MonoBehaviour
{
    // private State state;
    // private Proxy proxy;
    // private LevelVO levelVO;
    public float force = .2f;

    private Proxy proxy;
    private Setup setup;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
    }

    public void FixedUpdate()
    {
    	updateCircleVOForce();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        // setup = gameObject.GetComponent<Setup>();
        // proxy = setup.proxy;
        // state = gameObject.GetComponent<StateInfo>().state;
        // proxy = state.proxy as Proxy;
     //    proxy = Proxy.instance;
    	// levelVO = proxy.levelVO;    
    }

    /** Gravitation functions. */
    private void updateCircleVOForce()
    {
        foreach( Transform child in transform )
            gravitateToCenter( child.gameObject );
    }

    private void gravitateToCenter(GameObject gameObject)
    {
        Ray direction = new Ray( transform.position, gameObject.transform.position );
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

        float dist = Vector3.Distance( gameObject.transform.position, transform.position );

        if( rigidbody )
        {
            Vector3 forceVector3 = -direction.direction * force * dist;
            rigidbody.AddForce( forceVector3 );
        }
     }
}