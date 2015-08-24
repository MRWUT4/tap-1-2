using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static MonoBehaviour instance;

    void Awake () 
    {
        if( instance == null ) 
        {
            instance = this;
            DontDestroyOnLoad( gameObject );
        }
        else 
            Destroy( gameObject );
    }
}