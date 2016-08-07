using System;

public class Class1
{
	public Class1()
	{
        
	}

    List<int> cardsActive;

    bool knowCard;
    bool knowPrincess;

    Card card1;
    Card card2;
    Card choiceCard;

    int main()
    {
        
    }

    public int AIChooseCard()
    {
        //Princess forced moves
        if (card1.value == 8)
        {
            //Play card 2
            choiceCard = card2;
        }
        else if (card2.value == 8)
        {
            //Play card 1
            choiceCard = card1;
        }

        //Countess forced moves
        else if (card1.value == 7 && card2.value >= 5)
        {
            //Play card 1
            choiceCard = card1;
        }
        else if (card2.value == 7 && card1.value >= 5)
        {
            //Play card 2
            choiceCard = card2;
        }

        //Try to avoid playing King
        else if (card1.value == 6)
        {
            //Play card 2
            choiceCard = card2;
        }
        else if (card2.value == 6)
        {
            //Play card 1
            choiceCard = card1;
        }

        //Double card forced moves
        else if (card1.value == card2.value)
        {
            //Play card 1
            choiceCard = card1;
        }

        //Baron + Guard "forced moves"
        else if (card1.value == 3 && card2.value == 1)
        {
            //Play card 2
            choiceCard = card2;
        }
        else if (card2.value == 3 && card1.value == 1)
        {
            //Play card 1
            choiceCard = card1;
        }

        //Have a guard and know someone's card
        else if (card1.value == 1 && knowCard)
        {
            //Play card 1
            choiceCard = card1;
        }
        else if (card2.value == 1 && knowCard)
        {
            //Play card 2
            choiceCard = card2;
        }

        //Play handmaid in all cases (Possibly subject to change)
        else if (card1.value == 4)
        {
            //Play card 1
            choiceCard = card1;
        }
        else if (card2.value == 4)
        {
            //Play card 2
            choiceCard = card2;
        }

        //Play prince if know princess
        else if (card1.value == 5 && knowPrincess)
        {
            //Play card 1
            choiceCard = card1;
        }
        else if (card2.value == 5 && knowPrincess)
        {
            //Play card 2
            choiceCard = card2;
        }

        //Play baron with a card of 5 or higher
        else if (card1.value == 3 && card2.value >= 5)
        {
            //Play card 1
            choiceCard = card1;
        }
        else if (card2.value == 3 && card1.value >= 5)
        {
            //Play card 2
            choiceCard = card2;
        }

        //Play priest in remaining cases
        else if (card1.value == 2)
        {
            //Play card 1
            choiceCard = card1;
        }
        else if (card2.value == 2)
        {
            //Play card 2
            choiceCard = card2;
        }

        //Play guard in remaining cases
        else if (card1.value == 1)
        {
            //Play card 1
            choiceCard = card1;
        }
        else if (card2.value == 1)
        {
            //Play card 2
            choiceCard = card2;
        }

        else
        {
            //This shouldn't happen
            Console.WriteLine("Decision tree failed, playing card 1");
            choiceCard = card1;
        }

        return choiceCard.value;

    }

    public void AIChooseMove()
    {

    }

    public void AIPlayCard()
    {

    }
}
