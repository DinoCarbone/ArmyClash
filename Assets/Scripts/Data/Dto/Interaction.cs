using UnityEngine;

namespace Data.Dto
{
    public class DamageData : IDamageData
    {
        private readonly int damage;

        public int Damage => damage;

        public DamageData(int damage)
        {
            this.damage = damage;
        }
    }
    public class MockColladerChangedData : IColladerChangeData
    {
        public Vector3 Size { get; private set; }

        public MockColladerChangedData(Vector3 size)
        {
            Size = size;
        }
    }
    public class ShapeData : IModelChangeData, IColladerChangeData, IDamageModifierData, IHealthModifierData
    {
        private readonly GameObject prefub;
        private readonly Vector3 size;
        private readonly int health;
        private readonly int damage;

        public GameObject Prefub => prefub;
        public Vector3 Size => size;

        public int Damage => damage;

        public int Health => health;

        public ShapeData(GameObject prefub, Vector3 size, int damage, int health)
        {
            this.prefub = prefub;
            this.size = size;
            this.damage = damage;
            this.health = health;
        }
    }
    public class SizeData : IScaleChandeData, IHealthModifierData
    {
        private readonly int health;
        private readonly float scale;

        public int Health => health;
        public float Scale => scale;

        public SizeData(int health, float scale)
        {
            this.health = health;
            this.scale = scale;
        }
    }
}