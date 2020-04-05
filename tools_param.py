# -*- coding: utf-8 -*-
"""
Created on Wed Apr  1 19:23:35 2020

@author: pisnm
"""
from datetime import datetime
# =============================================================================
#                           TOOL PARAMETERS
# =============================================================================
tools_dict = {}
#-------------------------------Muscat----------------------------------------# 
tools_dict["Muscat"] = {}
tools_dict["Muscat"]['id'] = 1
tools_dict["Muscat"]['operate_volt'] = 22
tools_dict["Muscat"]['max_volt'] = 24
tools_dict["Muscat"]['min_volt'] = 16
tools_dict["Muscat"]['resistance'] = 0.5 / 2.0
tools_dict["Muscat"]['sink'] = True
tools_dict["Muscat"]['source'] = False
tools_dict["Muscat"]['avg_power'] = 5.0
tools_dict["Muscat"]['peak_power'] = 10.0 
#-----------------------------GeoPilot----------------------------------------#
tools_dict["GP9600"] = {}
tools_dict["GP9600"]['id'] = 2
tools_dict["GP9600"]['operate_volt'] = 20
tools_dict["GP9600"]['max_volt'] = 24
tools_dict["GP9600"]['min_volt'] = 16
tools_dict["GP9600"]['resistance'] = 1.5 / 2.0
tools_dict["GP9600"]['sink'] = True
tools_dict["GP9600"]['source'] = False
tools_dict["GP9600"]['avg_power'] = 5.0
tools_dict["GP9600"]['peak_power'] = 25.0
#-----------------------------Rio Base----------------------------------------#
tools_dict["RioBase"] = {}
tools_dict["RioBase"]['id'] = 3
tools_dict["RioBase"]['operate_volt'] = 22
tools_dict["RioBase"]['max_volt'] = 24
tools_dict["RioBase"]['min_volt'] = 16
tools_dict["RioBase"]['resistance'] = 0.15 / 2.0
tools_dict["RioBase"]['sink'] = True
tools_dict["RioBase"]['source'] = False
tools_dict["RioBase"]['avg_power'] = 6.2
tools_dict["RioBase"]['peak_power'] = 6.2 
#-----------------------------Rio Nuke----------------------------------------#
tools_dict["RioNuke"] = {}
tools_dict["RioNuke"]['id'] = 4
tools_dict["RioNuke"]['operate_volt'] = 22
tools_dict["RioNuke"]['max_volt'] = 24
tools_dict["RioNuke"]['min_volt'] = 16
tools_dict["RioNuke"]['resistance'] = 0.5 / 2.0
tools_dict["RioNuke"]['sink'] = True
tools_dict["RioNuke"]['source'] = False
tools_dict["RioNuke"]['avg_power'] = 4.26
tools_dict["RioNuke"]['peak_power'] = 4.26
#------------------------------Brussels---------------------------------------#
tools_dict["Brussels"] = {}
tools_dict["Brussels"]['id'] = 0
tools_dict["Brussels"]['operate_volt'] = 24
tools_dict["Brussels"]['max_volt'] = 24
tools_dict["Brussels"]['min_volt'] = 16
tools_dict["Brussels"]['resistance'] = 0.5 / 2.0
tools_dict["Brussels"]['sink'] = False
tools_dict["Brussels"]['source'] = True
tools_dict["Brussels"]['avg_power'] = 7.2
tools_dict["Brussels"]['peak_power'] = 12.0 
#------------------------------EarthStar TX-----------------------------------#
tools_dict["ESTX"] = {}
tools_dict["ESTX"]['id'] = 5
tools_dict["ESTX"]['operate_volt'] = 22
tools_dict["ESTX"]['max_volt'] = 24
tools_dict["ESTX"]['min_volt'] = 16
tools_dict["ESTX"]['resistance'] = 1.5 / 2.0
tools_dict["ESTX"]['sink'] = True
tools_dict["ESTX"]['source'] = False
tools_dict["ESTX"]['avg_power'] = 0.33
tools_dict["ESTX"]['peak_power'] = 0.33 
#--------------------------EarthStar RX - model 1-----------------------------#
tools_dict["ESRX1"] = {}
tools_dict["ESRX1"]['id'] = 6
tools_dict["ESRX1"]['operate_volt'] = 22
tools_dict["ESRX1"]['max_volt'] = 24
tools_dict["ESRX1"]['min_volt'] = 16
tools_dict["ESRX1"]['resistance'] = 2.0 / 2.0
tools_dict["ESRX1"]['sink'] = True
tools_dict["ESRX1"]['source'] = False
tools_dict["ESRX1"]['avg_power'] = 5.5
tools_dict["ESRX1"]['peak_power'] = 5.5 
#--------------------------EarthStar RX - model 2-----------------------------#
tools_dict["ESRX2"] = {}
tools_dict["ESRX2"]['id'] = 7 
tools_dict["ESRX2"]['operate_volt'] = 22
tools_dict["ESRX2"]['max_volt'] = 24
tools_dict["ESRX2"]['min_volt'] = 16
tools_dict["ESRX2"]['resistance'] = 1.8 / 2.0
tools_dict["ESRX2"]['sink'] = True
tools_dict["ESRX2"]['source'] = False
tools_dict["ESRX2"]['avg_power'] = 5.5
tools_dict["ESRX2"]['peak_power'] = 5.5 
#------------------------------Yokohama---------------------------------------#
tools_dict["Yoko"] = {}
tools_dict["Yoko"]['id'] = 8 
tools_dict["Yoko"]['operate_volt'] = 22
tools_dict["Yoko"]['max_volt'] = 24
tools_dict["Yoko"]['min_volt'] = 16
tools_dict["Yoko"]['resistance'] = 0.5 / 2.0
tools_dict["Yoko"]['sink'] = True
tools_dict["Yoko"]['source'] = False
tools_dict["Yoko"]['avg_power'] = 6.84
tools_dict["Yoko"]['peak_power'] = 11.4 
#----------------------------------AFR----------------------------------------#
tools_dict["AFR"] = {}
tools_dict["AFR"]['id'] = 9 
tools_dict["AFR"]['operate_volt'] = 22
tools_dict["AFR"]['max_volt'] = 24
tools_dict["AFR"]['min_volt'] = 16
tools_dict["AFR"]['resistance'] = 0.5
tools_dict["AFR"]['sink'] = True
tools_dict["AFR"]['source'] = False
tools_dict["AFR"]['avg_power'] = 3.6
tools_dict["AFR"]['peak_power'] = 5.0 
#---------------------------------XBAT----------------------------------------#
tools_dict["XBAT"] = {}
tools_dict["XBAT"]['id'] = 10 
tools_dict["XBAT"]['operate_volt'] = 22
tools_dict["XBAT"]['max_volt'] = 24
tools_dict["XBAT"]['min_volt'] = 16
tools_dict["XBAT"]['resistance'] = 1.5 / 2
tools_dict["XBAT"]['sink'] = True
tools_dict["XBAT"]['source'] = False
tools_dict["XBAT"]['avg_power'] = 1.6
tools_dict["XBAT"]['peak_power'] = 1.6
#---------------------------------DUMMY----------------------------------------#
tools_dict["DUMMY"] = {}
tools_dict["DUMMY"]['id'] = 10 
tools_dict["DUMMY"]['operate_volt'] = 24
tools_dict["DUMMY"]['max_volt'] = 24
tools_dict["DUMMY"]['min_volt'] = 16
tools_dict["DUMMY"]['resistance'] = 0.5 / 2.0
tools_dict["DUMMY"]['sink'] = False
tools_dict["DUMMY"]['source'] = False
tools_dict["DUMMY"]['avg_power'] = 1.0
tools_dict["DUMMY"]['peak_power'] = 1.0

# =============================================================================
# 
# =============================================================================
def get_tool_params():
    tool_file = open('tools_setup.txt',"r")
    tool_setup = tool_file.read().splitlines()
    return tool_setup

def write_to_Apps(voltage,current,Tool_List, Mode = 'down'):
    
    if Mode == 'down':
        output_file = open('downhole_result.txt','w')
    else:
        output_file = open('uphole_result.txt','w')
        
    dateTimeObj = datetime.now()

    output_file.write(str(dateTimeObj.strftime("%Y-%m-%d, %H:%M:%S")) + '\n')

    for index_tool, tool in enumerate(Tool_List,start=0):
        outFile_V = round(voltage[index_tool],3)
        outFile_I = current[len(current) - index_tool - 1]
        outFile_P = round(voltage[index_tool] * current[len(current) - index_tool - 1],3)
    
        output_file.write(tool + ': ' 
                      + str(outFile_V) + '[V], ' 
                      + str(outFile_I) + '[A], '
                      + str(outFile_P) + '[W]  '
                      + '\n')  
    output_file.close()

    
