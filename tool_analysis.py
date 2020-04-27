# -*- coding: utf-8 -*-
"""
Created on Mon Apr 27 10:13:06 2020

@author: pisnm
"""
from datetime import datetime
import json as json
from tools_param import *
# =============================================================================
# 
# =============================================================================
def write_Report_to_Apps(voltage,current,power,gen,batt,setup):
    
    output_file = open('calculation.txt','w')
        
    dateTimeObj = datetime.now()

    output_file.write(str(dateTimeObj.strftime("%Y-%m-%d, %H:%M:%S")) + '\n')

    for index_tool, tool in enumerate(setup,start=0):
        outFile_V = voltage[index_tool]
        outFile_I = current[index_tool]
        outFile_P = power[index_tool]
        outFile_Gen = gen[index_tool]
        outFile_Batt = batt[index_tool]
        
        if "Load" in setup[tool]:  
            output_file.write(tool + ' ' 
                          + str(outFile_V) + '[V] ' 
                          + str(outFile_I) + '[A] '
                          + str(outFile_P) + '[W] '
                          + ' ') 
            
        elif "Generator" in setup[tool] and setup[tool]["Connected"] == True: # enable 
            output_file.write(tool + ' - '                            
              + 'Generator: ' + ' '
              + str(outFile_V) + '[V] ' 
              + str(outFile_Gen) + '[A] '            
              + ' ')   
            
        elif "Battery" in setup[tool] and setup[tool]["Connected"] == True: #enable battery
            output_file.write(tool + ' - '                            
              + 'Battery: ' + ' '
              + str(outFile_V) + '[V] ' 
              + str(outFile_Batt) + '[A] '            
              + ' ')  
        else:
           output_file.write(tool + ' ' 
                          + str(outFile_V) + '[V] ' 
                          + str(outFile_I) + '[A] ' 
                          + ' ') 
                
        output_file.write('\n')
            
    print('\nSuccessfully write report to application')
    
    output_file.close()
# =============================================================================
# 
# =============================================================================
def write_Error_to_Apps(voltage,current,power,generator,battery,setup,database):
    
    power_tolerance = 0.01
        
    error_file = open('report.txt','w')
    
    dateTimeObj = datetime.now()
    
    error_file.write(str(dateTimeObj.strftime("%Y-%m-%d, %H:%M:%S")) + '\n')
      
    for index_tool, tool in enumerate(setup,start = 0):      
        # checking Imax if source
                # Configure source (generator + battery)
        if "Generator" in setup[tool] and setup[tool]["Connected"] == True: # enable
            print('\nChecking Imax on {} limit at: {} [A] '.format(tool,database[tool]['Source']['Imax']))  
            delta_gen = database[tool]['Source']['Imax'] - generator[index_tool]
            if current_fail(delta_gen) == True:               
                error_file.write('\nImax fail on Generator - ' + tool + '  ' 
                         + 'Limit: ' + str(database[tool]['Source']['Imax']) + ' [A]' 
                         + ' vs. '
                         + 'Meas:' + str(generator[index_tool]) + ' [A]')
            else:
                error_file.write('\nImax Pass on Generator - ' + tool + '  ' 
                         + 'Limit: ' + str(database[tool]['Source']['Imax']) + ' [A]' 
                         + ' vs. '
                         + 'Meas: ' + str(generator[index_tool]) + ' [A]') 

        elif "Battery" in setup[tool] and setup[tool]["Connected"] == True: #enable battery
            print('\nChecking Imax on {} limit at: {} [A] '.format(tool,database[tool]['Source']['Imax']))  
            delta_batt = database[tool]['Source']['Imax'] - battery[index_tool]             
            if current_fail(delta_batt) == True:
                error_file.write('\nImax fail on Battery - ' + tool + '  '
                             + 'Limit: ' + str(database[tool]['Source']['Imax']) + ' [A]'
                             + ' vs. '
                             + 'Meas: ' + str(battery[index_tool]) + ' [A]')
            else:
                error_file.write('\nImax Pass on Battery - ' + tool + '  '
                             + 'Limit: ' + str(database[tool]['Source']['Imax']) + ' [A]'
                             + ' vs. '
                             + 'Meas: ' + str(battery[index_tool]) + ' [A]')
                           
        if "Load" in setup[tool]:  
            # checking Power 
            delta = round(abs(setup[tool]['Load'] - power[index_tool]),4)
            
            if power_fail(delta,power_tolerance) == True:
                 print('\nPower fail on {}: {} vs. {} , Difference: {} '.format(tool,setup[tool]['Load'],power[index_tool],delta))
                 
                 error_file.write('\nPower fail on ' + tool + ': ' 
                                  + str(setup[tool]['Load']) + ' [W] ' + ' vs. ' 
                                  + str(power[index_tool]) + ' , Difference: '
                                  + str(delta))
            else:
                 print('\nPower pass on {}: {} vs. {} , Difference: {} '.format(tool,setup[tool]['Load'],power[index_tool],delta))
                 
                 error_file.write('\nPower pass on ' + tool + ': ' 
                                  + str(setup[tool]['Load']) + ' [W] ' + ' vs. ' 
                                  + str(power[index_tool]) + ' , Difference: '
                                  + str(delta))
        error_file.write('\n')  
        
    
    error_file.close()
            
    print('\nSuccessfully analyze the calcualtion result for the application')
# =============================================================================
# 
# =============================================================================
def power_fail(delta,tolerance):   
    if delta > tolerance:
        return True
    else:
        return False
# =============================================================================
# 
# =============================================================================
def current_fail(delta):   
    if delta < 0:
        return True
    else:
        return False
# =============================================================================
#     Export JSON File - Tool Params
# =============================================================================
def export_tool_param():
    with open('tool_params.json', 'w') as outFile:
        json.dump(tools_dict, outFile)       
# =============================================================================
#     Import JSON File - Tool Params
# =============================================================================
def import_tool_param():
    with open('tool_params.json', 'r') as inFile:
        tool_params = json.load(inFile)
    return tool_params