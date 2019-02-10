package laxe;

public class DictArticle 
{
	public String word;
	public String trans;
	public String signature;
	
	// Verbo
	public boolean isVerb = false;
	public byte conjugation;
	public byte group = -1;
	public byte individual;
	
	// Nombre
	public boolean isNombre = false;
	public byte family = -1;
	
	// Adjetivo
	public boolean isAdjetivo = false;
	
	// Pronombre
	public boolean isPronombre = false;
	
	
	public void parseSignature()
	{
		char type = signature.charAt(0);
		char c;
		
		switch (type)
		{
		case 'V':
			isVerb = true;
			if (signature.length() < 2) break;
			c = signature.charAt(1);
			conjugation = (byte)Character.digit(c, 10);
			
			c = signature.charAt(2);			
			if (c == 'R') 
				group = Conjugator.GROUP_REGULAR;			
			else 
				group = (byte)Character.digit(c, 10);
			
			if (group == 0)
				individual = Byte.parseByte(signature.substring(3));
			
			break;
			
		case 'N':
			isNombre = true;
			if (signature.length() < 2) break;
			c = signature.charAt(1);
			if (c == 'M')
				family = 0;
			else if (c == 'F')
				family = 1;
			break;
			
		case 'A':
			isAdjetivo = true;
			break;
			
		case 'P':
			isPronombre = true;
			break;
		}
		
	}


        public String toString()
        {
            return word;
        }
}