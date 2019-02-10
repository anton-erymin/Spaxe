package laxe;

import java.lang.Thread;


public class MyTimerTask extends Thread
{
	private WordLister wl;
	private boolean active;

	
	public MyTimerTask()
	{
		active = true;
	}
	
	public void set(WordLister wl)
	{
		this.wl = wl;
	}
	
	public void run()
	{
		try {
			Thread.sleep(1200);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		if (active) wl.timeExpired();
	}
	
	public void cancel()
	{
		active = false;
	}
}