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
	
	public int level = 0;
	public int numCircles = 3;
	public int numLevels = 10;
	public GameObject circlePrefab;


	public CircleVOFactory(){}


	/**
	 * Public interface.
	 */

	public List<CircleVO> getList()
	{
    	List<CircleVO> list = new List<CircleVO>();

    	for( int i = 0; i < numCircles; ++i )
    	{
    		CircleVO circleVO = new CircleVO();
    		NotationVO notationVO = null;

    		do
    		{
				float seed = Random.value;
      			notationVO = levelNotationDelegate( level, i, seed );  		
      		}
    		while( notationValueIsInList( notationVO, list ) );

    		circleVO.level = level;
    		circleVO.gameObject = Assist.GetGameObjectClone( circlePrefab );
    		circleVO.notationVO = notationVO;

    	    list.Add( circleVO );
    	}

    	return list;
	}

	// public GameObject getCircle()
	// {
	// 	GameObject gameObject = Assist.GetGameObjectClone( circlePrefab );
	// 	SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

	// 	spriteRenderer.color = colorBackground; 

	// 	return gameObject;
	// }

	public NotationDelegate levelNotationDelegate
	{
		get 
	    { 
	    	float currentLevel = Mathf.Min( level, numLevels );
	    	float count = (float)NotationDelegateList.Count;
	    	float range = Linear.EaseNone( currentLevel, 0, count, numLevels );
	    	int index = (int)Mathf.Floor( Random.value * range );

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
	        	Notation.positive,
	        	Notation.negative,
	        	Notation.multiplication
	        }; 
	    }
	}


	/**
	 * Private interface.
	 */

	private bool notationValueIsInList(NotationVO notationVO, List<CircleVO> list)
	{
		if( notationVO != null )
		{
			for( int i = 0; i < list.Count; ++i )
			{
			    CircleVO circleVO = list[ i ];
				NotationVO compareVO = circleVO.notationVO;

				if( compareVO.compareValue == notationVO.compareValue )
					return true; 
			}
		}	

		return false;
	}
}