package laxe;

import laxe.ui.Keys;
import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Font;
import javax.microedition.lcdui.Graphics;
import java.util.Vector;

public class ConjVerb extends Canvas 
{
	private Main main;
	
	private int[] x;
	private int[] y;
	
	private final byte[][] ctenses = {					
									{Conjugator.TENSE_INDICATIVO_PRESENTE, Conjugator.TENSE_INDICATIVO_PRETERITO_IMPERFECTO, Conjugator.TENSE_INDICATIVO_PRETERITO_INDEFINIDO, 
										Conjugator.TENSE_INDICATIVO_FUTURO_IMPERFECTO, Conjugator.TENSE_INDICATIVO_PRETERITO_PERFECTO,
										Conjugator.TENSE_INDICATIVO_PRETERITO_PLUSCUAMPERFECTO, Conjugator.TENSE_INDICATIVO_PRETERITO_ANTERIOR, 
										Conjugator.TENSE_INDICATIVO_FUTURO_PERFECTO},
										
									{Conjugator.TENSE_POTENCIAL_IMPERFECTO, Conjugator.TENSE_POTENCIAL_PERFECTO},
									
									{Conjugator.TENSE_SUBJUNTIVO_PRESENTE, Conjugator.TENSE_SUBJUNTIVO_PRETERITO_IMPERFECTO, Conjugator.TENSE_SUBJUNTIVO_FUTURO_IMPERFECTO,
										Conjugator.TENSE_SUBJUNTIVO_PRETERITO_PERFECTO, Conjugator.TENSE_SUBJUNTIVO_PRETERITO_PLUSCUAMPERFECTO,
										Conjugator.TENSE_SUBJUNTIVO_FUTURO_PERFECTO}
									};					
	
	private int curTense;
	private int curModo;
	
	private DictArticle art = null;
	
	public ConjVerb(Main main)
	{
		this.main = main;
		
		this.setFullScreenMode(true);
		
		x = new int[3];
		y = new int[7];
		
		x[0] = 0;
		x[1] = 30;
		x[2] = this.getWidth();
		y[0] = Props.smallFont.getHeight();
			
		int dy = (this.getHeight() - 2*y[0]) / 6;
		for (int i = 0; i < 6; i++)
			y[i + 1] = y[0] + (i + 1)*dy;

		curTense = 0;
		curModo = 0;
	}
	
	public void setWord(DictArticle art)
	{
		this.art = art;
	}
	
	private void drawTable(Graphics gr)
	{
		// Fill the strips
		gr.setColor(Props.darkColor);
		gr.fillRect(0, 0, x[1], this.getHeight());
		gr.fillRect(x[1], 0, this.getWidth() - x[1], y[0]);
		
		// Bottom strip
		gr.fillRect(0, this.getHeight() - Props.smallFont.getHeight(), this.getWidth(), Props.smallFont.getHeight());
		
		// Draw horizontal lines
		
		gr.setColor(Props.lightColor);
		for (int i = 1; i < 6; i++)
			gr.drawLine(0, y[i], x[1], y[i]);
		gr.drawLine(0, y[0], x[1], y[0]);
		gr.drawLine(0, this.getHeight() - y[0] - 1, x[1], this.getHeight() - y[0] - 1);
		
		gr.setColor(Props.darkColor);
		for (int i = 1; i < 6; i++)
			gr.drawLine(x[1], y[i], this.getWidth(), y[i]);
		
		// Draw a header
		gr.setFont(Props.smallFont);
		gr.setColor(Props.lightColor);
		gr.drawString(Props.tenses[curModo][curTense], 0, 0, Graphics.TOP | Graphics.LEFT);
		gr.drawString(Props.modos[curModo], this.getWidth(), 0, Graphics.TOP | Graphics.RIGHT);
		
		// Draw "back"
		gr.drawString("назад", this.getWidth(), this.getHeight(), Graphics.RIGHT | Graphics.BOTTOM);
		
		// Draw pronouns
		int h2 = Props.smallFont.getHeight() / 2;
		int xc = (x[1] - x[0]) / 2;
		int dy = (y[1] - y[0]) / 2;		
		for (int i = 0; i < 6; i++)
		{	 
			 gr.drawString(Props.pronouns[i], xc, y[i] + dy - h2, Graphics.TOP | Graphics.HCENTER);
		}
		
		// Draw word forms
		
		Vector forms = Conjugator.Conjugate(art, ctenses[curModo][curTense]);
		
		gr.setFont(Props.largeBoldFont);
		gr.setColor(Props.darkColor);
		h2 = Props.largeBoldFont.getHeight() / 2;
		int h3 = Props.mediumBoldFont.getHeight() / 2;
		xc = x[1] + 6;
		for (int j = 0; j < forms.size(); j++)
		{
			String s = (String)forms.elementAt(j);
			if (Props.largeBoldFont.stringWidth(s) > x[2] - x[1] - 6)
			{
				gr.setFont(Props.mediumBoldFont);
				gr.drawString(s, xc, y[j] + dy - h3, Graphics.TOP | Graphics.LEFT);
				gr.setFont(Props.largeBoldFont);
			}
			else gr.drawString(s, xc, y[j] + dy - h2, Graphics.TOP | Graphics.LEFT);
		}
	}
	
	protected void paint(Graphics gr) 
	{
		gr.setColor(Props.lightColor);
		gr.fillRect(0, 0, this.getWidth(), this.getHeight());
		
		this.drawTable(gr);
	}

	protected void keyReleased(int keyCode)
	{
		int key = Keys.translateKey(keyCode);
		
		switch (key)
		{
			case Keys.KEYRIGHT:
				curTense++;
				if (curTense == Props.tenses[curModo].length) curTense = 0;
				this.repaint();
				break;		
			
			case Keys.KEYLEFT:
				curTense--;
				if (curTense < 0) curTense = Props.tenses[curModo].length - 1;
				this.repaint();
				break;	
				
			case Keys.KEYDOWN:
				curModo++;
				curTense = 0;
				if (curModo == Props.modos.length) curModo = 0;
				this.repaint();
				break;
				
			case Keys.KEYUP:
				curModo--;
				if (curModo < 0) curModo = Props.modos.length - 1;
				this.repaint();
				break;	
				
			case Keys.KEYRSOFT:
				main.goWordLister();
				break;
		}

	}


}
