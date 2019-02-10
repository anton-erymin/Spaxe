package laxe;

import java.util.Vector;


public class AdvancedVerbFinder 
{
	public static int find(String form, DictManager dict, Filter filter, Vector inds)
	{
		if (filter.filtered && !filter.showVerbs) return -1;
		
		int num = 0;
		if (filter.filtered)
			num = inds.size();
		else num = dict.count;
		
		String first = new Character(form.charAt(0)).toString();
		
		for (int i = 0; i < num; i++)
		{
			DictArticle art;
			
			if (filter.filtered)
			{
				Integer q = (Integer)inds.elementAt(i);
				art = dict.getArticle(q.intValue());
			}
			else art = dict.getArticle(i);
			
			if (!art.word.startsWith(first) || !art.isVerb) continue;
			
			System.out.println(art.word);
			
			Vector forms;
			for (int j = 0; j < 16; j++)
			{
				forms = Conjugator.Conjugate(art, (byte)j);
				for (int k = 0; k < forms.size(); k++)
				{
					String f = (String)forms.elementAt(k);
					if (form.equals(f)) return i;
				}
				
			}
		}
		
		return -1;
	}
}