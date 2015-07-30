using UnityEngine;

public class SetupCircle : MonoBehaviour
{
    private State state;
    private Proxy proxy;
    private LevelVO levelVO;
    private TweenFactory tweenFactory;
    private DoTween doTween;
    

    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initGameObjectCopys();
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
        tweenFactory = proxy.tweenFactory;
        levelVO = proxy.levelVO;
    }


    /** Create GameObject Copys. */
    private void initGameObjectCopys()
    {
        for( int i = 0; i < levelVO.circleVOList.Count; ++i )
        {
            CircleVO circleVO = levelVO.circleVOList[ i ];
            circleVO.index = i;


            setupCircleText( circleVO );
            animateGameObject( circleVO);
        }
    }


    /** Init intro animation. */
    private void animateGameObject(CircleVO circleVO)
    {
        float i = (float)circleVO.index;
        GameObject gameObject = circleVO.gameObject;
        
        Mutate mutate = gameObject.GetComponent<Mutate>();
        
        mutate.y = i * .1f + Random.Range( -.5f, .5f );
        mutate.x = i * .1f + Random.Range( -.5f, .5f );
        mutate.scaleX = mutate.scaleY = 0;
        mutate.alpha = 0;


        float scale = Random.Range( levelVO.minScale, levelVO.maxScale );
        doTween.Add( tweenFactory.AlphaScaleShowBounceInOut( mutate, scale, i ) );

        // doTween.To( mutate, 1.2f, new 
        // { 
        //     delay = i * .1,
        //     alpha = 1,
        //     scaleX = scale,
        //     scaleY = scale,
        //     ease = "Back.EaseInOut"
        // });
    }


    /** Setup Number values. */
    private void setupCircleText(CircleVO circleVO)
    {
        GameObject gameObject = circleVO.gameObject;
        gameObject.transform.SetParent( transform );

        TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
        textMesh.text = circleVO.notationVO.text;

        // textMesh.text = ( i + 1 ).ToString();
    }
}