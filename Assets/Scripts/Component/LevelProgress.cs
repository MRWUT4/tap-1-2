using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    private Setup setup;
    private Proxy proxy;
    private Mutate mutate;
    private FrameTimer frameTimer;
    private State state;
    private DoTween doTween;
    private TweenFactory tweenFactory;

    public GameObject progressBar;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initLevelFrameTimer();
        initEventHandler();
        initTweenIn();
    }

    public void FixedUpdate()
    {
    	frameTimer.Update();
        doTween.Update();
    }

    public void TweenIn()
    {
        doTween.Add( tweenFactory.HeightQuadIn( mutate ) );
    }

    public void TweenOut()
    {
        doTween.Add( tweenFactory.HeightQuadOut( mutate ) );
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        doTween = new DoTween();
        setup = gameObject.GetComponent<Setup>();
        Debug.Log( setup );

        proxy = setup.proxy as Proxy;
		// progressBar = GameObject.Find( Names.ProgressBar );
		mutate = progressBar.GetComponent<Mutate>();
		state = setup.state;
        tweenFactory = proxy.tweenFactory;
    }


    /** Event handler. */
    private void initEventHandler(bool boolean = true)
    {
        if( boolean )
            state.OnExit += stateOnExitHandler;    
        else
            state.OnExit -= stateOnExitHandler;
    }

    private void stateOnExitHandler(State state, string message)
    {
        initEventHandler( false );
        changeProxyRestTime();
    }


    /** Tween in functions. */
    private void initTweenIn()
    {
        TweenIn();
    }


    /** Level FrameTimer */
    private void initLevelFrameTimer()
    {
        frameTimer = new FrameTimer( proxy.time );
        Debug.Log( frameTimer );

        frameTimer.OnChange += frameTimerOnChangeHandler;
        frameTimer.OnComplete += frameTimerOnCompleteHandler;
        frameTimer.Start();
    }

    private void frameTimerOnChangeHandler(FrameTimer frameTimer)
    {
        float progress = frameTimer.currentTime / proxy.levelTime;
    	mutate.scaleX = progress;
    }

    private void frameTimerOnCompleteHandler(FrameTimer frameTimer)
    {
    	state.InvokeExit( Game.GAMEOVER );
    }


    /** Rest time handling. */
    private void changeProxyRestTime()
    {
        proxy.time = frameTimer.currentTime * 2;
    }
}	