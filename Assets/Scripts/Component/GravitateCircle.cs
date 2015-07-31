using UnityEngine;

public class GravitateCircle : MonoBehaviour
{
    private State state;
    private Proxy proxy;
    private LevelVO levelVO;


    /**
     * Component interface.
     */

    public void Awake()
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
        state = gameObject.GetComponent<StateInfo>().state;
        proxy = state.proxy as Proxy;
    
    	levelVO = proxy.levelVO;    
    }

    /** Gravitation functions. */
    private void updateCircleVOForce()
    {
        for( int i = 0; i < levelVO.circleVOList.Count; ++i )
        {
            CircleVO circleVO = levelVO.circleVOList[ i ];
            gravitateToCenter( circleVO.gameObject );
        }
    }

    private void gravitateToCenter(GameObject gameObject)
    {
        Ray direction = new Ray( transform.position, gameObject.transform.position );
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

        float dist = Vector3.Distance( gameObject.transform.position, transform.position );

        if( rigidbody )
        {
            Vector3 forceVector3 = -direction.direction * levelVO.force * dist;
            rigidbody.AddForce( forceVector3 );
        }
     }
}