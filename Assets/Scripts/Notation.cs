using UnityEngine;

public class Notation
{
	public static NotationVO positive(int level, int index, float seed)
	{
		NotationVO notationVO = new NotationVO();

		float a = seed * 30 * ( index + 1 ) % 99;
		string aString = ( (int)a ).ToString();

		notationVO.value = a;
		notationVO.text = aString;

		return notationVO;
	}

	public static NotationVO negative(int level, int index, float seed)
	{
		return Notation.positive( level, index, -1 * seed );
	}
}


public class NotationVO
{
	public string text;
	public float value;
}