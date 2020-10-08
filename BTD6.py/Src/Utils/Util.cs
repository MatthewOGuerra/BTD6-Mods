
using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Behaviors.Attack;

namespace BTD6.py.Utils
{
    public class Util
    {
        public AttackModel BehaviorToAttackModel(Model m)
        {
            return m.Cast<AttackModel>();
        }

        //TODO   more.
    }
}
