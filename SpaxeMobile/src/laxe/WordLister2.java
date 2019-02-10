package laxe;

import java.io.IOException;
import javax.microedition.lcdui.Graphics;
import laxe.ui.Form;
import laxe.ui.ListBox;
import laxe.ui.TextBox;
import laxe.ui.WordCard;


public class WordLister2 extends Form
{
    private DictManager dictManager;

    private ListBox listBoxDictionary;
    private TextBox textBoxSearch;
    private WordCard wordCard;

    private boolean mode;


    public WordLister2() throws IOException
    {
        InitializeControls();

        dictManager = new DictManager();
        dictManager.open("/dict.txt");
        listBoxDictionary.setItems(dictManager.dict);

        mode = false;
    }


    protected void keyPressed(int keyCode)
    {
        super.keyPressed(keyCode);
        
        int key = laxe.ui.Keys.translateKey(keyCode);

        if (key == laxe.ui.Keys.KEYDOWN || key == laxe.ui.Keys.KEYUP)
        {
            DictArticle art = (DictArticle)listBoxDictionary.items.elementAt(listBoxDictionary.selectedIndex);
            wordCard.word = art.word;
            wordCard.trans = art.trans;
            if (mode)
                this.repaint();
        }
    }
        
    


//    protected void paint(Graphics g)
//    {
//        super.paint(g);
//    }


    protected void btnRightPressed()
    {
    }


    protected void btnLeftPressed()
    {
    }


    protected void btnCenterPressed()
    {
        if (mode)
        {
            mode = false;
            listBoxDictionary.visible = true;
            textBoxSearch.visible = true;
            wordCard.visible = false;
        }
        else
        {
            mode = true;
            listBoxDictionary.visible = false;
            textBoxSearch.visible = false;
            wordCard.visible = true;
        }
        this.repaint();
    }


    private void InitializeControls()
    {
        this.btnLeft = "фильтр";
        this.btnRight = "назад";
        this.btnCenter = "вид";
        this.bgColor = Props.lightColor;
        this.captionColor = Props.darkColor;
        this.captionVisible = false;

        textBoxSearch = new TextBox();
        this.addControl(textBoxSearch);
        textBoxSearch.setLeft(4);
        textBoxSearch.setTop(4);
        textBoxSearch.setWidth(this.width - 9);
        textBoxSearch.setHeight(font.getHeight() + 3);
        textBoxSearch.bgColor = 0xFFFFFF;

        listBoxDictionary = new ListBox();
        this.addControl(listBoxDictionary);
        listBoxDictionary.setLeft(4);
        listBoxDictionary.setTop(textBoxSearch.bottom + 4);
        listBoxDictionary.setWidth(this.width - 9);
        listBoxDictionary.setHeight(270);

        textBoxSearch.text = "pensar";

        wordCard = new WordCard();
        this.addControl(wordCard);
        wordCard.setLeft(10);
        wordCard.setTop(50);
        wordCard.setWidth(200);
        wordCard.setHeight(200);
        wordCard.visible = false;
    }
}