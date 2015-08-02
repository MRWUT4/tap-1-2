using UnityEngine;
using System.Collections.Generic;

public class InteractionCircle : MonoBehaviour
{
    public const string CONTINUE = "CONTINUE";
    public const string GAMEOVER = "GAMEOVER";

    private State state;
    private Proxy proxy;
    private LevelVO levelVO;
    private DoTween doTween;
    private TweenFactory tweenFactory;
    private List<CircleVO> circleVOList;


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

    public void Awake()
    {
        initVariables();
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
        doTween = new DoTween();
        state = gameObject.GetComponent<StateInfo>().state;
        proxy = state.proxy as Proxy;
        levelVO = proxy.levelVO;
        tweenFactory = proxy.tweenFactory;
        circleVOList = levelVO.circleVOList;
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
            Tween tween = tweenCircleOut( circleVO );
            addTweenCompleteHandler( tween );
        }
        else
        if( selectionIsSmallest )
        {
            circleVO.active = false;
            // selectionList.Add( notationVO );
            tweenCircleOut( circleVO );
        }
        else
        {
            initCircleInteraction( false );
            Debug.Log( "game over" );
            // TODO: game over
        }

    }


    /** Tween complete handler functions. */
    private void addTweenCompleteHandler(Tween tween)
    {
        tween.OnComplete += tweenOnCompleteHandler;
    }

    private void tweenOnCompleteHandler(Tween tween)
    {
        state.InvokeExit( CONTINUE );
    }


    /** Tween functions. */
    private Tween tweenCircleOut(CircleVO circleVO)
    {
        Mutate mutate = circleVO.gameObject.GetComponent<Mutate>();
        Tween tween = doTween.Add( tweenFactory.AlphaScaleHideBounceInOut( mutate ) );

    	return tween;
    }
}