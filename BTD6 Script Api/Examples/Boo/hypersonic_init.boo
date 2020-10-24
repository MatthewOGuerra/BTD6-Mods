import BTD6.Script.Utils
import MelonLoader

class Hypersonic:
    def Entry():
        for tower in Util.instance.GetGameModel().towers:
            for behavior in tower.behaviors:
                if 'AttackModel' in behavior.name:
                    attackModel = Util.instance.BehaviorToAttackModel(behavior)
                    if len(attackModel.weapons) > 0:
                        attackModel.weapons[0].rateFrames = 0
                        attackModel.weapons[0].rate = 0
                        attackModel.weapons[0].projectile.pierce = 500000

hypersonic = Hypersonic()
hypersonic.Entry()
MelonLogger.Log("Successfully made all towers hypersonic")
