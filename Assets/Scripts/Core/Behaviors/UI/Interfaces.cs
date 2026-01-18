namespace Core.Behaviors.UI
{
    public interface IValueDisplay
    {
        /// <summary>Устанавливает максимальное значение для отображения (например, максимальное здоровье).</summary>
        void SetMaxValue(int value);

        /// <summary>Обновляет отображаемое значение.</summary>
        void DisplayValue(int value);
    }
}