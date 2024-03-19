import pickle

global Qtable

global file_path

import random


def loadfile(path):
    global  Qtable
    global file_path
    file_path=path[1:]+".bin"
    try:
        with open(file_path, 'rb') as file:
            return pickle.load(file)
    except FileNotFoundError:
        return {}

def savefile():
    global Qtable
    with open(file_path, 'wb') as file:
        pickle.dump(Qtable, file)



def findmax(values):
    max_q_value = float('-inf')  # Set to negative infinity to ensure any Q value will be greater
    max_q_key = None

    # Iterate over the dictionary
    for key, value in values.items():
        # Check if the 'Q' value of the current item is greater than the current maximum
        if value['Q'] > max_q_value:
            max_q_key = key
            max_q_value=value['Q']
    
    return max_q_key

def convertor(data):
    global Qtable
    numbers = data.decode()
    if (numbers[0]=='2'):
        Qtable=loadfile(numbers)
        return "file loded"
    elif(numbers[0]=='3'):
        savefile()
        return "file saved"
    elif(numbers[0]=='0'):
        state=numbers[1:5]
        action=numbers[5]
        reward=numbers[6:]
        incremental(state,int(action),int(reward))
        return "data saved"
    elif(numbers[0]=='1'):
        state=numbers[1:5]
        # print(state)
        # print(Qtable)
        if (state not in Qtable.keys()):
            return str(random.randint(0,3))
        action=findmax(Qtable[state])
        return str(action)
    

#models

def incremental(state,action,reward):
    global Qtable
    if(state in Qtable.keys()):
        Qtable[state][action]['n']+=1
        Qtable[state][action]['Q']+=(1/Qtable[state][action]['n'])*(reward-Qtable[state][action]['Q'])
    
    else :
        data={}
        for i in range(4):
            data[i]={'n':0,"Q":0}
        Qtable[state]=data
        #print("done")
        Qtable[state][action]['n']+=1
        Qtable[state][action]['Q']+=(1/Qtable[state][action]['n'])*(reward-Qtable[state][action]['Q'])
        
        