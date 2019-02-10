package laxe;

import java.io.*;
import java.util.Vector;


public class DictManager 
{
	private InputStream is;
	private DataInputStream dis;

	public Vector dict;
	
	public int count; 
	
	
	public DictManager()
	{
            dict = new Vector();
	}
	
	public boolean open(String s) throws IOException
	{
		is = this.getClass().getResourceAsStream(s);
		if (is == null) 
			return false;
		
		dis = new DataInputStream(is);
		
		// Read UTF-8 signature
		dis.readByte();
		dis.readByte();
		
		String sc = this.readUTF16String();
		count = Integer.parseInt(sc);	
		
		for (int i = 0; i < count; i++)
			dict.addElement(this.getNextArticle());
		
		this.close();
		
		return true;
	}
	
	public void close() throws IOException
	{
		dis.close();
		is.close();
	}
	
	public String readUTF16String() throws IOException
	{
		StringBuffer sbuf = new StringBuffer();
		char c;
		
		try
		{
			c = dis.readChar();
			while (c != 0xA)
			{
				sbuf.append(c);
				c = dis.readChar();
			}
		}
		catch (EOFException e)
		{
			return sbuf.toString();
		}
		
		return sbuf.toString();
	}
	
	public DictArticle getNextArticle() throws IOException
	{
		String art = this.readUTF16String();
		if (art.length() == 0) return null;
		
		DictArticle article = new DictArticle();
		
		char[] artc = art.toCharArray();
		int i = 0;
		while (artc[i++] != '\t');
		article.word = art.substring(0, i - 1);
		
		if (artc[i] == '\t')
			while (artc[i] == '\t') i++;
		int j = i;
		while (artc[i++] != '\t');
		article.signature = art.substring(j, i - 1);
		
		if (artc[i] == '\t')
			while (artc[i] == '\t') i++;
		article.trans = art.substring(i);
		
		//System.out.println(article.word + "#" + article.signature + "#" + article.trans);
		
		article.parseSignature();
		
		return article;
	}
	
	public DictArticle getArticle(int i)
	{
		return (DictArticle)dict.elementAt(i);
	}
}
