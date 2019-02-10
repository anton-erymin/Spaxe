package laxe;

import laxe.ui.Keys;
import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Font;
import javax.microedition.lcdui.Graphics;
import java.util.Random;
import javax.microedition.rms.*;
import java.io.*;


public class LearnWords extends Canvas
{
	private static final byte MODE_LOOKTHROUGH = 0;
	private static final byte MODE_GUESS_TRANSLATION = 1;
	private static final byte MODE_GUESS_WORD = 2;
	private static final byte MODE_WRITE_WORD = 3;
	private static final byte MODE_FINISHED = 4;
	
	private static final byte WORD_COUNT = 10;
	private static final byte VARIANTS_COUNT = 5;
	
	private static final byte SCORE_1 = 2;
	private static final byte SCORE_2 = 4;
	
	private Main main;
	
	// For saving
	private int[] wordInds;
	private byte[] score;
	private byte mode;
	private byte curWord;
	private int checkedWords;
	
	private int[] randWords;
	private Random rand;
	private DictArticle curArt;
	private int curVariant;
	private int rightVariant;
	private int scoreLevel;

        
	public LearnWords(Main main)
	{
		this.main = main;
		this.setFullScreenMode(true);
		rand = new Random();
		mode = MODE_FINISHED;
		wordInds = new int[WORD_COUNT];
		score = new byte[WORD_COUNT];
		randWords = new int[VARIANTS_COUNT - 1];
		curVariant = 0;
		rightVariant = 0;
	}
	
	public boolean init()
	{
		if (!this.isFinished()) return true;
		
		if (!load()) 
		{
			this.newGame();
			return true;
		}
		
		return false;
	}
	
