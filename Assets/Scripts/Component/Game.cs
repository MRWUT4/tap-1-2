using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public const string CONTINUE = "CONTINUE";
    public const string GAMEOVER = "GAMEOVER";

    // private State state;
    private Setup setup;
    private Proxy proxy;
    private LevelVO levelVO;
    private DoTween doTween;
    private TweenFactory tweenFactory;
    private TweenCircle tweenCircle;
    private List<CircleVO> circleVOList;
    private State state;
    private LevelProgress levelProgress;


    /**
     * Getter / Setter
     */ 

    public NotationVO smallestNotationVO
    {
        get 
        {
            NotationVO smallestVO = null;

            for( int i = 0; i < circleVOList.Count; ++i )
            {
                CircleVO circleVO = circleVOList[ i ];
                NotationVO notationVO = circleVO.notationVO;

                bool isSmaller = smallestVO == null || notationVO.value < smallestVO.value;

                if( circleVO.active && isSmaller )
                    smallestVO = notationVO;
            }

            return smallestVO; 
        }
    }
    
    public bool selectionIsComplete
    {
        get 
        { 
            int numActive = 0;

            for( int i = 0; i < circleVOList.Count; ++i )
            {
                CircleVO circleVO = circleVOList[ i ];
                
                if( circleVO.active )
                    numActive++;   
            }

            bool isComplete = numActive == 1;

            return isComplete; 
        }
    }

    private CircleVO getGameObjectCircleVO(GameObject gameObject)
    {
    	for( int i = 0; i < circleVOList.Count; ++i )
    	{
    	    CircleVO circleVO = circleVOList[ i ];
    	    
    	    if( circleVO.gameObject == gameObject )
    	 		return circleVO;   
    	}

    	return null;
    }


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initProxyLevel();
        initCircleInteraction();
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
        doTween = new DoTween( true );
        setup = gameObject.GetComponent<Setup>();
        tweenCircle = gameObject.GetComponent<TweenCircle>();
        levelProgress = gameObject.GetComponent<LevelProgress>();
        state = setup.state;
        proxy = setup.proxy;
        levelVO = proxy.levelVO;
        tweenFactory = proxy.tweenFactory;
        circleVOList = levelVO.circleVOList;
    }


    /** Init proxy level. */
    private void initProxyLevel()
    {
        proxy.level++;
    }


    /** Circle interaction. */
    private void initCircleInteraction(bool boolean = true)
    {
    	for( int i = 0; i < circleVOList.Count; ++i )
    	{
    	    CircleVO circleVO = circleVOList[ i ];
    	    
    	    GameObject gameObject = circleVO.gameObject;
    	    Interaction interaction = gameObject.GetComponent<Interaction>();

            if( boolean )
               interaction.OnMouseUp += interactionOnMouseUpHandler;
            else
    	       interaction.OnMouseUp -= interactionOnMouseUpHandler;
    	}
    }

    private void interactionOnMouseUpHandler(MonoBehaviour monoBehaviour)
    {
        Interaction interaction = monoBehaviour as Interaction;
    	interaction.OnMouseUp -= interactionOnMouseUpHandler;

        CircleVO circleVO = getGameObjectCircleVO( interaction.gameObject );

        validateCircleVO( circleVO );
    }


    /** Circle validation functions. */
    private void validateCircleVO(CircleVO circleVO)
    {
        NotationVO smallestNotationVO = this.smallestNotationVO;
        NotationVO notationVO = circleVO.notationVO;

        bool selectionIsSmallest = smallestNotationVO == notationVO;

        if( selectionIsComplete )
        {
            // TODO: next level 
            
            disableCircleCollider( circleVO );
            Tween tween = tweenCircleToFillScreen( circleVO );
            addTweenCompleteHandler( tween );
        }
        else
        if( selectionIsSmallest )
        {
            circleVO.active = false;
            tweenCircleOut( circleVO );
        }
        else
            gameOverHandler();
    }


    /** Disable Circle Collider 2D */
    private void disableCircleCollider(CircleVO circleVO)
    {
        GameObject gameObject = circleVO.gameObject;
        CircleCollider2D circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
    }


    /** Tween complete handler functions. */
    private void addTweenCompleteHandler(Tween tween)
    {
        tween.OnComplete += tweenOnCompleteHandler;
    }

    private void tweenOnCompleteHandler(Tween tween)
    {
        state.InvokeExit( CONTINUE );
    	// Application.LoadLevel( "Game" );
        // state.InvokeExit( CONTINUE );
    }


    /** Game Over. */
    private void gameOverHandler()
    {
        initCircleInteraction( false );
        
        levelProgress.TweenOut();

        Tween tween = tweenCircle.AllOut();
        tween.OnComplete += tweenCircleAllOutCompleteHandler;
    }

    private void tweenCircleAllOutCompleteHandler(Tween tween)
    {
        state.InvokeExit( GAMEOVER );
    }


    /** Tween functions. */
    private Tween tweenCircleToFillScreen(CircleVO circleVO)
    {
        Mutate mutate = circleVO.gameObject.GetComponent<Mutate>();
        List<Tween> list = doTween.Add( tweenFactory.ScaleFillScreenBounceIn( mutate ) );

        return list[ list.Count - 1 ];        
        // return null;
    }

    private Tween tweenCircleOut(CircleVO circleVO)
    {
        Mutate mutate = circleVO.gameObject.GetComponent<Mutate>();
        Tween tween = doTween.Add( tweenFactory.AlphaScaleHideBounceInOut( mutate ) );

    	return tween;
    }
}