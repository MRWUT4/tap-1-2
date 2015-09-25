using UnityEngine;

public class Test : MonoBehaviour
{
	private GameObject cirlce;
	private Transform circleTransform;
	private DoTween doTween;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initTween();
    }

    public void FixedUpdate()
    {
    	doTween.Update();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
    	doTween = new DoTween();

    	cirlce = GameObject.FindGameObjectsWithTag( "Circle" )[ 0 ];
    	circleTransform = cirlce.GetComponent<Transform>();
    }


    /** Tween functions. */
    private void initTween()
    {
    	Vector3 position = circleTransform.position;

    	Tween tween = doTween.To( position, 1, new 
    	{ 
    		delay = 3,
    		x = 2
    	
    	}, Bounce.EaseOut );

    	tween.OnUpdate += tweenOnUpdateHandler;
    }

    private void tweenOnUpdateHandler(Tween tween)
    {
    	circleTransform.position = ( (Vector3)tween.Target );
    }
}