	public void newGame()
	{
		boolean flag;
		for (int i = 0; i < WORD_COUNT; i++)
		{
			do
			{
				wordInds[i] = rand.nextInt(main.dict.count);
				
				flag = false;
				for (int j = 0; j < i; j++)
					if (wordInds[i] == wordInds[j])
					{
						flag = true;
						break;
					}
				
			} while (flag);
			
			
			score[i] = 0;
		}	
		
		mode = MODE_LOOKTHROUGH;
		curWord = 0;
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
	
	
	private void drawLookthrough(Graphics gr)
	{
		gr.drawString("Этап 1: Просмотр слов", 0, 0, Graphics.LEFT | Graphics.TOP);
		
		gr.setColor(Props.darkColor);
		gr.drawLine(0, this.getHeight() / 2, this.getWidth(), this.getHeight() / 2);
		
		curArt = main.dict.getArticle(wordInds[curWord]);
		
		if (curArt != null)
		{
			this.drawStringAligned(gr, curArt.trans, this.getHeight() / 2 + (this.getHeight() - Props.smallFont.getHeight() - this.getHeight() / 2) / 2);
			gr.setFont(Props.largeBoldFont);
			this.drawStringAligned(gr, curArt.word, Props.smallFont.getHeight() + (this.getHeight() / 2 - Props.smallFont.getHeight()) / 2);
		}
	}
	
	private void drawGuessTrans(Graphics gr)
	{
		gr.drawString("Этап 2: Выбор перевода", 0, 0, Graphics.LEFT | Graphics.TOP);
		
		int fh = Props.smallFont.getHeight();
		int height = (this.getHeight() - 2*fh) / (VARIANTS_COUNT + 1);
		
		// White Field
		gr.setColor(0xFFFFFF);
		gr.fillRect(0, fh + height, this.getWidth(), this.getHeight() - 2*fh - height);
		gr.setColor(Props.darkColor);
		gr.drawLine(0, fh + height, this.getWidth(), fh + height);
		
		int y = fh + height / 2;
		gr.setFont(Props.largeBoldFont);
		
		DictArticle art;
		curArt = main.dict.getArticle(wordInds[curWord]);
		if (curArt != null)
		{
			this.drawStringAligned(gr, curArt.word, y);
			y += height;
			gr.setFont(Props.smallFont);
			
			for (int i = 0, j = 0; i < VARIANTS_COUNT; i++)
			{
				if (i == curVariant)
				{
					gr.setColor(Props.selectionColor);
					gr.fillRect(0, fh + (curVariant + 1)*height + 4, this.getWidth(), height - 4);
					gr.setColor(Props.darkColor);
				}
				
				if (i == rightVariant)
				{
					this.drawStringAligned(gr, curArt.trans, y);
				}
				else
				{
					art = main.dict.getArticle(randWords[j++]);
					this.drawStringAligned(gr, art.trans, y);
				}
				y += height;
			}
		}
		
		
	}
	
	private void drawGuessWord(Graphics gr)
	{
		gr.drawString("Этап 3: Выбор испанского слова", 0, 0, Graphics.LEFT | Graphics.TOP);
		
		int fh = Props.smallFont.getHeight();
		int height = (this.getHeight() - 2*fh) / (VARIANTS_COUNT + 1);
		
		// White Field
		gr.setColor(0xFFFFFF);
		gr.fillRect(0, fh + height, this.getWidth(), this.getHeight() - 2*fh - height);
		gr.setColor(Props.darkColor);
		gr.drawLine(0, fh + height, this.getWidth(), fh + height);
		
		int y = fh + height / 2;
		gr.setFont(Props.largeBoldFont);
		
		DictArticle art;
		curArt = main.dict.getArticle(wordInds[curWord]);
		if (curArt != null)
		{
			this.drawStringAligned(gr, curArt.trans, y);
			y += height;
			gr.setFont(Props.smallFont);
			
			for (int i = 0, j = 0; i < VARIANTS_COUNT; i++)
			{
				if (i == curVariant)
				{
					gr.setColor(Props.selectionColor);
					gr.fillRect(0, fh + (curVariant + 1)*height + 4, this.getWidth(), height - 4);
					gr.setColor(Props.darkColor);
				}
				
				if (i == rightVariant)
				{
					this.drawStringAligned(gr, curArt.word, y);
				}
				else
				{
					art = main.dict.getArticle(randWords[j++]);
					this.drawStringAligned(gr, art.word, y);
				}
				y += height;
			}
		}
	}
	
	private void drawWriteWord(Graphics gr)
	{
		gr.drawString("Этап 4: Напишите слово", 0, 0, Graphics.LEFT | Graphics.TOP);
	}

	protected void paint(Graphics gr) 
	{	
		gr.setColor(Props.lightColor);
		gr.fillRect(0, 0, this.getWidth(), this.getHeight());
		
		// Strips
		gr.setColor(Props.darkColor);
		gr.fillRect(0, 0, this.getWidth(), Props.smallFont.getHeight());
		gr.fillRect(0, this.getHeight() - Props.smallFont.getHeight(), this.getWidth(), Props.smallFont.getHeight());
		
		// "Back"
		gr.setColor(Props.lightColor);
		gr.setFont(Props.smallFont);
		gr.drawString("назад", this.getWidth(), this.getHeight(), Graphics.BOTTOM | Graphics.RIGHT);
		// "New"
		gr.drawString("заново", 0, this.getHeight(), Graphics.BOTTOM | Graphics.LEFT);
		
		switch (mode)
		{
		case MODE_LOOKTHROUGH:
			this.drawLookthrough(gr);
			break;
		case MODE_GUESS_TRANSLATION:
			this.drawGuessTrans(gr);
			break;
		case MODE_GUESS_WORD:
			this.drawGuessWord(gr);
			break;
		case MODE_WRITE_WORD:
			this.drawWriteWord(gr);
		}
	}

	private void generateRandWords()
	{
		for (int i = 0; i < VARIANTS_COUNT - 1; i++)
			randWords[i] = rand.nextInt(main.dict.count);
		rightVariant = rand.nextInt(5);
	}
	
	private byte selectNextWord()
	{
		if (checkedWords == WORD_COUNT) return -1;	
		
		byte n = 0, p = 0;
		for (byte i = 0; i < WORD_COUNT; i++)
			if (score[i] < scoreLevel) 
			{
				n++;
				p = i;
			}
		
		if (n == 1) return p;
		
		byte r = (byte)rand.nextInt(WORD_COUNT);
		while (score[r] >= scoreLevel || r == curWord)
			r = (byte)rand.nextInt(WORD_COUNT);
		
		return r;
	}
	
	protected void keyPressed(int keyCode)
	{
		int key = Keys.translateKey(keyCode);	
		
		switch (key)
		{
		case Keys.KEYDOWN:
			if (mode == MODE_GUESS_TRANSLATION || mode == MODE_GUESS_WORD)
			{
				curVariant++;
				if (curVariant >= VARIANTS_COUNT) curVariant = 0;
				this.repaint();
			}
			break;
		case Keys.KEYUP:
			if (mode == MODE_GUESS_TRANSLATION || mode == MODE_GUESS_WORD)
			{
				curVariant--;
				if (curVariant < 0) curVariant = VARIANTS_COUNT - 1;
				this.repaint();
			}
			break;
		case Keys.KEYFIRE:
			if (mode == MODE_LOOKTHROUGH)
			{
				curWord++;
				if (curWord >= WORD_COUNT) 
				{
					mode = MODE_GUESS_TRANSLATION;
					scoreLevel = SCORE_1;
					checkedWords = 0;
					curWord = selectNextWord();
					generateRandWords();
				}
			}
			
			if (mode == MODE_GUESS_TRANSLATION)
			{
				if (curVariant == rightVariant)
				{
					score[curWord]++;
					if (score[curWord] == scoreLevel) checkedWords++;
				}
				
				curWord = selectNextWord();
				
				if (curWord < 0)
				{
					mode++;
					checkedWords = 0;
					scoreLevel = SCORE_2;
					curWord = selectNextWord();
					
				}
				
				generateRandWords();
			}
			
			if (mode == MODE_GUESS_WORD)
			{
				if (curVariant == rightVariant)
				{
					score[curWord]++;
					if (score[curWord] == scoreLevel) checkedWords++;
				}
				
				curWord = selectNextWord();
				
				if (curWord < 0)
				{
					mode = MODE_FINISHED;
					main.goMenu();
				}
				
				generateRandWords();
			}
			this.repaint();
			
			break;		
		
		case Keys.KEYLSOFT:
			this.newGame();
			this.repaint();
			break;
			
		case Keys.KEYRSOFT:
			main.goMenu();
			break;
		}
	}
	
	public void save()
	{	
		RecordStore rs = null;
		
		try
		{
			rs = RecordStore.openRecordStore("spaxe", true);
			
			ByteArrayOutputStream baos = new ByteArrayOutputStream();
			DataOutputStream dos = new DataOutputStream(baos);
			
			dos.writeByte(mode);
			dos.writeByte(curWord);
			dos.writeInt(checkedWords);
			dos.write(score);
			for (int i = 0; i < WORD_COUNT; i++)
				dos.writeInt(wordInds[i]);
			
			byte[] record = baos.toByteArray();
			
			if (rs.getNumRecords() == 0)
			{
				rs.addRecord(record, 0, record.length);
			}
			else
			{
				rs.setRecord(1, record, 0, record.length);
			}

			dos.close();
			baos.close();
			rs.closeRecordStore();
		} 
		
		catch (RecordStoreFullException e) 
		{
			e.printStackTrace();
		} 
		catch (RecordStoreNotFoundException e) 
		{
			e.printStackTrace();
		} 
		catch (RecordStoreException e) 
		{
			e.printStackTrace();
		} 
		catch (IOException e) 
		{
			e.printStackTrace();
		}
	}
	
	public boolean load()
	{
		RecordStore rs = null;
		
		try
		{
			rs = RecordStore.openRecordStore("spaxe", false);
			
			if (rs.getNumRecords() == 0) return false;
			
			byte[] record = rs.getRecord(1);

			ByteArrayInputStream bais = new ByteArrayInputStream(record);
			DataInputStream dis = new DataInputStream(bais);
			
			mode = dis.readByte();
			curWord = dis.readByte();
			checkedWords = dis.readInt();
			dis.read(score, 0, WORD_COUNT);
			for (int i = 0; i < WORD_COUNT; i++)
				wordInds[i] = dis.readInt();
			
			dis.close();
			bais.close();
			rs.closeRecordStore();
			
			RecordStore.deleteRecordStore("spaxe");
		} 
		catch (RecordStoreFullException e) 
		{
			e.printStackTrace();
		} 
		catch (RecordStoreNotFoundException e) 
		{
			e.printStackTrace();
			return false;
		} 
		catch (RecordStoreException e) 
		{
			e.printStackTrace();
		} 
		catch (IOException e) 
		{
			e.printStackTrace();
		}
		
		if (mode == MODE_GUESS_TRANSLATION) scoreLevel = SCORE_1;
		else if (mode == MODE_GUESS_WORD) scoreLevel = SCORE_2;
		
		generateRandWords();
		
		main.showMsg(main.msgLoad);
		
		return true;
	}
	
	public boolean isFinished()
	{
		if (mode == MODE_FINISHED) return true;
		else return false;
	}
}