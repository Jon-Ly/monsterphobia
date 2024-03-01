using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Monsterphobia
{
    public class MonsterReplacement : MonoBehaviour
    {
        private string pathToMesh = "assets/meshes/";
        private string pathToMaterial = "assets/materials/";

        public Dictionary<string, string> assets = new Dictionary<string, string>()
        {
            // Meshes
            { ScriptNames.baboonScript, "baboon_hawk.mesh" }, // Baboon Hawk +
            { ScriptNames.slimeScript, "slime.mesh" }, // Slime +
            { ScriptNames.thumperScript, "thumper.mesh" }, // Thumper +
            { ScriptNames.snareFleaScript, "snare_flea.mesh" }, // Snare Flea +
            { "CentipedeAIClinging", "snare_flea_clinging.mesh" }, // My custom key for when the flea hugs your face
            { ScriptNames.locustScript, "" }, // Roaming Locust
            { ScriptNames.manticoilScript, "" }, // Manticoil
            { ScriptNames.ghostGirlScript, "ghost_girl.mesh" }, // Ghost Girl +
            { ScriptNames.brackenScript, "bracken.mesh" }, // Bracken +
            { ScriptNames.forestKeeperScript, "forest_giant.mesh" }, // Forest Keeper +
            { ScriptNames.hoardingBugScript, "hoarding_bug.mesh" }, // Hoarding Bug +
            { ScriptNames.jesterScript, "jester.mesh" }, // Jester +
            { ScriptNames.lassoManScript, "" }, // Lasso Man
            { ScriptNames.eyelessDogScript, "eyeless_dog.mesh" }, // Eyeless Dog +
            { ScriptNames.nutcrackerScript, "nutcracker.mesh" }, // Nutcracker +
            { ScriptNames.sandSpiderScript, "" }, // Spider - Handled by the game
            { ScriptNames.sporeLizardScript, "spore_lizard.mesh" }, // Spore Lizard +
            { ScriptNames.earthLeviathanScript, "earth_leviathan.mesh" }, // Earth Leviathan +
            { ScriptNames.coilHeadScript, "coil_head.mesh" }, // Coil-Head +
            { ScriptNames.maskedScript, "masked.mesh" }, // Masked

            // Material
            { "RedCord", "red_cord.mat" }
        };

        public EnemyAI enemyAI;

        public Mesh safeMesh = null;

        public Material safeMaterial = null;

        private string enemyScriptName = "";

        private bool meshesReplaced = false;

        // Snare flea
        public Mesh safeSnareFleaMeshClinging = null;

        void Start()
        {
            enemyAI = gameObject.GetComponent<EnemyAI>();

            if (enemyAI == null)
            {
                Console.WriteLine($"Monsterphobia: Unable to find {enemyAI.GetType().Name}");
                return;
            }

            enemyScriptName = enemyAI.GetType().Name;

            SetSafeAssets();
        }

        private void Update()
        {
            bool isSafeMeshEnabled = SafeMeshEnabled();

            if (enemyAI != null && enemyAI.skinnedMeshRenderers != null && safeMesh != null && Config.enableMonsterphbia.Value && isSafeMeshEnabled)
            {
                ReplaceMeshes();
            }
            else
            {
                ResetMeshes();
            }
        }

        private bool SafeMeshEnabled()
        {
            if (enemyScriptName == ScriptNames.baboonScript)
            {
                return Config.enableBaboonText.Value;
            }
            if (enemyScriptName == ScriptNames.slimeScript)
            {
                return Config.enableSlimeText.Value;
            }
            if (enemyScriptName == ScriptNames.thumperScript)
            {
                return Config.enableThumperText.Value;
            }
            if (enemyScriptName == ScriptNames.snareFleaScript)
            {
                return Config.enableSnareFleaText.Value;
            }
            if (enemyScriptName == ScriptNames.ghostGirlScript)
            {
                return Config.enableGhostGirlText.Value;
            }
            if (enemyScriptName == ScriptNames.brackenScript)
            {
                return Config.enableBrackenText.Value;
            }
            if (enemyScriptName == ScriptNames.forestKeeperScript)
            {
                return Config.enableForestGiantText.Value;
            }
            if (enemyScriptName == ScriptNames.hoardingBugScript)
            {
                return Config.enableHoardingBugText.Value;
            }
            if (enemyScriptName == ScriptNames.jesterScript)
            {
                return Config.enableJesterText.Value;
            }
            if (enemyScriptName == ScriptNames.eyelessDogScript)
            {
                return Config.enableEyelessDogText.Value;
            }
            if (enemyScriptName == ScriptNames.nutcrackerScript)
            {
                return Config.enableNutcrackerText.Value;
            }
            if (enemyScriptName == ScriptNames.sporeLizardScript)
            {
                return Config.enableSporeLizardText.Value;
            }
            if (enemyScriptName == ScriptNames.earthLeviathanScript)
            {
                return Config.enableEarthLeviathanText.Value;
            }
            if (enemyScriptName == ScriptNames.coilHeadScript)
            {
                return Config.enableCoilHeadText.Value;
            }
            if (enemyScriptName == ScriptNames.maskedScript)
            {
                return Config.enableMaskedText.Value;
            }

            return false;
        }

        private void ReplaceMeshes()
        {
            // Disable original meshes
            for (int i = 0; i < enemyAI.skinnedMeshRenderers.Length; i++)
            {
                enemyAI.skinnedMeshRenderers[i].enabled = false;
            }

            for (int i = 0; i < enemyAI.meshRenderers.Length; i++)
            {
                // Don't disable dots for the ship map
                string materialName = enemyAI.meshRenderers[i].material.name;
                bool isEnemyPartMesh = !materialName.ToLower().Contains("mapdot") && !materialName.ToLower().Contains("scannode");

                if (isEnemyPartMesh)
                {
                    enemyAI.meshRenderers[i].enabled = false;
                }
            }

            // Replace material
            if (enemyAI.gameObject.GetComponent<MeshRenderer>() == null)
            {
                enemyAI.gameObject.AddComponent<MeshRenderer>().material = safeMaterial;
            }
            else
            {
                enemyAI.gameObject.GetComponent<MeshRenderer>().material = safeMaterial;
            }

            // Replace mesh
            if (enemyAI.gameObject.GetComponent<MeshFilter>() == null)
            {
                enemyAI.gameObject.AddComponent<MeshFilter>().mesh = safeMesh;
            }

            if (enemyScriptName == ScriptNames.snareFleaScript)
            {
                SetSnareFleaMesh();
            }
            else
            {
                enemyAI.gameObject.GetComponent<MeshFilter>().mesh = safeMesh;
            }

            meshesReplaced = true;
        }

        private void ResetMeshes()
        {
            if (meshesReplaced)
            {
                for (int i = 0; i < enemyAI.skinnedMeshRenderers.Length; i++)
                {
                    enemyAI.skinnedMeshRenderers[i].enabled = true;
                }

                for (int i = 0; i < enemyAI.meshRenderers.Length; i++)
                {
                    enemyAI.meshRenderers[i].enabled = true;
                }

                enemyAI.gameObject.GetComponent<MeshFilter>().mesh = null;
                enemyAI.gameObject.GetComponent<MeshRenderer>().material = null;
            }
        }

        private void SetSnareFleaMesh()
        {
            CentipedeAI snareFlea = enemyAI as CentipedeAI;

            if (enemyAI.gameObject.GetComponent<MeshFilter>() == null)
            {
                enemyAI.gameObject.AddComponent<MeshFilter>().mesh = safeMesh;
            }
            else if (snareFlea.clingingToPlayer)
            {
                enemyAI.gameObject.GetComponent<MeshFilter>().mesh = safeSnareFleaMeshClinging;
            }
            else
            {
                enemyAI.gameObject.GetComponent<MeshFilter>().mesh = safeMesh;
            }
        }

        private void SetSafeAssets()
        {
            string[] assetNames = Assets.Bundle.GetAllAssetNames();

            try
            {
                safeMaterial = Assets.Bundle.LoadAsset<Material>(pathToMaterial + assets["RedCord"]);
                safeMesh = Assets.Bundle.LoadAsset<Mesh>(pathToMesh + assets[enemyScriptName]);

                if (safeSnareFleaMeshClinging == null)
                {
                    safeSnareFleaMeshClinging = Assets.Bundle.LoadAsset<Mesh>(pathToMesh + assets["CentipedeAIClinging"]);
                }

                if (safeMesh == null)
                {
                    Console.WriteLine($"{enemyAI.GetType().Name} is missing safeMesh");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error when loading monster replacement meshes or material for {enemyScriptName}. {e}");
                return;
            }
        }
    }
}