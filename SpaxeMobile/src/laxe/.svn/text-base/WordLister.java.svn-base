package laxe;

import laxe.ui.Keys;
import java.io.IOException;
import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Font;
import java.lang.Runnable;
import java.util.Vector;


public class WordLister extends Canvas implements Runnable
{
	private final String[] letters = {"aábc", "deéf", "ghií", "jkl", "mnñoó", "pqrs", "tuúv", "wxyz"}; 
	
	private Main main;
	
	private DictArticle curArt;
	private int cur;
	private int start;
	
	private int mode;
	
	private int numDrawn;
	private boolean isNumDrawn;
	
	private volatile int key;
	private volatile boolean isPressed;
	private volatile boolean isHeld;
	private Thread thr;
	
	private StringBuffer toFind;
	private boolean isTyping;
	private int curKey;
	private int curLetter;
	private MyTimerTask timerTask;
	
	private Filter filter;
	private boolean filterActive;
	private byte filterPosition;
	private Vector filterInds;
	
	private int numShowed;
	
	
	public WordLister(Main main) throws IOException
	{
		this.main = main;
	
		this.setFullScreenMode(true);
		
		toFind = new StringBuffer();
		
		mode = 0;
		numDrawn = 0;
		isNumDrawn = false;
		
		filter = new Filter();
		filterActive = false;
		filterInds = new Vector();
		filterPosition = 0;
		
		this.init();
		
		numShowed = main.dict.count;
	}
	
	public void reset()
	{
		toFind.delete(0, toFind.length());
		isTyping = false;
		filterActive = false;
	}
	
	public void init() throws IOException
	{
		main.dict = new DictManager();
		if (!main.dict.open("/dict.txt"))
		{
				
		}
			
		cur = 0;
		start = 0;
		curArt = main.dict.getArticle(cur);
	}
	
	/*
	 * Draws a string on the screen aligned on the width
	 */
	public void drawStringAligned(Graphics gr, String s, int yc)
	{
		int gapw = 0;
		int gaph = 0;
		int margin = 10;
		Font font = gr.getFont();
		int fonth = font.getHeight();
		
		int len = font.stringWidth(s);
		int numRows = len / (this.getWidth() - 2*margin) + 1;
		int y = yc - (numRows*fonth + (numRows - 1)*gaph) / 2;
		
		int row = 1;
		for (int i = 0; i < s.length(); i++)
		{
			int x = margin;
			
			if (row == numRows)
			{
				gr.drawString(s.substring(i), this.getWidth() / 2, y, Graphics.TOP | Graphics.HCENTER);
				break;
			}
			
			while (x < this.getWidth() && i < s.length())
			{
				char c = s.charAt(i);
				gr.drawChar(c, x, y, Graphics.LEFT | Graphics.TOP);
				x += (font.charWidth(c) + gapw);
				if (x + font.charWidth(s.charAt(i + 1)) > this.getWidth() - margin) break;
				i++;
			}
			
			y += (fonth + gaph);
			row++;
		}
	}
	
	private void drawLetters(Graphics gr)
	{
		if (!isTyping) return; 			
		
		int x = this.getWidth() - Props.smallFont.stringWidth(letters[curKey - 2]) - 2;
		int y = 0;
		int gap = 0;
		
		gr.setColor(Props.lightColor);
		gr.setFont(Props.smallFont);
		for (int i = 0; i < letters[curKey - 2].length(); i++)
		{
			char c = letters[curKey - 2].charAt(i);
			if (i == curLetter)
			{
				gr.setColor(0x00FF00);
				gr.drawChar(c, x, y, Graphics.TOP | Graphics.LEFT);
				gr.setColor(Props.lightColor);
			}
			else gr.drawChar(c, x, y, Graphics.TOP | Graphics.LEFT);
			x += (Props.smallFont.charWidth(c) + gap);
		}
	}
	
	private void drawFilter(Graphics gr)
	{
		int w = 3*this.getWidth()/4;
		int h = 3*this.getHeight()/4;
		int x = (this.getWidth() - w)/2;
		int y = (this.getHeight() - h)/2;
		
		gr.setColor(Props.darkColor);
		gr.setFont(Props.smallFont);
		
		gr.fillRect(x, y, w, h);
		
		gr.setColor(100, 100, 100);
		gr.fillRect(x, y, w, Props.smallFont.getHeight());
		
		gr.setColor(255, 255, 255);
		gr.drawString("Фильтрация", this.getWidth() / 2, y, Graphics.TOP | Graphics.HCENTER);
		
		x += 2;
		y += (Props.smallFont.getHeight() + 2);
		int x2 = x + 50;
		int dy = Props.smallFont.getHeight() + 2;
		
		gr.setColor(100, 100, 100);
		gr.fillRect(x, y + filterPosition*dy, w - 4, Props.smallFont.getHeight());
		
		gr.setColor(255, 255, 255);
		
		gr.drawString("Nom.:", x, y, Graphics.LEFT | Graphics.TOP);
		gr.drawString(filter.getNombreMode(), x2, y, Graphics.LEFT | Graphics.TOP);
		
		y += dy;
		gr.drawString("Adj.:", x, y, Graphics.LEFT | Graphics.TOP);
		gr.drawString(filter.getAdjetivoMode(), x2, y, Graphics.LEFT | Graphics.TOP);
		
		y += dy;
		gr.drawString("Verbo.:", x, y, Graphics.LEFT | Graphics.TOP);
		gr.drawString(filter.getVerbMode(), x2, y, Graphics.LEFT | Graphics.TOP);
		
		y += dy;
		gr.drawString("Pronom.:", x, y, Graphics.LEFT | Graphics.TOP);
		gr.drawString(filter.getPronombreMode(), x2, y, Graphics.LEFT | Graphics.TOP);
	}
	
