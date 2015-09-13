using UnityEngine;
using System.Collections.Generic;

public class Notation
{
	public static NotationVO positive(int level, int index, float seed)
	{
		NotationVO notationVO = new NotationVO();

		float a = seed * 100 * ( index + 1 ) % 99;
		string aString = ( (int)a ).ToString();

		notationVO.value = a;
		notationVO.text = aString;

		return notationVO;
	}

	public static NotationVO negative(int level, int index, float seed)
	{
		return Notation.positive( level, index, -1 * seed );
	}

	public static NotationVO multiplication(int level, int index, float seed)
	{
		NotationVO notationVO = new NotationVO();

		List<float> list = new List<float>
		{
			1 + ( Random.value * seed * 100 ) % 9,
			1 + ( Random.value * seed * 100 ) % 9
		};

		list.Sort();

		float value = list[ 0 ] * list[ 1 ];

		notationVO.value = value;
		notationVO.text = (int)list[ 0 ] + "x" + (int)list[ 1 ];

		return notationVO;
	}
}


public class NotationVO
{
	public string text;
	public float value;


	/**
	 * Getter / Setter.
	 */
	
	public int compareValue
	{
		get 
	    { 
	        return (int)Mathf.Floor( value ); 
	    }
	}
}