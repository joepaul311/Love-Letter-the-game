using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoveLetter
{
    public partial class Form1 : Form
    {
        Game g;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void printDeck()
        {
            String s = "";
            List<Card> l = g.deck.ToList();
            for (int i = 0; i < l.Count; i++) { s += l[i] + "\n"; }
            //while (g.deck.Count() != 0) { s += g.deck.Pop()  + "\n"; }
            //label9.Text = g.deck.ToString();
            label9.Text = s + " /n " + g.deck.Count();
        }
        private void printHands()
        {
            //String s = myObj == null ? "" : myObj.ToString();

            label1.Text = g.players[0].card1 == null ? "empty" : g.players[0].card1.ToString();
            label2.Text = g.players[0].card2 == null ? "empty" : g.players[0].card2.ToString();

            label3.Text = g.players[1].card1 == null ? "e" : g.players[1].card1.ToString();
            label4.Text = g.players[1].card2 == null ? "e" : g.players[1].card2.ToString();

            label5.Text = g.players[2].card1 == null ? "e" : g.players[2].card1.ToString();
            label6.Text = g.players[2].card2 == null ? "e" : g.players[2].card2.ToString();

            label7.Text = g.players[3].card1 == null ? "e" : g.players[3].card1.ToString();
            label8.Text = g.players[3].card2 == null ? "e" : g.players[3].card2.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            g = new Game();
            g.dealCards();
            printDeck();
            printHands();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //g.dealCards();
            g.drawCard(); // doPlayerTurn();

            printDeck();
            printHands();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int numOut = 0;
            for (int i = 0; i < g.numPlayers; i++) { if (g.players[i].lost == true) { numOut++; } }
            if (numOut >= 3) {
                for (int i = 0; i < g.numPlayers; i++) { if (g.players[i].lost == false) { g.players[i].score++; } }
                
                g.resetHands(); }
            g.doPlayerTurn();
            printDeck();
            printHands();
            
        }
    }

    //1 - Guard - Guess non-guard
    //2 - Priest - look at card
    //3 - Baron - 
    //4 - Handmaiden 
    //5 - Prince
    //6 - King
    //7 - Countess
    //8 - Princess

    public class Card
    {
        public int value;
        public Game g;
        public int[] parameters;
        public Card() { }
        public Card(int val, Game g2) { value = val; g = g2; }
        virtual public void act() { }
        public String numToCard(int num)
        {
            if (num == 1) { return "Guard"; }
            if (num == 2) { return "Priest"; }
            if (num == 3) { return "Baron"; }
            if (num == 4) { return "Handmaiden"; }
            if (num == 5) { return "Prince"; }
            if (num == 6) { return "King"; }
            if (num == 7) { return "Countess"; }
            else { return "Princess"; }
        }
    }
    //param 0 - player target
    //param 1 - guess
    public class Guard : Card
    {
        public Guard(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            g.setToCard1();
            String output = "Player " + g.playersTurn + " guessed that Player " + parameters[0] + " has a " + numToCard(parameters[1]) + ".";
            if (parameters[0] != g.playersTurn && g.players[parameters[0]].card1 != null && g.players[parameters[0]].card1.value == parameters[1])
            {
                g.players[parameters[0]].card1 = null;
                g.players[parameters[0]].card2 = null;
                g.players[parameters[0]].lost = true;
                output += " Player " + g.playersTurn + " was correct! Player " + parameters[0] + " is eliminated.";

            }
            else if (parameters[0] != g.playersTurn && g.players[parameters[0]].card2 != null && g.players[parameters[0]].card2.value == parameters[1])
            {
                g.players[parameters[0]].lost = true;
                g.players[parameters[0]].card1 = null;
                g.players[parameters[0]].card2 = null;
                output += " Player " + g.playersTurn + " was correct! Player " + parameters[0] + " is eliminated.";
            }
            else if (parameters[0] == g.playersTurn) { output = "Player " + g.playersTurn + " threw the guard away."; }
            else
            {
                output += " Player " + g.playersTurn + " was wrong with the Guard guess against Player " + parameters[0] + "!";
            }
            Console.WriteLine(output);
            if (g.players[g.playersTurn].card1 != null && g.players[g.playersTurn].card1.value == 1) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
        }
    }

    //param 0 - target player
    public class Priest : Card
    {
        public Priest(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            g.setToCard1();
            if (parameters[0] != g.playersTurn && g.players[parameters[0]].card1 != null)
            {
                Console.WriteLine("Player " + g.playersTurn + " priested Player " + parameters[0] + ". Player " + parameters[0] + "'s card: " + "" + g.players[parameters[0]].card1.value);
                //    if (g.players[parameters[0]].card1.value == parameters[1] || g.players[parameters[0]].card2.value == parameters[1])
                //    {
                //        g.players[parameters[0]].lost = true;
                //    }
            }
            if (g.players[g.playersTurn].card1 != null && g.players[g.playersTurn].card1.value == 2) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
        }
    }
    //param 0 - target player 
    public class Baron : Card
    {
        public Baron(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            if (g.players[g.playersTurn].card1.value == 3) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
            g.setToCard1();

            int maxval;
            String output = "Player " + g.playersTurn + " played a Baron against Player " + parameters[0] + ".";
            if (g.players[g.playersTurn].card1 != null) { maxval = g.players[g.playersTurn].card1.value; }
            else { maxval = g.players[g.playersTurn].card2.value; }
            //Console.WriteLine(maxval + " - " + g.players[parameters[0]].card1.value);
            Console.WriteLine(g.players[parameters[0]].ToString());
            if (g.playersTurn == parameters[0]) { output = ("Baron was thrown away."); }
            else if (g.players[parameters[0]].card1 != null && g.players[parameters[0]].card1.value == maxval)
            {
                output += " It was a tie!";
            }
            else if (g.players[parameters[0]].card1 != null && g.players[parameters[0]].card1.value > maxval)
            {
                output += " Player " + parameters[0] + " beat Player " + g.playersTurn + "'s " + numToCard(maxval) + " with a " + numToCard(g.players[parameters[0]].card1.value) + ".";

                g.players[g.playersTurn].lost = true;
                g.players[g.playersTurn].card1 = null;
                g.players[g.playersTurn].card2 = null;
            }
            else if (g.players[parameters[0]].card1 != null && g.players[parameters[0]].card1.value < maxval)
            {
                output += " Player " + g.playersTurn + " beat Player " + parameters[0] + "'s " + numToCard(g.players[parameters[0]].card1.value) + " with a " + numToCard(maxval);

                g.players[parameters[0]].lost = true;
                g.players[parameters[0]].card1 = null;
                g.players[parameters[0]].card2 = null;
            }
            else
            {
                Console.WriteLine("NOT LESS GREATER OR EQUAL TO!!!!");
            }
            Console.WriteLine(output);

        }
    }
    public class Handmaiden : Card
    {
        public Handmaiden(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            g.setToCard1();
            g.players[g.playersTurn].handmaided = true;
            //    if (g.players[parameters[0]].card1.value == parameters[1] || g.players[parameters[0]].card2.value == parameters[1])
            //    {
            //        g.players[parameters[0]].lost = true;
            //    }
            if (g.players[g.playersTurn].card1 != null && g.players[g.playersTurn].card1.value == 4) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
        }
    }
    public class Prince : Card
    {
        public Prince(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            g.setToCard1();
            if (g.players[parameters[0]].card1.value == 8) { g.players[parameters[0]].card1 = null; g.players[parameters[0]].lost = true; }
            else if (g.players[parameters[0]].card2 != null && g.players[parameters[0]].card2.value == 8) { g.players[parameters[0]].card2 = null; g.players[parameters[0]].lost = true; }
            else
            {
                if (parameters[0] == g.playersTurn)
                {
                    g.players[parameters[0]].card1 = null;
                    g.players[parameters[0]].card2 = null;
                    g.drawCard(g.players[parameters[0]]);
                }
                else
                {
                    g.players[parameters[0]].card1 = null;
                    g.drawCard(g.players[parameters[0]]);
                }
            }
            //if (g.players[parameters[0]].card1.value == parameters[1] || g.players[parameters[0]].card2.value == parameters[1])
            //{
            //    g.players[parameters[0]].lost = true;
            //}
            if (g.players[g.playersTurn].card1 != null && g.players[g.playersTurn].card1.value == 5) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
        }
    }
    public class King : Card
    {
        public King(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            g.setToCard1();

            if (g.players[g.playersTurn].card1 != null && g.players[g.playersTurn].card1.value == 6) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
            g.setToCard1();
            Card temp = g.players[g.playersTurn].card1;
            g.players[g.playersTurn].card1 = g.players[parameters[0]].card1;
            g.players[parameters[0]].card1 = temp;
            Console.WriteLine("Player " + g.players[g.playersTurn] + " swapped cards with Player " + parameters[0]);
        }
    }
    public class Countess : Card
    {
        public Countess(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            g.setToCard1();
            //    if (g.players[parameters[0]].card1.value == parameters[1] || g.players[parameters[0]].card2.value == parameters[1])
            //    {
            //        g.players[parameters[0]].lost = true;
            //    }
            if (g.players[g.playersTurn].card1 != null && g.players[g.playersTurn].card1.value == 7) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
            Console.WriteLine("Player " + g.players[g.playersTurn] + " dropped the Countess.");
        }
    }
    public class Princess : Card
    {
        public Princess(int val, Game g2) { value = val; g = g2; }

        override public void act()
        {
            g.setToCard1();
            //  if (g.players[parameters[0]].card1.value == parameters[1] || g.players[parameters[0]].card2.value == parameters[1])
            //  {
            //      g.players[parameters[0]].lost = true;
            //  }
            if (g.players[g.playersTurn].card1 != null && g.players[g.playersTurn].card1.value == 8) { g.players[g.playersTurn].card1 = null; }
            else { g.players[g.playersTurn].card2 = null; }
            g.players[g.playersTurn].card1 = null;
            g.players[g.playersTurn].card2 = null;
            g.players[g.playersTurn].lost = true;
            Console.WriteLine("Player " + g.playersTurn + " was eliminated for dropping the Princess.");
        }
    }
    public class Player
    {
        public Game g;

        public List<int> unPlayed = new List<int>();

        public int[,] playerData;

        //public int num = 0;
        public Card choiceCard;
        //public int numplayers = 4;
        //public bool m_KnowCard;
        //public int PlayerNumber;

        public bool lost = false;
        public bool handmaided = false;
        public Card card1;
        public bool knowCard = false;
        public int[] knownCard;
        public int[] knownPrincess;
        public bool knowPrincess = false;
        public Card card2;

        List<CardInfo> cardKnowledge;

        public int score;
        struct CardInfo
        {
            int player;
            int value;
        }
        public Player(Game g2)
        {
            g = g2;
            cardKnowledge = new List<CardInfo>();
            //PlayerNumber = 4;
            //playerData = new int[3, 6];
            //Card m_card1;
            //Card m_card2;
            //Game* m_game;
            //int num = 0;
            //Card ChoiceCard;
            //cardType = choiceCard.value;
            //int numplayers = 4;
        }
        public void addCard(Card card)
        {
            if (card1 == null) { card1 = card; }
            else { card2 = card; }


        }
        public int findCard()
        {

            bool knowCard = false;
            //Princess forced moves
            if (card1.value == 8)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }
            else if (card2.value == 8)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }

            //Countess forced moves
            else if (card1.value == 7 && card2.value >= 5)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }
            else if (card2.value == 7 && card1.value >= 5)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }

            //Try to avoid playing King
            else if (card1.value == 6)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }
            else if (card2.value == 6)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }

            //Double card forced moves
            else if (card1.value == card2.value)
            {
                //Play card 1
                choiceCard = card2;
                return 2;
            }

            //Baron + Guard "forced moves"
            else if (card1.value == 3 && card2.value == 1)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }
            else if (card2.value == 3 && card1.value == 1)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }

            //Have a guard and know someone's card
            else if (card1.value == 1 && knowCard)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }
            else if (card2.value == 1 && knowCard)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }

            //Play handmaid in all cases (Possibly subject to change)
            else if (card1.value == 4)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }
            else if (card2.value == 4)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }

            //Play prince if know princess
            else if (card1.value == 5 && knowPrincess)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }
            else if (card2.value == 5 && knowPrincess)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }

            //Play baron with a card of 5 or higher
            else if (card1.value == 3 && card2.value >= 5)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }
            else if (card2.value == 3 && card1.value >= 5)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }

            //Play priest in remaining cases
            else if (card1.value == 2)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }
            else if (card2.value == 2)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }

            //Play guard in remaining cases
            else if (card1.value == 1)
            {
                //Play card 1
                choiceCard = card1;
                return 1;
            }
            else if (card2.value == 1)
            {
                //Play card 2
                choiceCard = card2;
                return 2;
            }

            else
            {
                //This shouldn't happen
                Console.WriteLine("Decision tree failed, playing card 1");
                choiceCard = card1;
                return 1;
            }

        }
        public int numInstances(List<int> lis, int num)
        {
            int count = 0;
            foreach (int c in lis)
            {
                if (c == num) { count++; }
            }
            return count;
        }
        bool ingame(int playercount)
        {
            if (g.players[playercount].lost == false) {
                return (true);


            }
            else
            {
                return (false);
            }
        }
        public void playCard()
        {
            List<Card> iterate = g.deck.ToList();

            for (int i = 0; i < g.deck.Count(); i++) { unPlayed.Add(iterate[i].value); }
            for (int i = 0; i < g.numPlayers; i++)
            {
                if (g.playersTurn != i && g.players[i].lost == false) { unPlayed.Add(g.players[i].card1.value); }
            }
            double[] probability = new double[8];
            for (int i = 0; i < 8; i++) { Console.WriteLine(numInstances(unPlayed, i + 1) + " ____"); probability[i] = numInstances(unPlayed, i + 1) / unPlayed.Count; Console.WriteLine(probability[i]); }

            int[] param = new int[2];
            handmaided = false;
            //drawCard();
            //g.players[g.playersTurn].playCard();
            int selectedCard = findCard();

            int cardValue = 0;
            if (1 == selectedCard)
            {
                cardValue = g.players[g.playersTurn].card1.value;
            }
            else
            {
                cardValue = g.players[g.playersTurn].card2.value;
            }
            switch (cardValue)
            {
                case 1:
                    double probabilityMax = -1;
                    int index = -1;
                    for (int i = 0; i < 8; i++) 
                    {
                        Console.WriteLine(probability[i]);
                        if (probability[i] > probabilityMax) { index = i; }
                    }
                    param[1] = new Random().Next(2,9); //index + 1;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;

            }


            param[0] = (g.playersTurn + 1) % 4;
            bool targSelf = true;
            for (int i = 0; i < g.numPlayers; i++)
            {
                if (i != g.playersTurn)
                {
                    if (g.players[i].handmaided == true || g.players[i].lost == true) { }
                    else { targSelf = false; }
                }
            }


            if (targSelf) { param[0] = g.playersTurn; }
            else
            {
                while ((g.players[param[0]].lost == true && param[0] != g.playersTurn) || (g.players[param[0]].handmaided == true)) { param[0] = (param[0] + 1) % 4; }
            }
            //param[1] = new Random().Next(1,9);

            if (selectedCard == 1)
            {
                card1.parameters = param;
                card1.act();
            }
            else
            {
                card2.parameters = param;
                card2.act();
            }
            Console.WriteLine(card1 + " - " + card2);

        }
    }
    public class Game
    {
        public Random r = new Random();
        public int numPlayers = 4;
        public Stack<Card> deck;
        public List<Player> players;
        public int playersTurn = 0;
        public Game() { resetGame(); }
        //reset whole game
        public void resetGame()
        {
            //for (int i = 0; i < numPlayers; i++) { players[i].score = 0; }
            resetHands();
            players = new List<Player>();
            for (int i = 0; i < numPlayers; i++) { players.Add(new Player(this)); }
        }
        //new round
        public void resetHands()
        {
            deck = new Stack<Card>();

            deck.Push(new Guard(1, this));
            deck.Push(new Guard(1, this));
            deck.Push(new Guard(1, this));
            deck.Push(new Guard(1, this));
            deck.Push(new Guard(1, this));

            deck.Push(new Priest(2, this));
            deck.Push(new Priest(2, this));

            deck.Push(new Baron(3, this));
            deck.Push(new Baron(3, this));

            deck.Push(new Handmaiden(4, this));
            deck.Push(new Handmaiden(4, this));

            deck.Push(new Prince(5, this));
            deck.Push(new Prince(5, this));

            deck.Push(new King(6, this));
            deck.Push(new Countess(7, this));
            deck.Push(new Princess(8, this));

            List<Card> shuffle = deck.ToList();
            r = new Random();
            for (int n = shuffle.Count - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                Card temp = shuffle[n];
                shuffle[n] = shuffle[k];
                shuffle[k] = temp;
            }
            for (int i = 0; i < shuffle.Count; i++)
            {
                Console.WriteLine(shuffle[i]);
            }
            Console.WriteLine(shuffle);
            //while (deck != null) { deck.Pop(); }
            deck.Clear();
            for (int i = 0; i < shuffle.Count; i++)
            {
                Console.WriteLine(shuffle[i] + " _ ");
                deck.Push(shuffle[i]);
            }


        }
        public void dealCards()
        {
            Console.WriteLine(deck.Count());
            for (int i = 0; i < numPlayers; i++)
            {
                players[i].card1 = null;
                players[i].card2 = null;
                players[i].addCard(deck.Pop());
            }
        }
        public void drawCard(Player p)
        {
            if (deck.Count != 0)
            {
                p.addCard(deck.Pop());
            }
        }
        public void drawCard()
        {
            if (deck.Count == 0) {  }
            else
            {
                players[playersTurn].addCard(deck.Pop());
            }
        }
        public void doPlayerTurn()
        {
            //drawCard();
            players[playersTurn].playCard();

            playersTurn++;
            playersTurn %= numPlayers;
            setToCard1();
            while (players[playersTurn].lost == true) { playersTurn++; playersTurn %= numPlayers; }

        }
        public void setToCard1()
        {
            for (int i = 0; i < numPlayers; i++)
            {
                if (players[i].card1 == null)
                {
                    players[i].card1 = players[i].card2; players[i].card2 = null;
                }
            }
        }

    }
}
