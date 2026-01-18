using System.Collections.Generic;
using System.Linq;
using Core.Services;
using Core.Simulation;
using Data.Dto;
using Data.ScriptableObjects.Configs;
using Data.ScriptableObjects.Configs.Formation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Demonstration
{
    /// <summary>
    /// Данный класс представлен исключительно как тестовый.
    /// только для выбора настроек - как быстрое решение.
    /// </summary>
    public class DemonstrationMenu : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Dropdown armyADropdown;
        [SerializeField] private TMP_Dropdown armyBDropdown;
        [SerializeField] private Toggle armyARandomizeToggle;
        [SerializeField] private Toggle armyBRandomizeToggle;
        [SerializeField] private Button startButton;

        [Header("Formation Configs")]
        [SerializeField] private List<FormationConfig> formationConfigs;
        [SerializeField] private FormationConfig defaultFormation;

        [Header("Unit Configs")]
        [SerializeField] private List<ShapeConfig> shapeConfigs;
        [SerializeField] private List<SizeConfig> sizeConfigs;
        [SerializeField] private List<MaterialConfig> materialConfigs;

        [Header("General Configs")]
        [SerializeField] private List<BaseMultiConfig> generalConfigsA; // Для армии A
        [SerializeField] private List<BaseMultiConfig> generalConfigsB; // Для армии B

        [Header("Settings")]
        [SerializeField] private int armySize = 20;

        private ISimulationEditor simulationEditor;
        private IStartGameService startGameService;

        private FormationConfig selectedFormationA;
        private FormationConfig selectedFormationB;

        private void Start()
        {
            ValidateConfigs();
            InitializeUI();
            SetupEventListeners();
        }

        [Inject]
        public void Construct(ISimulationEditor simulationEditor, IStartGameService gameFlowService)
        {
            this.simulationEditor = simulationEditor;
            this.startGameService = gameFlowService;
        }

        private void ValidateConfigs()
        {
            if (formationConfigs == null || formationConfigs.Count == 0)
            {
                Debug.LogError("No formation configs assigned to DemonstrationMenu!");
            }

            if (shapeConfigs == null || shapeConfigs.Count == 0)
            {
                Debug.LogError("No shape configs assigned to DemonstrationMenu!");
            }

            if (sizeConfigs == null || sizeConfigs.Count == 0)
            {
                Debug.LogError("No size configs assigned to DemonstrationMenu!");
            }

            if (materialConfigs == null || materialConfigs.Count == 0)
            {
                Debug.LogError("No material configs assigned to DemonstrationMenu!");
            }
        }

        private void InitializeUI()
        {
            armyADropdown.ClearOptions();
            armyBDropdown.ClearOptions();

            var formationNames = formationConfigs
                .Where(f => f != null)
                .Select(f => f.name)
                .ToList();

            armyADropdown.AddOptions(formationNames);
            armyBDropdown.AddOptions(formationNames);

            int defaultIndex = formationConfigs.IndexOf(defaultFormation);
            if (defaultIndex >= 0)
            {
                armyADropdown.value = defaultIndex;
                armyBDropdown.value = defaultIndex;
                selectedFormationA = defaultFormation;
                selectedFormationB = defaultFormation;
            }
            else if (formationConfigs.Count > 0)
            {
                selectedFormationA = formationConfigs[0];
                selectedFormationB = formationConfigs[0];
            }
        }

        private void SetupEventListeners()
        {
            armyADropdown.onValueChanged.AddListener(OnArmyAFormationChanged);
            armyBDropdown.onValueChanged.AddListener(OnArmyBFormationChanged);
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnArmyAFormationChanged(int index)
        {
            if (index >= 0 && index < formationConfigs.Count)
            {
                selectedFormationA = formationConfigs[index];
            }
        }

        private void OnArmyBFormationChanged(int index)
        {
            if (index >= 0 && index < formationConfigs.Count)
            {
                selectedFormationB = formationConfigs[index];
            }
        }

        private void OnStartButtonClicked()
        {
            ArmyData armyA = CreateArmy(selectedFormationA, armyARandomizeToggle.isOn, generalConfigsA);
            ArmyData armyB = CreateArmy(selectedFormationB, armyBRandomizeToggle.isOn, generalConfigsB);

            simulationEditor.SetArmyA(armyA);
            simulationEditor.SetArmyB(armyB);

            StartSimulation();
        }

        private ArmyData CreateArmy(FormationConfig formation, bool isRandomized, List<BaseMultiConfig> generalConfigs)
        {
            if (isRandomized)
            {
                return CreateRandomizedArmy(formation, generalConfigs);
            }
            else
            {
                return CreateBalancedArmy(formation, generalConfigs);
            }
        }

        private ArmyData CreateBalancedArmy(FormationConfig formation, List<BaseMultiConfig> generalConfigs)
        {
            var armyUnits = new List<(int count, List<BaseMultiConfig> configs)>();

            var allCombinations = new List<List<BaseMultiConfig>>();

            foreach (var shape in shapeConfigs)
            {
                foreach (var size in sizeConfigs)
                {
                    foreach (var material in materialConfigs)
                    {
                        var unitConfigs = new List<BaseMultiConfig>
                        {
                            shape,
                            size,
                            material
                        };

                        if (generalConfigs != null)
                        {
                            unitConfigs.AddRange(generalConfigs);
                        }

                        allCombinations.Add(unitConfigs);
                    }
                }
            }

            if (allCombinations.Count == 0)
            {
                Debug.LogError("No valid combinations of configs!");
                return new ArmyData(formation, new List<(int, List<BaseMultiConfig>)>(),
                    generalConfigs ?? new List<BaseMultiConfig>());
            }

            // Распределяем 20 юнитов равномерно по всем комбинациям
            int baseCount = armySize / allCombinations.Count;
            int remainder = armySize % allCombinations.Count;

            for (int i = 0; i < allCombinations.Count; i++)
            {
                int count = baseCount + (i < remainder ? 1 : 0);
                if (count > 0)
                {
                    armyUnits.Add((count, allCombinations[i]));
                }
            }

            // Проверяем что получилось ровно 20 юнитов
            int totalUnits = armyUnits.Sum(u => u.count);
            if (totalUnits != armySize)
            {
                Debug.LogWarning($"Army has {totalUnits} units instead of {armySize}. Adjusting...");
                AdjustUnitCount(ref armyUnits, armySize);
            }

            return new ArmyData(formation, armyUnits, generalConfigs ?? new List<BaseMultiConfig>());
        }

        private ArmyData CreateRandomizedArmy(FormationConfig formation, List<BaseMultiConfig> generalConfigs)
        {
            var armyUnits = new List<(int count, List<BaseMultiConfig> configs)>();
            int unitsLeft = armySize;

            while (unitsLeft > 0)
            {
                int maxGroupSize = Mathf.Min(5, unitsLeft);
                int groupSize = Random.Range(1, maxGroupSize + 1);

                var shapeConfig = GetRandomConfig(shapeConfigs);
                var sizeConfig = GetRandomConfig(sizeConfigs);
                var materialConfig = GetRandomConfig(materialConfigs);

                var configs = new List<BaseMultiConfig>
            {
                shapeConfig,
                sizeConfig,
                materialConfig
            };

                if (generalConfigs != null)
                {
                    configs.AddRange(generalConfigs);
                }

                var existingUnit = armyUnits.FirstOrDefault(u =>
                    ConfigsEqual(u.configs, configs));

                if (existingUnit.configs != null)
                {
                    int index = armyUnits.IndexOf(existingUnit);
                    armyUnits[index] = (existingUnit.count + groupSize, existingUnit.configs);
                }
                else
                {
                    armyUnits.Add((groupSize, configs));
                }

                unitsLeft -= groupSize;
            }

            return new ArmyData(formation, armyUnits, generalConfigs ?? new List<BaseMultiConfig>());
        }

        private bool ConfigsEqual(List<BaseMultiConfig> configs1, List<BaseMultiConfig> configs2)
        {
            if (configs1.Count != configs2.Count) return false;

            for (int i = 0; i < configs1.Count; i++)
            {
                if (configs1[i] != configs2[i]) return false;
            }

            return true;
        }

        private T GetRandomConfig<T>(List<T> configs) where T : BaseMultiConfig
        {
            if (configs == null || configs.Count == 0)
                return null;

            return configs[Random.Range(0, configs.Count)];
        }

        private void AdjustUnitCount(ref List<(int count, List<BaseMultiConfig> configs)> armyUnits, int targetCount)
        {
            int currentCount = armyUnits.Sum(u => u.count);

            if (currentCount < targetCount)
            {
                int randomIndex = Random.Range(0, armyUnits.Count);
                var unit = armyUnits[randomIndex];
                armyUnits[randomIndex] = (unit.count + (targetCount - currentCount), unit.configs);
            }
            else if (currentCount > targetCount)
            {
                int toRemove = currentCount - targetCount;
                while (toRemove > 0)
                {
                    int randomIndex = Random.Range(0, armyUnits.Count);
                    var unit = armyUnits[randomIndex];

                    int removeFromThis = Mathf.Min(toRemove, unit.count - 1); 
                    if (removeFromThis > 0)
                    {
                        armyUnits[randomIndex] = (unit.count - removeFromThis, unit.configs);
                        toRemove -= removeFromThis;
                    }
                }
            }
        }

        private void StartSimulation()
        {
            startGameService.StartGame();
        }

        private void OnDestroy()
        {
            if (armyADropdown != null)
                armyADropdown.onValueChanged.RemoveListener(OnArmyAFormationChanged);

            if (armyBDropdown != null)
                armyBDropdown.onValueChanged.RemoveListener(OnArmyBFormationChanged);

            if (startButton != null)
                startButton.onClick.RemoveListener(OnStartButtonClicked);
        }
    }
}