	private void draw0(Graphics gr)
	{
		int fieldh = Props.smallFont.getHeight();
		int marginx = 5;
		int gap = 0;
		int scrollw = 4;
		int scrollx = this.getWidth() - scrollw;
		
		gr.setColor(Props.darkColor);
		gr.setFont(Props.smallFont);
		
		// Field
		gr.fillRect(0, 0, this.getWidth(), fieldh);
		
		// Bottom strip
		gr.fillRect(0, this.getHeight() - fieldh, this.getWidth(), fieldh);
		
		// Scroll line
		gr.setColor(0);
		gr.drawLine(scrollx, fieldh, scrollx, this.getHeight() - fieldh);
		
		int x = marginx;
		int y = fieldh + 1;
		int p = start;
		
		while (y < this.getHeight() - 2*fieldh && p < numShowed)
		{
			if (p == cur)
			{
				gr.setColor(210, 210, 200);
				gr.fillRect(0, y, scrollx, fieldh + 1);
				gr.setColor(0);
			}
			
			DictArticle art;
			if (filter.filtered) 
			{
				Integer q = (Integer)(filterInds.elementAt(p++));
				art = main.dict.getArticle(q.intValue());
			}
			else art = main.dict.getArticle(p++);
			
			gr.drawString(art.word, x, y, Graphics.LEFT | Graphics.TOP);
			y += (fieldh + gap);
			
			if (!isNumDrawn) numDrawn++;
		}
		
		if (!isNumDrawn) isNumDrawn = true;
		
		float k = (float)numDrawn / (float)numShowed;
		int scrollh = (int)(k*((float)this.getHeight() - 2.0f*(float)fieldh));
		k = (float)start / (float)numShowed;
		int scrolly = fieldh + (int)(k*((float)this.getHeight() - 2.0f*(float)fieldh));
		
		gr.setColor(Props.darkColor);
		gr.fillRect(scrollx, scrolly, scrollw, scrollh);
		
		gr.setColor(Props.lightColor);
		gr.drawString(toFind.toString(), marginx, 0, Graphics.LEFT | Graphics.TOP);
		
		this.drawLetters(gr);
		
		gr.setFont(Props.smallFont);
		gr.drawString("фильтр", 0, this.getHeight(), Graphics.LEFT | Graphics.BOTTOM);
		gr.drawString("вид", this.getWidth() / 2, this.getHeight(), Graphics.HCENTER | Graphics.BOTTOM);
		gr.drawString("назад", this.getWidth(), this.getHeight(), Graphics.RIGHT | Graphics.BOTTOM);
		
		if (filterActive) this.drawFilter(gr);
		
		gr.setColor(0);
	}
	
	private void draw1(Graphics gr)
	{
		int fieldh = Props.smallFont.getHeight();
		
		gr.drawLine(0, this.getHeight() / 2, this.getWidth(), this.getHeight() / 2);
		
		gr.setColor(Props.darkColor);
		
		// Top strip
		gr.fillRect(0, 0, this.getWidth(), fieldh);
		
		// Bottom strip
		gr.fillRect(0, this.getHeight() - fieldh, this.getWidth(), fieldh);
		
		gr.setFont(Props.smallFont);
		gr.setColor(Props.lightColor);
		
		gr.drawString("формы", 0, this.getHeight(), Graphics.LEFT | Graphics.BOTTOM);
		gr.drawString("вид", this.getWidth() / 2, this.getHeight(), Graphics.HCENTER | Graphics.BOTTOM);
		gr.drawString("назад", this.getWidth(), this.getHeight(), Graphics.RIGHT | Graphics.BOTTOM);
				
		this.getCurArt();
		if (curArt != null)
		{
			gr.setColor(Props.darkColor);
			this.drawStringAligned(gr, curArt.trans, this.getHeight() / 2 + (this.getHeight() / 2 - fieldh) / 2);
			gr.setFont(Props.largeBoldFont);
			this.drawStringAligned(gr, curArt.word, fieldh + (this.getHeight() / 2 - fieldh) / 2);
		}
	}
	
	protected void paint(Graphics gr) 
	{		
		gr.setColor(Props.lightColor);
		gr.fillRect(0, 0, this.getWidth(), this.getHeight());
		gr.setColor(0);
		
		if (mode == 0) this.draw0(gr);
		else this.draw1(gr);
	}
	
