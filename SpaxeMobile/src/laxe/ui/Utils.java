package laxe.ui;

import javax.microedition.lcdui.Font;
import javax.microedition.lcdui.Graphics;


public class Utils
{
    public static void drawStringAligned(Graphics gr, String s, int left, int width, int yc)
	{
		int gapw = 0;
		int gaph = 0;
		int margin = 10;
		Font font = gr.getFont();
		int fonth = font.getHeight();

		int len = font.stringWidth(s);
		int numRows = len / (width - 2*margin) + 1;
		int y = yc - (numRows*fonth + (numRows - 1)*gaph) / 2;

		int row = 1;
		for (int i = 0; i < s.length(); i++)
		{
			int x = left + margin;

			if (row == numRows)
			{
				gr.drawString(s.substring(i), width / 2, y, Graphics.TOP | Graphics.HCENTER);
				break;
			}

			while (x < width && i < s.length())
			{
				char c = s.charAt(i);
				gr.drawChar(c, x, y, Graphics.LEFT | Graphics.TOP);
				x += (font.charWidth(c) + gapw);
				if (x + font.charWidth(s.charAt(i + 1)) > width - margin) break;
				i++;
			}

			y += (fonth + gaph);
			row++;
		}
	}
}
