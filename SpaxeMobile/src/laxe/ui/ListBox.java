package laxe.ui;

import java.util.Vector;
import javax.microedition.lcdui.Graphics;


public class ListBox extends Control implements Runnable
{
    // Элементы списка
    public Vector items;

    // Индекс выбранного элемента в списке
    public int selectedIndex;
    // Выбранная позиция на экране
    public int selectedPosition;
    // Цвет подсветки
    public int selectedColor;

    // Количество вмещяемых в окно контролла элементов
    public int countVisible;
    // Индекс первого показываемого элемента в окне
    public int startIndex;

    // Коэффициент для вычисления длины полосы прокрутки
    private float k;
    // Длина полосы прокрутки
    private int scrollLen;

    // Зажатая клавиша
    private volatile int key;
    // Удерживается ли клавиша вниз или вверх
    private volatile boolean scrolling;


    public ListBox()
    {
        items = new Vector();
        selectedIndex = 0;
        selectedColor = 0xd2d2c8;
        startIndex = 0;
        countVisible = 0;
        selectedPosition = 0;

        k = 0.0f;
        scrollLen = 0;
    }


    public void addItem(String item)
    {
        items.addElement(item);
        calcScrollLength();
    }


    private void calcScrollLength()
    {
        k = (float)countVisible / (float)items.size();
        scrollLen = (int)(k*(height - 2));
        if (scrollLen == 0)
            scrollLen = 1;
    }


    public void setItems(Vector items)
    {
        this.items = items;
        calcScrollLength();
    }


    protected void paint(Graphics g)
    {
        super.paint(g);

        int padding = 2;
        int posx = left + padding;
        int posy = top + padding;
        int fonth = this.font.getHeight();

        g.setColor(borderColor);
        for (int i = 0; i < countVisible; i++)
        {
            if (i > items.size() - 1) break;

            if (startIndex + i == selectedIndex)
            {
                g.setColor(selectedColor);
                g.fillRect(posx, posy, width - 2*padding - 6, fonth);
                g.setColor(bgColor);
                g.drawString(items.elementAt(startIndex + i).toString(), posx, posy, Graphics.LEFT | Graphics.TOP);
            }

            g.setColor(fontColor);
            g.drawString(items.elementAt(startIndex + i).toString(), posx, posy, Graphics.LEFT | Graphics.TOP);
            posy += fonth;
        }

        // Рисуем полосу прокрутки
        if (countVisible < items.size())
        {
            int offset = (int)((float)(height - scrollLen - 1)*(float)startIndex/(float)(items.size() - countVisible));

            g.setColor(borderColor);
            g.drawLine(right - 6, top, right - 6, bottom);
            g.fillRect(right - 6, top + offset + 1, 6, scrollLen);
        }
    }


    protected void keyPressed(int key)
    {
        switch (key)
        {
            case Keys.KEYDOWN:
            case Keys.KEYUP:
            {
                if (!scrolling)
                {
                    scrolling = true;
                    this.key = key;
                    new Thread(this).start();
                }
                break;
            }
        }
    }


    protected void keyReleased(int key)
    {
        scrolling = false;
    }


    public void setHeight(int height)
    {
        super.setHeight(height);

        int padding = 2;
        int posy = top + padding;
        int fonth = this.font.getHeight();

        int i = 0;
        while (posy + fonth + padding <= bottom)
        {
            posy += fonth;
            i++;
        }
        countVisible = i;
    }

    
    public void run()
    {
        int delay = 150;

        switch (key)
        {
            case Keys.KEYDOWN:
            {
                while (scrolling)
                {
                    if (selectedIndex < items.size() - 1)
                    {
                        selectedIndex++;
                        if (selectedPosition == countVisible - 1)
                            startIndex++;
                        else selectedPosition++;

                        form.repaint();

                        try
                        {
                            Thread.sleep(delay);
                            if (delay > 15) delay -= 15;
                        }
                        catch (InterruptedException ex)
                        {
                            ex.printStackTrace();
                        }
                        
                    }
                }
                break;
            }
            case Keys.KEYUP:
            {
                while (scrolling)
                {
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                        if (selectedPosition == 0)
                            startIndex--;
                        else selectedPosition--;

                        form.repaint();

                        try
                        {
                            Thread.sleep(delay);
                            if (delay > 15) delay -= 15;
                        }
                        catch (InterruptedException ex)
                        {
                            ex.printStackTrace();
                        }
                    }
                }

                break;
            }
        }
    }
}