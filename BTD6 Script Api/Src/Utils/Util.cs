
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using UnhollowerBaseLib;

namespace BTD6.Script.Utils
{
    public class Util
    {
        public AttackModel BehaviorToAttackModel(Model m)
        {
            return m.Cast<AttackModel>();
        }

        public Model FirstOrDefault(Il2CppArrayBase<Model> models, string name)
        {
            return models.FirstOrDefault(tmp => tmp.name.Contains(name));
        }

        //TODO   more.
    }
}
