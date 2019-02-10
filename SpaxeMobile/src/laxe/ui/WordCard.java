package laxe.ui;

import javax.microedition.lcdui.Graphics;


public class WordCard extends Control
{
    public String word;
    public String trans;


    protected void paint(Graphics g)
    {
        super.paint(g);

        g.setColor(borderColor);
        g.drawLine(left, top + height / 2, right, top + height / 2);

        Utils.drawStringAligned(g, word, left, width, top + height / 4);
        Utils.drawStringAligned(g, trans, left, width, bottom - height / 4);
    }


    protected void keyPressed(int key)
    {
    }

    protected void keyReleased(int key)
    {
    }
}
