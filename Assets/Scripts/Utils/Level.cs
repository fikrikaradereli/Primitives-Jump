public class Level
{
    public string Name { get; private set; }
    public int EnemyNumber { get; private set; }

    public Level(string name, int enemyNumber)
    {
        Name = name;
        EnemyNumber = enemyNumber;
    }
}