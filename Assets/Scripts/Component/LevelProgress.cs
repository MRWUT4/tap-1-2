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


    /** Level FrameTimer */
    private void initLevelFrameTimer()
    {
        frameTimer = new FrameTimer( proxy.levelTime );
        frameTimer.OnChange += frameTimerOnChangeHandler;
        frameTimer.OnComplete += frameTimerOnCompleteHandler;
        frameTimer.Start();
    }

    private void frameTimerOnChangeHandler(FrameTimer frameTimer)
    {
    	mutate.scaleX = frameTimer.progress;
    }

    private void frameTimerOnCompleteHandler(FrameTimer frameTimer)
    {
    	state.InvokeExit( Game.GAMEOVER );
    }
}	