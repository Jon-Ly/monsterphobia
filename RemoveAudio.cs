using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Monsterphobia
{
    public class RemoveAudio : MonoBehaviour
    {
        private string enemyScriptName = "";

        public EnemyAI enemyAI;

        void Start()
        {
            enemyAI = gameObject.GetComponent<EnemyAI>();

            if (enemyAI == null)
            {
                Console.WriteLine($"Monsterphobia: Unable to find {enemyAI.GetType().Name}");
                return;
            }

            enemyScriptName = enemyAI.GetType().Name;

            RemoveAudioSources();
        }

        private void RemoveAudioSources()
        {
            if (enemyScriptName == ScriptNames.baboonScript && Config.disableBaboonSounds.Value)
            {
                BaboonBirdAI enemy = enemyAI as BaboonBirdAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.aggressionAudio.volume = 0;
                enemy.cawLaughSFX = [];
                enemy.cawScreamSFX = [];
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.slimeScript && Config.disableSlimeSounds.Value)
            {
                BlobAI enemy = enemyAI as BlobAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.movableAudioSource.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.thumperScript && Config.disableThumperSounds.Value)
            {
                CrawlerAI enemy = enemyAI as CrawlerAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.snareFleaScript &&  Config.disableSnareFleaSounds.Value)
            {
                CentipedeAI enemy = enemyAI as CentipedeAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.clingingToPlayer2DAudio.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.ghostGirlScript && Config.disableGhostGirlSounds.Value)
            {
                DressGirlAI enemy = enemyAI as DressGirlAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.heartbeatMusic.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.brackenScript && Config.disableBrackenSounds.Value)
            {
                FlowermanAI enemy = enemyAI as FlowermanAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.creatureAngerVoice.volume = 0;
                enemy.crackNeckAudio.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.forestKeeperScript && Config.disableForestGiantSounds.Value)
            {
                ForestGiantAI enemy = enemyAI as ForestGiantAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.farWideSFX.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.hoardingBugScript && Config.disableHoardingBugSounds.Value)
            {
                HoarderBugAI enemy = enemyAI as HoarderBugAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.jesterScript && Config.disableJesterSounds.Value)
            {
                JesterAI enemy = enemyAI as JesterAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.farAudio.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.eyelessDogScript && Config.disableEyelessDogSounds.Value)
            {
                MouthDogAI enemy = enemyAI as MouthDogAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.nutcrackerScript && Config.disableNutcrackerSounds.Value)
            {
                NutcrackerEnemyAI enemy = enemyAI as NutcrackerEnemyAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.torsoTurnAudio.volume = 0;
                enemy.longRangeAudio.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.sandSpiderScript && Config.disableSpiderSounds.Value)
            {
                SandSpiderAI enemy = enemyAI as SandSpiderAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.footstepAudio.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.sporeLizardScript && Config.disableSporeLizardSounds.Value)
            {
                PufferAI enemy = enemyAI as PufferAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.earthLeviathanScript && Config.disableEarthLeviathanSounds.Value)
            {
                SandWormAI enemy = enemyAI as SandWormAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.groundAudio.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.coilHeadScript && Config.disableCoilHeadSounds.Value)
            {
                SpringManAI enemy = enemyAI as SpringManAI;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
            if (enemyScriptName == ScriptNames.maskedScript && Config.disableMaskedSounds.Value)
            {
                MaskedPlayerEnemy enemy = enemyAI as MaskedPlayerEnemy;
                enemy.creatureSFX.volume = 0;
                enemy.creatureVoice.volume = 0;
                enemy.movementAudio.volume = 0;
                Console.WriteLine($"Monsterphobia: Muted {enemyScriptName}");
            }
        }
    }
}