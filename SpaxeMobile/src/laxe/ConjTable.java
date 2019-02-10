package laxe;

import laxe.ui.Keys;
import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Graphics;


public class ConjTable extends Canvas 
{
	private int[] x;
	private int[] y;
	
	private final String[][][] flexes = 
	{
		// MODO INDICATIVO
		{
			{"-o", 		 "-as", 		"-a", 		"-amos", 		"-áis", 		"-an", 			"-o", 		"-es", 			"-e", 		"-emos", 		"-éis", 		"-en", 			"-o", 		"-es", 			"-e", 		"-imos", 		"-ís", 			"-en"			},
			{"-aba", 	 "-abas", 		"-aba", 	"-ábamos", 		"-abais", 		"-aban", 		"-ía", 		"-ías", 		"-ía", 		"-íamos", 		"-íais", 		"-ían", 		"-ía", 		"-ías", 		"-ía", 		"-íamos", 		"-íais", 		"-ían"			},
			{"-é", 		 "-aste", 		"-ó", 		"-amos", 		"-asteis", 		"-aron", 		"-í", 		"-iste", 		"-ió", 		"-imos", 		"-isteis", 		"-ieron", 		"-í", 		"-iste", 		"-ió", 		"-imos", 		"-isteis", 		"-ieron"		},
			{"-é",		 "-ás", 		"-á", 		"-emos", 		"-éis", 		"-án", 			"-é", 		"-ás", 			"-á", 		"-emos", 		"-éis", 		"-án", 			"-é", 		"-ás", 			"-á", 		"-emos", 		"-éis", 		"-án"			},
			{"he p.",	 "has p.", 		"ha p.", 	"hemos p.", 	"habéis p.", 	"han p.", 		"he p.", 	"has p.", 		"ha p.", 	"hemos p.", 	"habéis p.", 	"han p.", 		"he p.", 	"has p.",	 	"ha p.", 	"hemos p.", 	"habéis p.", 	"han p."		},
			{"había p.", "habías p.", 	"había p.", "habíamos p.", 	"habíais p.", 	"habían p.", 	"había p.", "habías p.", 	"había p.", "habíamos p.", 	"habíais p.", 	"habían p.", 	"había p.", "habías p.", 	"había p.", "habíamos p.", 	"habíais p.", 	"habían p."		},
			{"hube p.",	 "hubiste p.", 	"hubo p.", 	"hubimos p.", 	"hubisteis p.", "hubieron p.", 	"hube p.", 	"hubiste p.", 	"hubo p.", 	"hubimos p.", 	"hubisteis p.", "hubieron p.", 	"hube p.", 	"hubiste p.", 	"hubo p.", 	"hubimos p.", 	"hubisteis p.", "hubieron p."	},
			{"habré p.", "habrás p.", 	"habrá p.", "habremos p.", 	"habréis p.", 	"habrán p.", 	"habré p.", "habrás p.", 	"habrá p.", "habremos p.",	"habréis p.", 	"habrán p.", 	"habré p.", "habrás p.", 	"habrá p.", "habremos p.", 	"habréis p.", 	"habrán p."		}
		},
		
		// MODO POTENCIAL
		{
			{"-ía", 		"-ías", 		"-ía",		 "-íamos", 			"-íais", 		"-ían",		  "-ía", 		"-ías", 		"-ía", 			"-íamos", 		"-íais", 		"-ían", 		"-ía", 			"-ías", 		"-ía", 			"-íamos", 		"-íais", 	   "-ían"		}, 
			{"habría p.", 	"habrías p.", 	"habría p.", "habriamos p.", 	"habríais p.", 	"habrían p.", "habría p.", 	"habrías p.", 	"habría p.", 	"habriamos p.", "habríais p.",  "habrían p.", 	"habría p.", 	"habrías p.", 	"habría p.", 	"habriamos p.", "habríais p.", "habrían p."	}
		},
		
		// MODO SUBJUNTIVO
		{
			{"-e", 			"-es", 			"-e", 			"-emos", 			"-éis", 		"-en", 			"-a", 			"-as", 			"-a", 			"-amos", 			"-áis", 			"-an", 			"-a", 			"-as", 			"-a", 			"-amos", 			"-áis", 			"-an"			},
			{"-ara/ase", 	"-aras/ases", 	"-ara/ase", 	"-áramos/ásemos", 	"-arais/aseis", "-aran/asen", 	"-iera/iese", 	"-ieras/ieses", "-iera/iese", 	"-iéramos/iésemos", "-ierais/ieseis", 	"-ieran/iesen", "-iera/iese", 	"-ieras/ieses", "-iera/iese", 	"-iéramos/iésemos", "-ierais/ieseis", 	"-ieran/iesen"	},
			{"-are", 		"-ares", 		"-are", 		"-áremos", 			"-areis", 		"-aren", 		"-iere", 		"-ieres", 		"-iere", 		"-iéremos", 		"-iereis", 			"-ieren", 		"-iere", 		"-ieres",	 	"-iere", 		"-iéremos", 		"-iereis", 			"-ieren"		},
			{"haya p.", 	"hayas p.", 	"haya p.", 		"hayamos p.", 		"hayáis p.", 	"hayan p.", 	"haya p.", 		"hayas p.", 	"haya p.", 		"hayamos p.", 		"hayáis p.", 		"hayan p.", 	"haya p.", 		"hayas p.", 	"haya p.", 		"hayamos p.", 		"hayáis p.", 		"hayan p."		},
			{"", 			"", 			"", 			"", 				"", 			"", 			"", 			"", 			"", 			"", 				"", 				"", 			"", 			"", 			"", 			"", 				"", 				""				},
			{"hubierá p.", 	"hubieres p.", 	"hubiere p.", 	"hubiéremos p.", 	"hubiereis p.", "hubieren p.", 	"hubierá p.", 	"hubieres p.", 	"hubiere p.", 	"hubiéremos p.", 	"hubiereis p.", 	"hubieren p.", 	"hubierá p.", 	"hubieres p.", 	"hubiere p.", 	"hubiéremos p.", 	"hubiereis p.", 	"hubieren p."	}
		}
	};
										
	
	private int curModo;
	private int curTense;
	private int curConj;
	
