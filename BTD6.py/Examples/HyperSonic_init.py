# Have init in the file name for it to only run once
convert

for tower in gameModel.towers:
    for behavior in tower.behaviors:
        if 'AttackModel' in behavior.name:
            attackModel = convert.BehaviorToAttackModel(behavior)
            if len(attackModel.weapons) > 0:
                attackModel.weapons[0].rateFrames = 0
                attackModel.weapons[0].rate = 0
                attackModel.weapons[0].projectile.pierce = 500000
