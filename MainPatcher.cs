using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Nautilus;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;

namespace PDALag
{

    [BepInPlugin("mikjaw.com.pdalag", "PDA Lag", "1.0")]
    [BepInDependency(Nautilus.PluginInfo.PLUGIN_GUID, "1.0.0.32")]
    public class MainPatcher : BaseUnityPlugin
    {
        public void Start()
        {
            DoNautilusThing();
        }
        public void DoNautilusThing()
        {
            Nautilus.Crafting.RecipeData moduleRecipe = new Nautilus.Crafting.RecipeData();
            moduleRecipe.Ingredients.Add(new CraftData.Ingredient(TechType.Titanium, 1));
            PrefabInfo module_info = PrefabInfo
                .WithTechType("MyClassID", "MyDisplayName", "MyDescription", unlockAtStart: true);
            CustomPrefab module_CustomPrefab = new CustomPrefab(module_info);
            PrefabTemplate moduleTemplate = new CloneTemplate(module_info, TechType.SeamothElectricalDefense);
            module_CustomPrefab.SetGameObject(moduleTemplate);
            module_CustomPrefab
                .SetRecipe(moduleRecipe)
                .WithCraftingTime(3)
                .WithFabricatorType(CraftTree.Type.Workbench);
            module_CustomPrefab.SetPdaGroupCategory(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades);
            module_CustomPrefab
                .SetEquipment(EquipmentType.VehicleModule)
                .WithQuickSlotType(QuickSlotType.Passive);
            module_CustomPrefab.Register(); // this line causes PDA voice lag by 1.5 seconds ???????
        }
    }
}
