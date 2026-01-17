namespace Data.Dto
{
    public class DamageData : IDamageEvent
    {
        private readonly int damage;

        public int Damage => damage;

        public DamageData(int damage)
        {
            this.damage = damage;
        }
    }
}