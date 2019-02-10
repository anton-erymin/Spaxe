package laxe;

import laxe.ui.Keys;
import java.io.IOException;

import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;
import javax.microedition.lcdui.Font;


public class Menu extends Canvas
{
	private final String[] items = {"Обучение", "Таблицы спряжения", "Словарь", "Настройки", "О программе", "Выход"};
	
	private Image bg;
	private int curItem;
	private Font font;
	private Main main;

	public Menu(Main main)
	{
		this.main = main;
		
		try 
		{
			bg = Image.createImage("/menu.png");
		} 
		catch (IOException e) 
		{
			e.printStackTrace();
		}
		
		curItem = 0;
		font = Font.getFont(Font.FACE_SYSTEM, Font.STYLE_PLAIN, Font.SIZE_SMALL);
		
		this.setFullScreenMode(true);
	}
	
	protected void paint(Graphics gr) 
	{
		if (bg != null)
			gr.drawImage(bg, 0, 0, 0);
		
		int gap = 5;
		int y = (this.getHeight() - (items.length*font.getHeight() + (items.length - 1)*gap)) / 2;
		int x = this.getWidth() / 2;
	
		gr.setColor(0);
		
		for (int i = 0; i < items.length; i++)
		{
			if (i == curItem)
			{
				gr.setColor(0x26769E);
				gr.drawString(items[i], x, y, Graphics.TOP | Graphics.HCENTER);
				gr.setColor(0);
			}
			else gr.drawString(items[i], x, y, Graphics.TOP | Graphics.HCENTER);
			y += (font.getHeight() + gap);
		}
	}
	
	protected void keyReleased(int keyCode)
	{
		int key = Keys.translateKey(keyCode);
		
		switch (key)
		{
			case Keys.KEYDOWN:
				curItem++;
				if (curItem == items.length) curItem = 0;
				this.repaint();
				break;
				
			case Keys.KEYUP:
				curItem--;
				if (curItem < 0) curItem = items.length - 1;
				this.repaint();
				break;
				
			case Keys.KEYFIRE:
				switch (curItem)
				{
				case 0:
					main.goLearnWords();
					break;
				case 1:
					main.goConjTable();
					break;
				case 2:
					main.goWordLister();
					break;
				case 3:
					break;
				case 4:
					break;
				case 5:
					main.exit();
					break;
				}
				break;
		}

	}

}
