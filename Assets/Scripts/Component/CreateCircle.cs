using UnityEngine;

public class CreateCircle : MonoBehaviour
{
    private State state;
    private Proxy proxy;
    private LevelVO levelVO;
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
        levelVO = proxy.levelVO;
    }


    /** GameObject Copys. */
    private void initGameObjectCopys()
    {
        for( int i = 0; i < levelVO.numCircles; ++i )
        {
            GameObject gameObject = Assist.GetGameObjectClone( proxy.circlePrefab );
            gameObject.transform.SetParent( transform );  

            Mutate mutate = gameObject.GetComponent<Mutate>();
            mutate.y = (float)i * .1f + Random.Range( -1, 1 );
            mutate.x = (float)i * .1f + Random.Range( -1, 1 );

            // gravitateToCenter( gameObject );

            float scale = Random.Range( levelVO.minScale, levelVO.maxScale );
            mutate.scaleX = mutate.scaleY = .1f;
            mutate.alpha = 0;

            doTween.To( mutate, 1, new 
            { 
                delay = i * .1,
                alpha = 1,
                scaleX = scale,
                scaleY = scale,
                ease = "Back.EaseInOut"
            });

            levelVO.gameObjectList.Add( gameObject );
        }
    }
}