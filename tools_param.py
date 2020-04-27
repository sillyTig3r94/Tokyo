# -*- coding: utf-8 -*-
"""
Created on Wed Apr  1 19:23:35 2020

@author: pisnm
"""
from datetime import datetime
import json as json
# =============================================================================
#                           TOOL PARAMETERS
# =============================================================================
tools_dict = {}
#-------------------------------Muscat----------------------------------------# 
tools_dict["Muscat"] = {}
tools_dict["Muscat"]['id'] = 1
tools_dict["Muscat"]['operate_volt'] = 22
tools_dict["Muscat"]['max_volt'] = 24
tools_dict["Muscat"]['min_volt'] = 18.5
tools_dict["Muscat"]['wire_res'] = 0.5 / 2.0
tools_dict["Muscat"]['sink'] = True
tools_dict["Muscat"]['source'] = False
tools_dict["Muscat"]['src_type'] = 'na'
tools_dict["Muscat"]['calc_option'] = 'average'
tools_dict["Muscat"]['source_voltage'] = 0
tools_dict["Muscat"]['avg_power'] = 5.0
tools_dict["Muscat"]['peak_power'] = 10.0 
tools_dict["Muscat"]['Imax'] = 0
#-----------------------------GeoPilot----------------------------------------#
tools_dict["GP9600"] = {}
tools_dict["GP9600"]['id'] = 2
tools_dict["GP9600"]['operate_volt'] = 20
tools_dict["GP9600"]['max_volt'] = 24
tools_dict["GP9600"]['min_volt'] = 18.5
tools_dict["GP9600"]['wire_res'] = 1.5 / 2.0
tools_dict["GP9600"]['sink'] = True
tools_dict["GP9600"]['source'] = False
#tools_dict["GP9600"]['src_type'] = 'na'
#tools_dict["GP9600"]['calc_option'] = 'average'
tools_dict["GP9600"]['source_voltage'] = 0
tools_dict["GP9600"]['avg_power'] = 5.0
tools_dict["GP9600"]['peak_power'] = 25.0
tools_dict["GP9600"]['Imax'] = 0
#-----------------------------Rio Base----------------------------------------#
tools_dict["RioBase"] = {}
tools_dict["RioBase"]['id'] = 3
tools_dict["RioBase"]['operate_volt'] = 22
tools_dict["RioBase"]['max_volt'] = 24
tools_dict["RioBase"]['min_volt'] = 18.5
tools_dict["RioBase"]['wire_res'] = 0.15 / 2.0
tools_dict["RioBase"]['sink'] = True
tools_dict["RioBase"]['source'] = False
#tools_dict["RioBase"]['src_type'] = 'na'
#tools_dict["RioBase"]['calc_option'] = 'average'
tools_dict["RioBase"]['source_voltage'] = 0
tools_dict["RioBase"]['avg_power'] = 6.2
tools_dict["RioBase"]['peak_power'] = 6.2 
tools_dict["RioBase"]['Imax'] = 0
#-----------------------------Rio Nuke----------------------------------------#
tools_dict["RioNuke"] = {}
tools_dict["RioNuke"]['id'] = 4
tools_dict["RioNuke"]['operate_volt'] = 22
tools_dict["RioNuke"]['max_volt'] = 24
tools_dict["RioNuke"]['min_volt'] = 18.5
tools_dict["RioNuke"]['wire_res'] = 0.5 / 2.0
tools_dict["RioNuke"]['sink'] = True
tools_dict["RioNuke"]['source'] = False
#tools_dict["RioNuke"]['src_type'] = 'na'
#tools_dict["RioNuke"]['calc_option'] = 'average'
tools_dict["RioNuke"]['source_voltage'] = 0
tools_dict["RioNuke"]['avg_power'] = 4.26
tools_dict["RioNuke"]['peak_power'] = 4.26
tools_dict["RioNuke"]['Imax'] = 0
#------------------------------Brussels---------------------------------------#
tools_dict["Brussel"] = {}
tools_dict["Brussel"]['id'] = 0
tools_dict["Brussel"]['operate_volt'] = 24
tools_dict["Brussel"]['max_volt'] = 24
tools_dict["Brussel"]['min_volt'] = 18.5
tools_dict["Brussel"]['wire_res'] = 0.5 / 2.0
tools_dict["Brussel"]['sink'] = False
tools_dict["Brussel"]['source'] = True
tools_dict["Brussel"]['src_type'] = 'gen'
tools_dict["Brussel"]['calc_option'] = 'average'
tools_dict["Brussel"]['source_voltage'] = 24
tools_dict["Brussel"]['internal_res'] = 0.5
tools_dict["Brussel"]['avg_power'] = 7.2
tools_dict["Brussel"]['peak_power'] = 12.0 
tools_dict["Brussel"]['Imax'] = 3.0
#------------------------------EarthStar TX-----------------------------------#
tools_dict["ESTX"] = {}
tools_dict["ESTX"]['id'] = 5
tools_dict["ESTX"]['operate_volt'] = 22
tools_dict["ESTX"]['max_volt'] = 24
tools_dict["ESTX"]['min_volt'] = 18.5
tools_dict["ESTX"]['wire_res'] = 1.5 / 2.0
tools_dict["ESTX"]['sink'] = True
tools_dict["ESTX"]['source'] = False
#tools_dict["ESTX"]['src_type'] = 'na'
#tools_dict["ESTX"]['calc_option'] = 'average'
tools_dict["ESTX"]['source_voltage'] = 0
tools_dict["ESTX"]['avg_power'] = 0.33
tools_dict["ESTX"]['peak_power'] = 0.33 
tools_dict["ESTX"]['Imax'] = 0
#--------------------------EarthStar RX - model 1-----------------------------#
tools_dict["ESRX1"] = {}
tools_dict["ESRX1"]['id'] = 6
tools_dict["ESRX1"]['operate_volt'] = 22
tools_dict["ESRX1"]['max_volt'] = 24
tools_dict["ESRX1"]['min_volt'] = 18.5
tools_dict["ESRX1"]['wire_res'] = 2.0 / 2.0
tools_dict["ESRX1"]['sink'] = True
tools_dict["ESRX1"]['source'] = False
#tools_dict["ESRX1"]['src_type'] = 'na'
#tools_dict["ESRX1"]['calc_option'] = 'average'
tools_dict["ESRX1"]['source_voltage'] = 0
tools_dict["ESRX1"]['avg_power'] = 5.5
tools_dict["ESRX1"]['peak_power'] = 5.5 
tools_dict["ESRX1"]['Imax'] = 0
#--------------------------EarthStar RX - model 2-----------------------------#
tools_dict["ESRX2"] = {}
tools_dict["ESRX2"]['id'] = 7 
tools_dict["ESRX2"]['operate_volt'] = 22
tools_dict["ESRX2"]['max_volt'] = 24
tools_dict["ESRX2"]['min_volt'] = 18.5
tools_dict["ESRX2"]['wire_res'] = 1.8 / 2.0
tools_dict["ESRX2"]['sink'] = True
tools_dict["ESRX2"]['source'] = False
#tools_dict["ESRX2"]['src_type'] = 'na'
#tools_dict["ESRX2"]['calc_option'] = 'average'
tools_dict["ESRX2"]['source_voltage'] = 0
tools_dict["ESRX2"]['avg_power'] = 5.5
tools_dict["ESRX2"]['peak_power'] = 5.5 
tools_dict["ESRX2"]['Imax'] = 0
#------------------------------Yokohama---------------------------------------#
tools_dict["Yoko"] = {}
tools_dict["Yoko"]['id'] = 8 
tools_dict["Yoko"]['operate_volt'] = 22
tools_dict["Yoko"]['max_volt'] = 24
tools_dict["Yoko"]['min_volt'] = 21
tools_dict["Yoko"]['wire_res'] = 0.5 / 2.0
tools_dict["Yoko"]['sink'] = True
tools_dict["Yoko"]['source'] = False
#tools_dict["Yoko"]['src_type'] = 'na'
#tools_dict["Yoko"]['calc_option'] = 'average'
tools_dict["Yoko"]['source_voltage'] = 0
tools_dict["Yoko"]['avg_power'] = 6.84
tools_dict["Yoko"]['peak_power'] = 11.4 
tools_dict["Yoko"]['Imax'] = 0
#----------------------------------AFR----------------------------------------#
tools_dict["AFR0"] = {}
tools_dict["AFR0"]['id'] = 9 
tools_dict["AFR0"]['operate_volt'] = 22
tools_dict["AFR0"]['max_volt'] = 24
tools_dict["AFR0"]['min_volt'] = 18.5
tools_dict["AFR0"]['wire_res'] = 0.5
tools_dict["AFR0"]['sink'] = True
tools_dict["AFR0"]['source'] = False
#tools_dict["AFR0"]['src_type'] = 'na'
#tools_dict["AFR0"]['calc_option'] = 'average'
tools_dict["AFR0"]['source_voltage'] = 0
tools_dict["AFR0"]['avg_power'] = 3.6
tools_dict["AFR0"]['peak_power'] = 5.0 
tools_dict["AFR0"]['Imax'] = 0
#---------------------------------XBAT----------------------------------------#
tools_dict["XBAT"] = {}
tools_dict["XBAT"]['id'] = 10 
tools_dict["XBAT"]['operate_volt'] = 22
tools_dict["XBAT"]['max_volt'] = 24
tools_dict["XBAT"]['min_volt'] = 18.5
tools_dict["XBAT"]['wire_res'] = 1.5 / 2
tools_dict["XBAT"]['sink'] = True
tools_dict["XBAT"]['source'] = False
#tools_dict["XBAT"]['src_type'] = 'na'
#tools_dict["XBAT"]['calc_option'] = 'average'
tools_dict["XBAT"]['source_voltage'] = 0
tools_dict["XBAT"]['avg_power'] = 1.6
tools_dict["XBAT"]['peak_power'] = 1.6
tools_dict["XBAT"]['Imax'] = 0
#---------------------------------ADR-----------------------------------------#
tools_dict["ADR4"] = {}
tools_dict["ADR4"]['id'] = 11
tools_dict["ADR4"]['operate_volt'] = 22
tools_dict["ADR4"]['max_volt'] = 24
tools_dict["ADR4"]['min_volt'] = 18.5
tools_dict["ADR4"]['wire_res'] = 1.0 / 2
tools_dict["ADR4"]['sink'] = True
tools_dict["ADR4"]['source'] = False
#tools_dict["ADR4"]['src_type'] = 'na'
#tools_dict["ADR4"]['calc_option'] = 'average'
tools_dict["ADR4"]['source_voltage'] = 0
tools_dict["ADR4"]['avg_power'] = 5
tools_dict["ADR4"]['peak_power'] = 7
tools_dict["ADR4"]['Imax'] = 0
#---------------------------------MRIL----------------------------------------#
tools_dict["MRIL"] = {}
tools_dict["MRIL"]['id'] = 12
tools_dict["MRIL"]['operate_volt'] = 22
tools_dict["MRIL"]['max_volt'] = 24
tools_dict["MRIL"]['min_volt'] = 18.5
tools_dict["MRIL"]['wire_res'] = 1.0 / 2
tools_dict["MRIL"]['sink'] = False
tools_dict["MRIL"]['source'] = False
tools_dict["MRIL"]['src_type'] = 'batt'
#tools_dict["MRIL"]['calc_option'] = 'average'
tools_dict["MRIL"]['source_voltage'] = 0
tools_dict["MRIL"]['avg_power'] = 1.6
tools_dict["MRIL"]['peak_power'] = 1.6
tools_dict["MRIL"]['Imax'] = 0
#---------------------------------DUMMY---------------------------------------#
tools_dict["DUMMY"] = {}
tools_dict["DUMMY"]['id'] = 13 
tools_dict["DUMMY"]['operate_volt'] = 24
tools_dict["DUMMY"]['max_volt'] = 24
tools_dict["DUMMY"]['min_volt'] = 18.5
tools_dict["DUMMY"]['wire_res'] = 0.5 / 2.0
tools_dict["DUMMY"]['sink'] = False
tools_dict["DUMMY"]['source'] = False
tools_dict["DUMMY"]['src_type'] = 'na'
#tools_dict["DUMMY"]['calc_option'] = 'average'
tools_dict["DUMMY"]['source_voltage'] = 0
tools_dict["DUMMY"]['avg_power'] = 0.1
tools_dict["DUMMY"]['peak_power'] = 0.1
tools_dict["DUMMY"]['Imax'] = 0

