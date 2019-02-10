package laxe;

import java.io.IOException;

import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;


public class Logo extends Canvas  implements Runnable
{
	private Main main;
	private Image logo;
		
	public Logo(Main main)
	{
		this.main = main;
		
		try 
		{
			logo = Image.createImage("/logo.png");
		} 
		catch (IOException e) 
		{
			e.printStackTrace();
		}
		
		this.setFullScreenMode(true);	
	}
	
	public void show()
	{
		new Thread(this).start();
	}
	
	protected void paint(Graphics gr) 
	{
		if (logo != null)
			gr.drawImage(logo, 0, 0, 0);
	}

	public void run() 
	{
		try 
		{
			Thread.sleep(2500);
		} 
		catch (InterruptedException e) 
		{
			e.printStackTrace();
		}
		
		main.goSplash();
	}

}
