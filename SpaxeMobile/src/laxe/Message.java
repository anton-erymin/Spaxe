package laxe;

import laxe.ui.Keys;
import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Font;
import javax.microedition.lcdui.Graphics;


public class Message extends Canvas 
{
	public static final int TYPE_INFO = 0;
	public static final int TYPE_QUESTION = 1;
	
	public static final int M_YES = 0;
	public static final int M_NO = 1;
	
	private String caption;
	private String text;
	private int type;
	private IMessage im;

	
	private Message(String caption, String text, int type, IMessage im)
	{
		this.caption = caption;
		this.text = text;
		this.type = type;
		this.im = im;
		
		this.setFullScreenMode(true);
	}
	
	public static Message msg(String caption, String text, int type, IMessage im)
	{
		return new Message(caption, text, type, im);
	}
	
	protected void paint(Graphics gr) 
	{
		gr.setColor(Props.lightColor);
		gr.fillRect(0, 0, this.getWidth(), this.getHeight());
		
		// Strips
		gr.setColor(Props.darkColor);
		gr.fillRect(0, 0, this.getWidth(), Props.smallFont.getHeight());
		gr.fillRect(0, this.getHeight() - Props.smallFont.getHeight(), this.getWidth(), Props.smallFont.getHeight());
		
		this.drawStringAligned(gr, text, this.getHeight() / 2);
		
		// Soft keys
		gr.setColor(Props.lightColor);
		gr.setFont(Props.smallFont);
		
		if (type == TYPE_INFO)
			gr.drawString("ok", this.getWidth(), this.getHeight(), Graphics.BOTTOM | Graphics.RIGHT);
		if (type == TYPE_QUESTION)
		{
			gr.drawString("да", 0, this.getHeight(), Graphics.BOTTOM | Graphics.LEFT);
			gr.drawString("нет", this.getWidth(), this.getHeight(), Graphics.BOTTOM | Graphics.RIGHT);
		}
		
		// Caption
		gr.drawString(caption, 0, 0, Graphics.TOP | Graphics.LEFT);		
	}
	
	protected void keyPressed(int keyCode)
	{
		int key = Keys.translateKey(keyCode);
		
		switch (key)
		{
		case Keys.KEYLSOFT:
			if (type == TYPE_QUESTION) im.msgAction(M_YES, this);
			break;
		case Keys.KEYRSOFT:
			if (type == TYPE_QUESTION) im.msgAction(M_NO, this);
			else im.msgAction(0, this);
			break;
		}
	}
	
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
}