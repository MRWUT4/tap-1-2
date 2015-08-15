using System.Collections.Generic;
using UnityEngine;


/**
 * CircleVO
 */

public class CircleVO
{
	public GameObject gameObject;
	public NotationVO notationVO;
	public int index;
	public int level;
	public bool active = true;
}


public class CircleVOFactory
{
	public delegate NotationVO NotationDelegate(int level, int index, float seed);

	public CircleVOFactory()
	{
		
	}


	public List<CircleVO> getList(int level, int numCircles, GameObject prefab)
	{
		float seed = UnityEngine.Random.value;

    	List<CircleVO> list = new List<CircleVO>();

    	Debug.Log( numCircles );

    	for( int i = 0; i < numCircles; ++i )
    	{
    		CircleVO circleVO = new CircleVO();
    		
    		circleVO.level = level;
    		circleVO.gameObject = Assist.GetGameObjectClone( prefab );
    		circleVO.notationVO = levelNotationDelegate( level, i, seed );

    	    list.Add( circleVO );
    	}

    	return list;
	}


	public NotationDelegate levelNotationDelegate
	{
		get 
	    { 
	    	float count = (float)NotationDelegateList.Count;
	    	float random = UnityEngine.Random.value;
	    	
	    	int index = (int)Mathf.Floor( random * count );
	        NotationDelegate notationDelegate = NotationDelegateList[ index ];

	        return notationDelegate; 
	    }
	}


	public List<NotationDelegate> NotationDelegateList
	{	
		get 
	    { 	
	        return new List<NotationDelegate>
	        {
	        	Notation.addition
	        }; 
	    }
	}
}