	private Main main;
	
	public ConjTable(Main main)
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

		curModo = 0;
		curTense = 0;
		curConj = 1;
	}
	
	private void drawTable(Graphics gr)
	{
		this.drawConjugation(gr);
		
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
		
		// Draw flexes
		gr.setFont(Props.largeBoldFont);
		gr.setColor(Props.darkColor);
		h2 = Props.largeBoldFont.getHeight() / 2;
		int flex = 6*(curConj - 1);
		xc = x[1] + 6;
		for (int j = 0; j < 6; j++)
		{
			gr.drawString(flexes[curModo][curTense][flex++], xc, y[j] + dy - h2, Graphics.TOP | Graphics.LEFT);
		}
	}
	
	private void drawConjugation(Graphics gr)
	{
		int thickness = 15;
		int height = 120;
		int gap = 16;
		gr.setColor(255, 248, 181);
		
		int xc = (x[1] + (x[2] - x[1]) / 2) - (curConj*thickness + (curConj - 1)*gap) / 2;
		int yc = (this.getHeight() - height) / 2;
		
		for (int i = 1; i <= curConj; i++)
		{
			gr.fillRect(xc, yc, thickness, height);
			xc += (thickness + gap);
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
				if (curTense == Props.tenses[curModo].length)
				{
					curModo++;
					if (curModo == Props.modos.length)
						curModo = 0;
					curTense = 0;
				}
				this.repaint();
				break;		
			
			case Keys.KEYLEFT:
				curTense--;
				if (curTense < 0)
				{
					curModo--;
					if (curModo < 0)
						curModo = Props.modos.length - 1;
					curTense = Props.tenses[curModo].length - 1;
					
				}
				this.repaint();
				break;	
				
			case Keys.KEYDOWN:
				curConj++;
				if (curConj > 3) curConj = 1;
				this.repaint();
				break;
				
			case Keys.KEYUP:
				curConj--;
				if (curConj < 1) curConj = 3;
				this.repaint();
				break;		
				
			case Keys.KEYRSOFT:
				main.goMenu();
				break;
		}

	}
	
}
