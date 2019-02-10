package laxe.ui;

public class Keys 
{
	public static final int KEYNONE = 0;
	public static final int KEYLEFT = 1;
	public static final int KEYRIGHT = 2;
	public static final int KEYUP = 3;
	public static final int KEYDOWN = 4;
	public static final int KEYFIRE = 5;
	public static final int KEYLSOFT = 6;
	public static final int KEYRSOFT = 7;
	public static final int KEYNUM = 8;
	
	public static int translateKey(int code)
	{
		switch(code) 
		{
           			 // коды софткеев и джойстика для NOKIA и SE
        case -1:     // изменяются при препроцессинге
            return KEYUP;
        case -2:
            return KEYDOWN;
        case -3:
            return KEYLEFT;
        case -4:
            return KEYRIGHT;
        case -5:
            return KEYFIRE;
        case -6:
            return KEYLSOFT;
        case -7:
            return KEYRSOFT;
        case '0':
        case '1':
        case '2':
        case '3':
        case '4':
        case '5':
        case '6':
        case '7':
        case '8':
        case '9':
        	return KEYNUM;
        default:
            return KEYNONE;
		}
	}
}