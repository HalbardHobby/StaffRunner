using UnityEngine;
using System.Collections;

public static class ChordNames {

	//nombre de los acrodes mayores
	public static string I   = "I";
	public static string II  = "II";
	public static string III = "III";
	public static string IV  = "IV";
	public static string V   = "V";
	public static string VI  = "VI";
	public static string VII = "VII";

	//nombre de los acordes menores
	public static string i   = "i";
	public static string ii  = "ii";
	public static string iii = "iii";
	public static string iv  = "iv";
	public static string v   = "v";
	public static string vi  = "vi";
	public static string vii = "vii";

	//nombre de los acordes disminuidos
	public static string iDim   = "i°";
	public static string iiDim  = "ii°";
	public static string iiiDim = "iii°";
	public static string ivDim  = "iv°";
	public static string vDim   = "v°";
	public static string viDim  = "vi°";
	public static string viiDim = "vii°";

	//distribucionde tonos de los acordes
	public static int[] mayor = new int[3]{0,4,7};
	public static int[] minor = new int[3]{0,3,7};
	public static int[] dim   = new int[3]{0,3,6};
	public static int[] aug   = new int[3]{0,7,8};

	//semitonos en una escala
	public static int semitones = 12;
	public static int middleC = 60;

	//definicion de semitonos de una escala
	public static int[] Mayor = new int[8]{0,2,4,5,7,9,11,12};
	public static int[] Minor = new int[8]{0,2,3,5,7,8,10,12};
}
