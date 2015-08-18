using UnityEngine;

public class TweenCircle : MonoBehaviour
{
    // private State state;
    private Setup setup;
    private Proxy proxy;
    // private int i;
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
     * Public interface.
     */

    public Tween AllIn()
    {
        Mutate[] list = gameObject.GetComponentsInChildren<Mutate>();
        Tween tween = null;

        for( int i = 0; i < list.Length; ++i )
        {
            Mutate mutate = list[ i ];

            float startScale = mutate.scaleX;

            mutate.y = i * .1f + Random.Range( -.5f, .5f );
            mutate.x = i * .1f + Random.Range( -.5f, .5f );
            mutate.scaleX = mutate.scaleY = 0;
            mutate.alpha = 0;

            if( levelVO != null )
            {
                float scale = Random.Range( levelVO.minScale, levelVO.maxScale );
                tween = tweenFactory.AlphaScaleShowBounceInOut( mutate, scale, i );

                doTween.Add( tween );
            }
            else
            {
                tween = tweenFactory.AlphaScaleShowBounceInOut( mutate, startScale, i );
                doTween.Add( tween );
            }
        }

        return tween;
    }

    public Tween AllOut()
    {
        Mutate[] list = gameObject.GetComponentsInChildren<Mutate>();
        Tween tween = null;

        for( int i = 0; i < list.Length; ++i )
        {
            Mutate mutate = list[ i ];
            tween = tweenFactory.AlphaScaleBackOut( mutate, i );

            doTween.Add( tween );
        }

        return tween;
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
    }


    /** Init intro tween. */
    private void initIntroTween()
    {
        AllIn();
    }


    private void tweenCircleOut(Mutate mutate, int i)
    {
        // Mutate mutate = gameObject.GetComponent<Mutate>();
        

    }
}