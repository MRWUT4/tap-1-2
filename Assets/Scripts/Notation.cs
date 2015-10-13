using UnityEngine;
using System.Collections.Generic;

public class Notation
{
	/** Real numbers. */
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


	/** Multiplication. */
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


	/** Roman notation. */
	public static NotationVO roman(int level, int index, float seed)
	{
		NotationVO notationVO = new NotationVO();

		float value = 1 + Mathf.Floor( Random.value * 99 );
		string decimalString = ( "0" + value.ToString() );

		if( decimalString.Length > 2 )
			decimalString = decimalString.Substring( decimalString.Length - 2, decimalString.Length - 1 );
		
		string text = romanOnes( stringAt( decimalString, 1 ), romanTens( stringAt( decimalString, 0 ) ) );

		notationVO.value = value;
		notationVO.text = text;

		return notationVO;
	}

	public static string stringAt(string value, int index)
	{
		return value[ index ].ToString();
	}

	public static string romanOnes(string value, string mod = "")
	{
		switch( value )
		{
			case "0": return mod;
			case "1": return mod + "I";
			case "2": return mod + "II";
			case "3": return mod + "III";
			case "4": return mod + "IV";
			case "5": return mod + "V";
			case "6": return mod + "VI";
			case "7": return mod + "VII";
			case "8": return mod + "VIII";
			case "9": return mod + "IX";
		}

		return "";
	}

	public static string romanTens(string value)
	{
		switch( value )
		{
			case "0": return "";
			case "1": return "X";
			case "2": return "XX";
			case "3": return "XXX";
			case "4": return "XL";
			case "5": return "L";
			case "6": return "LX";
			case "7": return "LXX";
			case "8": return "LXXX";
			case "9": return "LC";
		}

		return "";
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