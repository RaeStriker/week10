using System;

namespace week10
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(12);
            Enemy enemy = new Enemy(10);
            bool win = false;

            player.setEnemy(enemy);
            enemy.setEnemy(player);

            while (checkAlive(player, enemy))
            {
                if (checkAlive(player, enemy))
                    player.takeAction();
                if (checkAlive(player, enemy))
                    enemy.takeAction();
                if (player.dead)
                    //do nothing
                    win = false;
                if (enemy.dead)
                    win = true;
            }

            if (win)
                Console.WriteLine("The enemy has died - you win!");
            else
                Console.WriteLine("The player has died - you lose!");
        }

        static public bool checkAlive(Fighter sock, Fighter sick)
        {
            if (sock.dead)
                return false;
            else if (sick.dead)
                return false;
            else
                return true;
        }
    }
    public class Fighter
    {
        // Properties
        protected int maxHealth;
        protected int currentHealth;
        protected int damage;
        protected int heal;
        public bool dead;
        protected Fighter opponent;

        // Mutators?
        public Fighter() { /*???*/ }

        public void takeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
                death();
        }

        public void death() { dead = true; }
        public virtual void setEnemy(Fighter fight) { opponent = fight; }
        public virtual void takeAction() { /*???*/ }
        public int getCurrentHealth() { return currentHealth; }
    }

    class Enemy : Fighter
    {
        public Enemy(int mHealth)
        {

            maxHealth = mHealth;
            currentHealth = maxHealth;
            dead = false;
            damage = 5;
        }
        public override void takeAction()
        {
            Console.WriteLine("== Enemy Turn ==");
            Console.WriteLine("Player's Health: " + opponent.getCurrentHealth());
            Console.WriteLine("Enemy's Health: " + getCurrentHealth());
            Console.WriteLine("Attacking player for " + damage + " points of damage");
            opponent.takeDamage(5);
        }

        public void setEnemy(Player fight)
        {
            opponent = fight;
        }
    }

    class Player : Fighter
    {
        int choice = 0;
        int potions = 3;
        public Player(int mHealth)
        {

            maxHealth = mHealth;
            currentHealth = maxHealth;
            dead = false;
            damage = 5;
            heal = 7;
        }
        
        public override void takeAction()
        {
            Console.WriteLine("== Player Turn ==");
            Console.WriteLine("Player's Health: " + getCurrentHealth());
            Console.WriteLine("Enemy's Health: " + opponent.getCurrentHealth());
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - Fight");
            Console.WriteLine("2 - Drink Potion (" + potions + " remaining)");

            choice = Int32.Parse(Console.ReadLine());

            if (choice == 1)
                attackEnemy();
            else if (potions > 0 && choice == 2)
                drinkPotion();
            else
                Console.WriteLine("Whoops! No potions left!");

        }
        public void setEnemy(Enemy fight)
        {
            opponent = fight;
        }

        public void attackEnemy() { opponent.takeDamage(damage); Console.WriteLine("Attacking enemy for " + damage + " pointrs of damage!"); }
        public void drinkPotion()
        {
            currentHealth += heal;
            potions--;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }
}
