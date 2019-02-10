package laxe;

import java.io.IOException;

import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;



public class Splash extends Canvas  implements Runnable
{
	private Main main;
	private Image splash;
		
	public Splash(Main main)
	{
		this.main = main;
		
		try 
		{
			splash = Image.createImage("/splash.png");
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
		if (splash != null)
			gr.drawImage(splash, 0, 0, 0);
	}

	public void run() 
	{
		this.repaint();
		
		try 
		{
			Thread.sleep(2000);
		} 
		catch (InterruptedException e) 
		{
			e.printStackTrace();
		}
		
		main.goMenu();
	}
}
