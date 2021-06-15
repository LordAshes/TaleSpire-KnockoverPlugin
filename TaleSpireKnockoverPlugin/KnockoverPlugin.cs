using UnityEngine;
using BepInEx;
using Bounce.Unmanaged;

namespace LordAshes
{
    [BepInPlugin(Guid, "Knockover Plug-In", Version)]
    [BepInDependency(RadialUI.RadialUIPlugin.Guid)]
    public class HandoutsPlugin : BaseUnityPlugin
    {
        // Plugin info
        private const string Guid = "org.lordashes.plugins.knockover";
        private const string Version = "1.0.0.0";

        // Content directory
        private string dir = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("/")) + "/TaleSpire_CustomData/";

        // Holds the creature of the radial menu
        private CreatureGuid radialCreature = CreatureGuid.Empty;

        /// <summary>
        /// Function for initializing plugin
        /// This function is called once by TaleSpire
        /// </summary>
        void Awake()
        {
            UnityEngine.Debug.Log("Lord Ashes Knockover Plugin Active.");

            Texture2D tex = new Texture2D(32, 32);
            tex.LoadImage(System.IO.File.ReadAllBytes(dir + "Images/Icons/KO.Png"));
            Sprite icon = Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));

            RadialUI.RadialUIPlugin.AddOnCharacter(Guid, new MapMenu.ItemArgs
            {
                Action = (mmi,obj)=>
                {
                    CreatureBoardAsset asset;
                    CreaturePresenter.TryGetAsset(radialCreature, out asset);
                    if(asset!=null)
                    {
                        asset.Creature.Knockdown();
                    }
                },
                Icon = icon,
                Title = "On Character",
                CloseMenuOnActivate = true
            }, Reporter);        
        }

        private bool Reporter(NGuid selected, NGuid radialMenu)
        {
            radialCreature = new CreatureGuid(radialMenu);
            return true;
        }

        /// <summary>
        /// Function for determining if view mode has been toggled and, if so, activating or deactivating Character View mode.
        /// This function is called periodically by TaleSpire.
        /// </summary>
        void Update()
        {
        }
    }
}
