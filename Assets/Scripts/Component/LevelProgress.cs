using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    private Setup setup;
    private Proxy proxy;
    private Mutate mutate;
    private FrameTimer frameTimer;
    private State state;

    public GameObject progressBar;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initLevelFrameTimer();
        initEventHandler();
    }

    public void FixedUpdate()
    {
    	frameTimer.Update();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        setup = gameObject.GetComponent<Setup>();
        proxy = setup.proxy as Proxy;
		// progressBar = GameObject.Find( Names.ProgressBar );
		mutate = progressBar.GetComponent<Mutate>();
		state = setup.state;
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


    /** Level FrameTimer */
    private void initLevelFrameTimer()
    {
        frameTimer = new FrameTimer( proxy.time );
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
        proxy.time = proxy.time + frameTimer.currentTime;
    }
}	