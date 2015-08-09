using UnityEngine;

public class TweenCircle : MonoBehaviour
{
    // private State state;
    private Setup setup;
    private Proxy proxy;
    private int index;
    private LevelVO levelVO;
    private DoTween doTween;
    private TweenFactory tweenFactory;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initIntroTween();
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

        // state = gameObject.GetComponent<StateInfo>().state;
        // proxy = state.proxy as Proxy;
		setup = gameObject.GetComponent<Setup>();
        proxy = setup.proxy;
        levelVO = proxy.levelVO;
        tweenFactory = proxy.tweenFactory;
        index = 0;
    }


    /** Init intro tween. */
    private void initIntroTween()
    {
        foreach( Transform child in transform )
            tweenCircleVOIn( child.gameObject );
    }


	/** Init intro animation. */
    private void tweenCircleVOIn(GameObject gameObject)
    {
        // float i = (float)circleVO.index;
        // GameObject gameObject = circleVO.gameObject;
        
        Mutate mutate = gameObject.GetComponent<Mutate>();
        
        mutate.y = index * .1f + Random.Range( -.5f, .5f );
        mutate.x = index * .1f + Random.Range( -.5f, .5f );
        mutate.scaleX = mutate.scaleY = 0;
        mutate.alpha = 0;


        float scale = Random.Range( levelVO.minScale, levelVO.maxScale );
        doTween.Add( tweenFactory.AlphaScaleShowBounceInOut( mutate, scale, index ) );


        index++;
    }
}