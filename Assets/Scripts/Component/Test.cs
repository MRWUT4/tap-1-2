using UnityEngine;
using DavidOchmann.Extensions;

public class Test : MonoBehaviour
{
	private GameObject cirlce;
	private Transform circleTransform;
	private DoTween doTween;
    private SpriteRenderer spriteRenderer;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initTween();
        initColorTest();
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


    /** Color Test. */
    private void initColorTest()
    {
        Color color = new Color( 1, 0, 0, 1 );

        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = color.HSB( 1, .5f, .5f, .5f );
    }
}