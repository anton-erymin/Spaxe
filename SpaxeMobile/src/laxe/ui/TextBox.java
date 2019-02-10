package laxe.ui;

import javax.microedition.lcdui.Graphics;


public class TextBox extends Control implements Runnable
{
    public String text;

    
    public TextBox()
    {
        text = "";
    }


    protected void paint(Graphics g)
    {
        super.paint(g);

        int padding = 2;
        if (!text.equals(""))
        {
            g.setColor(fontColor);
            g.drawString(text, left + padding, top + padding, Graphics.LEFT | Graphics.TOP);
        }

        int posx = left + padding + font.stringWidth(text) + 2;
        g.drawLine(posx, top + padding, posx, top + font.getHeight());
    }


    protected void keyPressed(int key)
    {
        switch (key)
        {
        }
    }


    protected void keyReleased(int key)
    {
    }


    public void run()
    {
    }


}