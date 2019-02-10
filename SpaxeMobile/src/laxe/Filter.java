package laxe;


public class Filter 
{
	public boolean filtered;
	
	public boolean showNombres;
	public byte family;
	
	public boolean showAdjetivos;
	
	public boolean showVerbs;
	public byte group;
	
	public boolean showPronombres;
	
	
	public Filter()
	{
		filtered = false;
		showNombres = true;
		family = -1;
		showVerbs = true;
		group = -1;
		showPronombres = true;
		showAdjetivos = true;
	}
	
	private void filtered()
	{
		if (showNombres && family == -1 &&
			showVerbs && group == -1 &&
			showPronombres &&
			showAdjetivos)
			filtered = false;
		else filtered = true;
	}
	
	public void nextVerbMode()
	{
		if (!showVerbs) 
		{
			showVerbs = true;
			group = -1;
		}
		else
		{
			group++;
			if (group > 10) showVerbs = false;
		}
		
		this.filtered();
	}
	
	public void nextNombreMode()
	{
		if (!showNombres) 
		{
			showNombres = true;
			family = -1;
		}
		else
		{
			family++;
			if (family > 1) showNombres = false;
		}
		
		this.filtered();
	}
	
	public void nextAdjetivoMode()
	{
		if (!showAdjetivos)
			showAdjetivos = true;
		else showAdjetivos = false;
		
		this.filtered();
	}
	
	public void nextPronombreMode()
	{
		if (!showPronombres)
			showPronombres = true;
		else showPronombres = false;
		
		this.filtered();
	}
	
	public String getNombreMode()
	{
		String res = new String();
		
		if (!showNombres)
			res = "нет";
		else
		{
			switch (family)
			{
			case -1:
				res = "все";
				break;
			case 0:
				res = "муж.р.";
				break;
			case 1:
				res = "жен.р.";
				break;
			}
		}
		
		return res;
	}
	
	public String getVerbMode()
	{
		String res = new String();
		
		if (!showVerbs)
			res = "нет";
		else
		{
			switch (group)
			{
			case -1:
				res = "все";
				break;
			case Conjugator.GROUP_IRREGULAR_INDIVIDUAL:
				res = "индивид.";
				break;
			case Conjugator.GROUP_IRREGULAR_I:
				res = "I гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_II:
				res = "II гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_III:
				res = "III гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_IV:
				res = "IV гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_V:
				res = "V гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_VI:
				res = "VI гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_VII:
				res = "VII гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_VIII:
				res = "VIII гр.";
				break;
			case Conjugator.GROUP_IRREGULAR_IX:
				res = "IX гр.";
				break;
			case Conjugator.GROUP_REGULAR:
				res = "прав.";
				break;
			}
		}
		
		return res;
	}
	
	public String getAdjetivoMode()
	{
		String res = new String();
		
		if (!showAdjetivos)
			res = "нет";
		else
			res = "все";
		
		return res;
	}
	
	public String getPronombreMode()
	{
		String res = new String();
		
		if (!showPronombres)
			res = "нет";
		else
			res = "все";
		
		return res;
	}
	
	public boolean match(DictArticle art)
	{
		boolean res = false;
		
		if (art.isNombre)
		{
			if (!showNombres) res = false;
			else if (family == -1) res = true;
			else if (art.family == family) res = true;
			else res = false;
		}
		
		if (art.isAdjetivo)
		{
			if (!showAdjetivos) res = false;
			else res = true; 
		}
		
		if (art.isPronombre)
		{
			if (!showPronombres) res = false;
			else res = true; 
		}
		
		if (art.isVerb)
		{
			if (!showVerbs) res = false;
			else if (group == -1) res = true;
			else if (art.group == group) res = true;
			else res = false;
		}
		
		return res;
	}
}