#---------------------------------BATT----------------------------------------#
tools_dict["BATT"] = {}
tools_dict["BATT"]['id'] = 14
tools_dict["BATT"]['operate_volt'] = 24
tools_dict["BATT"]['max_volt'] = 24
tools_dict["BATT"]['min_volt'] = 18.5
tools_dict["BATT"]['wire_res'] = 0.5 / 2.0
tools_dict["BATT"]['sink'] = False
tools_dict["BATT"]['source'] = True
tools_dict["BATT"]['src_type'] = 'batt'
#tools_dict["BATT"]['calc_option'] = 'average'
tools_dict["BATT"]['source_voltage'] = 22
tools_dict["BATT"]['avg_power'] = 1.0
tools_dict["BATT"]['peak_power'] = 1.0
tools_dict["BATT"]['Imax'] = 2.5

# =============================================================================

# =============================================================================
# 
# =============================================================================
#def get_tool_params():
#    tool_file = open('tools_setup.txt',"r")
#    all_lines = tool_file.read().splitlines()
#    single_line = []
#    
#    for line in all_lines:
#         single_line.append(line.split())
#    
#    tool_name = []
#    tool_sink = []
#    tool_source = []
#    tool_calc = []
#    tool_enable = []
#    
#    for line in single_line:   
#        for item in line:
#            if 'tool_name:' in item:
#                slpt = (item.split(':'))
#                tool_name.append(slpt[1])
#            if 'sink:' in item:
#                slpt = (item.split(':'))
#                tool_sink.append(slpt[1])
#            if 'source:' in item:
#                slpt = (item.split(':'))
#                tool_source.append(slpt[1])  
#            if 'pwr_calc:' in item:
#                slpt = (item.split(':'))
#                tool_calc.append(slpt[1])  
#            if 'enable:' in item:
#                slpt = (item.split(':'))
#                tool_enable.append(slpt[1])
#                  
#    return tool_name, tool_sink, tool_source, tool_calc, tool_enable
# =============================================================================
# 
# =============================================================================
#def set_calc_setup(tool_name, tool_sink, tool_source, tool_calc, tool_enable):
#    
#    for index, tool in enumerate(tool_name,start = 0):
#    #print(tool)
#        tools_dict[tool]['calc_option'] = tool_calc[index]
#        
#        if tool_sink[index] == 'True':       
#            if tool_enable[index] == 'True':
#                tools_dict[tool]['sink'] = True
#            else:
#                 tools_dict[tool]['sink'] = False
#        else:
#           tools_dict[tool]['sink'] = False
#           
#        if tool_source[index] == 'Gen':      
#            if tool_enable[index] == 'True':
#                tools_dict[tool]['source'] = True
#            else:
#                tools_dict[tool]['source'] = False
#            tools_dict[tool]['src_type'] = 'gen'
#            
#        elif tool_source[index] == 'Batt':
#            
#            if tool_enable[index] == 'True':
#                tools_dict[tool]['source'] = True
#            else:
#                tools_dict[tool]['source'] = False
#                     
#            tools_dict[tool]['src_type'] = 'batt'      
#        else:  
#            tools_dict[tool]['source'] = False
## =============================================================================
## 
## =============================================================================
#def write_Report_to_Apps(voltage,current,power,gen,batt,tool_name):
#    
#    output_file = open('calc_report.txt','w')
#        
#    dateTimeObj = datetime.now()
#
#    output_file.write(str(dateTimeObj.strftime("%Y-%m-%d, %H:%M:%S")) + '\n')
#
#    for index_tool, tool in enumerate(tool_name,start=0):
#        outFile_V = voltage[index_tool]
#        outFile_I = current[index_tool]
#        outFile_P = power[index_tool]
#        outFile_Gen = gen[index_tool]
#        outFile_Batt = batt[index_tool]
#        
#        if tools_dict[tool]['sink'] == True:
#            output_file.write(tool + ' ' 
#                          + str(outFile_V) + '[V] ' 
#                          + str(outFile_I) + '[A] '
#                          + str(outFile_P) + '[W] '
#                          + ' ') 
#        elif tools_dict[tool]['source'] == True:
#            if tools_dict[tool]['src_type'] == 'gen':
#                output_file.write(tool + ' - '                            
#                  + 'Generator: ' + ' '
#                  + str(outFile_V) + '[V] ' 
#                  + str(outFile_Gen) + '[A] '            
#                  + ' ')   
#            if tools_dict[tool]['src_type'] == 'batt':
#                output_file.write(tool + ' - '                            
#                  + 'Battery: ' + ' '
#                  + str(outFile_V) + '[V] ' 
#                  + str(outFile_Batt) + '[A] '            
#                  + ' ')  
#        else:
#           output_file.write(tool + ' ' 
#                          + str(outFile_V) + '[V] ' 
#                          + str(outFile_I) + '[A] ' 
#                          + ' ') 
#                
#        output_file.write('\n')
#            
#    print('\nSuccessully write report to application')
#    
#    output_file.close()
## =============================================================================
## 
## =============================================================================
#def write_Error_to_Apps(voltage,current,power,generator,battery,tool_name):
#    
#    power_tolerance = 0.01
#        
#    error_file = open('error_report.txt','w')
#    
#    dateTimeObj = datetime.now()
#    
#    error_file.write(str(dateTimeObj.strftime("%Y-%m-%d, %H:%M:%S")) + '\n')
#      
#    for index_tool, tool in enumerate(tool_name,start = 0):      
#        # checking Imax if source
#        if tools_dict[tool]['source'] == True:
#            print('\nChecking Imax on {} limit at: {} [A] '.format(tool,tools_dict[tool]['Imax']))  
#            
#            delta_gen = tools_dict[tool]['Imax'] - generator[index_tool] 
#            
#            delta_batt = tools_dict[tool]['Imax'] - battery[index_tool]
#                           
#            if tools_dict[tool]['src_type'] == 'gen': 
#                if current_fail(delta_gen) == True:               
#                    error_file.write('\nImax fail on Generator - ' + tool + '  ' 
#                             + 'Limit: ' + str(tools_dict[tool]['Imax']) + ' [A]' 
#                             + ' vs. '
#                             + 'Meas:' + str(generator[index_tool]) + ' [A]')
#                else:
#                    error_file.write('\nImax Pass on Generator - ' + tool + '  ' 
#                             + 'Limit: ' + str(tools_dict[tool]['Imax']) + ' [A]' 
#                                 + ' vs. '
#                                 + 'Meas: ' + str(generator[index_tool]) + ' [A]')  
#                    
#            if tools_dict[tool]['src_type'] == 'batt':
#                if current_fail(delta_batt) == True:
#                    error_file.write('\nImax fail on Battery - ' + tool + '  '
#                                 + 'Limit: ' + str(tools_dict[tool]['Imax']) + ' [A]'
#                                 + ' vs. '
#                                 + 'Meas: ' + str(battery[index_tool]) + ' [A]')
#                else:
#                    error_file.write('\nImax Pass on Battery - ' + tool + '  '
#                                 + 'Limit: ' + str(tools_dict[tool]['Imax']) + ' [A]'
#                                 + ' vs. '
#                                 + 'Meas: ' + str(battery[index_tool]) + ' [A]')
#                
#            
#        elif tools_dict[tool]['sink'] == True:   #if not source then check the power calculation if tool is sink.  
#            # checking Power 
#            if tools_dict[tool]['calc_option'] == 'average':
#                delta = round(abs(tools_dict[tool]['avg_power'] - power[index_tool]),4)
#                
#                if power_fail(delta,power_tolerance) == True:
#                     print('\nPower fail on {}: {} vs. {} , Difference: {} '.format(tool,tools_dict[tool]['avg_power'],power[index_tool],delta))
#                     
#                     error_file.write('\nPower fail on ' + tool + ': ' 
#                                      + str(tools_dict[tool]['avg_power']) + ' [W] ' + ' vs. ' 
#                                      + str(power[index_tool]) + ' , Difference: '
#                                      + str(delta))
#                else:
#                     print('\nPower pass on {}: {} vs. {} , Difference: {} '.format(tool,tools_dict[tool]['avg_power'],power[index_tool],delta))
#                     
#                     error_file.write('\nPower pass on ' + tool + ': ' 
#                                      + str(tools_dict[tool]['avg_power']) + ' [W] ' + ' vs. ' 
#                                      + str(power[index_tool]) + ' , Difference: '
#                                      + str(delta))
#            else:
#                delta = round(abs(tools_dict[tool]['peak_power'] - power[index_tool]) ,4)
#                if power_fail(delta,power_tolerance) == True:
#                     print('\nPower fail on {}: {} vs. {} , Difference: {} '.format(tool,tools_dict[tool]['peak_power'],power[index_tool],delta))
#                     
#                     error_file.write('\nPower fail on ' + tool + ': ' 
#                                      + str(tools_dict[tool]['peak_power']) + ' [W] ' + ' vs. ' 
#                                      + str(power[index_tool]) + ' , Difference: '
#                                      + str(delta))
#                else:
#                     print('\nPower pass on {}: {} vs. {} , Difference: {} '.format(tool,tools_dict[tool]['peak_power'],power[index_tool],delta))
#                     
#                     error_file.write('\nPower pass on ' + tool + ': ' 
#                                      + str(tools_dict[tool]['peak_power']) + ' [W] ' + ' vs. ' 
#                                      + str(power[index_tool]) + ' , Difference: '
#                                      + str(delta))
#        else: # no sink or no source type, mean tool is disable
#              error_file.write('\nTool - ' + tool + ' disable')
#        error_file.write('\n')  
#        
#    
#    error_file.close()
#            
#    print('\nSuccessully analyze the calcualtion result for the application')
# =============================================================================
# 
# =============================================================================
#def power_fail(delta,tolerance):   
#    if delta > tolerance:
#        return True
#    else:
#        return False
# =============================================================================
# 
# =============================================================================
#def current_fail(delta):   
#    if delta < 0:
#        return True
#    else:
#        return False
# =============================================================================
#     Export JSON File - Tool Params
# =============================================================================
#def export_tool_param():
#    with open('tool_params.json', 'w') as outFile:
#        json.dump(tools_dict, outFile)       
# =============================================================================
#     Import JSON File - Tool Params
# =============================================================================
#def import_tool_param():
#    with open('tool_params.json', 'r') as inFile:
#        tool_params = json.load(inFile)
#    return tool_params
    

    