	protected void keyPressed(int keyCode)
	{
		int key = Keys.translateKey(keyCode);	
		
		switch (key)
		{
		case Keys.KEYDOWN:
		case Keys.KEYUP:
			if (!filterActive)
			{
				if (!isPressed)
				{
					this.key = key;
					thr = new Thread(this);
					thr.start();
					isHeld = true;
					this.repaint();
				}
			}
			else
			{
				if (key == Keys.KEYDOWN)
				{
					filterPosition++;
					if (filterPosition > 3)
						filterPosition = 0;
				}
				else
				{
					filterPosition--;
					if (filterPosition < 0)
						filterPosition = 3;
				}
				this.repaint();
			}
			break;
			
		case Keys.KEYFIRE:
			if (!filterActive)
			{
				if (mode == 0) mode = 1;
				else mode = 0;
			}
			else
			{
				switch (filterPosition)
				{
				case 0:
					filter.nextNombreMode();
					break;
				case 1:
					filter.nextAdjetivoMode();
					break;
				case 2:
					filter.nextVerbMode();
					break;
				case 3:
					filter.nextPronombreMode();
					break;
				}
				this.filterOut();
			}
			
			this.repaint();
			break;		
		
		case Keys.KEYLSOFT:
			if (mode == 1)
			{
				this.getCurArt();
				main.goConjVerb(curArt);
			}
			else if (mode == 0)
			{
				if (!filterActive)
					filterActive = true;
				else
				{
					filterActive = false;
				}
				this.repaint();
			}
			break;
			
		case Keys.KEYRSOFT:
			main.goMenu();
			break;
			
		case Keys.KEYNUM:
			this.numKeyPressed(keyCode);
			break;
		}
	}
	
	protected void keyReleased(int keyCode)
	{	
		isHeld = false;
	}
	
	private void numKeyPressed(int keyCode)
	{
		if (mode != 0) return;
		
		int i = keyCode - '0';
		if (i < 2) return;
			
		if (!isTyping)
		{
			isTyping = true;
			curKey = i;
			curLetter = 0;
			
			timerTask = new MyTimerTask();
			timerTask.set(this);
			timerTask.start();
			
		}
		else
		{
			if (curKey != i) 
			{
				this.refresh();
				curKey = i;
				curLetter = 0;
			}
			else
			{
				curLetter++;
				if (curLetter >= letters[curKey - 2].length()) curLetter = 0;
			}

			timerTask.cancel();
			timerTask = new MyTimerTask();
			timerTask.set(this);
			timerTask.start();
		}
		
		this.repaint();
	}

	public void run() 
	{		
		isPressed = true;
		
		int delay = 150;
		
		switch (key)
		{					
		case Keys.KEYDOWN:
			while (isHeld)
			{
				cur++;
				if (cur > start + numDrawn - 1) 
				{
					start++;
				}
				if (cur >= numShowed)
				{
					cur = 0;
					start = 0;
				}			
				
				this.repaint();
				try 
				{
					Thread.sleep(delay);
					if (delay > 25) delay -= 30;
					
				} 
				catch (InterruptedException e) 
				{
					e.printStackTrace();
				}
			}
			break;
				
		case Keys.KEYUP:
			while (isHeld)
			{
				cur--;
				if (cur < start)
				{
					start--;
					if (start < 0)
					{
						start = numShowed - numDrawn;
						if (start < 0)
								start = 0;
						cur = numShowed - 1;
					}
				}
				
				this.repaint();
				try 
				{
					Thread.sleep(delay);
					if (delay > 25) delay -= 30;
				} 
				catch (InterruptedException e) 
				{
					e.printStackTrace();
				}
			}			
			break;		
		}
		
		isPressed = false;
	}
	
	public void timeExpired()
	{
		this.refresh();
		this.repaint();
		timerTask = null;
		isTyping = false;
		System.gc();
	}
	
	private void refresh()
	{
		toFind.append(letters[curKey - 2].charAt(curLetter));
		String findstr = toFind.toString();
		
		for (int i = 0; i < numShowed; i++)
		{
			DictArticle art = null;
			
			if (filter.filtered)
			{
				Integer q = (Integer)(filterInds.elementAt(i));
				art = main.dict.getArticle(q.intValue());
			}
			else art = main.dict.getArticle(i);	
			
			if (art.word.startsWith(findstr))
			{
				start = i;
				cur = i;
				return;
			}
		}
		
		System.out.println("Find with advanced finder");
		
		int i = AdvancedVerbFinder.find(findstr, main.dict, filter, filterInds);
		if (i != -1)
		{
			start = i;
			cur = i;
		}
	}
	
	private void filterOut()
	{
		if (!filter.filtered) return;
		
		filterInds.removeAllElements();
		for (int i = 0; i < main.dict.count; i++)
		{
			if (filter.match(main.dict.getArticle(i)))
				filterInds.addElement(new Integer(i));
		}
		
		start = 0;
		cur = 0;
		numShowed = filterInds.size();
		
	}
	
	private void getCurArt()
	{
		if (filter.filtered)
		{
			Integer q = (Integer)(filterInds.elementAt(cur));
			curArt = main.dict.getArticle(q.intValue());
		}
		else curArt = main.dict.getArticle(cur);	
	}
}