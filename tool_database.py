# -*- coding: utf-8 -*-
"""
Created on Tue Apr 21 15:30:26 2020

@author: pisnm
"""

from tools_param import *
# =============================================================================
# 
# =============================================================================
def import_tool_database():
    with open('database.json','r') as inFile:
        database = json.load(inFile)
    return database
# =============================================================================
# 
# =============================================================================
def import_tool_setting():
    with open('setup.json', 'r') as inFile:
        setup = json.load(inFile)
    return setup
# =============================================================================
# 
# =============================================================================
def ltspice_setup(setup,database):
    
    params_file = open('ltspice_setting.txt','w')
    
    myIndex = 0
     
    # write params for uphole tools
    for index, tool in enumerate(setup,start = 1):
        
        print(tool)    
        params_file.write('\n' + ';Tools : ' + tool + '\n')
        # Set average / peak power params for calculation

        # Enable node
        params_file.write('.param' + ' ' + 'enable_load' + str(index) + ' = -5' + '\n')
        params_file.write('.param' + ' ' + 'R' + str(index) + ' = ' + str(database[tool]['Rwire']) + '\n')
        
        # Configure sink load
        if "Load" in setup[tool]:        
            params_file.write('.param' + ' ' + 'P_load' + str(index) + ' = ' + str(setup[tool]['Load']) + '\n')
            params_file.write('.param' + ' ' + 'Rmin_load' + str(index) + ' = ' + str(database[tool]['Load']['Vmin']**2 /setup[tool]['Load']) + '\n')
            if setup[tool]["Connected"] == True:
                params_file.write('.param' + ' ' + 'enable_sink' + str(index) + '= -5' + '\n')
            else:
                params_file.write('.param' + ' ' + 'enable_sink' + str(index) + ' = 5' + '\n')
        else:
            params_file.write('.param' + ' ' + 'P_load' + str(index) + ' = ' + str(tools_dict["DUMMY"]['avg_power']) + '\n')
            params_file.write('.param' + ' ' + 'Rmin_load' + str(index) + ' = ' + str(tools_dict["DUMMY"]['min_volt']**2 / tools_dict["DUMMY"]['avg_power']) + '\n')
            params_file.write('.param' + ' ' + 'enable_sink' + str(index) + ' = 5' + '\n')
           
        # Configure source (generator + battery)
        if "Generator" in setup[tool] and setup[tool]["Connected"] == True: # enable 
                params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = -5' + '\n')    
                params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = ' + str(setup[tool]["Generator"])  + '\n') 
                params_file.write('.param' + ' ' + 'enable_batt'  + str(index) + ' = 5' + '\n')
                params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = 0' + '\n')  

        elif "Battery" in setup[tool] and setup[tool]["Connected"] == True: #enable battery
                params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = 5' + '\n')
                params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = 0' + '\n')  
                params_file.write('.param' + ' ' + 'enable_batt' + str(index) + ' = -5' + '\n')
                params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = ' + str(setup[tool]["Battery"])  + '\n')
        else:
               params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = 5' + '\n') 
               params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = 0' + '\n')  
               params_file.write('.param' + ' ' + 'enable_batt' + str(index) + ' = 5' + '\n')  
               params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = 0' + '\n')    
                
        myIndex = index
        
    # disable the rest of the setup 
    for index in range(myIndex + 1,20 + 1):
        
        params_file.write('\n' + ';Tools : Dummy ' + '\n')
        # disable node
        params_file.write('.param' + ' ' + 'enable_load' + str(index) + ' = 5' + '\n')   
        params_file.write('.param' + ' ' + 'R' + str(index) + ' = ' + str(tools_dict['DUMMY']['wire_res']) + '\n') 
        # Since this node is not valid so the value of this does not matter
        params_file.write('.param' + ' ' + 'P_load' + str(index) + ' = ' + str(tools_dict["DUMMY"]['avg_power']) + '\n')
        params_file.write('.param' + ' ' + 'Rmin_load' + str(index) + ' = ' + str(tools_dict["DUMMY"]['min_volt']**2 / tools_dict["DUMMY"]['avg_power']) + '\n')
        params_file.write('.param' + ' ' + 'enable_sink' + str(index) + ' = 5' + '\n')
        params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = 5' + '\n')
        params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = 0' + '\n')  
        params_file.write('.param' + ' ' + 'enable_batt' + str(index) + ' = 5' + '\n')
        params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = 0' + '\n') 

                        
    params_file.close()