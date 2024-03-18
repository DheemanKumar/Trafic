Qtable={}

def incremental(state,action,reward):
    if(state in Qtable.keys()):
        if(action in Qtable[state].keys()):
            Qtable[state][action]['n']+=1
            Qtable[state][action]['Q']+=(1/Qtable[state][action]['n'])*(reward-Qtable[state][action]['Q'])
        else:
            Qtable[state][action]={'n':1,"Q":reward}
    
    else :
        Qtable[state]={action:{'n':1,"Q":reward}}


def convertor(data):
    numbers = data.decode()
    print("data")
    print(type(numbers))
    #numbers =int(data)
    state=numbers[2:2+int(numbers[1])]
    action=numbers[3+int(numbers[1]):3+int(numbers[1])+int(numbers[2+int(numbers[1])])]
    reward=numbers[3+int(numbers[1])+int(numbers[2+int(numbers[1])]):]
    incremental(int(state),int(action),int(reward))
    
    print(Qtable)
        