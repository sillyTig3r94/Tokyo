# -*- coding: utf-8 -*-
"""
Created on Wed Apr  1 11:28:56 2020

@author: pisnm
"""
from tools_param import *
# =============================================================================
#                           LTSPICE FUNCTIONS/METHODS
# =============================================================================
def get_voltage(arg):
    item = str(arg).splitlines()
    for attr in item:
        if attr == r"type:'voltage'":
            voltage = round(float(item[3].strip("[]")),4)
            if voltage < 0.000001:
                return 0
            else:
                return voltage
    return "Invalid"

def get_current(arg):
    item = str(arg).splitlines()
    for attr in item:
        if attr == r"type:'device_current'":
            current = round(float(item[3].strip("[]")),4)
            if current < 0.00000000001:
                return 0
            else:
                return current
    return "Invalid" 
   
def set_params(tools_setup):
    params_file = open('params_setting.txt','w')
    
    myIndex = 0
   
    flg_uphole = True
    
    uphole_tools = []
    
    downhole_tools = []

    params_file.write(';Source Tools Paramter:' + '\n')
    
    # write params source base
    for tool in tools_setup:
        if tools_dict[tool]['id'] == 0:      
            params_file.write('.param' + ' ' + 'V_source = ' + str(tools_dict[tool]['operate_volt']) + '\n')
            params_file.write('.param' + ' ' + 'R_source = ' + str(tools_dict[tool]['resistance']) + '\n') 
            flg_uphole = False
            continue
            
        if flg_uphole:
            uphole_tools.append(tool)
        else:
            downhole_tools.append(tool)
            

 
    # write params for uphole tools
    params_file.write('\n' + ';Uphole Tools Paramter: ' + '\n')
    for index, tool in enumerate(uphole_tools,start = 1):
        params_file.write('.param' + ' ' + 'P_uh_load' + str(index) + ' = ' + str(tools_dict[tool]['avg_power']) + '\n')
        params_file.write('.param' + ' ' + 'R_uh_' + str(index) + ' = ' + str(tools_dict[tool]['resistance']) + '\n') 
        params_file.write('.param' + ' ' + 'load' + str(index) + '_uh = -1' + '\n')
        myIndex = index
    # write params to remove unused downhole tools    
    for i in range(myIndex + 1, 3 + 1):
        params_file.write('.param' + ' ' + 'P_uh_load' + str(i) + ' = ' + str(tools_dict["DUMMY"]['avg_power']) + '\n')
        params_file.write('.param' + ' ' + 'R_uh_' + str(i) + ' = ' + str(tools_dict['DUMMY']['resistance']) + '\n') 
        params_file.write('.param' + ' ' + 'load' + str(i) + '_uh = 1' + '\n')  
        
    # write params for downhole tools
    params_file.write('\n' + ';Downhold Tools Paramter: ' + '\n')
    for index, tool in enumerate(downhole_tools,start = 1):
        params_file.write('.param' + ' ' + 'P_dh_load' + str(index) + ' = ' + str(tools_dict[tool]['avg_power']) + '\n')
        params_file.write('.param' + ' ' + 'R_dh_' + str(index) + ' = ' + str(tools_dict[tool]['resistance']) + '\n') 
        params_file.write('.param' + ' ' + 'load' + str(index) + '_dh = -1' + '\n')
        myIndex = index      
    # write params to remove unused downhole tools
    for i in range(myIndex + 1, 7 + 1):
        params_file.write('.param' + ' ' + 'P_dh_load' + str(i) + ' = ' + str(tools_dict["DUMMY"]['avg_power']) + '\n')
        params_file.write('.param' + ' ' + 'R_dh_' + str(i) + ' = ' + str(tools_dict['DUMMY']['resistance']) + '\n') 
        params_file.write('.param' + ' ' + 'load' + str(i) + '_dh = 1' + '\n')
               
    params_file.close()
    
    return uphole_tools,downhole_tools
    
def get_tool(LTSPICE):
    
    keyword = LTSPICE.get_trace_names()
    
    tools_uh_V = []
    tools_uh_I = []
    tools_dh_V = []
    tools_dh_I = []
    
    volt_dh_index = 1 
    
    curr_dh_index = 7
    
    volt_uh_index = 1 
    
    curr_uh_index = 1
    
    for key in keyword:
        key_dh_volt = 'V(v_dh_load' + str(volt_dh_index) + ')' 
        key_dh_curr = 'I(Load_dh_' + str(curr_dh_index) + ')'
        key_uh_volt = 'V(v_uh_load' + str(volt_uh_index) + ')' 
        key_uh_curr = 'I(Load_uh_' + str(curr_uh_index) + ')'
        
        if key == key_dh_volt:
            V = get_voltage(LTSPICE.get_trace(key))
            if V != 0:
                tools_dh_V.append(V)
            volt_dh_index += 1
            
        if key == key_dh_curr:
            I = get_current(LTSPICE.get_trace(key))
            if I != 0:
                tools_dh_I.append(I)
            curr_dh_index -= 1
            
        if key == key_uh_volt:
            V = get_voltage(LTSPICE.get_trace(key))
            if V != 0:
                tools_uh_V.append(V)
            volt_uh_index += 1
            
        if key == key_uh_curr:
            I = get_current(LTSPICE.get_trace(key))
            if I != 0:
                tools_uh_I.append(I)
            curr_uh_index += 1
            
    return tools_uh_V, tools_uh_I, tools_dh_V, tools_dh_I

#myTool = set_params(['Brussels','RioBase','Muscat','GP9600'])