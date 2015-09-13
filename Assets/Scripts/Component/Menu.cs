using UnityEngine;

public class Menu : MonoBehaviour
{
    private Setup setup;
    private Proxy proxy;
    private State state;

    private TweenCircle tweenCircle;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initInteractions();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        setup = gameObject.GetComponent<Setup>();
        tweenCircle = gameObject.GetComponent<TweenCircle>();
        // proxy = setup.proxy as Proxy;
        state = setup.state;
    }


    /** Init interactions. */
    private void initInteractions()
    {
        Interaction[] interactionList = gameObject.GetComponentsInChildren<Interaction>();

        for( int i = 0; i < interactionList.Length; ++i )
        {
            Interaction interaction = interactionList[ i ];
            interaction.OnMouseUp += interactionOnMouseUpHandler;
        }
    }

    private void interactionOnMouseUpHandler(MonoBehaviour monoBehaviour)
    {
        Interaction interaction = monoBehaviour as Interaction;

        switch( interaction.tag )
        {
            case Names.ButtonPlay:
                buttonPlayMouseUpHandler();
                break;
        }
    }


    /** ButtonPlay MouseUp handler. */
    private void buttonPlayMouseUpHandler()
    {
        Tween tween = tweenCircle.AllOut();
        tween.OnComplete += tweenOnCompleteHandler;
    }

    private void tweenOnCompleteHandler(Tween tween)
    {
        state.InvokeExit();   
    }
}