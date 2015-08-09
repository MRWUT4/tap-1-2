using UnityEngine;

public class Setup : MonoBehaviour
{
	public Facade facade;
    

	/**
	 * Getter / Setter
	 */
	 
    private Proxy _proxy;
    
    public Proxy proxy
    {
    	get 
        { 
            _proxy = _proxy != null ? _proxy : facade.proxy;
            return _proxy; 
        }
    }

    private StateMachine _stateMachine;
    
    public StateMachine stateMachine
    {
        get 
        { 
            _stateMachine = _stateMachine != null ? _stateMachine : facade.stateMachine;
            return _stateMachine; 
        }
    }


    private State _state;
    
    public State state
    {
        get 
        { 
            _state = _state != null ? _state : stateMachine.currentState;
            return _state; 
        }
    }
}