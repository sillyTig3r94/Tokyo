# -*- coding: utf-8 -*-
"""
Created on Wed Apr  1 11:28:56 2020

@author: pisnm
"""
from tools_param import *
# =============================================================================
#                       Query voltage measurement on LTPSICE
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
# =============================================================================
#                       Query current measurement on LTSPICE
# =============================================================================
def get_current(arg):
    item = str(arg).splitlines()
    for attr in item:
        if attr == r"type:'device_current'":
            # current might be negative because of the direction convention on LTSPICE
            # so using absolute value here
            current = abs(round(float(item[3].strip("[]")),4)) 
            if current < 0.005:
                return 0
            else:
                return current
    return "Invalid" 
# =============================================================================
#                       Setting parameter for LTSPICE
# =============================================================================
def set_params(tools_setup):
    params_file = open('params_setting.txt','w')
    
    myIndex = 0
     
    # write params for uphole tools
    for index, tool in enumerate(tools_setup,start = 1):
        
        params_file.write('\n' + ';Tools : ' + tool + '\n')

            
        # Enable node
        params_file.write('.param' + ' ' + 'enable_load' + str(index) + ' = -5' + '\n')
        params_file.write('.param' + ' ' + 'R' + str(index) + ' = ' + str(tools_dict[tool]['wire_res']) + '\n')
        
        # Set average / peak power params for calculation
        if tools_dict[tool]['calc_option'] == 'average':
            params_file.write('.param' + ' ' + 'P_load' + str(index) + ' = ' + str(tools_dict[tool]['avg_power']) + '\n')
            params_file.write('.param' + ' ' + 'Rmin_load' + str(index) + ' = ' + str(tools_dict[tool]['min_volt']**2 / tools_dict[tool]['avg_power']) + '\n')
        else:
            params_file.write('.param' + ' ' + 'P_load' + str(index) + ' = ' + str(tools_dict[tool]['peak_power']) + '\n')
            params_file.write('.param' + ' ' + 'Rmin_load' + str(index) + ' = ' + str(tools_dict[tool]['min_volt']**2 / tools_dict[tool]['peak_power']) + '\n')
            
        # Configure sink load
        if tools_dict[tool]['sink'] == True:
            params_file.write('.param' + ' ' + 'enable_sink' + str(index) + '= -5' + '\n')
        else:
           params_file.write('.param' + ' ' + 'enable_sink' + str(index) + ' = 5' + '\n')
           
        # Configure source (generator + battery)
        if tools_dict[tool]['source'] == True:
            
            if tools_dict[tool]['src_type'] == 'gen':
                params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = -5' + '\n')
                params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = ' + str(tools_dict[tool]['source_voltage'])  + '\n') 
                params_file.write('.param' + ' ' + 'enable_batt'  + str(index) + ' = 5' + '\n')
                params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = 0' + '\n')                
            elif tools_dict[tool]['src_type'] == 'batt':
                params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = 5' + '\n')
                params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = 0' + '\n')  
                params_file.write('.param' + ' ' + 'enable_batt' + str(index) + ' = -5' + '\n')
                params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = ' + str(tools_dict[tool]['source_voltage'])  + '\n')
            else:
                params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = -5' + '\n')
                params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = 0' + '\n')
                params_file.write('.param' + ' ' + 'enable_batt' + str(index) + ' = 5' + '\n')
                params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = 0' + '\n')    
        else:
            params_file.write('.param' + ' ' + 'enable_gen'  + str(index) + ' = 5' + '\n')
            params_file.write('.param' + ' ' + 'V_gen'  + str(index) + ' = 0' + '\n')    
            params_file.write('.param' + ' ' + 'enable_batt' + str(index) + ' = 5' + '\n')  
            params_file.write('.param' + ' ' + 'V_batt'  + str(index) + ' = 0' + '\n')    
        
           
           
                  
        myIndex = index
        
    # write params to remove unused uphole tools    
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

# =============================================================================
# 
# =============================================================================
    
def get_tool(LTSPICE,tool_setup):
    
    keyword = LTSPICE.get_trace_names()
    
    tools_V , tools_I = [] , [] 
    
    tools_Gen , tools_Batt = [] , []
        
    volt_index = 1 
    
    curr_index = 19
    
    gen_index = 19
    
    batt_index = 19
    
    
    for key in keyword:
        key_volt = 'V(v_load' + str(volt_index) + ')' 
        key_curr = 'I(Load' + str(curr_index) + ')'
        key_gen = 'I(Gen' + str(gen_index) + ')'
        key_batt = 'I(Batt' + str(batt_index) + ')'
        
        if key == key_volt:
            V = get_voltage(LTSPICE.get_trace(key))
            tools_V.append(V)
            volt_index += 1
            
        if key == key_curr:
            I = get_current(LTSPICE.get_trace(key))
            tools_I.append(I)
            curr_index -= 1
        if key == key_gen:
            Gen = get_current(LTSPICE.get_trace(key))
            tools_Gen.append(Gen)
            gen_index -= 1
        if key == key_batt:
            Batt = get_current(LTSPICE.get_trace(key))
            tools_Batt.append(Batt)
            batt_index -= 1 
            
                       
    #remove unsed uphole tools    
    #sliding
    tools_P = []
    tools_I.reverse()
    tools_Gen.reverse()
    tools_Batt.reverse()
    for index in range(len(tools_V)):
        tools_P.append(round(tools_I[index]*tools_V[index],2))
    
#    return tools_uh_V, tools_uh_I, tools_dh_V, tools_dh_I  
    
    return tools_V,tools_I,tools_P,tools_Gen,tools_Batt

