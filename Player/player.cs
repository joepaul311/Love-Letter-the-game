using System;

public class Player
{
    public int[,] playerData;
    public Card m_card1;
    public Card m_card2;
    public Game* m_game;
    public int num = 0;
    public Card ChoiceCard;
    public int numplayers = m_game->numplayers;
    public bool m_KnowCard;
    public int PlayerNumber;



    public Player(int PlayerNumberHold )
    {

        PlayerNumber = PlayerNumberHold;
        this.playerData = new int[3, 6];
        //Card m_card1;
        //Card m_card2;
        //Game* m_game;
        //int num = 0;
        //Card ChoiceCard;
        cardType = ChoiceCard->CardValue;
        int numplayers = m_game->numplayers;

        //(player)(0 In game)(1 Played Count)(2 Card in hand )(3 not in hand)    
    }

    //========================================
    void UseCard(int cardType)
    {




        if (cardType == 1)//GAURD
        {
            Random random = new Random();
            int rdm = random.Next(0, this.numplayers);

            if (this.m_KnowCard)
            {
                for (int i = 0; i < this.numplayers; i++)
                {
                    if (this.playerData[i, 2] > 0 && ingame(i))
                    {
                        PlayCard(i, this.playerdata[i, 2]);
                        this.m_KnowCard = true;
                        break; 
                    }
                }
            }

            else
            {
                while (ingame(this.numplayers % rdm) != true)
                {
                    Random random = new Random();
                    int rdm = random.Next(0, this.numplayers);
                }

                num = PlayCard(this.numplayers % rdm, m_game->StillIn[Len(StillIn) % rdm]);//more random 


                this.m_KnowCard = true;
            }
        }
//----------------------------------------------------------------------------------
        if (cardType == 2)//PRIEST 
        {
            Random random = new Random();
            int rdm = random.Next(0, numplayers);

            while (ingame(this.numplayers % rdm) != true)
            {
                Random random = new Random();
                int rdm = random.Next(0, this.numplayers);
            }
            int rdmPlayer = this.numplayers % rdm;
            num = PlayCard(rdmPlayer);
            this.playerData[rdmPlayer, 2] = num;
            //edit in cards

            
        }
//----------------------------------------------------------------------------------

        if (cardType == 3)//BARON 
        {
            if(this.m_card2->CardValue == 8 || this.m_card2->CardValue < 3)
            {
                for ( int i = 0; i < this.numplayers; i ++)
                {
                        if(playerData[i,1] == 0 && ingame(i))
                    {
                        PlayCard(i);
                        break; 
                    }
                }
            }

            else if(this.m_KnowCard){
                    for( int i = 0; i < this.numplayers; i ++)
                {
                    if ( this.playerData[i,2] < this.m_card2->CardValue && ingame(i))
                    {
                        PlayCard(i);
                        break; 
                    } 
                }
            }
        }

//----------------------------------------------------------------------------------

        if (cardType == 4)//HMAID 
        {
            PlayCard();
        }

//------------------------------------------------------------------------------

        if (cardType == 5)//PRINCE 
        {
            Random random = new Random();
            int rdm = random.Next(0, numplayers);
            int rdmPlayer = this.numplayers % rdm;
            int holdCard = this.m_card2->CardValue;
            num = PlayCard(rdmPlayer);
            //Some pointer magic may be needed in here 
            this.playerData[rdmPlayer, 2] = holdCard;
            this.m_KnowCard = true; 
            //Prince
        }
//------------------------------------------------------------------------------

        if (cardType == 6)//KING
        {
            Random random = new Random();
            int rdm = random.Next(0, numplayers);
            int rdmPlayer = this.numplayers % rdm;
            num = PlayCard(rdmPlayer);
            //King 
        }

//-------------------------------------------------------------------------------
        if (cardType == 7)//COUNTESS 

        {  //Countess 
            PlayCard();
        }
//--------------------------------------------------------------------------------
        if (cardType == 8)//PRINCESS 
        {
            //Edge case 
            PlayCard();
        }


    }
    //========================================================
    int PlayCard(int player = 0, int value = 0)
    {
        return (this.ChoiceCard->play((player + PlayerNumber + 1  ) % 3 , value)); //This may throw an error 
    }

    //Update credentials =================================================
    void ShowCard(int CardNumber, int playerNumber)
    {

        if (this.KnowCard && this.playerData[playerNumber, 2] == CardNumber)
        {
            this.playerData[playerNumber, 2] == CardNumber;
            this.m_KnowCard = false;



        }
        if (CardNumber == 7)
        {
            this.playerData[playerNumber, 1] = 1; 
        }
    }



    bool ingame(int playercount)
    {       
         if (playerData[playercount,0] = 1)
        {
            return (true);

            
        }
        else
        {
            return (false); 
        }
    }
}   
