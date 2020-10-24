
using System;
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using Neo.IronLua;
using UnhollowerBaseLib;

namespace BTD6.Script.Utils
{
    public class Util
    {
        public static Util instance = new Util();
        public GameModel gameModel
        {
            get
            {
                return Game.instance.model;
            }

            set
            {
                Game.instance.model = value;
            }
        }

        public AttackModel BehaviorToAttackModel(Model m)
        {
            return m.Cast<AttackModel>();
        }

        public Model FirstOrDefault(Il2CppArrayBase<Model> models, string name)
        {
            return models.FirstOrDefault(tmp => tmp.name.Contains(name));
        }

        public GameModel GetGameModel()
        {
            return gameModel;
        }

        public void SetGameModel(object l)
        {
            gameModel = (GameModel)l;
            return;
        }

        public InGame GetInGame()
        {
            return InGame.instance;
        }

        public void print(object s)
        {
            MelonLogger.Log(s);
        }

        public TowerModel[] GetTowerModels()
        {
            return Game.instance.model.towers;
        }

        public BloonModel[] GetBloonModels()
        {
            return Game.instance.model.bloons;
        }

        public LuaTable GetTowerModelTable()
        {
            LuaTable lt = new LuaTable();

            for (int i = 0; i < Game.instance.model.towers.Count; i++)
            {
                lt[i] = Game.instance.model.towers[i];
            }

            return lt;
        }

        public LuaTable GetBloonModelTable()
        {
            LuaTable lt = new LuaTable();

            for (int i = 0; i < Game.instance.model.bloons.Count; i++)
            {
                lt[i] = Game.instance.model.bloons[i];
            }

            return lt;
        }

        public void UpdateTowerModel(TowerModel tower)
        {
            for (int i = 0; i < Game.instance.model.towers.Count; i++)
            {
                TowerModel t = Game.instance.model.towers[i];
                if (t.name.ToLower().Equals(tower.name.ToLower()))
                {
                    Game.instance.model.towers[i] = tower;
                }
            }
        }

        //TODO   more.
    }
}
