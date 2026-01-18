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

    public class DamageModifierData : IDamageModifierData
    {
        private readonly int damage;

        public int Damage => damage;

        public DamageModifierData(int damage)
        {
            this.damage = damage;
        }
    }

    public class ShapeData : IModelChangeData, IColladerChangeData
    {
        private readonly GameObject prefub;
        private readonly Vector3 size;

        public GameObject Prefub => prefub;
        public Vector3 Size => size;

        public ShapeData(GameObject prefub, Vector3 size)
        {
            this.prefub = prefub;
            this.size = size;
        }
    }

    public class HealthModifierData : IHealthModifierData
    {
        private readonly int health;

        public int Health => health;

        public HealthModifierData(int health)
        {
            this.health = health;
        }
    }

    public class SpeedModifierData : ISpeedModifierData
    {
        private readonly float speed;

        public float Speed => speed;

        public SpeedModifierData(float speed)
        {
            this.speed = speed;
        }
    }

    public class AttackSpeedModifierData : IAttackSpeedModifierData
    {
        private readonly float attackSpeed;

        public float AttackSpeed => attackSpeed;

        public AttackSpeedModifierData(float attackSpeed)
        {
            this.attackSpeed = attackSpeed;
        }
    }

    public class SizeData : IScaleChandeData
    {
        private readonly float scale;

        public float Scale => scale;

        public SizeData(float scale)
        {
            this.scale = scale;
        }
    }
    
    public class MaterialData : IMaterialChangeData
    {
        private readonly Material material;

        public Material Material => material;

        public MaterialData(Material material)
        {
            this.material = material;
        }
    }
}