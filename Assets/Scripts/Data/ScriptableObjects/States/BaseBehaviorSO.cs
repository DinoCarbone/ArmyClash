using Core.Services.States;

namespace Data.ScriptableObjects.States
{
     /// <summary>
     /// Базовый ScriptableObject для описания поведения (создаёт конфигурационное состояние по контекстам).
     /// </summary>
     public abstract class BaseBehaviorSO : BaseBehaviorTypeSO
     {
              public abstract IState CreateConfigState(params object[] dependencies);
     }
}
