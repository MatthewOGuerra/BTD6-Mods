
util

roundNum = 0
for rounds in gameModel.GetRoundSet().rounds:
    roundNum += 1
    print ("Round " + str(roundNum))
    for emission in rounds.groups:
        print (" - " + str(emission.bloon))
    print ""
