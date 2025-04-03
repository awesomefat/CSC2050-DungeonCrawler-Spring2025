using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Fight
{
    private Inhabitant attacker;
    private Inhabitant defender;

    private Monster theMonster;

    private bool fightOver = false;

    public Fight(Monster m)
    {
        this.theMonster = m;

        //initially determine who goes first
        int roll = Random.Range(0, 20) + 1;
        if (roll <= 10)
        {
            Debug.Log("Monster goes first");
            this.attacker = m;
            this.defender = Core.thePlayer;
        }
        else
        {
            Debug.Log("Player goes first");
            this.attacker = Core.thePlayer;
            this.defender = m;
        }

    }

    public bool isPlayerTurn()
    {
        return this.attacker is Player;
    }


    public bool isFightOver()
    {
        return this.fightOver;
    }

    public void takeASwing(GameObject playerGameObject, GameObject monsterGameObject)
    {
        int attackRoll = Random.Range(0, 20) + 1;
        Debug.Log("Attack Roll: " + attackRoll);
        Debug.Log("Defender AC: " + this.defender.getAC());

        if(attackRoll >= this.defender.getAC())
        {
            //attacker hits the defender
            int damage = Random.Range(1, 6); //1 to 5 damage
            this.defender.takeDamage(damage);

            if(this.defender.isDead())
            {
                this.fightOver = true;
                Debug.Log(this.attacker.getName() + " killed " + this.defender.getName());
                if(this.defender is Player)
                {
                    //player died
                    Debug.Log("Player died");
                    //end the game
                    playerGameObject.SetActive(false); //hide the player
                }
                else
                {
                    //monster died
                    Debug.Log("Monster died");
                    //remove the monster from the scene
                    GameObject.Destroy(monsterGameObject); //remove the monster from the scene
                }
            }
        }
        else
        {
            Debug.Log(this.attacker.getName() + " missed " + this.defender.getName());
        }

        Inhabitant temp = this.attacker;
        this.attacker = this.defender;
        this.defender = temp;
    }

    public void startFight(GameObject playerGameObject, GameObject monsterGameObject)
    {
        //should have the attacker and defender fight each until one of them dies.
        //the attacker and defender should alternate between each fight round and
        //the one who goes first was determined in the constructor.
        while(true)
        {
            int attackRoll = Random.Range(0, 20) + 1;
            if(attackRoll >= this.defender.getAC())
            {
                //attacker hits the defender
                int damage = Random.Range(1, 6); //1 to 5 damage
                this.defender.takeDamage(damage);

                if(this.defender.isDead())
                {
                    Debug.Log(this.attacker.getName() + " killed " + this.defender.getName());
                    if(this.defender is Player)
                    {
                        //player died
                        Debug.Log("Player died");
                        //end the game
                        playerGameObject.SetActive(false); //hide the player
                    }
                    else
                    {
                        //monster died
                        Debug.Log("Monster died");
                        //remove the monster from the scene
                        GameObject.Destroy(monsterGameObject); //remove the monster from the scene
                    }
                    break; //fight is over
                }
            }
            else
            {
                Debug.Log(this.attacker.getName() + " missed " + this.defender.getName());
            }
            Inhabitant temp = this.attacker;
            this.attacker = this.defender;
            this.defender = temp;
        }
    }
}
